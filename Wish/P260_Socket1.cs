//=============================================================================
//                                 A220101
//=============================================================================
//
// WCS 主流程。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/25
//      创建
//
//-----------------------------------------------------------------------------


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

using Wish.HWConfig.Models;
using Wish.Service;
using ASRS.WCS.PLC;
using ASRS.WCS.Common.Util;
using WISH.WCS.Device.SrmSocket.WishSrmV10Udt;
using ASRS.WCS.Common.Enum;
using WISH.WCS.Device.SrmSocket;
using Wish.Areas.TaskConfig.Model;
using MySqlConnector;
using NPOI.SS.Formula.Functions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using HslCommunication.Enthernet;
using Quartz.Impl.Triggers;
using Wish.Model;
using Wish.Models.ImportDto;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Services;
using SixLabors.ImageSharp.PixelFormats;
using Wish.Controllers;
using log4net;
using System.Text;
using WISH.Helper.Common;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;

namespace WISH.Custome
{
    public class P260_Socket1
    {
        private bool isRunning = false;
        private Task currentRunTask = null;
        private static ILog logger = LogManager.GetLogger(typeof(P260_Socket1));
        public void Start()
        {
            isRunning = true;
            Task.Factory.StartNew(async () =>
            {
                while (isRunning)
                {
                    //Task.Run(Run).Wait();
                    await Run();
                    //Thread.Sleep(1000);
                    await Task.Delay(TimeSpan.FromMilliseconds(300));
                    //Task.Delay(TimeSpan.FromMilliseconds(500)).Wait();
                }
            });
            //Task.Factory.StartNew(ExecuteLoop);
        }
        //private async Task ExecuteLoop()
        //{
        //    while (isRunning)
        //    {
        //        if (currentRunTask == null || currentRunTask.IsCompleted)
        //        {
        //            currentRunTask = Run();
        //            await currentRunTask;
        //        }
        //        await Task.Delay(TimeSpan.FromMilliseconds(500));
        //    }
        //}
        public void Stop()
        {
            isRunning = false;
        }

        public P260_Socket1()
        {
        }

        /// <summary>
        /// 业务逻辑实现
        /// </summary>
        private async Task Run()
        {
            try
            {
                await DoSrmToWcsWork();
                Thread.Sleep(300);

            }
            catch (Exception ex)
            {
                logger.Warn("设备循环："+ex.Message);
            }
            //try
            //{
            //    //await WcsToWmsDeviceInfo();//设备信息
            //    //WcsToWmsDeviceInfo();//设备信息
            //    //await DoWcsToSrmWork();
            //}
            //catch (Exception ex)
            //{

            //    ContextService.Log.Error(ex.Message);
            //}
        }
        //private List<DeviceConfig> srmDevices = null;

