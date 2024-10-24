using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;
using WISH.Helper.Common;
using Wish.Models.ImportDto;
using Wish.Model;
using Microsoft.EntityFrameworkCore;
using Com.Wish.Model.Business;
using Z.BulkOperations;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.Areas.BasWhouse.Model;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.Common;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Tsp;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.Areas.Config.Model;
using Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs;
using Wish.Models;
using Org.BouncyCastle.Pqc.Crypto.Lms;


namespace Wish.ViewModel.WcsCmd.SrmCmdVMs
{
    public partial class SrmCmdVM
    {
        /// <summary>
        /// 查找堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> GetSrmCmdInfoAsync(GetSrmTaskDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "堆垛机指令:";
            try
            {
                if (input == null)
                {
                    msg = $"{desc}:入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    msg = $"{desc}:设备编号为空";
                    return result.Error(msg);
                }
                List<SrmCmd> srmTask = new List<SrmCmd>();
                SrmTaskDto srmCmd = new SrmTaskDto();
                var srmCmdAlarm = DC.Set<SrmCmd>().Where(x => x.Fork_No == input.deviceNo).OrderBy(x => x.ID).FirstOrDefault();
                if (srmCmdAlarm != null)
                {
                    srmCmd = new SrmTaskDto
                    {
                        TaskNo = srmCmdAlarm.SubTask_No,
                        SerialNo = srmCmdAlarm.Serial_No,
                        ActionType = (short)(srmCmdAlarm.Task_No.Equals("WMS99999999") ? 27 : 26),//26有相同晶圆ID指令报警，27库内有相同晶圆ID报警
                        TaskType = srmCmdAlarm.Task_Type,
                        PalletBarcode = srmCmdAlarm.Pallet_Barcode,
                        SourceRow = srmCmdAlarm.From_ForkDirection,
                        SourceColumn = srmCmdAlarm.From_Column,
                        SourceLayer = srmCmdAlarm.From_Layer,
                        SourceStationNo = srmCmdAlarm.From_Station,
                        TargetStationNo = srmCmdAlarm.To_Station,
                        TargetRow = srmCmdAlarm.To_ForkDirection,
                        TargetColumn = srmCmdAlarm.To_Column,
                        TargetLayer = srmCmdAlarm.To_Layer,
                        AlarmCode = 0,
                        Station = 0,
                        Sign = 0,
                        CheckPoint = srmCmdAlarm.Check_Point,
                    };
                    msg = $"{desc}:当前设备{input.deviceNo}查询报警指令成功";
                    logger.Warn(msg);
                }
                else
                {
                    srmTask = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Exec_Status < 90).AsQueryable().ToListAsync();
                    if (srmTask.Any())
                    {
                        int unFinish = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status != 3).Count();
                        if (unFinish > 0 && unFinish <= 1)
                        {
                            if (string.IsNullOrWhiteSpace(input.taskNo))
                            {
                                msg = $"{desc}:当前设备{input.deviceNo}存在未完成的指令，且堆垛机对应的指令号为空";
                                logger.Warn(msg);
                                return result.Error(msg);
                            }
                            var query = new List<SrmCmd>();
                            if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 5).Any())
                            {
                                query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 5).ToList();
                            }
                            else if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 20).Any())
                            {
                                query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 20).ToList();
                            }
                            else if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 25).Any())
                            {
                                query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 25).ToList();
                            }
                            else
                            {
                                msg = $"{desc}:当前设备{input.deviceNo}不存在未完成交互的指令";
                                logger.Warn(msg);
                                return result.Error(msg);
                            }
                            srmCmd = query.Select(x => new SrmTaskDto
                            {
                                TaskNo = x.SubTask_No,
                                SerialNo = x.Serial_No,
                                ActionType = x.Task_Cmd,
                                TaskType = x.Task_Type,
                                PalletBarcode = x.Pallet_Barcode,
                                SourceRow = x.From_ForkDirection,
                                SourceColumn = x.From_Column,
                                SourceLayer = x.From_Layer,
                                SourceStationNo = x.From_Station,
                                TargetStationNo = x.To_Station,
                                TargetRow = x.To_ForkDirection,
                                TargetColumn = x.To_Column,
                                TargetLayer = x.To_Layer,
                                AlarmCode = 0,
                                Station = 0,
                                Sign = 0,
                                CheckPoint = x.Check_Point,
                            }).FirstOrDefault();
                        }
                        else if (unFinish > 1)
                        {
                            msg = $"{desc}:当前设备{input.deviceNo}存在多条未完成的指令";
                            logger.Warn(msg);
                            return result.Error(msg);
                        }
                        else if (unFinish == 0)
                        {
                            srmCmd = srmTask.Where(x => x.Exec_Status == 0).OrderBy(x => x.ID).Select(x => new SrmTaskDto
                            {
                                TaskNo = x.SubTask_No,
                                SerialNo = x.Serial_No,
                                ActionType = x.Task_Cmd,
                                TaskType = x.Task_Type,
                                PalletBarcode = x.Pallet_Barcode,
                                SourceRow = x.From_ForkDirection,
                                SourceColumn = x.From_Column,
                                SourceLayer = x.From_Layer,
                                SourceStationNo = x.From_Station,
                                TargetStationNo = x.To_Station,
                                TargetRow = x.To_ForkDirection,
                                TargetColumn = x.To_Column,
                                TargetLayer = x.To_Layer,
                                AlarmCode = 0,
                                Station = 0,
                                Sign = 0,
                                CheckPoint = x.Check_Point,
                            }).FirstOrDefault();
                        }
                    }
                    else
                    {
                        msg = $"{desc}:当前设备{input.deviceNo}没有指令";
                        logger.Warn(msg);
                        return result.Error(msg);
                    }
                }
                msg = $"{desc}:当前设备{input.deviceNo}查询指令成功";
                logger.Warn(msg);
                return result.Ok(msg, srmCmd);
            }
            catch (Exception ex)
            {
                logger.Warn($"{desc}:{ex.Message}");
                return result.Error($"{desc}:{ex.Message}");
            }
        }
        /// <summary>
        /// 查找出库堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> GetSrmOutCmdInfoAsync(GetSrmTaskDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "堆垛机指令:";
            try
            {
                if (input == null)
                {
                    msg = $"{desc}:入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    msg = $"{desc}:设备编号为空";
                    return result.Error(msg);
                }
                List<SrmCmd> srmTask = new List<SrmCmd>();
                SrmTaskDto srmCmd = new SrmTaskDto();
                srmTask = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Exec_Status < 40 && x.Task_Type == "OUT").AsQueryable().ToListAsync();
                if (srmTask.Any())
                {
                    srmCmd = srmTask.OrderBy(x => x.ID).Select(x => new SrmTaskDto
                    {
                        TaskNo = x.SubTask_No,
                        SerialNo = x.Serial_No,
                        ActionType = x.Task_Cmd,
                        TaskType = x.Task_Type,
                        PalletBarcode = x.Pallet_Barcode,
                        SourceRow = x.From_ForkDirection,
                        SourceColumn = x.From_Column,
                        SourceLayer = x.From_Layer,
                        SourceStationNo = x.From_Station,
                        TargetStationNo = x.To_Station,
                        TargetRow = x.To_ForkDirection,
                        TargetColumn = x.To_Column,
                        TargetLayer = x.To_Layer,
                        AlarmCode = 0,
                        Station = 0,
                        Sign = 0,
                        CheckPoint = x.Check_Point,
                    }).FirstOrDefault();
                }
                else
                {
                    msg = $"{desc}:当前设备{input.deviceNo}没有指令";
                    return result.Error(msg);
                }
                msg = $"{desc}:当前设备{input.deviceNo}查询指令成功";
                return result.Ok(msg, srmCmd);
            }
            catch (Exception ex)
            {
                return result.Error($"{desc}:{ex.Message}");
            }
        }
        /// <summary>
        /// 处理堆垛机反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DealSrmTaskAsync(DealSrmTaskDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "堆垛机指令反馈:";
            BasWBinVM vm = Wtm.CreateVM<BasWBinVM>();
            WmsTaskVM wmsTaskVM = Wtm.CreateVM<WmsTaskVM>();
            WmsInReceiptRecordVM wmsInReceiptRecordVM = Wtm.CreateVM<WmsInReceiptRecordVM>();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            AllotBinResultDto allotBinResultDto = new AllotBinResultDto();
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }

            try
            {
                if (input == null)
                {
                    msg = $"{desc}:入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    msg = $"{desc}:设备编号为空";
                    return result.Error(msg);
                }
                if (input.actionType == null)
                {
                    msg = $"{desc}:动作类型为空";
                    return result.Error(msg);
                }
                if (input.palletBarcode == null)
                {
                    msg = $"{desc}:托盘码为空";
                    return result.Error(msg);
                }
                List<int> actionDealTypes = new List<int>() { 14, 15, 16, 19, 20 };
                //if (input.checkPoint == null)
                //{
                //    msg = $"{desc}:检验点为空";
                //    return result.Error(msg);
                //}
                /*处理逻辑
                * 1、处理带有任务号的反馈，根据任务号，设备编码，托盘号，动作类型进行处理指令；
                * （1）完成取货完成，再次下发放货任务；
                * （2）完成放货任务，结束指令；
                * （3）异常处理。
                * 2、处理没有任务号的反馈，根据设备编码，托盘号，动作类型进行处理反馈；
                * （1）申请任务，调用创建指令逻辑。
                */
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                List<SrmCmd> srmCmds = new List<SrmCmd>();
                var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status >= 10).FirstOrDefaultAsync();
                if (srmCmd != null)
                {
                    //取货完成
                    if (input.actionType == 19)
                    {
                        if (srmCmd.Exec_Status != 10)
                        {
                            msg = $"{desc}:当前设备{input.deviceNo}指令状态不是取货已下发，无法进行取货完成";
                            return result.Error(msg);
                        }
                        srmCmd.Task_Cmd = 6;//放货
                        if (srmCmd.Task_Type.Equals("IN"))
                        {
                            srmCmd.From_Station = 1;
                            srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.To_ForkDirection + srmCmd.To_Column + srmCmd.To_Layer + srmCmd.From_Station);
                        }
                        else if (srmCmd.Task_Type.Equals("OUT"))
                        {
                            srmCmd.To_Station = 2;
                            srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.From_ForkDirection + srmCmd.From_Column + srmCmd.From_Layer + srmCmd.To_Station);
                        }
                        srmCmd.Exec_Status = 20;
                        srmCmd.Pick_Date = DateTime.Now;
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            srmCmd.UpdateTime = DateTime.Now;
                            srmCmd.UpdateBy = invoker;
                        }
                        //DC.UpdateEntity(srmCmd);
                        //DC.SaveChanges();
                        await ((DbContext)DC).SingleUpdateAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                    //放货完成
                    else if (input.actionType == 20)
                    {
                        if (srmCmd.Exec_Status != 30)
                        {
                            msg = $"{desc}:当前设备{input.deviceNo}指令状态不是放货已下发，无法进行放货完成";
                            return result.Error(msg);
                        }
                        srmCmd.Exec_Status = 40;
                        srmCmd.Put_Date = DateTime.Now;
                        srmCmd.Finish_Date = DateTime.Now;
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            srmCmd.UpdateTime = DateTime.Now;
                            srmCmd.UpdateBy = invoker;
                        }
                        //DC.UpdateEntity(srmCmd);
                        //DC.SaveChanges();
                        await ((DbContext)DC).SingleUpdateAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        //TODO:指令转历史，处理WMS任务，WMS单据，WMS库存
                        var wmsTask = await DC.Set<WmsTask>().Where(x => x.wmsTaskNo == srmCmd.Task_No && x.taskStatus == 40).FirstOrDefaultAsync();
                        if (wmsTask == null)
                        {
                            msg = $"{desc}:根据指令中的WMS任务号";
                            return result.Error(msg);
                        }
                        taskFeedbackInputDto taskFeedbackInput = new taskFeedbackInputDto()
                        {
                            invoker = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo,
                            palletBarcode = input.palletBarcode.ToString(),
                            wmsTaskNo = wmsTask.wmsTaskNo,
                            locNo = srmCmd.Task_Type == "OUT" ? wmsTask.toLocationNo : wmsTask.frLocationNo,
                            binNo = srmCmd.Task_Type == "OUT" ? wmsTask.frLocationNo : wmsTask.toLocationNo,
                            taskFeedbackType = "END",
                            feedbackDesc = desc + $"设备{input.deviceNo}反馈放货完成"
                        };
                        result = await wmsTaskVM.TaskFeedbackWCS(taskFeedbackInput);
                        if (result.code == ResCode.OK)
                        {
                            //SrmCmdHis his = CommonHelper.Map<SrmCmd, SrmCmdHis>(srmCmd, "ID");
                            //his.Exec_Status = 90;
                            //await ((DbContext)DC).SingleInsertAsync(his);
                            //await ((DbContext)DC).SingleDeleteAsync(srmCmd);
                            //await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else
                        {
                            msg = $"{desc}指令完成反馈报错，{result.msg}";
                            logger.Warn($"----->放货完成----->{desc}:{msg} ");
                            return result.Error(msg);
                        }
                    }
                    else if (input.actionType == 0)//拒绝任务
                    {
                        //TODO:暂停未发送的指令，如已有发送的指令则不处理该动作
                        msg = "已发送的指令则不处理该动作";
                    }
                    else
                    {
                        msg = $"{desc}:暂无其他反馈逻辑，动作代码{input.actionType}";
                        return result.Error(msg);
                    }
                }
                else
                {
                    srmCmds = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Exec_Status < 40).ToListAsync();
                    if (srmCmds.Any())
                    {
                        var srmCmdSending = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Exec_Status >= 5 && x.Task_Type == "OUT").FirstOrDefault();
                        if (srmCmdSending != null)
                        {
                            if (input.actionType == 5)
                            {
                                srmCmdSending.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                srmCmdSending.UpdateTime = DateTime.Now;
                                srmCmdSending.Remark_Desc = "入库请求失败，当前已有下发的出库任务，需排查";
                                await ((DbContext)DC).SingleUpdateAsync(srmCmdSending);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            else if (input.actionType == 7)
                            {
                                srmCmdSending.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                srmCmdSending.UpdateTime = DateTime.Now;
                                srmCmdSending.Remark_Desc = "出库申请失败，当前已有下发的出库任务，需排查";
                                await ((DbContext)DC).SingleUpdateAsync(srmCmdSending);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            msg = $"{desc}:当前设备{input.deviceNo}有正在下发或已下发的指令";
                            logger.Warn(msg);
                            result.Error(msg);
                        }
                        var srmCmdInit = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Exec_Status == 0 && x.Task_Type == "OUT").FirstOrDefault();
                        if (srmCmdInit != null)
                        {
                            if (input.actionType == 5)
                            {
                                srmCmdInit.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                srmCmdInit.UpdateTime = DateTime.Now;
                                srmCmdInit.Remark_Desc = "入库请求失败，当前已有初始的出库任务，请取消入库申请";
                                await ((DbContext)DC).SingleUpdateAsync(srmCmdInit);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            msg = $"{desc}:当前设备{input.deviceNo}已有可下发的出库指令";
                            result.Error(msg);
                        }
                        var srmCmdIns = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Task_Type == "IN" && x.Exec_Status >= 0).FirstOrDefault();
                        if (srmCmdIns != null)
                        {
                            if (!string.IsNullOrWhiteSpace(input.waferID))
                            {
                                if (input.actionType == 5)
                                {
                                    srmCmdIns.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                    srmCmdIns.UpdateTime = DateTime.Now;
                                    if (input.waferID.Equals(srmCmdIns.WaferID))
                                    {
                                        srmCmdIns.Remark_Desc = $"入库请求失败，晶圆ID{input.waferID}已存在，需排查";
                                    }
                                    else
                                    {
                                        srmCmdIns.Remark_Desc = $"入库请求失败，请求入库的晶圆ID【{input.waferID}】与当前晶圆ID【{srmCmdIns.WaferID}】不一致，需排查";
                                        srmCmdIns.Fork_No = input.deviceNo;
                                    }
                                    await ((DbContext)DC).SingleUpdateAsync(srmCmdIns);
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                }
                            }
                            else
                            {
                                if (input.actionType == 5)
                                {
                                    srmCmdIns.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                    srmCmdIns.UpdateTime = DateTime.Now;
                                    srmCmdIns.Remark_Desc = $"入库请求失败，晶圆ID{input.waferID}已存在，当前请求晶圆ID为空，需排查";
                                    await ((DbContext)DC).SingleUpdateAsync(srmCmdIns);
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                }
                            }
                            if (input.actionType == 7)
                            {
                                srmCmdIns.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                srmCmdIns.UpdateTime = DateTime.Now;
                                srmCmdIns.Remark_Desc = "出库申请失败，当前已有入库指令，需排查";
                                await ((DbContext)DC).SingleUpdateAsync(srmCmdIns);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            msg = $"{desc}:当前设备{input.deviceNo}已有下发的入库指令";
                            logger.Warn(msg);
                            result.Error(msg);
                        }
                        if (result.code == ResCode.Error)
                        {
                            if (hasParentTransaction == false)
                            {
                                await DC.Database.CommitTransactionAsync();
                            }
                            logger.Warn($"{desc}出入库请求存在指令:当前设备{input.deviceNo}{result.msg}");
                            return result;
                        }
                        //入库申请
                        if (input.actionType == 5)
                        {
                            //if (input.palletBarcode == 0)
                            //    //if (string.IsNullOrWhiteSpace(input.palletBarcode))
                            //{
                            //    msg = $"{desc}:当前设备【{input.deviceNo}】请求为入库申请【{input.actionType}】,没有携带托盘号请求";
                            //    return result.Error(msg);
                            //}
                            if (string.IsNullOrWhiteSpace(input.waferID))
                            {
                                msg = $"{desc}:当前设备【{input.deviceNo}】请求为入库申请【{input.actionType}】,没有携带晶圆ID请求";
                                logger.Warn(msg);
                                return result.Error(msg);
                            }
                            //根据设备请求入库的信息，生成入库记录，库存主表，库存明细表，库存唯一码表，
                            result = await wmsInReceiptRecordVM.CreateInforBySRM(input, "");
                            if (result.code == ResCode.Error)
                            {
                                msg = $"{desc}更新库存明细和库存唯一码中的信息报错，{result.msg}";
                                logger.Warn($"----->入库申请----->{desc}:{msg} ");
                                return result.Error(msg);
                            }

                        }
                        else if (input.actionType == 7)//出库申请
                        {
                            //TODO:查找WMS任务并生成出库堆垛机指令
                            var srmCmdOutInit = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Exec_Status == 0 && x.Task_Type.Contains("OUT")).ToList();
                            if (srmCmdOutInit.Count == 0)
                            {
                                var srmCmdWaits = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Exec_Status == 3 && x.Task_Type.Contains("OUT")).OrderBy(x => x.CreateTime).ThenByDescending(x => x.UpdateTime.HasValue ? x.UpdateTime.Value : DateTime.MinValue).ToList();
                                if (srmCmdWaits.Count == 0)
                                {
                                    msg = $"{desc}:当前设备{input.deviceNo},托盘{input.palletBarcode},存在{srmCmdWaits.Count}个待下发的指令";
                                    logger.Warn(msg);
                                    return result.Error(msg);
                                }
                                var srmCmdWait = srmCmdWaits.FirstOrDefault();
                                if (srmCmdWait != null)
                                {
                                    srmCmdWait.Exec_Status = 0;
                                    srmCmdWait.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                    srmCmdWait.UpdateTime = DateTime.Now;
                                    await ((DbContext)DC).SingleUpdateAsync(srmCmdWait);
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                    //DC.UpdateEntity(srmCmdWait);
                                    //DC.SaveChanges();
                                }
                            }
                        }
                        else if (input.actionType == 0)//拒绝任务
                        {
                            //TODO:暂停未发送的指令，如已有发送的指令则不处理该动作
                            var srmCmdRefuses = srmCmds.Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status == 0).ToList();
                            srmCmdRefuses.ForEach(t =>
                            {
                                t.Exec_Status = 3;
                                t.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                t.UpdateTime = DateTime.Now;
                                t.Remark_Desc = $"{input.deviceNo}设备拒绝任务:等待设备再次请求";
                            });
                            await ((DbContext)DC).BulkUpdateAsync(srmCmdRefuses);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                            //DC.UpdateEntity(srmCmdWait);
                            //DC.SaveChanges();
                        }
                        else
                        {
                            msg = $"{desc}:暂无其他申请逻辑，动作代码{input.actionType}";
                            return result.Error(msg);
                        }
                    }
                    else
                    {
                        //入库申请
                        if (input.actionType == 5)
                        {
                            //if (input.palletBarcode == 0)
                            //    //if (string.IsNullOrWhiteSpace(input.palletBarcode))
                            //{
                            //    msg = $"{desc}:当前设备【{input.deviceNo}】请求为入库申请【{input.actionType}】,没有携带托盘号请求";
                            //    return result.Error(msg);
                            //}
                            if (string.IsNullOrWhiteSpace(input.waferID))
                            {
                                msg = $"{desc}:当前设备【{input.deviceNo}】请求为入库申请【{input.actionType}】,没有携带晶圆ID请求";
                                logger.Warn(msg);
                                return result.Error(msg);
                            }
                            var srmCmdWafer = await DC.Set<SrmCmd>().Where(x => x.WaferID == input.waferID).FirstOrDefaultAsync();
                            if (srmCmdWafer != null)
                            {
                                srmCmdWafer.UpdateBy = !string.IsNullOrWhiteSpace(invoker) ? invoker : input.deviceNo;
                                srmCmdWafer.UpdateTime = DateTime.Now;
                                srmCmdWafer.Fork_No = input.deviceNo;
                                srmCmdWafer.Remark_Desc = $"入库请求失败，已有晶圆ID【{input.waferID}】在设备【{input.deviceNo}】指令，需排查";
                                await ((DbContext)DC).SingleUpdateAsync(srmCmdWafer);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                if (hasParentTransaction == false)
                                {
                                    await DC.Database.CommitTransactionAsync();
                                }
                                msg = $"{desc}入库请求失败，已有晶圆ID【{input.waferID}】在设备【{input.deviceNo}】指令";
                                logger.Warn(msg);
                                return result.Success(msg);
                            }
                            //根据设备请求入库的信息，生成入库记录，库存主表，库存明细表，库存唯一码表，
                            result = await wmsInReceiptRecordVM.CreateInforBySRM(input, "");
                            if (result.code == ResCode.Error)
                            {
                                msg = $"{desc}更新库存明细和库存唯一码中的信息报错，{result.msg}";
                                logger.Warn($"----->入库申请----->{desc}:{msg} ");
                                return result.Error(msg);
                            }


                            #region 逻辑改变，改为WMS自建单据生成任务和指令，此部分逻辑丢弃
                            //TODO:查找WMS任务并生成堆垛机指令
                            //var srmCmdWaits = srmCmds.Where(x => x.Exec_Status == 3 && x.Pallet_Barcode == input.palletBarcode && x.Task_Type == "IN").ToList();
                            //if (srmCmdWaits.Count > 1 || srmCmdWaits.Count == 0)
                            //{
                            //    msg = $"{desc}:当前设备【{input.deviceNo}】,托盘【{input.palletBarcode}】,存在【{srmCmdWaits.Count}】个待下发的指令";
                            //    return result.Error(msg);
                            //}
                            //var srmCmdWait = srmCmdWaits.FirstOrDefault();
                            //if (srmCmdWait != null)
                            //{
                            //    var basRoadways = await DC.Set<BasWRoadway>().Where(x => x.areaNo == "LM").ToListAsync();
                            //    if (basRoadways.Count == 0)
                            //    {
                            //        msg = $"{desc}:不存在LM区域的巷道";
                            //        return result.Error(msg);
                            //    }
                            //    string stockNo = input.deviceNo.Substring(input.deviceNo.Length - 2);
                            //    var basRoadway = basRoadways.Where(x => x.roadwayNo.Contains(stockNo)).FirstOrDefault();
                            //    if (basRoadway == null)
                            //    {
                            //        msg = $"{desc}:不存在{input.deviceNo}设备的巷道";
                            //        return result.Error(msg);
                            //    }
                            //    //查询设备站台对应关系
                            //    var cfgRelationship = DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Device&Station" && x.leftCode == input.deviceNo).FirstOrDefault();
                            //    if (cfgRelationship == null)
                            //    {
                            //        msg = $"{desc}:不存在{input.deviceNo}设备对应站台关系";
                            //        return result.Error(msg);
                            //    }
                            //    //调用库位分配
                            //    WcsAllotBinInputDto inputView = new WcsAllotBinInputDto
                            //    {
                            //        locNo1 = cfgRelationship.rightCode,//根据对应关系查找站台
                            //        locNo2 = "",
                            //        palletBarcode1 = input.palletBarcode.ToString(),
                            //        palletBarcode2 = "",
                            //        wcsAllotType = "0",
                            //        roadwayNos = basRoadway.roadwayNo,
                            //        height = 1000,
                            //        invoker = Wtm.LoginUserInfo?.ITCode ?? input.deviceNo,
                            //    };
                            //    result = await vm.WcsAllotRoadway(inputView);
                            //    if (result.code == ResCode.OK)
                            //    {
                            //        allotBinResultDto = CommonHelper.ConvertToEntity<AllotBinResultDto>(result.outParams);
                            //        if (allotBinResultDto == null)
                            //        {
                            //            msg = $"{desc}:分配{input.deviceNo}设备的巷道失败：{result.msg}--{JsonConvert.SerializeObject(result.outParams)}";
                            //            return result.Error(msg);
                            //        }
                            //        BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == allotBinResultDto.binNo).FirstOrDefaultAsync();
                            //        if (basWBin == null)
                            //        {
                            //            msg = $"{desc}:分配{input.deviceNo}设备的巷道失败：{result.msg}--{JsonConvert.SerializeObject(result.outParams)} -- 库位{allotBinResultDto.binNo}找不到对应库位数据";
                            //            return result.Error(msg);
                            //        }
                            //        srmCmdWait.Device_No = input.deviceNo;
                            //        srmCmdWait.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(input.deviceNo));
                            //        srmCmdWait.From_Station = Convert.ToInt16(cfgRelationship.rightCode);//根据对应关系查找站台
                            //        srmCmdWait.To_Column = (short)basWBin.binCol;
                            //        srmCmdWait.To_ForkDirection = (short)basWBin.binRow;
                            //        srmCmdWait.To_Layer = (short)basWBin.binLayer;
                            //        srmCmdWait.To_Deep = 0;
                            //        srmCmdWait.Check_Point = (short)(srmCmdWait.Serial_No + srmCmdWait.To_ForkDirection + srmCmdWait.To_Column + srmCmdWait.To_Layer);
                            //        srmCmdWait.Exec_Status = 0;
                            //        srmCmdWait.UpdateBy = input.deviceNo;
                            //        srmCmdWait.UpdateTime = DateTime.Now;
                            //        await ((DbContext)DC).SingleUpdateAsync(srmCmdWait);
                            //        await ((DbContext)DC).BulkSaveChangesAsync();
                            //    }
                            //    else
                            //    {
                            //        msg = $"{desc}:分配{input.deviceNo}设备的巷道失败：{result.msg}";
                            //        return result.Error(msg);
                            //    }
                            //}
                            #endregion

                        }
                        else if (input.actionType == 7)
                        {
                            msg = $"{desc}:暂无当前设备{input.deviceNo}待发送的出库指令，动作类型：{input.actionType}";
                            return result.Error(msg);
                        }
                        else
                        {
                            msg = $"{desc}:暂无当前设备{input.deviceNo}待发送指令，动作类型：{input.actionType}";
                            return result.Error(msg);
                        }
                    }
                }


                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }
                msg = $"{desc}:当前设备{input.deviceNo}处理指令反馈成功：{msg}";
                return result.Success(msg);
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                return result.Error($"{desc}:{ex.Message}");
            }
        }

        /// <summary>
        /// 处理指令下发
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DealCmdSendAsync(DealCmdSendDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "堆垛机指令下发处理";
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (input == null)
                {
                    msg = $"{desc}:入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    msg = $"{desc}:设备编号为空";
                    return result.Error(msg);
                }
                if (input.palletBarcode == null)
                {
                    msg = $"{desc}:托盘码为空";
                    logger.Warn(msg);
                    return result.Error(msg);
                }
                //if (input.checkPoint == null)
                //{
                //    msg = $"{desc}:检验点为空";
                //    return result.Error(msg);
                //}

                var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status < 30).FirstOrDefaultAsync();
                if (srmCmd == null)
                {
                    //throw new Exception($"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；查询指令为空");
                    msg = $"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；查询指令为空";
                    logger.Warn(msg);
                    return result.Error(msg);
                }
                var srmCmdAlarm = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status < 30 && x.Task_No == "WMS99999999").FirstOrDefaultAsync();
                var wmsTask = await DC.Set<WmsTask>().Where(x => x.wmsTaskNo == srmCmd.Task_No).FirstOrDefaultAsync();
                if (wmsTask == null)
                {
                    if (srmCmdAlarm == null)
                    {
                        //throw new Exception($"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；查询WMS任务为空");
                        msg = $"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；查询WMS任务为空";
                        logger.Warn(msg);
                        return result.Error(msg);
                    }
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                
                if (srmCmdAlarm != null)
                {
                    if (!string.IsNullOrWhiteSpace(invoker))
                    {
                        //SRM指令转历史
                        SrmCmdHis his = CommonHelper.Map<SrmCmd, SrmCmdHis>(srmCmdAlarm, "ID");
                        his.Exec_Status = 90;
                        his.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                        his.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).SingleInsertAsync(his);
                        await ((DbContext)DC).SingleDeleteAsync(srmCmdAlarm);
                    }
                    else
                    {
                        if (srmCmdAlarm.Exec_Status == 0)
                        {
                            srmCmdAlarm.Exec_Status = 5;
                            srmCmdAlarm.UpdateTime = DateTime.Now;
                            srmCmdAlarm.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                            await ((DbContext)DC).SingleUpdateAsync(srmCmdAlarm);
                        }
                        else if (srmCmdAlarm.Exec_Status == 5)
                        {
                            //SRM指令转历史
                            SrmCmdHis his = CommonHelper.Map<SrmCmd, SrmCmdHis>(srmCmdAlarm, "ID");
                            his.Exec_Status = 90;
                            his.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                            his.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).SingleInsertAsync(his);
                            await ((DbContext)DC).SingleDeleteAsync(srmCmdAlarm);
                        }
                    }
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(invoker))
                    {
                        if (srmCmd.Exec_Status == 0)
                        {
                            srmCmd.Exec_Status = 10;
                            srmCmd.Begin_Date = DateTime.Now;

                            wmsTask.taskDesc = "已下发";
                            wmsTask.taskStatus = 40;
                            wmsTask.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                            wmsTask.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).SingleUpdateAsync(wmsTask);
                        }
                        else if (srmCmd.Exec_Status == 3)
                        {
                            srmCmd.Exec_Status = 10;
                            srmCmd.Begin_Date = DateTime.Now;

                            wmsTask.taskDesc = "已下发";
                            wmsTask.taskStatus = 40;
                            wmsTask.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                            wmsTask.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).SingleUpdateAsync(wmsTask);
                        }
                        else if (srmCmd.Exec_Status == 20)
                        {
                            srmCmd.Exec_Status = 30;
                        }
                    }
                    else
                    {
                        if (srmCmd.Exec_Status == 0)
                        {
                            srmCmd.Exec_Status = 5;
                        }
                        else if (srmCmd.Exec_Status == 5)
                        {
                            srmCmd.Exec_Status = 10;
                            srmCmd.Begin_Date = DateTime.Now;

                            wmsTask.taskDesc = "已下发";
                            wmsTask.taskStatus = 40;
                            wmsTask.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                            wmsTask.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).SingleUpdateAsync(wmsTask);
                        }
                        else if (srmCmd.Exec_Status == 20)
                        {
                            srmCmd.Exec_Status = 25;
                        }
                        else if (srmCmd.Exec_Status == 25)
                        {
                            srmCmd.Exec_Status = 30;
                        }
                        else
                        {
                            throw new Exception($"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；状态不对无法进行指令下发动作");
                        }
                    }
                    srmCmd.UpdateTime = DateTime.Now;
                    srmCmd.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                    //DC.UpdateEntity(srmCmd);
                    //DC.SaveChanges();
                    await ((DbContext)DC).SingleUpdateAsync(srmCmd);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }
                msg = $"{desc}:当前设备{input.deviceNo}处理指令下发成功";
                logger.Warn(msg);
                return result.Success(msg);
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }
                logger.Warn($"{desc}:{ex.Message}");
                return result.Error($"{desc}:{ex.Message}");
            }

        }

        /// <summary>
        /// 堆垛机测试数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> SRM_TestCmd(CreateSrmCmdDto input)
        {
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (input == null)
                {
                    throw new Exception("失败，接收数据为null");
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                SrmCmd srmTask = new SrmCmd();
                srmTask.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                srmTask.Task_No = "WMS0000";
                srmTask.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(input.deviceNo));
                srmTask.Device_No = input.deviceNo;
                srmTask.Fork_No = "1";
                srmTask.Station_Type = "0";
                if (input.fromStationNo != 0)
                {
                    srmTask.Check_Point = (short)(srmTask.Serial_No + input.toRow + input.toColumn + input.toLayer + input.fromStationNo);
                    srmTask.From_Station = (short)input.fromStationNo;
                    srmTask.Task_Type = "IN";
                    srmTask.To_Column = (short)input.toColumn;
                    srmTask.To_ForkDirection = (short)input.toRow;
                    srmTask.To_Layer = (short)input.toLayer;
                    srmTask.To_Deep = 0;
                    srmTask.From_Column = 0;
                    srmTask.From_ForkDirection = 0;
                    srmTask.From_Layer = 0;
                    srmTask.From_Deep = 0;
                }
                else if (input.toStationNo != 0)
                {
                    srmTask.Check_Point = (short)(srmTask.Serial_No + input.fromRow + input.fromColumn + input.fromLayer + input.fromStationNo);
                    srmTask.To_Station = (short)input.toStationNo;
                    srmTask.Task_Type = "OUT";
                    srmTask.From_Column = (short)input.fromColumn;
                    srmTask.From_ForkDirection = (short)input.fromRow;
                    srmTask.From_Layer = (short)input.fromLayer;
                    srmTask.From_Deep = 0;
                    srmTask.To_Column = 0;
                    srmTask.To_ForkDirection = 0;
                    srmTask.To_Layer = 0;
                    srmTask.To_Deep = 0;
                }
                else
                {
                    srmTask.Check_Point = (short)(srmTask.Serial_No + input.fromRow + input.fromColumn + input.fromLayer + input.fromStationNo + 1);
                    srmTask.To_Station = (short)input.toStationNo;
                    srmTask.Task_Type = "MOVE";
                    srmTask.From_Column = (short)input.fromColumn;
                    srmTask.From_ForkDirection = (short)input.fromRow;
                    srmTask.From_Layer = (short)input.fromLayer;
                    srmTask.From_Deep = 0;
                    srmTask.To_Column = (short)input.toColumn;
                    srmTask.To_ForkDirection = (short)input.toRow;
                    srmTask.To_Layer = (short)input.toLayer;
                    srmTask.To_Deep = 0;
                }
                srmTask.Task_Cmd = 4;
                srmTask.Pallet_Barcode = input.palletBarcode;
                srmTask.WaferID = input.palletBarcode.ToString();
                srmTask.Exec_Status = 0;
                srmTask.Recive_Date = DateTime.Now;
                srmTask.CreateBy = "Test";
                srmTask.CreateTime = DateTime.Now;

                await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmTask);
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
            }
            return "ok";
        }


        public async Task<BusinessResult> CmdResendAsync(DealSrmTaskDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "堆垛机指令重发处理";
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (input == null)
                {
                    msg = $"{desc}:入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    msg = $"{desc}:设备编号为空";
                    return result.Error(msg);
                }
                //if (string.IsNullOrWhiteSpace(input.palletBarcode))
                if (input.palletBarcode == null || input.palletBarcode == 0)
                {
                    msg = $"{desc}:托盘码为空";
                    return result.Error(msg);
                }
                //if (input.checkPoint == null)
                //{
                //    msg = $"{desc}:检验点为空";
                //    return result.Error(msg);
                //}

                var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode).FirstOrDefaultAsync();
                if (srmCmd == null)
                {
                    throw new Exception($"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；查询指令为空");
                }

                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                if (srmCmd.Exec_Status == 10)
                {
                    srmCmd.Exec_Status = 0;
                }
                else if (srmCmd.Exec_Status == 30)
                {
                    srmCmd.Exec_Status = 20;
                    srmCmd.Begin_Date = DateTime.Now;
                }
                else
                {
                    throw new Exception($"{desc}失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}；状态不对无法进行指令重发操作");
                }
                srmCmd.UpdateTime = DateTime.Now;
                srmCmd.UpdateBy = string.IsNullOrWhiteSpace(invoker) ? input.deviceNo : invoker;
                //DC.UpdateEntity(srmCmd);
                //DC.SaveChanges();
                await ((DbContext)DC).SingleUpdateAsync(srmCmd);
                await ((DbContext)DC).BulkSaveChangesAsync();
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }
                msg = $"{desc}:当前设备{input.deviceNo}处理指令下发成功";
                return result.Success(msg);
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                return result.Error($"{desc}:{ex.Message}");
            }

        }
    }
}
