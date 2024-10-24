using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using ASRS.WCS.Common.Enum;
using System.Threading;
using Wish.Model;
using Wish.Models.ImportDto;
using Microsoft.EntityFrameworkCore;
using WISH.WCS.Device.SrmSocket;
using Wish.Service;
using ASRS.WCS.PLC;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using WISH.Helper.Common;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.Common;
using System.IO;
using Wish.Services;
using System.Text;
using ASRS.WCS.Common.Util;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigVM
    {
        bool isRunning = true;
        string msg = string.Empty;
        string desc = "WCS处理";
        public void Start()
        {
            isRunning = true;
            Task.Factory.StartNew(async () =>
            {
                while (isRunning)
                {
                    await Run();
                    await Task.Delay(TimeSpan.FromMilliseconds(300));
                }
            });
        }

        public void Stop()
        {
            isRunning = false;
        }

        private async Task Run()
        {
            try
            {
                await DoSrmWork();
            }
            catch (Exception ex)
            {
                msg = $"处理堆垛机逻辑出错：【{ex.Message}】";
                logger.Warn(desc + msg);
            }
        }

        private async Task DoSrmWork()
        {
            #region 1 堆垛机正常任务
            List<DeviceConfig> srmDevices = null;
            SrmCmdVM srmCmdVM = Wtm.CreateVM<SrmCmdVM>();
            BusinessResult result = new BusinessResult();
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            if (srmDevices == null)
                srmDevices = await DC.Set<DeviceConfig>().Include(x => x.PlcConfig).Include(x => x.StandardDevice).Where(x => x.StandardDevice.DeviceType == EDeviceType.S1Srm && "WISH.SRM.V10".Equals(x.StandardDevice.Device_Code) && x.IsEnabled == true)
                    .ToListAsync();
            foreach (var item in srmDevices)
            {
                WishSrmSocket srm = (WishSrmSocket)SocketClientService.GetClient().GetRegisterAutoReadObject(item.PlcConfig.Plc_Code, item.Device_Code);
                var plcInfoW = await DC.Set<PlcConfig>().Include(x => x.DBConfigs).Where(x => x.Plc_Name == item.Device_Code).ToListAsync();
                string plcCodeW = plcInfoW.Where(x => x.DBConfigs[0].Block_Name == "W").Select(x => x.Plc_Code).FirstOrDefault().ToString();
                string plcCodeS = plcInfoW.Where(x => x.DBConfigs[0].Block_Name == "S").Select(x => x.Plc_Code).FirstOrDefault().ToString();
                if (srm == null) continue;
                /// 修改设备在线状态
                if (srm.Sockets.IsConnect != item.IsOnline)
                {
                    item.IsOnline = srm.Sockets.IsConnect;
                    //DC.UpdateEntity(item);
                    await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                #region Plc2WcsStep = 0 srm2wcs 无交互信息
                if (item.Plc2WcsStep == 0)
                {
                    if (hasParentTransaction == false)
                    {
                        await DC.Database.BeginTransactionAsync();
                    }
                    try
                    {
                        //if (srm.srmSocketWcs.stb.Value == 1)
                        if (srm.srmSocketWcs.ack.Value == 1)
                        {
                            if (srm.srmSocketWcs.ActionType.Value == 0)
                            {
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "ACK为1动作类型为0", srm.srmSocketWcs.ToJson());
                            }
                            else if (srm.srmSocketWcs.ActionType.Value == 11)
                            {
                                //待命时先回复STB再找任务下发
                                byte[] bytes = ConvertSrmDataToByteArray(srm, 1);
                                SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                                socketByte.Value = bytes;
                                SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());
                                //找任务下发
                                SrmTaskDto srmTaskDto = new SrmTaskDto();


                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = item.Device_Code,
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                //获取堆垛机指令
                                result = await srmCmdVM.GetSrmCmdInfoAsync(input);

                                if (result.code == ResCode.OK)
                                {
                                    srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                                    if (srmTaskDto != null)
                                    {
                                        //下发指令
                                        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 1);
                                        if (res == true)
                                        {
                                            // 记录通讯报文
                                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "写任务记录", srmTaskDto.ToJson());
                                            DealCmdSendDto inputSend = new DealCmdSendDto()
                                            {
                                                deviceNo = item.Device_Code,
                                                taskNo = srmTaskDto.SerialNo,
                                                palletBarcode = srmTaskDto.PalletBarcode,
                                                checkPoint = srmTaskDto.CheckPoint,
                                            };
                                            result = await srmCmdVM.DealCmdSendAsync(inputSend,"");
                                            if (result.code == ResCode.OK)
                                            {
                                                item.Plc2WcsStep = 2;
                                                item.Wcs2PlcStep = 1;
                                                item.Mode = 20;
                                                //DC.UpdateEntity(item);
                                                //DC.SaveChanges();
                                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                                await ((DbContext)DC).BulkSaveChangesAsync();
                                            }
                                            else
                                            {
                                                item.Mode = 10;
                                                //DC.UpdateEntity(item);
                                                //DC.SaveChanges();
                                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                                await ((DbContext)DC).BulkSaveChangesAsync();
                                                msg = $"【{item.Device_Code}】处理指令下发失败,托盘号【{srmTaskDto.PalletBarcode}】,可继续下发";
                                                logger.Warn(desc + msg);
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception($"设备：{item.Device_Code}，指令下发失败，任务流水号:{srmTaskDto.SerialNo}");
                                        }
                                    }
                                }
                                else
                                {
                                    item.Mode = 10;
                                    item.Plc2WcsStep = 1;
                                    //DC.UpdateEntity(item);
                                    //DC.SaveChanges();
                                    await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                }
                            }
                            else
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());
                                item.Plc2WcsStep = 1;
                                //DC.UpdateEntity(item);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                        }
                        #region 根据标记的待机状态发送任务
                        else
                        {
                            if (item.Mode == 10)
                            {
                                SrmTaskDto srmTaskDto = new SrmTaskDto();

                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = item.Device_Code,
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                //获取堆垛机指令
                                result = await srmCmdVM.GetSrmCmdInfoAsync(input);
                                if (result.code == ResCode.OK)
                                {
                                    srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                                    if (srmTaskDto != null)
                                    {
                                        //下发指令
                                        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 1);
                                        if (res == true)
                                        {
                                            DealCmdSendDto inputSend = new DealCmdSendDto()
                                            {
                                                deviceNo = item.Device_Code,
                                                taskNo = srmTaskDto.SerialNo,
                                                palletBarcode = srmTaskDto.PalletBarcode,
                                                checkPoint = srmTaskDto.CheckPoint,
                                            };
                                            result = await srmCmdVM.DealCmdSendAsync(inputSend, "");
                                            if (result.code == ResCode.OK)
                                            {
                                                item.Plc2WcsStep = 2;
                                                item.Wcs2PlcStep = 1;
                                                item.Mode = 20;
                                                //DC.UpdateEntity(item);
                                                //DC.SaveChanges();
                                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                                await ((DbContext)DC).BulkSaveChangesAsync();
                                            }
                                            else
                                            {
                                                item.Mode = 10;
                                                //DC.UpdateEntity(item);
                                                //DC.SaveChanges();
                                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                                await ((DbContext)DC).BulkSaveChangesAsync();
                                                msg = $"【{item.Device_Code}】处理指令下发失败,托盘号【{srmTaskDto.PalletBarcode}】,可继续下发";
                                                logger.Warn(desc + msg);
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception($"设备：{item.Device_Code}，指令下发失败，任务流水号:{srmTaskDto.SerialNo}");
                                        }
                                    }
                                }

                            }
                        }
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                    #endregion
                }
                #endregion

                #region Plc2WcsStep = 1 收到srm2wcs信息 更新状态
                if (item.Plc2WcsStep == 1)
                {
                    SrmTaskDto srmTaskDto = new SrmTaskDto();
                    List<int> actionDealTypes = new List<int>() { 5, 7, 14, 15, 16, 19, 20 };
                    try
                    {
                        //await ((DbContext)dc).Database.BeginTransactionAsync();
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        if ((int)srm.srmSocketWcs.ActionType.Value == 0)
                        {
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "动作类型为0", srm.srmSocketWcs.ToJson());
                        }
                        // 处理堆垛机任务反馈
                        else if (actionDealTypes.Contains((int)srm.srmSocketWcs.ActionType.Value))
                        {
                            DealSrmTaskDto input = new DealSrmTaskDto()
                            {
                                deviceNo = item.Device_Code,
                                taskNo = srm.srmSocketWcs.TaskNo.Value,
                                palletBarcode = srm.srmSocketWcs.PalletBarcode.Value,
                                actionType = srm.srmSocketWcs.ActionType.Value,
                                checkPoint = srm.srmSocketWcs.CheckPoint.Value,
                                waferID = srm.srmSocketWcs.WaferID.Value.ToSiemensString(),
                            };
                            result = await srmCmdVM.DealSrmTaskAsync(input,"");
                            if (result.code == ResCode.OK)
                            {
                                if ((short)srm.srmSocketWcs.ActionType.Value == 5 || (short)srm.srmSocketWcs.ActionType.Value == 7)
                                {
                                    item.Mode = 10;
                                }
                                item.Plc2WcsStep = 4;
                                //DC.UpdateEntity(item);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            else
                            {
                                msg = $"【{item.Device_Code}】处理指令反馈失败,托盘号【{srm.srmSocketWcs.PalletBarcode.Value}】，动作类型【{srm.srmSocketWcs.ActionType.Value}】,可继续处理";
                                logger.Warn(desc + msg);
                            }
                        }
                        else
                        {
                            // 直接 完成 srm2wcs stb ack交互 
                            item.Plc2WcsStep = 4;
                            //DC.UpdateEntity(item);
                            //DC.SaveChanges();
                            await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }

                }
                #endregion

                #region  Plc2WcsStep = 2 / 20 完成 wcs下发任务的  stb ack交互  /   Plc2WcsStep = 20 下一步 Plc2WcsStep = 4
                if (item.Plc2WcsStep == 2 || item.Plc2WcsStep == 20)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        // 写 wcs2plc stb = 1
                        if (item.Wcs2PlcStep == 1)
                        {
                            // 写交互信号
                            if (srm.wcsSocketSrm.stb.Value == 1)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "", srm.wcsSocketSrm.ToJson());

                                item.Wcs2PlcStep = 2;
                                //DC.UpdateEntity(item);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                        }

                        // 等待 wcs2plc ack = 1
                        if (item.Wcs2PlcStep == 2)
                        {

                            if (srm.wcsSocketSrm.stb.Value == 1 && srm.wcsSocketSrm.ack.Value == 1)
                            {
                                Thread.Sleep(1000);
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "", srm.wcsSocketSrm.ToJson());
                                //写STB为0
                                SrmTaskDto srmTaskDto = new SrmTaskDto();
                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = item.Device_Code,
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                //获取堆垛机指令
                                result = await srmCmdVM.GetSrmCmdInfoAsync(input);
                                if (result.code == ResCode.OK)
                                {
                                    srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                                    if (srmTaskDto != null)
                                    {
                                        //下发指令
                                        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 0);

                                        // 记录通讯报文
                                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "写任务记录", srmTaskDto.ToJson() + srm.wcsSocketSrm.ToJson());
                                        item.Wcs2PlcStep = 0;
                                        item.Mode = 20;
                                        // 删除任务发送
                                        if (item.Plc2WcsStep == 20)
                                        {
                                            item.Plc2WcsStep = 4;
                                        }
                                        else
                                        {
                                            item.Plc2WcsStep = 3;
                                        }
                                        //DC.UpdateEntity(item);
                                        //DC.SaveChanges();
                                        await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                        await ((DbContext)DC).BulkSaveChangesAsync();
                                    }
                                }
                            }
                        }
                        #region 丢弃

                        // 写 wcs2plc stb = 0
                        if (item.Wcs2PlcStep == 3)
                        {
                            if (srm.wcsSocketSrm.stb.Value == 0 && (srm.wcsSocketSrm.ack.Value == 1 || srm.wcsSocketSrm.ack.Value == 0))//6-20更改
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "", srm.wcsSocketSrm.ToJson());

                                item.Wcs2PlcStep = 4;
                                //DC.UpdateEntity(item);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            else
                            {
                                SrmTaskDto srmTaskDto = new SrmTaskDto();
                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = item.Device_Code,
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                //获取堆垛机指令
                                result = await srmCmdVM.GetSrmCmdInfoAsync(input);
                                if (result.code == ResCode.OK)
                                {
                                    srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                                    if (srmTaskDto != null)
                                    {
                                        //下发指令
                                        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 0);
                                        // 记录通讯报文
                                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "写任务记录", srmTaskDto.ToJson());
                                    }
                                }
                            }
                        }

                        // 等待 wcs2plc ack = 0
                        if (item.Wcs2PlcStep == 4)
                        {
                            if (srm.wcsSocketSrm.stb.Value == 0 && srm.wcsSocketSrm.ack.Value == 0)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "", srm.wcsSocketSrm.ToJson());

                                item.Wcs2PlcStep = 0;

                                // 删除任务发送
                                if (item.Plc2WcsStep == 20)
                                {
                                    item.Plc2WcsStep = 4;
                                }
                                else
                                {
                                    item.Plc2WcsStep = 3;
                                }
                                //DC.UpdateEntity(item);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                        }
                        #endregion
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }

                }
                #endregion

                #region Plc2WcsStep = 3 调用DealCmdSendAsync更新任务状态
                if (item.Plc2WcsStep == 3)
                {
                    try
                    {
                        // 反馈的DataTable有那些数据, 如果DT > 0 ，回数据，负责为0或者Null回ACK                     
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        DealCmdSendDto input = new DealCmdSendDto()
                        {
                            deviceNo = item.Device_Code,
                            taskNo = srm.wcsSocketSrm.TaskNo.Value,
                            palletBarcode = srm.wcsSocketSrm.PalletBarcode.Value,
                            checkPoint = srm.wcsSocketSrm.CheckPoint.Value,
                        };
                        result = await srmCmdVM.DealCmdSendAsync(input, "");
                        if (result.code == ResCode.OK)
                        {
                            item.Plc2WcsStep = 7;
                            //DC.UpdateEntity(item);
                            //DC.SaveChanges();
                            await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else
                        {
                            msg = $"【{item.Device_Code}】处理指令下发失败,托盘号【{srm.wcsSocketSrm.PalletBarcode.Value}】,可继续下发";
                            logger.Warn(desc + msg);
                        }

                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                }
                #endregion
                #region Plc2WcsStep = 4 回 srm2wcs ack=1
                if (item.Plc2WcsStep == 4)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        byte[] bytes = ConvertSrmDataToByteArray(srm, 1);
                        SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                        socketByte.Value = bytes;
                        SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());

                        item.Plc2WcsStep = 7;
                        //DC.UpdateEntity(item);
                        //DC.SaveChanges();
                        await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                    
                }
                #endregion

                #region Plc2WcsStep = 5 等待 srm2wcs 的 stb = 0
                if (item.Plc2WcsStep == 5)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        if (srm.srmSocketWcs.ack.Value == 0)
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());

                            item.Plc2WcsStep = 6;
                            //DC.UpdateEntity(item);
                            //await DC.SaveChangesAsync();
                            await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                }
                #endregion

                #region Plc2WcsStep = 6 复位 srm2wcs ack=0
                if (item.Plc2WcsStep == 6)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        if (srm.srmSocketWcs.stb.Value == 0 && srm.srmSocketWcs.ack.Value == 0)
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());

                            item.Plc2WcsStep = 7;
                            //DC.UpdateEntity(item);
                            //await DC.SaveChangesAsync();
                            await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else
                        {
                            byte[] bytes = ConvertSrmDataToByteArray(srm, 0);
                            SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                            socketByte.Value = bytes;
                            SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                        }
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                    
                }
                #endregion

                #region Plc2WcsStep = 7 完成 srm2wcs的交互
                if (item.Plc2WcsStep == 7)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        item.Plc2WcsStep = 0;
                        //DC.UpdateEntity(item);
                        //await DC.SaveChangesAsync();
                        await ((DbContext)DC).Set<DeviceConfig>().SingleUpdateAsync(item);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = $"【{item.Device_Code}】处理PLC步序【{item.Plc2WcsStep}】失败,原因【{ex.Message}】";
                        logger.Warn(desc + msg);
                        continue;
                    }
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// 给Socket写指令
        /// </summary>
        /// <param name="srmTaskDto"></param>
        /// <param name="plcCode"></param>
        /// <param name="srm"></param>
        /// <param name="Stb"></param>
        /// <returns></returns>
        private bool DoWcs2SrmTask(SrmTaskDto srmTaskDto, string plcCode, WishSrmSocket srm, Int16 Stb)
        {
            bool res = false;
            #region 判断是否写入成功
            //res = DoForkTask(srm, plcCode, srmTaskDto, Stb);
            #endregion
            byte[] data = ConvertDataToByteArray(srmTaskDto, Stb);
            SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
            socketByte.Value = data;
            SocketClientService.GetClient().WriteSignal(plcCode, socketByte, null);
            res = true;
            return res;
        }

        /// <summary>
        /// 任务信息转byte[]
        /// </summary>
        /// <param name="srmTaskDto"></param>
        /// <param name="Stb"></param>
        /// <returns></returns>
        private static byte[] ConvertDataToByteArray(SrmTaskDto srmTaskDto, Int16 Stb)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(srmTaskDto.SerialNo);
                    binaryWriter.Write(srmTaskDto.ActionType);
                    binaryWriter.Write(srmTaskDto.PalletBarcode);
                    //string fixedLengthBarcode = srmTaskDto.PalletBarcode.PadRight(20, '\0');
                    //binaryWriter.Write(Encoding.UTF8.GetBytes(fixedLengthBarcode));
                    if (srmTaskDto.ActionType == (short)6 && srmTaskDto.TaskType.Equals("MOVE"))
                    {
                        binaryWriter.Write(srmTaskDto.TargetRow);
                        binaryWriter.Write(srmTaskDto.TargetColumn);
                        binaryWriter.Write(srmTaskDto.TargetLayer);
                        srmTaskDto.CheckPoint = (short)(srmTaskDto.SerialNo + srmTaskDto.TargetRow + srmTaskDto.TargetColumn + srmTaskDto.TargetLayer + 1);
                    }
                    else if (srmTaskDto.TaskType.Equals("OUT"))
                    {
                        binaryWriter.Write(srmTaskDto.TargetRow);
                        binaryWriter.Write(srmTaskDto.TargetColumn);
                        binaryWriter.Write(srmTaskDto.TargetLayer);
                    }
                    else
                    {
                        binaryWriter.Write(srmTaskDto.SourceRow);
                        binaryWriter.Write(srmTaskDto.SourceColumn);
                        binaryWriter.Write(srmTaskDto.SourceLayer);
                    }
                    short moveStation = 1;
                    if (srmTaskDto.TaskType.Equals("MOVE"))
                    {
                        binaryWriter.Write(moveStation);
                    }
                    else if (srmTaskDto.TaskType.Equals("OUT"))
                    {
                        binaryWriter.Write(srmTaskDto.TargetStationNo);
                    }
                    else
                    {
                        binaryWriter.Write(srmTaskDto.SourceStationNo);
                    }
                    short target = 0;
                    binaryWriter.Write(target);
                    binaryWriter.Write(target);
                    binaryWriter.Write(srmTaskDto.AlarmCode);
                    binaryWriter.Write(srmTaskDto.Station);
                    binaryWriter.Write(srmTaskDto.Sign);
                    binaryWriter.Write(srmTaskDto.CheckPoint);
                    binaryWriter.Write(Stb);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 回复socket的STB信息
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="Stb"></param>
        /// <returns></returns>
        private static byte[] ConvertSrmDataToByteArray(WishSrmSocket srm, Int16 Stb)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(srm.srmSocketWcs.TaskNo.Value);
                    binaryWriter.Write(srm.srmSocketWcs.ActionType.Value);
                    binaryWriter.Write(srm.srmSocketWcs.PalletBarcode.Value);
                    binaryWriter.Write(srm.srmSocketWcs.SourceRow.Value);
                    binaryWriter.Write(srm.srmSocketWcs.SourceColumn.Value);
                    binaryWriter.Write(srm.srmSocketWcs.SourceLayer.Value);
                    binaryWriter.Write(srm.srmSocketWcs.StationNo.Value);
                    binaryWriter.Write(srm.srmSocketWcs.TargetColumn.Value);
                    binaryWriter.Write(srm.srmSocketWcs.TargetLayer.Value);
                    binaryWriter.Write(srm.srmSocketWcs.AlarmCode.Value);
                    binaryWriter.Write(srm.srmSocketWcs.Station.Value);
                    binaryWriter.Write(srm.srmSocketWcs.Sign.Value);
                    binaryWriter.Write(srm.srmSocketWcs.CheckPoint.Value);
                    binaryWriter.Write(Stb);
                }
                return memoryStream.ToArray();
            }
        }

    }
}