        /// <summary>
        /// 处理堆垛机反馈流程
        /// </summary>
        private async Task DoSrmToWcsWork()
        {
            string desc = "处理堆垛机信号：";
            string msg = string.Empty;
            BusinessResult result = new BusinessResult();
            #region 1 堆垛机正常任务
            List<DeviceConfig> srmDevices = null;
            if (srmDevices == null)
                srmDevices = DCService.GetInstance().GetDC().Set<DeviceConfig>().Include(x => x.PlcConfig).Include(x => x.StandardDevice).Where(x => x.StandardDevice.DeviceType == EDeviceType.S1Srm && "WISH.SRM.V10".Equals(x.StandardDevice.Device_Code) && x.IsEnabled == true)
                    .ToList();
            foreach (var item in srmDevices)
            {
                WishSrmSocket srm = (WishSrmSocket)SocketClientService.GetClient().GetRegisterAutoReadObject(item.PlcConfig.Plc_Code, item.Device_Code);
                var plcInfoW = DCService.GetInstance().GetDC().Set<PlcConfig>().Include(x => x.DBConfigs).Where(x => x.Plc_Name == item.Device_Code).ToList();
                string plcCodeW=string.Empty;
                string plcCodeS=string.Empty;
                if (!item.Device_Code.Equals("SCA01"))
                {
                    plcCodeW = plcInfoW.Where(x => x.DBConfigs[0].Block_Name == "W").Select(x => x.Plc_Code).FirstOrDefault().ToString();
                    plcCodeS = plcInfoW.Where(x => x.DBConfigs[0].Block_Name == "S").Select(x => x.Plc_Code).FirstOrDefault().ToString();
                }
                
                if (srm == null) continue;
                /// 修改设备在线状态
                if (srm.Sockets.IsConnect != item.IsOnline)
                {
                    item.IsOnline = srm.Sockets.IsConnect;
                    DCService.GetInstance().UpdateEntity(item);
                }
                WcsTaskService wcsTaskService = new WcsTaskService();
                var hasParentTransaction = false;
                IDataContext dc = DCService.GetInstance().GetDC();
                if (dc.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }
                if ((item.Plc2WcsStep != 0 && item.Mode == 20) || item.Plc2WcsStep == 4 || item.Plc2WcsStep == 5 || item.Plc2WcsStep == 6 || item.Plc2WcsStep == 7)
                {
                    logger.Warn($"{DateTime.Now}----->{desc}{item.Device_Code}逻辑处理开始");
                    Console.WriteLine($"{DateTime.Now}----->{desc}{item.Device_Code}逻辑处理开始");
                }
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                #region Plc2WcsStep = 0 srm2wcs 无交互信息
                if (item.Plc2WcsStep == 0)
                {
                    if (hasParentTransaction == false)
                    {
                        await dc.Database.BeginTransactionAsync();
                    }

                    try
                    {
                        //if (srm.srmSocketWcs.stb.Value == 1)
                        if (srm.srmSocketWcs.ack.Value == 1)
                        {
                            if (srm.srmSocketWcs.ActionType.Value == 0)
                            {
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为0-ACK为1动作类型为0", srm.srmSocketWcs.ToJson());
                                logger.Warn($"设备{item.Device_Code}:步序为0-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}:步序为0-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                            }
                            //else if (srm.srmSocketWcs.ActionType.Value == 11)
                            //{
                            //    //待命时先回复STB再找任务下发
                            //    byte[] bytes = ConvertSrmDataToByteArray(srm, 1);
                            //    SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                            //    socketByte.Value = bytes;
                            //    SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                            //    // 记录通讯报文
                            //    MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为0-ACK为1动作类型为11", srm.srmSocketWcs.ToJson());
                            //    //找任务下发
                            //    SrmTaskDto srmTaskDto = new SrmTaskDto();


                            //    GetSrmTaskDto input = new GetSrmTaskDto
                            //    {
                            //        taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                            //        deviceNo = item.Device_Code,
                            //        palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                            //    };
                            //    //获取堆垛机指令
                            //    srmTaskDto = await wcsTaskService.GetSrmCmdAsync(input);

                            //    if (srmTaskDto != null)
                            //    {
                            //        //下发指令
                            //        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 1);
                            //        if (res == true)
                            //        {
                            //            // 记录通讯报文
                            //            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为0-ACK为1查询到指令并下发", srmTaskDto.ToJson());
                            //            DealCmdSendDto inputSend = new DealCmdSendDto()
                            //            {
                            //                deviceNo = item.Device_Code,
                            //                taskNo = srmTaskDto.SerialNo,
                            //                palletBarcode = srmTaskDto.PalletBarcode,
                            //                checkPoint = srmTaskDto.CheckPoint,
                            //            };
                            //            await wcsTaskService.DealCmdSendAsync(inputSend);
                            //            item.Plc2WcsStep = 2;
                            //            item.Wcs2PlcStep = 1;
                            //            item.Mode = 20;
                            //            dc.UpdateEntity(item);
                            //            dc.SaveChanges();
                            //        }
                            //        else
                            //        {
                            //            throw new Exception($"设备：{item.Device_Code}，指令下发失败，任务流水号:{srmTaskDto.SerialNo}");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        item.Mode = 10;
                            //        item.Plc2WcsStep = 1;
                            //        dc.UpdateEntity(item);
                            //        dc.SaveChanges();
                            //    }
                            //}
                            else
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, $"步序为0-ACK为1动作类型为其他{srm.srmSocketWcs.ActionType.Value}", srm.srmSocketWcs.ToJson());
                                logger.Warn($"设备{item.Device_Code}:步序为0-ACK为1动作类型为其他{srm.srmSocketWcs.ActionType.Value},{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}:步序为0-ACK为1动作类型为其他{srm.srmSocketWcs.ActionType.Value},{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                item.Plc2WcsStep = 1;
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                        }
                        #region 根据标记的待机状态发送任务
                        else
                        {
                            //if (srm.IsReady())
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
                                srmTaskDto = await wcsTaskService.GetSrmCmdAsync(input);

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
                                        result = await wcsTaskService.DealCmdSendAsync(inputSend);
                                        if (result.code == ResCode.Error)
                                        {
                                            msg = $"设备：「{item.Device_Code}」发生异常：{result.msg}";
                                            logger.Warn($"----->Warn----->{desc}:{msg} ");
                                            Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                                            //logger.Warn(msg);
                                        }
                                        item.Plc2WcsStep = 2;
                                        item.Wcs2PlcStep = 1;
                                        item.Mode = 20;
                                        dc.UpdateEntity(item);
                                        dc.SaveChanges();
                                    }
                                    else
                                    {
                                        throw new Exception($"设备：{item.Device_Code}，指令下发失败，任务流水号:{srmTaskDto.SerialNo}");
                                    }
                                }


                            }
                        }

                        //MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, $"步序为0-动作类型为{(int)srm.srmSocketWcs.ActionType.Value}", "");
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }
                    #endregion
                }
                #endregion

                #region Plc2WcsStep = 1 收到srm2wcs信息 更新状态
                if (item.Plc2WcsStep == 1)
                {
                    SrmTaskDto srmTaskDto = new SrmTaskDto();
                    //IDataContext dc = DCService.GetInstance().GetDC();
                    List<int> actionDealTypes = new List<int>() { 5, 7, 14, 15, 16, 19, 20 };
                    try
                    {
                        //await ((DbContext)dc).Database.BeginTransactionAsync();
                        //dc.Database.BeginTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.BeginTransactionAsync();
                        }

                        if ((int)srm.srmSocketWcs.ActionType.Value == 0)
                        {
                            if (srm.srmSocketWcs.ack.Value == 1)
                            {
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为1-ACK为1动作类型为0", srm.srmSocketWcs.ToJson());
                                logger.Warn($"设备{item.Device_Code}: 步序为1-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序为1-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                            }
                            else
                            {
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为1-ACK为0动作类型为0", srm.srmSocketWcs.ToJson());
                                logger.Warn($"设备{item.Device_Code}: 步序为1-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序为1-ACK为1动作类型为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                item.Plc2WcsStep = 7;
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                        }
                        // 处理堆垛机任务反馈
                        //else if (actionDealTypes.Contains((int)srm.srmSocketWcs.ActionType.Value))
                        else if (actionDealTypes.Where(x => x == (int)srm.srmSocketWcs.ActionType.Value).Any())
                        {
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, $"步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value}", srm.srmSocketWcs.ToJson());
                            logger.Warn($"设备{item.Device_Code}: 步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value},{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                            Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value},{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");

                            DealSrmTaskDto input = new DealSrmTaskDto()
                            {
                                deviceNo = item.Device_Code,
                                taskNo = srm.srmSocketWcs.TaskNo.Value,
                                palletBarcode = srm.srmSocketWcs.PalletBarcode.Value,
                                actionType = srm.srmSocketWcs.ActionType.Value,
                                checkPoint = srm.srmSocketWcs.CheckPoint.Value,
                                waferID = srm.srmSocketWcs.WaferID.Value.ToSiemensString(),
                            };
                            Stopwatch stopwatchBy1 = new Stopwatch();
                            stopwatchBy1.Start();
                            result = await wcsTaskService.DealSrmTaskAsync(input);
                            stopwatchBy1.Stop();
                            TimeSpan elapsedTimeBy1 = stopwatchBy1.Elapsed;

                            logger.Warn($"设备{item.Device_Code}: 步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value}，处理用时【{elapsedTimeBy1.TotalMilliseconds}】毫秒");
                            Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:设备{item.Device_Code}: 步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value}，处理用时【{elapsedTimeBy1.TotalMilliseconds}】毫秒 ");
                            if (result.code == ResCode.Error)
                            {
                                msg = $"设备：「{item.Device_Code}」发生异常：{result.msg}";
                                logger.Warn($"----->Warn----->{desc}:{msg} ");
                                Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, $"步序为1-动作类型为{(int)srm.srmSocketWcs.ActionType.Value}", msg);
                                //logger.Warn(msg);
                            }
                            else
                            {
                                msg = $"设备：「{item.Device_Code}」：{result.msg}";
                                logger.Warn($"----->Warn----->{desc}:{msg} ");
                            }
                            // 如果是空出/满入 发送删除任务
                            //if (srmTaskDto != null)
                            //{
                            //    // 删除任务
                            //    //if (dt.Rows[0]["COMMAND_TYPE"].ToString() == "3") { }
                            //    bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 1);
                            //    if (res == true)
                            //    {
                            //        item.Plc2WcsStep = 20;
                            //        item.Wcs2PlcStep = 1;
                            //        //item.Plc2WcsStep = 7;//P475
                            //        dc.UpdateEntity(item);
                            //        dc.SaveChanges();
                            //    }
                            //    else
                            //    {
                            //        throw new Exception("任务下发失败" + "dt.Rows[0][\"Task_Id\"].ToString()");
                            //    }
                            //}
                            //else
                            //{
                            // 直接 完成 srm2wcs stb ack交互 
                            if ((short)srm.srmSocketWcs.ActionType.Value == 5 || (short)srm.srmSocketWcs.ActionType.Value == 19 || (short)srm.srmSocketWcs.ActionType.Value == 7)
                            {
                                item.Mode = 10;
                            }
                            item.Plc2WcsStep = 4;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                            //}
                        }
                        else if (srm.srmSocketWcs.ActionType.Value == 11)
                        {
                            item.Mode = 10;
                            item.Plc2WcsStep = 4;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                        }
                        else if (srm.srmSocketWcs.ActionType.Value == 24)//工单反馈
                        {
                            if (item.Device_Code.Equals("SCA01"))//扫码枪
                            {
                                if (!string.IsNullOrWhiteSpace(srm.srmSocketWcs.WaferID.Value.ToSiemensString()))
                                {
                                    result = await wcsTaskService.CreateOutRequest(srm.srmSocketWcs.WaferID.Value.ToSiemensString(), item.Device_Code);
                                    if (result.code == ResCode.Error)
                                    {
                                        msg = $"设备：「{item.Device_Code}」发生异常：{result.msg}";
                                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                                        //logger.Warn(msg);
                                    }
                                }
                            }
                            item.Plc2WcsStep = 4;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                        }
                        else if (srm.srmSocketWcs.ActionType.Value == 25)//机械手请求获取出库指令是否存在
                        {
                            if (item.Device_Code.Equals("SCA01"))//机械手
                            {
                                SrmTaskDto srmTaskOutDto = new SrmTaskDto();

                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = $"SRM0{srm.srmSocketWcs.StationNo.Value.ToString()}",
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序1--》机械手反馈的堆垛机编号SRM0{srm.srmSocketWcs.StationNo.Value.ToString()}");
                                //获取堆垛机出库指令
                                srmTaskOutDto = await wcsTaskService.GetSrmOutCmdAsync(input);

                                if (srmTaskOutDto != null)
                                {
                                    byte[] bytes = ConvertMtpDataToByteArray(srm, 1);
                                    SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                                    socketByte.Value = bytes;
                                    SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                                    // 记录通讯报文
                                    MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, $"步序1-写STB为1堆垛机{item.Device_Code}有出库指令", srm.srmSocketWcs.ToJson());
                                    logger.Warn($"设备{item.Device_Code}: 步序1-写STB为1堆垛机{item.Device_Code}有出库指令,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                                    Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序1-写STB为1堆垛机{item.Device_Code}有出库指令,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");

                                    item.Plc2WcsStep = 5;
                                    dc.UpdateEntity(item);
                                    dc.SaveChanges();
                                }
                                else
                                {
                                    item.Plc2WcsStep = 4;
                                    dc.UpdateEntity(item);
                                    dc.SaveChanges();
                                }
                            }
                            else
                            {
                                item.Plc2WcsStep = 4;
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                        }
                        else
                        {
                            // 直接 完成 srm2wcs stb ack交互 
                            item.Plc2WcsStep = 4;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                        }

                        //dc.Database.CommitTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }

                    }
                    catch (Exception ex)
                    {
                        //dc.Database.RollbackTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        //ContextService.Log.Error(ex);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }

                }
                #endregion

                #region  Plc2WcsStep = 2 / 20 完成 wcs下发任务的  stb ack交互  /   Plc2WcsStep = 20 下一步 Plc2WcsStep = 4
                if (item.Plc2WcsStep == 2 || item.Plc2WcsStep == 20)
                {
                    //IDataContext dc = DCService.GetInstance().GetDC();
                    try
                    {
                        //dc.Database.BeginTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.BeginTransactionAsync();
                        }
                        // 写 wcs2plc stb = 1
                        if (item.Wcs2PlcStep == 1)
                        {
                            // 写交互信号
                            if (srm.wcsSocketSrm.stb.Value == 1)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "WCS步序为1-当前WCSTOPLC的STB为1", srm.wcsSocketSrm.ToJson());
                                logger.Warn($"设备{item.Device_Code}: WCS步序为1-当前WCSTOPLC的STB为1,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: WCS步序为1-当前WCSTOPLC的STB为1,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");

                                item.Wcs2PlcStep = 2;
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                            //else
                            //{
                            //    SocketSignal<bool> stb = (SocketSignal<bool>)srm.wcsSocketSrm.stb.Clone();
                            //    stb.Value = true;
                            //    SocketClientService.GetClient().WriteSignal(plcCodeW, stb, null);
                            //}
                        }

                        // 等待 wcs2plc ack = 1
                        if (item.Wcs2PlcStep == 2)
                        {

                            if (srm.wcsSocketSrm.stb.Value == 1 && srm.wcsSocketSrm.ack.Value == 1)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "WCS步序为2-当前WCSTOPLC的STB为1ACK为1", srm.wcsSocketSrm.ToJson());
                                logger.Warn($"设备{item.Device_Code}: WCS步序为2-当前WCSTOPLC的STB为1ACK为1,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: WCS步序为2-当前WCSTOPLC的STB为1ACK为1,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");
                                //写STB为0
                                SrmTaskDto srmTaskDto = new SrmTaskDto();
                                GetSrmTaskDto input = new GetSrmTaskDto
                                {
                                    taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                                    deviceNo = item.Device_Code,
                                    palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                                };
                                //获取堆垛机指令
                                srmTaskDto = await wcsTaskService.GetSrmCmdAsync(input);
                                if (srmTaskDto != null)
                                {
                                    //下发指令
                                    bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 0);

                                    // 记录通讯报文
                                    MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "WCS步序为2-写任务记录WCSTOPLC的STB为0", srmTaskDto.ToJson() + srm.wcsSocketSrm.ToJson());
                                    logger.Warn($"设备{item.Device_Code}: WCS步序为2-写任务记录WCSTOPLC的STB为0,{EPlcCommDirect.Wcs2Plc},{srmTaskDto.ToJson() + srm.wcsSocketSrm.ToJson()}");
                                    Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: WCS步序为2-写任务记录WCSTOPLC的STB为0,{EPlcCommDirect.Wcs2Plc},{srmTaskDto.ToJson() + srm.wcsSocketSrm.ToJson()}");
                                }
                                item.Wcs2PlcStep = 3;
                                //item.Wcs2PlcStep = 0;
                                item.Mode = 20;
                                //// 删除任务发送，不需要调用Pro_Srm_Set_Task
                                //if (item.Plc2WcsStep == 20)
                                //{
                                //    item.Plc2WcsStep = 4;
                                //}
                                //else
                                //{
                                //    item.Plc2WcsStep = 3;
                                //}
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                        }

                        // 写 wcs2plc stb = 0
                        if (item.Wcs2PlcStep == 3)
                        {
                            if (srm.wcsSocketSrm.stb.Value == 0 && srm.wcsSocketSrm.ack.Value == 0)//6-20更改
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "WCS步序为3-WCS的STB为0，ACK为0", srm.wcsSocketSrm.ToJson());
                                logger.Warn($"设备{item.Device_Code}: WCS步序为3-WCS的STB为0，ACK为0,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: WCS步序为3-WCS的STB为0，ACK为0,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");

                                item.Wcs2PlcStep = 4;
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                            //else
                            //{
                            //    SrmTaskDto srmTaskDto = new SrmTaskDto();
                            //    GetSrmTaskDto input = new GetSrmTaskDto
                            //    {
                            //        taskNo = srm.srmSocketWcs.TaskNo.Value.ToString(),
                            //        deviceNo = item.Device_Code,
                            //        palletBarcode = srm.srmSocketWcs.PalletBarcode.Value.ToString(),
                            //    };
                            //    //获取堆垛机指令
                            //    srmTaskDto = await wcsTaskService.GetSrmCmdAsync(input);
                            //    if (srmTaskDto != null)
                            //    {
                            //        //下发指令
                            //        bool res = DoWcs2SrmTask(srmTaskDto, plcCodeW, srm, 0);
                            //        // 记录通讯报文
                            //        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "写任务记录", srmTaskDto.ToJson());
                            //    }
                            //}
                        }

                        // 等待 wcs2plc ack = 0
                        if (item.Wcs2PlcStep == 4)
                        {
                            if (srm.wcsSocketSrm.stb.Value == 0 && srm.wcsSocketSrm.ack.Value == 0)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, "WCS步序为4", srm.wcsSocketSrm.ToJson());
                                logger.Warn($"设备{item.Device_Code}: WCS步序为4,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");
                                Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: WCS步序为4,{EPlcCommDirect.Wcs2Plc},{srm.wcsSocketSrm.ToJson()}");

                                item.Wcs2PlcStep = 0;

                                // 删除任务发送，不需要调用Pro_Srm_Set_Task
                                if (item.Plc2WcsStep == 20)
                                {
                                    item.Plc2WcsStep = 4;
                                }
                                else
                                {
                                    item.Plc2WcsStep = 3;
                                }
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                        }
                        //dc.Database.CommitTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        //dc.Database.RollbackTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }

                }
                #endregion

                #region Plc2WcsStep = 3 调用 PRO_Srm_Set_Task 更新任务状态
                if (item.Plc2WcsStep == 3)
                {

                    //IDataContext dc = DCService.GetInstance().GetDC();
                    try
                    {
                        // 反馈的DataTable有那些数据, 如果DT > 0 ，回数据，负责为0或者Null回ACK                     
                        //dc.Database.BeginTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.BeginTransactionAsync();
                        }
                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序3-更新指令下发状态", srm.srmSocketWcs.ToJson());
                        logger.Warn($"设备{item.Device_Code}: 步序3-更新指令下发状态,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                        Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序3-更新指令下发状态,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                        DealCmdSendDto input = new DealCmdSendDto()
                        {
                            deviceNo = item.Device_Code,
                            taskNo = srm.wcsSocketSrm.TaskNo.Value,
                            palletBarcode = srm.wcsSocketSrm.PalletBarcode.Value,
                            checkPoint = srm.wcsSocketSrm.CheckPoint.Value,
                        };
                        result = await wcsTaskService.DealCmdSendAsync(input);
                        if (result.code == ResCode.Error)
                        {
                            msg = $"设备：「{item.Device_Code}」发生异常：{result.msg}";
                            logger.Warn($"----->Warn----->{desc}:{msg} ");
                            Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                            //logger.Warn(msg);
                        }
                        item.Plc2WcsStep = 7;
                        dc.UpdateEntity(item);
                        dc.SaveChanges();
                        //dc.Database.CommitTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        //dc.Database.RollbackTransaction();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //ContextService.Log.Error(ex);
                        //logger.Warn(msg);
                        continue;
                    }
                }
                #endregion

                #region Plc2WcsStep = 4 回 srm2wcs ack=1
                if (item.Plc2WcsStep == 4)
                {
                    //if (srm.srmSocketWcs.ack.Value == 1)
                    //if (srm.srmSocketWcs.stb.Value == 1)
                    //{

                    //    // 记录通讯报文
                    //    MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "", srm.srmSocketWcs.ToJson());

                    //    item.Plc2WcsStep = 5;
                    //    DCService.GetInstance().UpdateEntity(item);
                    //}
                    //else
                    //{
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.BeginTransactionAsync();
                        }
                        byte[] bytes = ConvertSrmDataToByteArray(srm, 1);
                        SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                        socketByte.Value = bytes;
                        SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);
                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序4-写STB为1", srm.srmSocketWcs.ToJson());
                        logger.Warn($"设备{item.Device_Code}: 步序4-写STB为1,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                        Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序4-写STB为1,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");

                        //item.Plc2WcsStep = 7;
                        item.Plc2WcsStep = 5;
                        dc.UpdateEntity(item);
                        dc.SaveChanges();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }

                    //}
                }
                #endregion

                #region Plc2WcsStep = 5 等待 srm2wcs 的 stb = 0
                if (item.Plc2WcsStep == 5)
                {
                    try
                    {
                        //if (srm.srmSocketWcs.stb.Value == 0)//&& srm.srm2Wcs.ack.Value == true
                        if (srm.srmSocketWcs.ack.Value == 0)//&& srm.srm2Wcs.ack.Value == true
                        {

                            if (hasParentTransaction == false)
                            {
                                await dc.Database.BeginTransactionAsync();
                            }
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为5-PLCTOWCS写ACK为0", srm.srmSocketWcs.ToJson());
                            logger.Warn($"设备{item.Device_Code}: 步序为5-PLCTOWCS写ACK为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                            Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序为5-PLCTOWCS写ACK为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");

                            item.Plc2WcsStep = 6;
                            //item.Plc2WcsStep = 7;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                            if (hasParentTransaction == false)
                            {
                                await dc.Database.CommitTransactionAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }

                }
                #endregion

                #region Plc2WcsStep = 6 复位 srm2wcs ack=0
                if (item.Plc2WcsStep == 6)
                {
                    try
                    {
                        if (srm.srmSocketWcs.ack.Value == 0)
                        {
                            if (hasParentTransaction == false)
                            {
                                await dc.Database.BeginTransactionAsync();
                            }
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, "步序为6-PLCtoWcs写STB为0", srm.srmSocketWcs.ToJson());
                            logger.Warn($"设备{item.Device_Code}: 步序为6-PLCtoWcs写STB为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                            Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序为6-PLCtoWcs写STB为0,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");

                            //写stb为0
                            byte[] bytes = ConvertSrmDataToByteArray(srm, 0);
                            SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.srmSocketWcs.SocketByte.Clone();
                            socketByte.Value = bytes;
                            SocketClientService.GetClient().WriteSignal(item.PlcConfig.Plc_Code, socketByte, null);

                            item.Plc2WcsStep = 7;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                            if (hasParentTransaction == false)
                            {
                                await dc.Database.CommitTransactionAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
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
                            await dc.Database.BeginTransactionAsync();
                        }
                        logger.Warn($"设备{item.Device_Code}: 步序7-结束,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                        Console.WriteLine($"{DateTime.Now}--->设备{item.Device_Code}: 步序7-结束,{EPlcCommDirect.Plc2Wcs},{srm.srmSocketWcs.ToJson()}");
                        item.Plc2WcsStep = 0;
                        dc.UpdateEntity(item);
                        dc.SaveChanges();
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.CommitTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await dc.Database.RollbackTransactionAsync();
                        }
                        //MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        msg = $"设备：「{item.Device_Code}」步序「{item.Plc2WcsStep}」发生异常：{ex.Message}";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{msg} ");
                        //logger.Warn(msg);
                        continue;
                    }
                    stopwatch.Stop();
                    TimeSpan elapsedTimeBy7 = stopwatch.Elapsed;
                    logger.Warn($"{DateTime.Now}----->{desc}{item.Device_Code}结束步序【7-->{item.Plc2WcsStep}】逻辑处理用时【{elapsedTimeBy7.TotalMilliseconds}】毫秒");
                    Console.WriteLine($"{DateTime.Now}----->{desc}{item.Device_Code}结束步序【7-->{item.Plc2WcsStep}】逻辑处理用时【{elapsedTimeBy7.TotalMilliseconds}】毫秒");
                }
                #endregion

                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;
                if ((item.Plc2WcsStep !=0 && item.Mode == 20) || item.Plc2WcsStep == 4 || item.Plc2WcsStep == 5 ||  item.Plc2WcsStep == 6 ||  item.Plc2WcsStep == 7 )
                {
                    logger.Warn($"{DateTime.Now}----->{desc}{item.Device_Code}结束步序【{item.Plc2WcsStep}】逻辑处理用时【{elapsedTime.TotalMilliseconds}】毫秒");
                    Console.WriteLine($"{DateTime.Now}----->{desc}{item.Device_Code}结束步序【{item.Plc2WcsStep}】逻辑处理用时【{elapsedTime.TotalMilliseconds}】毫秒");
                }
                #endregion
            }
        }

        /// <summary>
        /// 处理WCS写数据流程
        /// </summary>
        /// <returns></returns>
        private async Task DoWcsToSrmWork()
        {

        }

        /// <summary>
        /// wcs2srm写任务，如果写入数据成功，返回true，接下来完成stb, ack交互
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="device"></param>
        /// <param name="srm"></param>
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
            logger.Warn($"{plcCode}写信息WCSTOPLC：{string.Join("/", data)}");
            Console.WriteLine($"{DateTime.Now}--->{plcCode}写信息WCSTOPLC：{string.Join("/", data)}");
            res = true;
            return res;

        }
        #region 指令信息转换
        private byte[] TaskInfoToByte(SrmTaskDto srmTaskDto)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(srmTaskDto.SerialNo);
                    binaryWriter.Write(srmTaskDto.ActionType);
                    binaryWriter.Write(srmTaskDto.PalletBarcode);
                    binaryWriter.Write(srmTaskDto.SourceRow);
                    binaryWriter.Write(srmTaskDto.SourceColumn);
                    binaryWriter.Write(srmTaskDto.SourceLayer);
                    //binaryWriter.Write(srmTaskDto.StationNo);
                    binaryWriter.Write(srmTaskDto.TargetColumn);
                    binaryWriter.Write(srmTaskDto.TargetLayer);
                    binaryWriter.Write(srmTaskDto.AlarmCode);
                    binaryWriter.Write(srmTaskDto.Station);
                    binaryWriter.Write(srmTaskDto.Sign);
                    binaryWriter.Write(srmTaskDto.CheckPoint);
                }
                return memoryStream.ToArray();
            }
        }
        #endregion

        #region 写任务
        private bool DoForkTask(WishSrmSocket srm, string plcCode, SrmTaskDto srmTaskDto, Int16 Stb)
        {
            bool res = false;
            #region 货叉数据体

            //流水号（任务号）
            SocketSignal<Int16> taskNo = (SocketSignal<Int16>)srm.wcsSocketSrm.TaskNo.Clone();
            taskNo.Value = srmTaskDto.SerialNo;

            //动作类型
            SocketSignal<Int16> actionType = (SocketSignal<Int16>)srm.wcsSocketSrm.ActionType.Clone();
            actionType.Value = srmTaskDto.ActionType;

            //托盘号
            SocketSignal<Int32> palletBarcode = (SocketSignal<Int32>)srm.wcsSocketSrm.PalletBarcode.Clone();
            //SocketSignal<string> palletBarcode = (SocketSignal<string>)srm.wcsSocketSrm.PalletBarcode.Clone();
            palletBarcode.Value = srmTaskDto.PalletBarcode;

            //源排
            SocketSignal<Int16> sourceRow = (SocketSignal<Int16>)srm.wcsSocketSrm.SourceRow.Clone();
            sourceRow.Value = srmTaskDto.SourceRow;

            //源列
            SocketSignal<Int16> sourceColumn = (SocketSignal<Int16>)srm.wcsSocketSrm.SourceColumn.Clone();
            sourceColumn.Value = srmTaskDto.SourceColumn;

            //源层
            SocketSignal<Int16> sourceLayer = (SocketSignal<Int16>)srm.wcsSocketSrm.SourceLayer.Clone();
            sourceLayer.Value = srmTaskDto.SourceLayer;

            //库台号（目的排)
            SocketSignal<Int16> stationNo = (SocketSignal<Int16>)srm.wcsSocketSrm.StationNo.Clone();
            if (srmTaskDto.TaskType.Equals("MOVE"))
            {
                stationNo.Value = srmTaskDto.TargetRow;
            }
            else if (srmTaskDto.TaskType.Equals("OUT"))
            {
                stationNo.Value = srmTaskDto.TargetStationNo;
            }
            else
            {
                stationNo.Value = srmTaskDto.SourceStationNo;
            }

            //srmTaskDto转byte[]
            byte[] data = ConvertDataToByteArray(srmTaskDto, Stb);
            //目的列
            SocketSignal<Int16> targetColumn = (SocketSignal<Int16>)srm.wcsSocketSrm.TargetColumn.Clone();
            targetColumn.Value = srmTaskDto.TargetColumn;

            //目的层
            SocketSignal<Int16> targetLayer = (SocketSignal<Int16>)srm.wcsSocketSrm.TargetLayer.Clone();
            targetLayer.Value = srmTaskDto.TargetLayer;

            //故障号
            SocketSignal<Int16> alarmCode = (SocketSignal<Int16>)srm.wcsSocketSrm.AlarmCode.Clone();
            alarmCode.Value = srmTaskDto.AlarmCode;

            //站
            SocketSignal<Int16> station = (SocketSignal<Int16>)srm.wcsSocketSrm.Station.Clone();
            station.Value = srmTaskDto.Station;

            //标志
            SocketSignal<Int16> sign = (SocketSignal<Int16>)srm.wcsSocketSrm.Sign.Clone();
            sign.Value = srmTaskDto.Sign;

            //校验位
            SocketSignal<Int16> checkPoint = (SocketSignal<Int16>)srm.wcsSocketSrm.CheckPoint.Clone();
            checkPoint.Value = srmTaskDto.CheckPoint;
            //STB
            SocketSignal<Int16> stb = (SocketSignal<Int16>)srm.wcsSocketSrm.stb.Clone();
            stb.Value = Stb;

            SocketSignal<byte[]> socketByte = (SocketSignal<byte[]>)srm.wcsSocketSrm.SocketByte.Clone();
            socketByte.Value = data;

            #endregion
            Thread.Sleep(200);//停顿200毫秒
            List<BaseSignal> signals = new List<BaseSignal>();
            // 检查WCS2Conv的反馈数据是否和发送的数据一致

            //任务号
            bool compRes = srm.wcsSocketSrm.TaskNo.Value.Equals(taskNo.Value);
            if (compRes == false)
                signals.Add(taskNo);
            //动作类型
            compRes = srm.wcsSocketSrm.ActionType.Value.Equals(actionType.Value);
            if (compRes == false)
                signals.Add(actionType);
            //托盘号
            compRes = srm.wcsSocketSrm.PalletBarcode.Value.Equals(palletBarcode.Value);
            if (compRes == false)
                signals.Add(palletBarcode);
            //源排
            compRes = srm.wcsSocketSrm.SourceRow.Value.Equals(sourceRow.Value);
            if (compRes == false)
                signals.Add(sourceRow);
            //源列
            compRes = srm.wcsSocketSrm.SourceColumn.Value.Equals(sourceColumn.Value);
            if (compRes == false)
                signals.Add(sourceColumn);
            //源层
            compRes = srm.wcsSocketSrm.SourceLayer.Value.Equals(sourceLayer.Value);
            if (compRes == false)
                signals.Add(sourceLayer);
            //库台号（目的排）
            compRes = srm.wcsSocketSrm.StationNo.Value.Equals(stationNo.Value);
            if (compRes == false)
                signals.Add(stationNo);
            //目的列
            compRes = srm.wcsSocketSrm.TargetColumn.Value.Equals(targetColumn.Value);
            if (compRes == false)
                signals.Add(targetColumn);
            //目的层
            compRes = srm.wcsSocketSrm.TargetLayer.Value.Equals(targetLayer.Value);
            if (compRes == false)
                signals.Add(targetLayer);
            //故障号
            compRes = srm.wcsSocketSrm.AlarmCode.Value.Equals(alarmCode.Value);
            if (compRes == false)
                signals.Add(alarmCode);
            //站
            compRes = srm.wcsSocketSrm.Station.Value.Equals(station.Value);
            if (compRes == false)
                signals.Add(station);
            //标志
            compRes = srm.wcsSocketSrm.Sign.Value.Equals(sign.Value);
            if (compRes == false)
                signals.Add(sign);
            //校验位
            compRes = srm.wcsSocketSrm.CheckPoint.Value.Equals(checkPoint.Value);
            if (compRes == false)
                signals.Add(checkPoint);
            //STB
            compRes = srm.wcsSocketSrm.stb.Value.Equals(stb.Value);
            if (compRes == false)
                signals.Add(stb);
            if (signals.Count > 0)
            {
                signals.Add(socketByte);
                foreach (var signal in signals)
                {
                    if (signal is SocketSignal<byte[]>)
                    {
                        SocketClientService.GetClient().WriteSignal(plcCode, signal, null);
                    }
                }
            }
            else
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// 指令数据转Byte数组
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
                    if (srmTaskDto.ActionType == (short)6 && srmTaskDto.TaskType.Equals("MOVE"))
                    {
                        binaryWriter.Write(srmTaskDto.TargetRow);
                        binaryWriter.Write(srmTaskDto.TargetColumn);
                        binaryWriter.Write(srmTaskDto.TargetLayer);
                        srmTaskDto.CheckPoint = (short)(srmTaskDto.SerialNo + srmTaskDto.TargetRow + srmTaskDto.TargetColumn + srmTaskDto.TargetLayer + 1);
                    }
                    else if (srmTaskDto.TaskType.Equals("IN"))
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
                    //binaryWriter.Write(Encoding.ASCII.GetBytes("c"));
                    binaryWriter.Write(Encoding.ASCII.GetBytes("\r"));
                }
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        ///  堆垛机反馈数据转byte[]
        /// </summary>
        /// <param name="srmTaskDto"></param>
        /// <param name="Ack"></param>
        /// <returns></returns>
        //private static byte[] ConvertSrmDataToByteArray(WishSrmSocket srm, Int16 Ack)
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
                    //binaryWriter.Write(Encoding.ASCII.GetBytes("c"));
                    binaryWriter.Write(Encoding.ASCII.GetBytes("\r"));
                }
                return memoryStream.ToArray();
            }
        }
        private static byte[] ConvertMtpDataToByteArray(WishSrmSocket srm, Int16 Stb)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(srm.srmSocketWcs.TaskNo.Value);
                    binaryWriter.Write(srm.srmSocketWcs.ActionType.Value);
                    binaryWriter.Write(10);
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
                    //binaryWriter.Write(Encoding.ASCII.GetBytes("c"));
                    binaryWriter.Write(Encoding.ASCII.GetBytes("\r"));
                }
                return memoryStream.ToArray();
            }
        }

        #endregion

        /// <summary>
        /// 处理堆垛机反馈任务异常
        /// </summary>
        /// <param name="device"></param>
        /// <param name="jobError"></param>
        /// <param name="jobState"></param>
        //private void DoSrmTaskError(DeviceConfig device, int jobError1, int jobState1)
        //{
        //    // 6 放货时有货
        //    // 8 放货后载货台有货
        //    // 11 放货阻塞
        //    // 7 取货后载货台无货
        //    // 9 取货时无货
        //    // 12 取货阻塞
        //    if (jobState1 == 9)
        //    {
        //        string msg = "";
        //        switch (jobError1)
        //        {
        //            case 6:
        //                msg = "货叉1满入: 放货时有货";
        //                break;
        //            case 8:
        //                msg = "货叉1满入: 放货后载货台有货";
        //                break;
        //            case 11:
        //                msg = "货叉1满入: 放货阻塞";
        //                break;
        //            case 7:
        //                msg = "货叉1空出: 取货后载货台无货";
        //                break;
        //            case 9:
        //                msg = "货叉1空出: 取货时无货";
        //                break;
        //            case 12:
        //                msg = "货叉1空出: 取货阻塞";
        //                break;
        //            default:
        //                msg = $"货叉1未知异常，错误代码 {jobError1}";
        //                break;
        //        }
        //        MsgService.GetInstance().NewDeviceMsg(device.Device_Code, ETaskExecMsgType.WARN, msg);
        //    }
        //}



    }



}