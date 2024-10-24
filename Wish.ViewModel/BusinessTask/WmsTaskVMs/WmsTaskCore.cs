using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.DirectoryServices.Protocols;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Z.BulkOperations;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Wish.ViewModel.BusinessPutaway.WmsPutawayVMs;
using Wish.ViewModel.BusinessPutdown.WmsPutdownVMs;
using Wish.ViewModel.Common;
using Wish.TaskConfig.Model;
using Wish.Model.Interface;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs;

namespace Wish.ViewModel.BusinessTask.WmsTaskVMs
{
    public partial class WmsTaskVM
    {
        #region 任务请求
        /// <summary>
        /// 任务请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskRequest(taskRequestInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            BasWBinVM vm = Wtm.CreateVM<BasWBinVM>();
            string desc = "任务请求:";
            try
            {
                #region 校验
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->任务请求--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.taskRequestType))
                {
                    msg = $"{desc}入参请求类型为空";
                    return result.Error(msg);
                }
                if (input.taskRequestType.Contains("IN"))
                {
                    if (string.IsNullOrWhiteSpace(input.palletBarcode))
                    {
                        msg = $"{desc}入参请求托盘号为空";
                        return result.Error(msg);
                    }
                    //if(input.height==null || input.height <= 0)
                    //{
                    //    msg = $"{desc}入参高度不能为空或者小于等于0";
                    //    return result.Error(msg);
                    //}
                    BasWBinVM binVM = Wtm.CreateVM<BasWBinVM>();
                    var palletTypeInfo = await binVM.GetPalletType(input.palletBarcode);
                    if (palletTypeInfo == null)
                    {
                        msg = $"{desc}入参载体条码【{input.palletBarcode}】未找到对应载体类型";
                        return result.Error(msg);
                    }
                    if (palletTypeInfo.palletTypeCode != "BX")
                    {
                        if (input.height == null || input.height <= 0)
                        {
                            msg = $"{desc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】,请求高度不能为空";
                            return result.Error(msg);
                        }
                    }

                }
                if (string.IsNullOrWhiteSpace(input.roadwayNos))
                {
                    msg = $"入参可用巷道集合为空";
                    return result.Error(desc + msg);
                }
                if (string.IsNullOrWhiteSpace(input.locNo))
                {
                    msg = $"{desc}入参请求站台号为空";
                    return result.Error(msg);
                }

                if (input.height != null && input.height > 0)
                {
                    var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                    if (stockInfo != null)
                    {
                        stockInfo.height = input.height;
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t.BatchSize = 2000);
                    }
                }

                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    if (taskInfo.taskStatus == 0)
                    //if (taskInfo.taskStatus == 3)
                    {
                        taskInfo.frLocationNo = input.locNo;
                        taskInfo.matHeight = input.height;
                        await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);


                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        msg = $"{desc}当前托盘【{input.palletBarcode}】请求成功：已存在任务，任务状态为初始创建，";
                        //新增--分配巷道
                        WcsAllotBinInputDto inputView = new WcsAllotBinInputDto
                        {
                            locNo1 = "",
                            locNo2 = "",
                            palletBarcode1 = input.palletBarcode,
                            palletBarcode2 = "",
                            wcsAllotType = "0",
                            roadwayNos = input.roadwayNos,
                            height = input.height,
                            invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS",
                        };
                        result = await vm.WcsAllotRoadway(inputView);
                        result.msg += msg;
                        if (result.code == ResCode.Error)
                        {
                            return result.Error(desc + msg);
                        }

                        return result.Success(msg);
                    }
                    else if (taskInfo.taskStatus == 5)
                    {
                        taskInfo.matHeight = input.height;
                        taskInfo.frLocationNo = input.locNo;
                        taskInfo.taskStatus = 0;
                        //taskInfo.taskStatus = 3;
                        taskInfo.taskDesc = "任务由请求下发状态变更为初始创建，等待下发";
                        taskInfo.UpdateBy = input.invoker;
                        taskInfo.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        //新增--分配巷道
                        WcsAllotBinInputDto inputView = new WcsAllotBinInputDto
                        {
                            locNo1 = "",
                            locNo2 = "",
                            palletBarcode1 = input.palletBarcode,
                            palletBarcode2 = "",
                            wcsAllotType = "0",
                            roadwayNos = input.roadwayNos,
                            height = input.height,
                            invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS",
                        };
                        result = await vm.WcsAllotRoadway(inputView);
                        msg = $"{desc}请求托盘【{input.palletBarcode}】存在任务，由请求下发状态变更为初始创建,起始站台更新为当前请求站台，等待下发";
                        result.msg += msg;
                        if (result.code == ResCode.Error)
                        {
                            return result.Error(desc + msg);
                        }
                        return result.Success(msg);
                    }
                    else
                    {
                        msg = $"{desc}当前托盘【{input.palletBarcode}】请求失败：已存在任务，任务状态不为初始创建，无需再次请求";
                        return result.Success(msg);
                    }
                }

                #endregion
                if (input.taskRequestType == "EMPTY_IN")
                {
                    result = await TaskRequestForEmptyIn(input);
                    //新增--分配巷道
                    if (result.code == ResCode.OK)
                    {
                        WcsAllotBinInputDto inputView = new WcsAllotBinInputDto
                        {
                            locNo1 = "",
                            locNo2 = "",
                            palletBarcode1 = input.palletBarcode,
                            palletBarcode2 = "",
                            wcsAllotType = "0",
                            roadwayNos = input.roadwayNos,
                            height = input.height,
                            invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS",
                        };
                        result = await vm.WcsAllotRoadway(inputView);
                        if (result.code == ResCode.Error)
                        {
                            return result.Error(desc + msg);
                        }
                    }
                }
                else if (input.taskRequestType == "IN")
                {

                    result = await TaskRequestForInAndTsf(input);
                    //新增--分配巷道
                    if (result.code == ResCode.OK)
                    {
                        WcsAllotBinInputDto inputView = new WcsAllotBinInputDto
                        {
                            locNo1 = "",
                            locNo2 = "",
                            palletBarcode1 = input.palletBarcode,
                            palletBarcode2 = "",
                            wcsAllotType = "0",
                            roadwayNos = input.roadwayNos,
                            height = input.height,
                            invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS",
                        };
                        result = await vm.WcsAllotRoadway(inputView);
                        if (result.code == ResCode.Error)
                        {
                            return result.Error(desc + msg);
                        }
                    }
                }
                else if (input.taskRequestType == "EMPTY_OUT")
                {
                    //执行空托出库
                    WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
                    EmptyStockAllotDto emptyStockAllot = new EmptyStockAllotDto()
                    {
                        allotType = 0,
                        palletTypeCode = input.palletTypeCode,
                        invoker = input.invoker,
                        roadwayNos = input.roadwayNos,
                        locNo = input.locNo,
                        qty = input.qty,
                    };
                    result = await wmsStockVM.AllotEmptyStock(emptyStockAllot);
                }

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:托盘【{input.palletBarcode}】，站台【{input.locNo}】任务请求：{result.msg}";
            logger.Warn($"----->Warn----->任务请求:{msg} ");
            return result;
        }
        /// <summary>
        /// 空托任务请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskRequestForEmptyIn(taskRequestInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务请求:";
            try
            {
                var locInfo = await DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo).FirstOrDefaultAsync();
                if (locInfo == null)
                {
                    msg = $"{desc}入参请求站台号【{input.locNo}】和请求类型【{input.taskRequestType}】未找到对应站台记录数据";
                    return result.Error(msg);
                }

                //是否已组盘
                var wmsstcok = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (wmsstcok != null)
                {
                    if (wmsstcok.stockStatus >= 50)
                    {
                        msg = $"{desc}请求托盘【{input.palletBarcode}】存在非入库中库存，实际状态【{wmsstcok.stockStatus}】";
                        return result.Error(msg);
                    }
                }
                else
                {
                    //空托组盘
                    BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
                    EmptyInDto emptyInView = new EmptyInDto()
                    {
                        palletBarcode = input.palletBarcode,
                        qty = input.qty,
                        invoker = input.invoker,
                        height = input.height,
                    };
                    result = await basWBinVM.EmptyIn(emptyInView);
                    if (result.code == ResCode.Error)
                    {
                        msg = $"{desc}请求托盘【{input.palletBarcode}】组盘失败：{result.msg}";
                        return result.Error(msg);
                    }
                }
                //空托上线
                WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                PutAwayOnlineDto putAwayOnlineParams = new PutAwayOnlineDto()
                {
                    palletBarcode = input.palletBarcode,
                    locNo = input.locNo
                };
                result = await wmsPutawayVM.PutAwayOnline(putAwayOnlineParams, input.invoker);
                if (result.code == ResCode.Error)
                {
                    msg = $"{desc}请求托盘【{input.palletBarcode}】上线失败：{result.msg}";
                    return result.Error(msg);
                }
                else
                {
                    msg = $"{desc}请求托盘【{input.palletBarcode}】上线成功：{result.msg}";
                    return result.Success(msg);
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;

        }
        /// <summary>
        /// 入库或者输送任务请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskRequestForInAndTsf(taskRequestInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务请求:";
            try
            {
                //是否已组盘
                var locInfo = await DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo).FirstOrDefaultAsync();
                if (locInfo == null)
                {
                    msg = $"{desc}入参请求站台号【{input.locNo}】和请求类型【{input.taskRequestType}】未找到对应站台记录数据";
                    return result.Error(msg);
                }
                string locGroupNo = locInfo.locGroupNo;
                var wmsstcok = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (wmsstcok != null)
                {
                    //
                    if (wmsstcok.stockStatus == 50)
                    {
                        msg = $"{desc}请求托盘【{input.palletBarcode}】存在在库库存";
                        return result.Error(msg);
                    }
                    else if (wmsstcok.stockStatus > 50)
                    {
                        var outRecordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.outRecordStatus < 90).ToListAsync();
                        if (outRecordInfos.Count > 0)
                        {
                            //是否还存在本站台的拣选任务
                            var recordInfo = outRecordInfos.FirstOrDefault(t => t.deliveryLocNo == input.locNo && t.outRecordStatus == 40);
                            if (recordInfo != null)
                            {
                                msg = $"{desc}请求托盘【{input.palletBarcode}】存在当前站台【{input.locNo}】未完成的拣选任务，请执行拣选操作或者撤销拣选操作";
                                return result.Error(msg);
                            }
                            else
                            {
                                var locGroupInfo = await DC.Set<BasWLocGroup>().Where(t => t.locGroupNo == locGroupNo).FirstOrDefaultAsync();
                                if (locGroupInfo != null)
                                {
                                    recordInfo = outRecordInfos.FirstOrDefault(t => t.deliveryLocNo == locGroupInfo.locGroupNo && t.outRecordStatus == 40);
                                    if (recordInfo != null)
                                    {
                                        msg = $"{desc}请求托盘【{input.palletBarcode}】存在当前站台组【{locGroupNo}】未完成的拣选任务，请执行拣选操作或者撤销拣选操作";
                                        return result.Error(msg);
                                    }
                                }
                            }
                            recordInfo = outRecordInfos.FirstOrDefault(t => t.deliveryLocNo != input.locNo && t.deliveryLocNo == locGroupNo);
                            if (recordInfo == null)
                            {
                                msg = $"{desc}请求托盘【{input.palletBarcode}】存在非入库中库存,且不存在其他站台(组)的拣选记录,请排查数据";
                                return result.Error(msg);
                            }

                        }
                        else
                        {
                            msg = $"{desc}请求托盘【{input.palletBarcode}】存在非入库中库存,实际状态【{wmsstcok.stockStatus}】";
                            return result.Error(msg);
                        }

                    }

                    if (input.height != null)
                    {
                        wmsstcok.height = input.height;
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsstcok);
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    }
                }
                else
                {
                    msg = $"{desc}请求托盘【{input.palletBarcode}】不存在组盘信息，请先托盘再请求入库";
                    return result.Error(msg);
                }
                //var putawayInfo =await DC.Set<WmsPutaway>().Where(t => t.palletBarcode == input.palletBarcode && t.putawayStatus < 90).FirstOrDefaultAsync();
                //if (putawayInfo == null)
                //{
                //    msg = $"{desc}请求托盘【{input.palletBarcode}】不存在未完成上架单，请先组盘再请求入库";
                //    return result.Error(msg);
                //}
                WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                PutAwayOnlineDto putAwayOnlineParams = new PutAwayOnlineDto()
                {
                    palletBarcode = input.palletBarcode,
                    locNo = input.locNo
                };
                result = await wmsPutawayVM.PutAwayOnline(putAwayOnlineParams, input.invoker);
                if (result.code == ResCode.Error)
                {
                    msg = $"{desc}请求托盘【{input.palletBarcode}】上线失败：{result.msg}";
                    return result.Error(msg);
                }
                else
                {
                    msg = $"{desc}请求托盘【{input.palletBarcode}】上线成功：{result.msg}";
                    return result.Success(msg);
                }
                #region old
                //var taskInfo = DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefault();
                //if (taskInfo == null)
                //{
                //    WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                //    PutAwayOnlineParams putAwayOnlineParams = new PutAwayOnlineParams()
                //    {
                //        palletBarcode = input.palletBarcode,
                //        locNo = input.locNo
                //    };
                //    result = wmsPutawayVM.PutAwayOnline(putAwayOnlineParams, input.invoker);
                //    if (result.code == ResCode.Error)
                //    {
                //        msg = $"{desc}请求托盘【{input.palletBarcode}】上线失败：{result.msg}";
                //        return result.Error(msg);
                //    }
                //    else
                //    {
                //        msg = $"{desc}请求托盘【{input.palletBarcode}】上线成功：{result.msg}";
                //        return result.Success(msg);
                //    }
                //}
                //else
                //{
                //    if (taskInfo.taskStatus == 0)
                //    {
                //        msg = $"{desc}请求托盘【{input.palletBarcode}】存在初始创建的任务，等待下发";
                //        return result.Success(msg);
                //    }
                //    else if (taskInfo.taskStatus == 5)
                //    {
                //        taskInfo.taskDesc = "任务由请求下发状态变更为初始创建，等待下发";
                //        taskInfo.UpdateBy = input.invoker;
                //        taskInfo.UpdateTime = DateTime.Now;
                //        DC.Set<WmsTask>().Update(taskInfo);
                //        DC.SaveChanges();
                //        msg = $"{desc}请求托盘【{input.palletBarcode}】存在任务，由请求下发状态变更为初始创建，等待下发";
                //        return result.Success(msg);
                //    }
                //    else
                //    {
                //        msg = $"{desc}请求托盘【{input.palletBarcode}】存在非初始创建的任务，等待下发";
                //        return result.Error(msg);
                //    }
                //} 
                #endregion

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;

        }
        #endregion

        #region 任务反馈
        /// <summary>
        /// 任务反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskFeedback(taskFeedbackInputDto input)
        {

            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务反馈:";
            try
            {

                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->任务反馈--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.wmsTaskNo))
                {
                    msg = $"{desc}入参反馈任务号为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    msg = $"{desc}入参反馈托盘号为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.taskFeedbackType))
                {
                    msg = $"{desc}入参反馈类型为空";
                    return result.Error(msg);
                }
                //if (string.IsNullOrWhiteSpace(input.binNo))
                //{
                //    msg = $"{desc}入参库位号为空";
                //    return result.Error(msg);
                //}
                if (input.taskFeedbackType != "END" && input.taskFeedbackType != "ERREND")
                {
                    msg = $"{desc}入参反馈类型【{input.taskFeedbackType}】不在取值范围【END,ERREND】";
                    return result.Error(msg);
                }
                if (input.taskFeedbackType == "ERREND")
                {
                    if (string.IsNullOrWhiteSpace(input.exceptionFlag))
                    {
                        msg = $"{desc}入参反馈类型为异常反馈时，反馈标识不能为空";
                        return result.Error(msg);
                    }
                    else if (input.exceptionFlag == "0")
                    {
                        msg = $"{desc}入参反馈类型为异常反馈时，反馈标识为0";
                        return result.Error(msg);
                    }

                }
                //if (string.IsNullOrWhiteSpace(input.locNo))
                //{
                //    msg = $"{desc}入参反馈站台号为空";
                //    return result.Error(msg);
                //}
                //var locInfo = DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo).FirstOrDefault();
                //if (locInfo == null)
                //{
                //    msg = $"{desc}入参反馈站台号【{input.locNo}】未找到对应站台记录";
                //    return result.Error(msg);
                //}
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.wmsTaskNo == input.wmsTaskNo && t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (taskInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：未找到对应任务";
                    return result.Error(msg);
                }
                if (taskInfo.taskStatus >= 90)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：对应任务状态不是可反馈状态，实际状态【{taskInfo.taskStatus}】";
                    return result.Error(msg);
                }
                if (taskInfo.taskTypeNo == "OUT")
                {
                    input.binNo = taskInfo.frLocationNo;
                }
                else if (taskInfo.taskTypeNo == "MOVE" || taskInfo.taskTypeNo.Contains("IN"))
                {
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.virtualFlag == 0 && t.binType == "ST").FirstOrDefaultAsync();
                    if (binInfo == null)
                    {
                        msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：对应库位【{input.binNo}】不是物理存储库位";
                        return result.Error(msg);
                    }
                }
                //正常完成
                if (input.taskFeedbackType == "END")
                {
                    if (taskInfo.taskTypeNo.Contains("IN"))
                    {
                        //todo:上架：空托入库、正常组盘入库、抽检回库、盘点回库、移库拣选回库、正常拣选回库
                        WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                        PutAwayDto putAwayParams = new PutAwayDto()
                        {
                            binNo = input.binNo,
                            palletBarcode = input.palletBarcode,
                        };
                        result = await wmsPutawayVM.PutAway(putAwayParams, input.invoker);

                    }
                    if (taskInfo.taskTypeNo.Contains("TSF"))
                    {
                        result = await TaskFeedbackForTsf(input);

                    }

                    else if (taskInfo.wmsTaskType == "MOVE" && taskInfo.taskTypeNo == "MOVE")//
                    {
                        //todo:巷道内移库
                        result = await TaskFeedbackForMove(input);
                    }
                    else if ((taskInfo.wmsTaskType == "MOVE" || taskInfo.wmsTaskType == "OUT" || taskInfo.wmsTaskType == "EMPTY_OUT" || taskInfo.wmsTaskType == "PICK_OUT" || taskInfo.wmsTaskType == "WHOLE_OUT") && taskInfo.taskTypeNo == "OUT")
                    {
                        //todo:下架：抽检、盘点、正常出库、空托出库、库区间移库
                        WmsPutdownVM wmsPutdownVM = Wtm.CreateVM<WmsPutdownVM>();
                        PutdownDto putdownView = new PutdownDto()
                        {
                            locNo = input.locNo,
                            palletBarcode = input.palletBarcode,
                            invoker = input.invoker,
                        };
                        result = await wmsPutdownVM.Putdown(putdownView);
                    }
                    else if (taskInfo.wmsTaskType == "EXCEPTION_OUT")//
                    {
                        //todo:异常出库
                        result = await TaskFeedbackForExceptionOut(input);

                    }


                }
                //异常完成
                else if (input.taskFeedbackType == "ERREND")
                {
                    if (taskInfo.taskTypeNo.Contains("IN"))
                    {
                        result = await TaskErrFeedbackForIn(input);
                    }
                    else if (taskInfo.wmsTaskType == "EXCEPTION_OUT")
                    {
                        result = await TaskErrFeedbackForExceptionOut(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("OUT") && taskInfo.wmsTaskType != "EXCEPTION_OUT")
                    {
                        result = await TaskErrFeedbackForOut(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("MOVE"))
                    {
                        result = await TaskErrFeedbackForMove(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("TSF"))
                    {
                        result.Error("输送任务请正常完成");
                    }
                }

                #region 任务处理
                if (result.code == ResCode.OK)
                {
                    if (input.taskFeedbackType == "END")
                    {
                        taskInfo.taskStatus = 90;
                        taskInfo.taskDesc = "正常完成";
                    }
                    else if (input.taskFeedbackType == "ERREND")
                    {
                        taskInfo.taskStatus = 91;
                        taskInfo.taskDesc = "异常完成";
                    }
                    taskInfo.feedbackStatus = 90;
                    taskInfo.UpdateBy = input.invoker;
                    taskInfo.UpdateTime = DateTime.Now;
                    taskInfo.feedbackDesc = input.feedbackDesc;

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsTask, WmsTaskHis>());
                    var mapper = config.CreateMapper();
                    WmsTaskHis wmsTaskHis = mapper.Map<WmsTaskHis>(taskInfo);

                    await ((DbContext)DC).Set<WmsTask>().SingleDeleteAsync(taskInfo);
                    await ((DbContext)DC).Set<WmsTaskHis>().AddAsync(wmsTaskHis);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                }
                else
                {
                    msg = $"{desc}" + result.msg;
                    result.Error(msg);
                }
                #endregion

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:wmsr任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】，站台【{input.locNo}】任务请求：{result.msg}";
            logger.Warn($"----->Warn----->任务反馈:{msg} ");
            return result;
        }

        /// <summary>
        /// 指令反馈处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskFeedbackWCS(taskFeedbackInputDto input)
        {

            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务反馈:";
            try
            {

                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->任务反馈--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.wmsTaskNo))
                {
                    msg = $"{desc}入参反馈任务号为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    msg = $"{desc}入参反馈托盘号为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.taskFeedbackType))
                {
                    msg = $"{desc}入参反馈类型为空";
                    return result.Error(msg);
                }
                //if (string.IsNullOrWhiteSpace(input.binNo))
                //{
                //    msg = $"{desc}入参库位号为空";
                //    return result.Error(msg);
                //}
                if (input.taskFeedbackType != "END" && input.taskFeedbackType != "ERREND")
                {
                    msg = $"{desc}入参反馈类型【{input.taskFeedbackType}】不在取值范围【END,ERREND】";
                    return result.Error(msg);
                }
                if (input.taskFeedbackType == "ERREND")
                {
                    if (string.IsNullOrWhiteSpace(input.exceptionFlag))
                    {
                        msg = $"{desc}入参反馈类型为异常反馈时，反馈标识不能为空";
                        return result.Error(msg);
                    }
                    else if (input.exceptionFlag == "0")
                    {
                        msg = $"{desc}入参反馈类型为异常反馈时，反馈标识为0";
                        return result.Error(msg);
                    }

                }
                //if (string.IsNullOrWhiteSpace(input.locNo))
                //{
                //    msg = $"{desc}入参反馈站台号为空";
                //    return result.Error(msg);
                //}
                //var locInfo = DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo).FirstOrDefault();
                //if (locInfo == null)
                //{
                //    msg = $"{desc}入参反馈站台号【{input.locNo}】未找到对应站台记录";
                //    return result.Error(msg);
                //}
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.wmsTaskNo == input.wmsTaskNo && t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (taskInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：未找到对应任务";
                    return result.Error(msg);
                }
                if (taskInfo.taskStatus >= 90)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：对应任务状态不是可反馈状态，实际状态【{taskInfo.taskStatus}】";
                    return result.Error(msg);
                }
                if (taskInfo.taskTypeNo == "OUT")
                {
                    input.binNo = taskInfo.frLocationNo;
                }
                else if (taskInfo.taskTypeNo == "MOVE" || taskInfo.taskTypeNo.Contains("IN"))
                {
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.virtualFlag == 0 && t.binType == "ST").FirstOrDefaultAsync();
                    if (binInfo == null)
                    {
                        msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：对应库位【{input.binNo}】不是物理存储库位";
                        return result.Error(msg);
                    }
                }
                var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Task_No == taskInfo.wmsTaskNo && x.Pallet_Barcode == Convert.ToInt32(input.palletBarcode) && x.Exec_Status >= 10 && x.Exec_Status <= 90).FirstOrDefaultAsync();
                //var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Task_No == taskInfo.wmsTaskNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status >= 10 && x.Exec_Status <= 90).FirstOrDefaultAsync();
                if (srmCmd == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：未找到对应指令";
                    return result.Error(msg);
                }
                //正常完成
                if (input.taskFeedbackType == "END")
                {
                    if (taskInfo.taskTypeNo.Contains("IN"))
                    {
                        //todo:上架：空托入库、正常组盘入库、抽检回库、盘点回库、移库拣选回库、正常拣选回库
                        WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                        PutAwayDto putAwayParams = new PutAwayDto()
                        {
                            binNo = input.binNo,
                            palletBarcode = input.palletBarcode,
                        };
                        result = await wmsPutawayVM.PutAwayByWCS(putAwayParams, input.invoker);

                    }
                    if (taskInfo.taskTypeNo.Contains("TSF"))
                    {
                        result = await TaskFeedbackForTsf(input);

                    }

                    else if (taskInfo.wmsTaskType == "MOVE" && taskInfo.taskTypeNo == "MOVE")//
                    {
                        //todo:巷道内移库
                        result = await TaskFeedbackForMove(input);
                    }
                    else if ((taskInfo.wmsTaskType == "MOVE" || taskInfo.wmsTaskType == "OUT" || taskInfo.wmsTaskType == "EMPTY_OUT" || taskInfo.wmsTaskType == "PICK_OUT" || taskInfo.wmsTaskType == "WHOLE_OUT") && taskInfo.taskTypeNo == "OUT")
                    {
                        //todo:下架：抽检、盘点、正常出库、空托出库、库区间移库
                        WmsPutdownVM wmsPutdownVM = Wtm.CreateVM<WmsPutdownVM>();
                        PutdownDto putdownView = new PutdownDto()
                        {
                            locNo = input.locNo,
                            palletBarcode = input.palletBarcode,
                            invoker = input.invoker,
                        };
                        result = await wmsPutdownVM.PutdownByWCS(putdownView);
                    }
                    else if (taskInfo.wmsTaskType == "EXCEPTION_OUT")//
                    {
                        //todo:异常出库
                        result = await TaskFeedbackForExceptionOut(input);

                    }


                }
                //异常完成
                else if (input.taskFeedbackType == "ERREND")
                {
                    if (taskInfo.taskTypeNo.Contains("IN"))
                    {
                        result = await TaskErrFeedbackForIn(input);
                    }
                    else if (taskInfo.wmsTaskType == "EXCEPTION_OUT")
                    {
                        result = await TaskErrFeedbackForExceptionOut(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("OUT") && taskInfo.wmsTaskType != "EXCEPTION_OUT")
                    {
                        result = await TaskErrFeedbackForOut(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("MOVE"))
                    {
                        result = await TaskErrFeedbackForMove(input);
                    }
                    else if (taskInfo.taskTypeNo.Contains("TSF"))
                    {
                        result.Error("输送任务请正常完成");
                    }
                }

                #region 任务处理
                if (result.code == ResCode.OK)
                {
                    if (input.taskFeedbackType == "END")
                    {
                        taskInfo.taskStatus = 90;
                        taskInfo.taskDesc = "正常完成";
                    }
                    else if (input.taskFeedbackType == "ERREND")
                    {
                        taskInfo.taskStatus = 91;
                        taskInfo.taskDesc = "异常完成";
                    }
                    taskInfo.feedbackStatus = 90;
                    taskInfo.UpdateBy = input.invoker;
                    taskInfo.UpdateTime = DateTime.Now;
                    taskInfo.feedbackDesc = input.feedbackDesc;

                    //var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsTask, WmsTaskHis>());
                    //var mapper = config.CreateMapper();
                    //WmsTaskHis wmsTaskHis = mapper.Map<WmsTaskHis>(taskInfo);
                    //WMS任务转历史
                    WmsTaskHis wmsTaskHis = CommonHelper.Map<WmsTask, WmsTaskHis>(taskInfo, "ID");
                    //SRM指令转历史
                    SrmCmdHis his = CommonHelper.Map<SrmCmd, SrmCmdHis>(srmCmd, "ID");
                    his.Exec_Status = 90;
                    his.UpdateBy = input.invoker;
                    his.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).SingleInsertAsync(his);
                    await ((DbContext)DC).SingleDeleteAsync(srmCmd);

                    await ((DbContext)DC).Set<WmsTask>().SingleDeleteAsync(taskInfo);
                    await ((DbContext)DC).Set<WmsTaskHis>().AddAsync(wmsTaskHis);
                    if (taskInfo.taskTypeNo.Contains("IN"))
                    {
                        //生成回传接口信息
                        FeedbackInputDto feedbackInputDto = new FeedbackInputDto()
                        {
                            waferID = srmCmd.WaferID
                        };
                        string sendInfo = JsonConvert.SerializeObject(feedbackInputDto);
                        InterfaceSendBack interfaceSendBack = new InterfaceSendBack()
                        {
                            interfaceCode = "IN_FEEDBACK",
                            interfaceName = "入库数据反馈",
                            interfaceSendInfo = sendInfo,
                            interfaceResult = "",
                            returnFlag = 0,
                            returnTimes = 0,
                            CreateBy = input.invoker,
                            CreateTime = DateTime.Now,
                            UpdateBy = input.invoker,
                            UpdateTime = DateTime.Now,
                        };
                        await ((DbContext)DC).Set<InterfaceSendBack>().SingleInsertAsync(interfaceSendBack);
                    }


                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                }
                else
                {
                    msg = $"{desc}" + result.msg;
                    result.Error(msg);
                }
                #endregion

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:wmsr任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】，站台【{input.locNo}】任务请求：{result.msg}";
            logger.Warn($"----->Warn----->任务反馈:{msg} ");
            return result;
        }
        /// <summary>
        /// 输送任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskFeedbackForTsf(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "输送任务反馈:";
            try
            {
                var locGroupInfo = await DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo).FirstOrDefaultAsync();
                if (locGroupInfo == null)
                {
                    msg = $"{desc}当前站台【{input.locNo}】未找到对应记录";
                    return result.Error(msg);
                }
                //查找出库中库存
                WmsStock outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                //if (outStockInfo == null)
                //{
                //    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                //    return result.Error(msg);
                //}

                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {

                        #region 出库库存更新
                        if (outStockInfo != null)
                        {
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            List<WmsStockDtl> outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        }
                        //查找记录

                        var recordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.deliveryLocNo == locGroupInfo.locGroupNo && t.outRecordStatus < 90).ToListAsync();
                        recordInfos.ForEach(t =>
                        {
                            t.outRecordStatus = 40;
                            t.deliveryLocNo = input.locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        await ((DbContext)DC).Set<WmsOutInvoiceRecord>().BulkUpdateAsync(recordInfos);
                        #endregion
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        /// <summary>
        /// 巷道内移库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskFeedbackForMove(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "移库任务反馈:";
            try
            {
                //todo:库位校验
                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：根据反馈库位号【{input.binNo}】未找到对应存储库位";
                    return result.Error(msg);
                }
                var binStockInfo = await DC.Set<WmsStock>().Where(t => t.binNo == input.binNo && t.palletBarcode != input.palletBarcode).FirstOrDefaultAsync();
                if (binStockInfo != null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：反馈库位号【{input.binNo}】存在其他库存,对应托盘【{binStockInfo.palletBarcode}】";
                    return result.Error(msg);
                }
                //查找入库中库存
                var inStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                if (inStockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到入库中库存";
                    return result.Error(msg);
                }
                // var inStockDtlInfos=DC.Set<WmsStockDtl>().Where(t =>t.stockCode==inStockInfo.stockCode && t.palletBarcode == input.palletBarcode ).ToList();
                // var inStockUniiInfos = DC.Set<WmsStockUniicode>().Where(t => t.stockCode == inStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToList();

                //查找出库中库存
                var outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                if (outStockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                    return result.Error(msg);
                }
                var outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                if (outStockDtlInfos.Count == 0)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存明细";
                    return result.Error(msg);
                }
                var outStockUniiInfos = await DC.Set<WmsStockUniicode>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                //查找移库记录
                var moveRecordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frPalletBarcode == outStockInfo.palletBarcode && t.frStockCode == outStockInfo.stockCode && t.frPalletBarcode == inStockInfo.palletBarcode && t.frStockCode == inStockInfo.stockCode && t.moveRecordStatus < 90).ToListAsync();
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        #region 入库中更新在库
                        inStockInfo.stockStatus = 50;
                        inStockInfo.binNo = binInfo.binNo;
                        inStockInfo.roadwayNo = binInfo.roadwayNo;
                        inStockInfo.regionNo = binInfo.regionNo;
                        inStockInfo.UpdateBy = input.invoker;
                        inStockInfo.UpdateTime = DateTime.Now;
                        //inStockDtlInfos.ForEach(t =>
                        //{
                        //    t.stockDtlStatus = 50;
                        //    t.UpdateBy = input.invoker;
                        //    t.UpdateTime = DateTime.Now;
                        //});
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(inStockInfo);
                        //((DbContext)DC).Set<WmsStockDtl>().UpdateRange(inStockDtlInfos);
                        #endregion

                        #region 出库库存移到历史表


                        outStockInfo.UpdateBy = input.invoker;
                        outStockInfo.UpdateTime = DateTime.Now;
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                        var mapper = config.CreateMapper();
                        WmsStockHis newStockHis = mapper.Map<WmsStockHis>(outStockInfo);



                        await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(outStockInfo);

                        await ((DbContext)DC).Set<WmsStockHis>().AddAsync(newStockHis);


                        #endregion

                        #region 明细更新
                        outStockDtlInfos.ForEach(t =>
                        {
                            t.stockCode = inStockInfo.stockCode;
                            t.occupyQty = 0;
                            t.stockDtlStatus = 50;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        outStockUniiInfos.ForEach(t =>
                        {
                            t.occupyQty = 0;
                            t.stockCode = inStockInfo.stockCode;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        await ((DbContext)DC).Set<WmsStockUniicode>().BulkUpdateAsync(outStockUniiInfos);
                        #endregion

                        #region 移库记录
                        moveRecordInfos.ForEach(t =>
                        {
                            t.moveRecordStatus = 90;
                            t.toLocationNo = binInfo.binNo;
                            t.curLocationNo = binInfo.binNo;
                            t.curStockCode = inStockInfo.stockCode;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkUpdateAsync(moveRecordInfos);
                        #endregion
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }

            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> TaskFeedbackForExceptionOut(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "异常出库任务反馈:";
            try
            {
                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：根据起始库位号【{input.binNo}】未找到对应存储库位";
                    return result.Error(msg);
                }
                //查找出库中库存
                WmsStock outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                List<WmsStockDtl> outStockDtlInfos = new List<WmsStockDtl>();
                if (outStockInfo == null)
                {
                    //msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                    //return result.Error(msg);
                }
                else
                {
                    outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                }
                var binInfos = await DC.Set<BasWBin>().Where(t => t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        binInfos.ForEach(t =>
                        {
                            t.binErrFlag = "0";
                            t.binErrMsg = "";
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        if (outStockInfo != null)
                        {
                            outStockInfo.stockStatus = 0;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            outStockInfo.regionNo = "YC01";
                            outStockInfo.roadwayNo = "00";
                            outStockInfo.binNo = "YC01_010101";
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        }

                        await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(binInfos);
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }


        #region 异常反馈

        public async Task<BusinessResult> TaskErrFeedbackForIn(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "入库任务异常反馈:";
            if (input.exceptionFlag == "0")
            {
                desc = "入库任务手动完成:";
            }
            try
            {
                //查找库存
                var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                if (stockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到入库中库存";
                    return result.Error(msg);
                }
                var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == stockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                var inRecordInfos = await DC.Set<WmsInReceiptRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.inRecordStatus < 90).ToListAsync();
                var putawayInfo = await DC.Set<WmsPutaway>().Where(t => t.palletBarcode == input.palletBarcode && t.putawayStatus < 90).FirstOrDefaultAsync();
                if (putawayInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到上架单";
                    return result.Error(msg);
                }
                var putawayDtlInfos = await DC.Set<WmsPutawayDtl>().Where(t => t.putawayNo == putawayInfo.putawayNo && t.palletBarcode == input.palletBarcode).ToListAsync();
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (input.exceptionFlag == "0")//手动完成
                        {
                            stockInfo.stockStatus = 0;
                            stockInfo.UpdateBy = input.invoker;
                            stockInfo.UpdateTime = DateTime.Now;
                            stockInfo.regionNo = "WS01";
                            stockInfo.roadwayNo = "00";
                            stockInfo.binNo = "WS_010101";

                            stockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            inRecordInfos.ForEach(t =>
                            {
                                t.inRecordStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            putawayInfo.putawayStatus = 0;
                            putawayInfo.UpdateBy = input.invoker;
                            putawayInfo.UpdateTime = DateTime.Now;
                            putawayInfo.regionNo = "WS01";
                            putawayDtlInfos.ForEach(t =>
                            {
                                t.putawayDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                        }
                        else
                        {
                            stockInfo.stockStatus = 0;
                            stockInfo.UpdateBy = input.invoker;
                            stockInfo.UpdateTime = DateTime.Now;
                            stockInfo.regionNo = "YC01";
                            stockInfo.roadwayNo = "00";
                            stockInfo.binNo = "YC01_010101";

                            stockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            inRecordInfos.ForEach(t =>
                            {
                                t.inRecordStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            putawayInfo.putawayStatus = 0;
                            putawayInfo.UpdateBy = input.invoker;
                            putawayInfo.UpdateTime = DateTime.Now;
                            putawayInfo.regionNo = "YC01";
                            putawayDtlInfos.ForEach(t =>
                            {
                                t.putawayDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                        }

                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(stockDtlInfos);
                        await ((DbContext)DC).Set<WmsPutaway>().SingleUpdateAsync(putawayInfo);
                        await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos);
                        await ((DbContext)DC).Set<WmsInReceiptRecord>().BulkUpdateAsync(inRecordInfos);
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> TaskErrFeedbackForInWCS(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "入库任务异常反馈:";
            if (input.exceptionFlag == "0")
            {
                desc = "入库任务手动完成:";
            }
            try
            {
                //查找库存
                var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                if (stockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到入库中库存";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == stockInfo.stockCode && t.palletBarcode == input.palletBarcode && t.stockDtlStatus < 50).FirstOrDefaultAsync();
                if (stockDtlInfos == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】库存编码【{stockInfo.stockCode}】未找到入库中库存明细";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                var stockUniicodes = await DC.Set<WmsStockUniicode>().Where(t => t.stockCode == stockInfo.stockCode && t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (stockUniicodes == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】库存编码【{stockInfo.stockCode}】未找到入库中库存唯一码";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                var inRecordInfos = await DC.Set<WmsInReceiptRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.inRecordStatus < 90).FirstOrDefaultAsync();
                if (inRecordInfos == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到入库中的入库记录";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (input.exceptionFlag == "0")//手动完成
                        {
                            stockInfo.stockStatus = 92;
                            stockInfo.UpdateBy = input.invoker;
                            stockInfo.UpdateTime = DateTime.Now;
                            stockInfo.regionNo = "WS01";
                            stockInfo.roadwayNo = "00";
                            stockInfo.binNo = "WS_010101";

                            stockDtlInfos.stockDtlStatus = 92;
                            stockDtlInfos.UpdateBy = input.invoker;
                            stockDtlInfos.UpdateTime = DateTime.Now;


                            inRecordInfos.inRecordStatus = 92;
                            inRecordInfos.UpdateBy = input.invoker;
                            inRecordInfos.UpdateTime = DateTime.Now;

                        }
                        else
                        {
                            stockInfo.stockStatus = 92;
                            stockInfo.UpdateBy = input.invoker;
                            stockInfo.UpdateTime = DateTime.Now;
                            stockInfo.regionNo = "YC01";
                            stockInfo.roadwayNo = "00";
                            stockInfo.binNo = "YC01_010101";


                            stockDtlInfos.stockDtlStatus = 92;
                            stockDtlInfos.UpdateBy = input.invoker;
                            stockDtlInfos.UpdateTime = DateTime.Now;


                            inRecordInfos.inRecordStatus = 92;
                            inRecordInfos.UpdateBy = input.invoker;
                            inRecordInfos.UpdateTime = DateTime.Now;


                        }
                        //库存主表转历史
                        WmsStockHis stockHis = CommonHelper.Map<WmsStock, WmsStockHis>(stockInfo, "ID");
                        //库存明细转历史
                        WmsStockDtlHis stockDtlHis = CommonHelper.Map<WmsStockDtl, WmsStockDtlHis>(stockDtlInfos, "ID");
                        //库存唯一码转历史
                        WmsStockUniicodeHis stockUniicodeHis = CommonHelper.Map<WmsStockUniicode, WmsStockUniicodeHis>(stockUniicodes, "ID");
                        //入库记录转历史
                        WmsInReceiptRecordHis inReceiptRecordHis = CommonHelper.Map<WmsInReceiptRecord, WmsInReceiptRecordHis>(inRecordInfos, "ID");

                        await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(stockInfo);
                        await ((DbContext)DC).Set<WmsStockHis>().SingleInsertAsync(stockHis);

                        await ((DbContext)DC).Set<WmsStockDtl>().SingleDeleteAsync(stockDtlInfos);
                        await ((DbContext)DC).Set<WmsStockDtlHis>().SingleInsertAsync(stockDtlHis);

                        await ((DbContext)DC).Set<WmsStockUniicode>().SingleDeleteAsync(stockUniicodes);
                        await ((DbContext)DC).Set<WmsStockUniicodeHis>().SingleInsertAsync(stockUniicodeHis);

                        await ((DbContext)DC).Set<WmsInReceiptRecord>().SingleDeleteAsync(inRecordInfos);
                        await ((DbContext)DC).Set<WmsInReceiptRecordHis>().SingleInsertAsync(inReceiptRecordHis);

                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        logger.Warn($"----->Warn----->{desc}--:{msg} ");
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        logger.Warn($"----->Warn----->{desc}--:{ex.Message}");
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> TaskErrFeedbackForOut(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "出库任务异常反馈:";
            try
            {
                //查找库存

                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：根据起始库位号【{input.binNo}】未找到对应存储库位";
                    return result.Error(msg);
                }
                var outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                if (outStockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                    return result.Error(msg);
                }
                var putDownInfo = await DC.Set<WmsPutdown>().Where(t => t.palletBarcode == input.palletBarcode && t.putdownStatus < 90).FirstOrDefaultAsync();
                if (putDownInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到未完成的下架单";
                    return result.Error(msg);
                }
                //todo:分配撤销
                if (input.exceptionFlag == "21")
                {
                    //删除库存
                    result = await RevokeAllotForPallet(input);
                    if (result.code == ResCode.Error)
                    {
                        result.msg = desc + result.msg;
                        return result;
                    }
                }
                else if (input.exceptionFlag == "22")
                {
                    //按托分配撤销,根据单据类型来查找记录（出库、移库、抽检、盘点）
                    result = await RevokeAllotForPallet(input);
                    if (result.code == ResCode.Error)
                    {
                        result.msg = desc + result.msg;
                        return result;
                    }
                }
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (input.exceptionFlag == "21")//空取
                        {
                            binInfo.binErrFlag = input.exceptionFlag;
                            binInfo.binErrMsg = input.feedbackDesc;
                            binInfo.UpdateBy = input.invoker;
                            binInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(binInfo);
                        }
                        else if (input.exceptionFlag == "22")
                        {
                            var binInfos = await DC.Set<BasWBin>().Where(t => t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
                            binInfos.ForEach(t =>
                            {
                                t.binErrFlag = input.exceptionFlag;
                                t.binErrMsg = input.feedbackDesc;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(binInfos);
                        }
                        else if (input.exceptionFlag == "31")//排异常口
                        {
                            outStockInfo.stockStatus = 0;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            outStockInfo.regionNo = "YC01";
                            outStockInfo.roadwayNo = "00";
                            outStockInfo.binNo = "YC01_010101";

                            var outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        }
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);

                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> TaskErrFeedbackForMove(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "移库任务异常反馈:";
            if (input.exceptionFlag == "0")
            {
                desc = "移库任务手动完成:";
            }
            try
            {
                //查找库存
                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：根据库位号【{input.binNo}】未找到对应存储库位";
                    return result.Error(msg);
                }
                var outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                if (outStockInfo == null)
                {
                    msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                    return result.Error(msg);
                }
                var outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                //todo:分配撤销

                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (input.exceptionFlag == "21")//空取
                        {
                            binInfo.binErrFlag = input.exceptionFlag;
                            binInfo.binErrMsg = input.feedbackDesc;
                            binInfo.UpdateBy = input.invoker;
                            binInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(binInfo);
                            msg = $"{desc}【{input.palletBarcode}】对应库位【{binInfo.binNo}】空取异常";
                            logger.Warn($"----->Warn----->任务反馈:{msg} ");

                            #region 库存
                            outStockInfo.stockStatus = 50;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 50;
                                t.occupyQty = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                            var inStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                            if (inStockInfo != null)
                            {
                                inStockInfo.UpdateBy = input.invoker;
                                inStockInfo.UpdateTime = DateTime.Now;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                                var mapper = config.CreateMapper();
                                WmsStockHis newStockHis = mapper.Map<WmsStockHis>(inStockInfo);
                                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(inStockInfo);
                                await ((DbContext)DC).Set<WmsStockHis>().AddAsync(newStockHis);
                            }
                            List<WmsItnMoveRecordHis> itnMoveRecordHisList = new List<WmsItnMoveRecordHis>();
                            var recordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frStockCode == outStockInfo.stockCode && t.toStockCode == inStockInfo.stockCode && t.moveRecordStatus < 90).ToListAsync();
                            if (recordInfos.Count > 0)
                            {
                                foreach (var item in recordInfos)
                                {
                                    item.moveRecordStatus = 91;
                                    item.UpdateBy = input.invoker;
                                    item.UpdateTime = DateTime.Now;
                                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMoveRecord, WmsItnMoveRecordHis>());
                                    var mapper = config.CreateMapper();
                                    WmsItnMoveRecordHis newRecordHis = mapper.Map<WmsItnMoveRecordHis>(item);
                                    itnMoveRecordHisList.Add(newRecordHis);
                                }
                                await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkDeleteAsync(recordInfos);
                                await ((DbContext)DC).Set<WmsItnMoveRecordHis>().AddRangeAsync(itnMoveRecordHisList);

                            }
                            #endregion
                        }
                        else if (input.exceptionFlag == "22")
                        {
                            var binInfos = await DC.Set<BasWBin>().Where(t => t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
                            binInfos.ForEach(t =>
                            {
                                t.binErrFlag = input.exceptionFlag;
                                t.binErrMsg = input.feedbackDesc;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(binInfos);

                            #region 库存和移库记录
                            outStockInfo.stockStatus = 50;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 50;
                                t.occupyQty = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                            var inStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                            if (inStockInfo != null)
                            {
                                inStockInfo.UpdateBy = input.invoker;
                                inStockInfo.UpdateTime = DateTime.Now;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                                var mapper = config.CreateMapper();
                                WmsStockHis newStockHis = mapper.Map<WmsStockHis>(inStockInfo);
                                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(inStockInfo);
                                await ((DbContext)DC).Set<WmsStockHis>().AddAsync(newStockHis);
                            }
                            List<WmsItnMoveRecordHis> itnMoveRecordHisList = new List<WmsItnMoveRecordHis>();
                            var recordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frStockCode == outStockInfo.stockCode && t.toStockCode == inStockInfo.stockCode && t.moveRecordStatus < 90).ToListAsync();
                            if (recordInfos.Count > 0)
                            {
                                foreach (var item in recordInfos)
                                {
                                    item.moveRecordStatus = 91;
                                    item.UpdateBy = input.invoker;
                                    item.UpdateTime = DateTime.Now;
                                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMoveRecord, WmsItnMoveRecordHis>());
                                    var mapper = config.CreateMapper();
                                    WmsItnMoveRecordHis newRecordHis = mapper.Map<WmsItnMoveRecordHis>(item);
                                    itnMoveRecordHisList.Add(newRecordHis);
                                }
                                await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkDeleteAsync(recordInfos);
                                await ((DbContext)DC).Set<WmsItnMoveRecordHis>().AddRangeAsync(itnMoveRecordHisList);
                            }
                            #endregion
                        }
                        else if (input.exceptionFlag == "31")//排异常口
                        {
                            outStockInfo.stockStatus = 0;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            outStockInfo.regionNo = "YC01";
                            outStockInfo.roadwayNo = "00";
                            outStockInfo.binNo = "YC01_010101";

                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });

                            var inStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                            if (inStockInfo != null)
                            {
                                inStockInfo.UpdateBy = input.invoker;
                                inStockInfo.UpdateTime = DateTime.Now;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                                var mapper = config.CreateMapper();
                                WmsStockHis newStockHis = mapper.Map<WmsStockHis>(inStockInfo);
                                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(inStockInfo);
                                await ((DbContext)DC).Set<WmsStockHis>().AddAsync(newStockHis);
                            }

                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        }
                        else if (input.exceptionFlag == "0")//手动完成
                        {
                            #region 库存
                            outStockInfo.stockStatus = 50;
                            outStockInfo.UpdateBy = input.invoker;
                            outStockInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                            outStockDtlInfos.ForEach(t =>
                            {
                                t.stockDtlStatus = 50;
                                t.occupyQty = 0;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                            var inStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
                            if (inStockInfo != null)
                            {
                                inStockInfo.UpdateBy = input.invoker;
                                inStockInfo.UpdateTime = DateTime.Now;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                                var mapper = config.CreateMapper();
                                WmsStockHis newStockHis = mapper.Map<WmsStockHis>(inStockInfo);
                                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(inStockInfo);
                                await ((DbContext)DC).Set<WmsStockHis>().AddAsync(newStockHis);
                            }
                            List<WmsItnMoveRecordHis> itnMoveRecordHisList = new List<WmsItnMoveRecordHis>();
                            var recordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frStockCode == outStockInfo.stockCode && t.toStockCode == inStockInfo.stockCode && t.moveRecordStatus < 90).ToListAsync();
                            if (recordInfos.Count > 0)
                            {
                                foreach (var item in recordInfos)
                                {
                                    item.moveRecordStatus = 92;
                                    item.UpdateBy = input.invoker;
                                    item.UpdateTime = DateTime.Now;
                                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMoveRecord, WmsItnMoveRecordHis>());
                                    var mapper = config.CreateMapper();
                                    WmsItnMoveRecordHis newRecordHis = mapper.Map<WmsItnMoveRecordHis>(item);
                                    itnMoveRecordHisList.Add(newRecordHis);
                                }
                                await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkDeleteAsync(recordInfos);
                                await ((DbContext)DC).Set<WmsItnMoveRecordHis>().AddRangeAsync(itnMoveRecordHisList);

                            }
                            #endregion
                        }
                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);

                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> TaskErrFeedbackForExceptionOut(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "异常出库任务异常反馈:";
            try
            {
                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}当前任务号【{input.wmsTaskNo}】,托盘【{input.palletBarcode}】反馈失败：根据起始库位号【{input.binNo}】未找到对应存储库位";
                    return result.Error(msg);
                }
                //查找出库中库存
                WmsStock outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                List<WmsStockDtl> outStockDtlInfos = new List<WmsStockDtl>();
                if (outStockInfo == null)
                {
                    //msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库中库存";
                    //return result.Error(msg);
                }
                else
                {
                    outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                }
                var binInfos = await DC.Set<BasWBin>().Where(t => t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
                //事务处理
                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (input.exceptionFlag == "21")
                        {
                            binInfo.binErrFlag = input.exceptionFlag;
                            binInfo.binErrMsg = "";
                            binInfo.UpdateBy = input.invoker;
                            binInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(binInfo);
                            //if(outStockInfo != null)
                            //{
                            //    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                            //    var mapper = config.CreateMapper();
                            //    WmsStockHis stockHis = mapper.Map<WmsStockHis>(outStockInfo);
                            //    ((DbContext)DC).Set<WmsStock>().Remove(outStockInfo);
                            //    ((DbContext)DC).Set<WmsStockHis>().Add(stockHis);
                            //    config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockDtl, WmsStockDtlHis>());
                            //    mapper = config.CreateMapper();
                            //    List<WmsStockDtlHis> stockDtlHisList = mapper.Map<List<WmsStockDtlHis>>(outStockDtlInfos);

                            //    ((DbContext)DC).Set<WmsStockDtl>().RemoveRange(outStockDtlInfos);
                            //    ((DbContext)DC).Set<WmsStockDtlHis>().AddRange(stockDtlHisList);

                            //    var stockUniiInfos = DC.Set<WmsStockUniicode>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToList();
                            //    if (stockUniiInfos.Count > 0)
                            //    {
                            //        config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicodeHis>());
                            //        mapper = config.CreateMapper();
                            //        List<WmsStockUniicodeHis> stockUniiHisList = mapper.Map<List<WmsStockUniicodeHis>>(stockUniiInfos);

                            //        ((DbContext)DC).Set<WmsStockUniicode>().RemoveRange(stockUniiInfos);
                            //        ((DbContext)DC).Set<WmsStockUniicodeHis>().AddRange(stockUniiHisList);
                            //    }

                            //    msg = $"{desc}【{input.palletBarcode}】空取，删除库存";
                            //    logger.Warn($"----->Warn----->任务反馈--异常出库任务异常反馈:{msg} ");
                            //}
                            if (outStockInfo != null)
                            {
                                outStockInfo.stockStatus = 50;
                                outStockInfo.UpdateBy = input.invoker;
                                outStockInfo.UpdateTime = DateTime.Now;
                                outStockDtlInfos.ForEach(t =>
                                {
                                    t.stockDtlStatus = 50;
                                    t.occupyQty = 0;
                                    t.UpdateBy = input.invoker;
                                    t.UpdateTime = DateTime.Now;
                                });

                                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                                await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                                msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                                logger.Warn($"----->Warn----->任务反馈--异常出库任务异常反馈:{msg} ");
                            }

                        }
                        else
                        {
                            binInfos.ForEach(t =>
                            {
                                t.binErrFlag = input.exceptionFlag;
                                t.binErrMsg = "";
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(binInfos);

                            if (outStockInfo != null)
                            {
                                outStockInfo.stockStatus = 50;
                                outStockInfo.UpdateBy = input.invoker;
                                outStockInfo.UpdateTime = DateTime.Now;
                                outStockDtlInfos.ForEach(t =>
                                {
                                    t.stockDtlStatus = 50;
                                    t.occupyQty = 0;
                                    t.UpdateBy = input.invoker;
                                    t.UpdateTime = DateTime.Now;
                                });

                                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                                await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                                msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                                logger.Warn($"----->Warn----->任务反馈--异常出库任务异常反馈:{msg} ");
                            }
                        }




                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        msg = $"{desc}当前托盘【{input.palletBarcode}】反馈完成";
                        result.Success(msg);
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        return result.Error($"{ex.Message};");
                    }
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }
            return result;
        }
        #endregion

        #region 按托撤销
        /// <summary>
        /// 按托撤销
        /// </summary>
        /// <param name="palletBarcode"></param>
        /// <returns></returns>
        public async Task<BusinessResult> RevokeAllotForPallet(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "按托撤销:";
            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {
                    int finishStatus = 91;
                    if (input.exceptionFlag == "0")
                    {
                        finishStatus = 92;
                    }
                    #region 查找库存

                    WmsStock outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                    if (outStockInfo == null)
                    {
                        msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库库存";
                        return result.Error(msg);
                    }

                    List<WmsStockDtl> outStockDtlInfos = new List<WmsStockDtl>();
                    outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                    if (input.exceptionFlag == "21")
                    {
                        outStockInfo.stockStatus = 50;
                        outStockInfo.UpdateBy = input.invoker;
                        outStockInfo.UpdateTime = DateTime.Now;

                        outStockDtlInfos.ForEach(t =>
                        {
                            t.occupyQty = 0;
                            t.stockDtlStatus = 50;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });

                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                        logger.Warn($"----->Warn----->任务反馈:{msg} ");
                    }
                    else
                    {
                        outStockInfo.stockStatus = 50;
                        outStockInfo.UpdateBy = input.invoker;
                        outStockInfo.UpdateTime = DateTime.Now;
                        outStockDtlInfos.ForEach(t =>
                        {
                            t.occupyQty = 0;
                            t.stockDtlStatus = 50;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });

                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                        logger.Warn($"----->Warn----->任务反馈:{msg} ");
                    }

                    #endregion

                    var putDownInfo = await DC.Set<WmsPutdown>().Where(t => t.palletBarcode == input.palletBarcode && t.putdownStatus < 90).FirstOrDefaultAsync();
                    if (putDownInfo != null)
                    {
                        var putDownDtlInfos = await DC.Set<WmsPutdownDtl>().Where(t => t.putdownNo == putDownInfo.putdownNo && t.palletBarcode == input.palletBarcode).ToListAsync();
                        var docInfo = await DC.Set<CfgDocType>().Where(t => t.docTypeCode == putDownInfo.docTypeCode).FirstOrDefaultAsync();
                        if (docInfo == null)
                        {
                            msg = $"{desc}当前托盘【{input.palletBarcode}】对应下架单【{putDownInfo.putdownNo}】的单据类型【{putDownInfo.docTypeCode}】未查询到数据";
                            return result.Error(msg);

                        }

                        #region 下架单
                        putDownInfo.putdownStatus = finishStatus;
                        putDownInfo.UpdateBy = input.invoker;
                        putDownInfo.UpdateTime = DateTime.Now;
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsPutdown, WmsPutdownHis>());
                        var mapper = config.CreateMapper();
                        WmsPutdownHis putdownHis = mapper.Map<WmsPutdownHis>(putDownInfo);
                        await ((DbContext)DC).Set<WmsPutdown>().SingleDeleteAsync(putDownInfo);
                        await ((DbContext)DC).Set<WmsPutdownHis>().AddAsync(putdownHis);
                        putDownDtlInfos.ForEach(t =>
                        {
                            t.putdownDtlStatus = putDownInfo.putdownStatus;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        config = new MapperConfiguration(cfg => cfg.CreateMap<WmsPutdownDtl, WmsPutdownDtlHis>());
                        mapper = config.CreateMapper();
                        List<WmsPutdownDtlHis> putdownDtlHisList = mapper.Map<List<WmsPutdownDtlHis>>(putDownDtlInfos);

                        await ((DbContext)DC).Set<WmsPutdownDtl>().BulkDeleteAsync(putDownDtlInfos);
                        await ((DbContext)DC).Set<WmsPutdownDtlHis>().AddRangeAsync(putdownDtlHisList);
                        #endregion


                        #region 根据业务类型撤销单据
                        if (docInfo.businessCode == "EMPTY_OUT")
                        {
                            var recordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.outRecordStatus < 90).ToListAsync();
                            recordInfos.ForEach(t =>
                            {
                                t.outRecordStatus = finishStatus;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            config = new MapperConfiguration(cfg => cfg.CreateMap<WmsOutInvoiceRecord, WmsOutInvoiceRecordHis>());
                            mapper = config.CreateMapper();
                            List<WmsOutInvoiceRecordHis> recordHisList = mapper.Map<List<WmsOutInvoiceRecordHis>>(recordInfos);
                            await ((DbContext)DC).Set<WmsOutInvoiceRecord>().BulkDeleteAsync(recordInfos);
                            await ((DbContext)DC).Set<WmsOutInvoiceRecordHis>().AddRangeAsync(recordHisList);
                        }
                        else if (docInfo.businessCode == "OUT")
                        {
                            var recordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.outRecordStatus < 90).ToListAsync();
                            recordInfos.ForEach(t =>
                            {
                                t.outRecordStatus = finishStatus;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            config = new MapperConfiguration(cfg => cfg.CreateMap<WmsOutInvoiceRecord, WmsOutInvoiceRecordHis>());
                            mapper = config.CreateMapper();
                            List<WmsOutInvoiceRecordHis> recordHisList = mapper.Map<List<WmsOutInvoiceRecordHis>>(recordInfos);
                            await ((DbContext)DC).Set<WmsOutInvoiceRecord>().BulkDeleteAsync(recordInfos);
                            await ((DbContext)DC).Set<WmsOutInvoiceRecordHis>().AddRangeAsync(recordHisList);

                            #region 单据明细和单据
                            var docDtlIdList = recordInfos.Select(t => t.invoiceDtlId).ToList();
                            var docNoList = recordInfos.Select(t => t.invoiceNo).ToList();

                            var docDtlInfos = await DC.Set<WmsOutInvoiceDtl>().Where(t => docNoList.Contains(t.invoiceNo)).ToListAsync();
                            docDtlInfos.ForEach(t =>
                            {
                                var recordInfoForDtls = recordInfos.Where(x => x.invoiceDtlId == t.ID).ToList();
                                t.allotQty = t.allotQty - recordInfoForDtls.Sum(t => t.allotQty);
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                                t.allocatResult = "按托撤销";
                                if (t.allotQty == 0)
                                {
                                    t.invoiceDtlStatus = 0;
                                }
                                else
                                {
                                    if (t.erpUndeliverQty > t.allotQty - t.completeQty)
                                    {
                                        t.invoiceDtlStatus = 21;
                                    }
                                }

                                if (t.erpUndeliverQty == 0)
                                {
                                    t.invoiceDtlStatus = 90;
                                }
                            });
                            var docInfos = await DC.Set<WmsOutInvoice>().Where(t => docNoList.Contains(t.invoiceNo)).ToListAsync();
                            docInfos.ForEach(t =>
                            {
                                var dtl = docDtlInfos.FirstOrDefault(x => x.invoiceNo == t.invoiceNo && x.invoiceDtlStatus < 90);
                                if (dtl == null)
                                {
                                    t.invoiceStatus = 90;
                                }
                                else
                                {
                                    dtl = docDtlInfos.FirstOrDefault(x => x.invoiceNo == t.invoiceNo && x.invoiceDtlStatus > 0);
                                    if (dtl != null)
                                    {
                                        t.invoiceStatus = 41;
                                    }
                                    else
                                    {
                                        t.invoiceStatus = 0;
                                    }
                                }

                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsOutInvoiceDtl>().BulkUpdateAsync(docDtlInfos);
                            await ((DbContext)DC).Set<WmsOutInvoice>().BulkUpdateAsync(docInfos);
                            #endregion
                        }
                        else if (docInfo.businessCode == "MOVE")
                        {
                            #region 记录更新
                            var recordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frPalletBarcode == input.palletBarcode && t.frStockCode == outStockInfo.stockCode && t.moveRecordStatus < 90).ToListAsync();
                            recordInfos.ForEach(t =>
                            {
                                t.moveRecordStatus = finishStatus;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMoveRecord, WmsItnMoveRecordHis>());
                            mapper = config.CreateMapper();
                            List<WmsItnMoveRecordHis> recordHisList = mapper.Map<List<WmsItnMoveRecordHis>>(recordInfos);
                            await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkDeleteAsync(recordInfos);
                            await ((DbContext)DC).Set<WmsItnMoveRecordHis>().AddRangeAsync(recordHisList);
                            #endregion

                            #region 单据明细和单据
                            var docDtlIdList = recordInfos.Select(t => t.moveDtlId).ToList();
                            var docNoList = recordInfos.Select(t => t.moveNo).ToList();

                            var docDtlInfos = await DC.Set<WmsItnMoveDtl>().Where(t => docNoList.Contains(t.moveNo)).ToListAsync();
                            docDtlInfos.ForEach(t =>
                            {
                                var recordInfoForDtls = recordInfos.Where(x => x.moveDtlId == t.ID).ToList();
                                t.moveQty = t.moveQty - recordInfoForDtls.Sum(t => t.moveQty);
                                //t.sta = 51;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                                if (t.moveQty == 0)
                                {
                                    t.moveDtlStatus = finishStatus;
                                }
                            });

                            List<WmsItnMoveDtl> updateDtlList = docDtlInfos.Where(t => t.moveQty > 0).ToList();
                            if (updateDtlList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnMoveDtl>().BulkUpdateAsync(updateDtlList);
                            }
                            List<WmsItnMoveDtl> delDtlList = docDtlInfos.Where(t => t.moveQty == 0).ToList();
                            if (delDtlList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMoveDtl, WmsItnMoveDtlHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnMoveDtlHis> dtlHisList = mapper.Map<List<WmsItnMoveDtlHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnMoveDtl>().BulkDeleteAsync(delDtlList);
                                await ((DbContext)DC).Set<WmsItnMoveDtlHis>().AddRangeAsync(dtlHisList);
                            }

                            var docInfos = await DC.Set<WmsItnMove>().Where(t => docNoList.Contains(t.moveNo)).ToListAsync();
                            docInfos.ForEach(t =>
                            {
                                var dtls = docDtlInfos.Where(x => x.moveNo == t.moveNo && x.moveQty > 0).ToList();
                                if (dtls.Count == 0)
                                {
                                    t.moveStatus = finishStatus;
                                }
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            List<WmsItnMove> updateDocList = docInfos.Where(t => t.moveStatus < 90).ToList();
                            if (updateDocList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnMove>().BulkUpdateAsync(updateDocList);
                            }
                            List<WmsItnMove> delDocList = docInfos.Where(t => t.moveStatus > 90).ToList();
                            if (delDocList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnMove, WmsItnMoveHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnMoveHis> docHisList = mapper.Map<List<WmsItnMoveHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnMove>().BulkDeleteAsync(delDocList);
                                await ((DbContext)DC).Set<WmsItnMoveHis>().AddRangeAsync(docHisList);
                            }
                            #endregion


                        }
                        else if (docInfo.businessCode == "QC")
                        {
                            #region 记录更新
                            var recordInfos = await DC.Set<WmsItnQcRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == outStockInfo.stockCode && t.itnQcStatus < 90).ToListAsync();
                            recordInfos.ForEach(t =>
                            {
                                t.itnQcStatus = finishStatus;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnQcRecord, WmsItnQcRecordHis>());
                            mapper = config.CreateMapper();
                            List<WmsItnQcRecordHis> recordHisList = mapper.Map<List<WmsItnQcRecordHis>>(recordInfos);
                            await ((DbContext)DC).Set<WmsItnQcRecord>().BulkDeleteAsync(recordInfos);
                            await ((DbContext)DC).Set<WmsItnQcRecordHis>().AddRangeAsync(recordHisList);
                            #endregion

                            #region 单据明细和单据
                            var docDtlIdList = recordInfos.Select(t => t.itnQcDtlId).ToList();
                            var docNoList = recordInfos.Select(t => t.itnQcNo).ToList();

                            var docDtlInfos = await DC.Set<WmsItnQcDtl>().Where(t => docNoList.Contains(t.itnQcNo)).ToListAsync();
                            docDtlInfos.ForEach(t =>
                            {
                                var recordInfoForDtls = recordInfos.Where(x => x.itnQcDtlId == t.ID).ToList();
                                t.itnQcQty = t.itnQcQty - recordInfoForDtls.Sum(t => t.stockQty.Value);
                                //t.sta = 51;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                                if (t.itnQcQty == 0)
                                {
                                    t.itnQcDtlStatus = Convert.ToInt32(finishStatus.ToString());
                                }
                            });

                            List<WmsItnQcDtl> updateDtlList = docDtlInfos.Where(t => t.itnQcQty > 0).ToList();
                            if (updateDtlList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnQcDtl>().BulkUpdateAsync(updateDtlList);
                            }
                            List<WmsItnQcDtl> delDtlList = docDtlInfos.Where(t => t.itnQcQty == 0).ToList();
                            if (delDtlList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnQcDtl, WmsItnQcDtlHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnQcDtlHis> dtlHisList = mapper.Map<List<WmsItnQcDtlHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnQcDtl>().BulkDeleteAsync(delDtlList);
                                await ((DbContext)DC).Set<WmsItnQcDtlHis>().AddRangeAsync(dtlHisList);
                            }

                            var docInfos = await DC.Set<WmsItnQc>().Where(t => docNoList.Contains(t.itnQcNo)).ToListAsync();
                            docInfos.ForEach(t =>
                            {
                                var dtls = docDtlInfos.Where(x => x.itnQcNo == t.itnQcNo && x.itnQcQty > 0).ToList();
                                if (dtls.Count == 0)
                                {
                                    t.itnQcStatus = Convert.ToInt32(finishStatus.ToString());
                                }
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            List<WmsItnQc> updateDocList = docInfos.Where(t => t.itnQcStatus != 91).ToList();
                            if (updateDocList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnQc>().BulkUpdateAsync(updateDocList);
                            }
                            List<WmsItnQc> delDocList = docInfos.Where(t => t.itnQcStatus == 91).ToList();
                            if (delDocList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnQc, WmsItnQcHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnQcHis> docHisList = mapper.Map<List<WmsItnQcHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnQc>().BulkDeleteAsync(delDocList);
                                await ((DbContext)DC).Set<WmsItnQcHis>().AddRangeAsync(docHisList);
                            }
                            #endregion
                        }
                        else if (docInfo.businessCode == "INVENTORY")
                        {
                            #region 记录更新
                            var recordInfos = await DC.Set<WmsItnInventoryRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == outStockInfo.stockCode && t.inventoryRecordStatus < 90).ToListAsync();
                            recordInfos.ForEach(t =>
                            {
                                t.inventoryRecordStatus = finishStatus;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnInventoryRecord, WmsItnInventoryRecordHis>());
                            mapper = config.CreateMapper();
                            List<WmsItnInventoryRecordHis> recordHisList = mapper.Map<List<WmsItnInventoryRecordHis>>(recordInfos);
                            await ((DbContext)DC).Set<WmsItnInventoryRecord>().BulkDeleteAsync(recordInfos);
                            await ((DbContext)DC).Set<WmsItnInventoryRecordHis>().AddRangeAsync(recordHisList);
                            #endregion

                            #region 单据明细和单据
                            var docDtlIdList = recordInfos.Select(t => t.inventoryDtlId).ToList();
                            var docNoList = recordInfos.Select(t => t.inventoryNo).ToList();

                            var docDtlInfos = await DC.Set<WmsItnInventoryDtl>().Where(t => docNoList.Contains(t.inventoryNo)).ToListAsync();
                            docDtlInfos.ForEach(t =>
                            {
                                var recordInfoForDtls = recordInfos.Where(x => x.inventoryDtlId == t.ID).ToList();
                                t.inventoryQty = t.inventoryQty - recordInfoForDtls.Sum(t => t.stockQty);
                                //t.sta = 51;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                                if (t.inventoryQty == 0)
                                {
                                    t.inventoryDtlStatus = Convert.ToInt32(finishStatus.ToString());
                                }
                            });

                            List<WmsItnInventoryDtl> updateDtlList = docDtlInfos.Where(t => t.inventoryQty > 0).ToList();
                            if (updateDtlList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnInventoryDtl>().BulkUpdateAsync(updateDtlList);
                            }
                            List<WmsItnInventoryDtl> delDtlList = docDtlInfos.Where(t => t.inventoryQty == 0).ToList();
                            if (delDtlList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnInventoryDtl, WmsItnInventoryDtlHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnInventoryDtlHis> dtlHisList = mapper.Map<List<WmsItnInventoryDtlHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnInventoryDtl>().BulkDeleteAsync(delDtlList);
                                await ((DbContext)DC).Set<WmsItnInventoryDtlHis>().AddRangeAsync(dtlHisList);
                            }

                            var docInfos = await DC.Set<WmsItnInventory>().Where(t => docNoList.Contains(t.inventoryNo)).ToListAsync();
                            docInfos.ForEach(t =>
                            {
                                var dtls = docDtlInfos.Where(x => x.inventoryNo == t.inventoryNo && x.inventoryQty > 0).ToList();
                                if (dtls.Count == 0)
                                {
                                    t.inventoryStatus = Convert.ToInt32(finishStatus.ToString());
                                }
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            List<WmsItnInventory> updateDocList = docInfos.Where(t => t.inventoryStatus != 90).ToList();
                            if (updateDocList.Count > 0)
                            {
                                await ((DbContext)DC).Set<WmsItnInventory>().BulkUpdateAsync(updateDocList);
                            }
                            List<WmsItnInventory> delDocList = docInfos.Where(t => t.inventoryStatus == 91).ToList();
                            if (delDocList.Count > 0)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsItnInventory, WmsItnInventoryHis>());
                                mapper = config.CreateMapper();
                                List<WmsItnInventoryHis> docHisList = mapper.Map<List<WmsItnInventoryHis>>(delDtlList);
                                await ((DbContext)DC).Set<WmsItnInventory>().BulkDeleteAsync(delDocList);
                                await ((DbContext)DC).Set<WmsItnInventoryHis>().AddRangeAsync(docHisList);
                            }
                            #endregion

                        }
                        #endregion

                    }

                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    await tran.CommitAsync();
                    result.Success($"托盘【{input.palletBarcode}】撤销成功");

                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    msg = $"{desc}" + e.Message;
                    result.Error(msg);
                }
                return result;
            }

        }
        /// <summary>
        /// 按托撤销
        /// </summary>
        /// <param name="palletBarcode"></param>
        /// <returns></returns>
        public async Task<BusinessResult> RevokeAllotForPalletWCS(taskFeedbackInputDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "按托撤销:";
            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {
                    int finishStatus = 91;
                    if (input.exceptionFlag == "0")
                    {
                        finishStatus = 92;
                    }
                    #region 查找库存

                    WmsStock outStockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus > 50).FirstOrDefaultAsync();
                    if (outStockInfo == null)
                    {
                        msg = $"{desc}当前托盘【{input.palletBarcode}】未找到出库库存";
                        logger.Warn($"----->Warn----->{desc}:{msg} ");
                        return result.Error(msg);
                    }

                    List<WmsStockDtl> outStockDtlInfos = new List<WmsStockDtl>();
                    outStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == outStockInfo.stockCode && t.palletBarcode == input.palletBarcode).ToListAsync();
                    if (input.exceptionFlag == "21")
                    {
                        outStockInfo.stockStatus = 50;
                        outStockInfo.UpdateBy = input.invoker;
                        outStockInfo.UpdateTime = DateTime.Now;

                        outStockDtlInfos.ForEach(t =>
                        {
                            t.occupyQty = 0;
                            t.stockDtlStatus = 50;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });

                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                        logger.Warn($"----->Warn----->任务反馈:{msg} ");
                    }
                    else
                    {
                        outStockInfo.stockStatus = 50;
                        outStockInfo.UpdateBy = input.invoker;
                        outStockInfo.UpdateTime = DateTime.Now;
                        outStockDtlInfos.ForEach(t =>
                        {
                            t.occupyQty = 0;
                            t.stockDtlStatus = 50;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });

                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(outStockInfo);
                        await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(outStockDtlInfos);
                        msg = $"{desc}【{input.palletBarcode}】异常代码【{input.exceptionFlag}】，更新库存";
                        logger.Warn($"----->Warn----->任务反馈:{msg} ");
                    }

                    #endregion

                    #region 根据业务类型撤销单据

                    var recordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.outRecordStatus < 90).FirstOrDefaultAsync();
                    WmsOutInvoiceRecordHis his = CommonHelper.Map<WmsOutInvoiceRecord, WmsOutInvoiceRecordHis>(recordInfos, "ID");
                    his.outRecordStatus = finishStatus;
                    his.UpdateBy = input.invoker;
                    his.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsOutInvoiceRecord>().SingleDeleteAsync(recordInfos);
                    await ((DbContext)DC).Set<WmsOutInvoiceRecordHis>().SingleInsertAsync(his);

                    #region 单据明细和单据
                    #endregion

                    #endregion


                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    await tran.CommitAsync();
                    logger.Warn($"----->Warn----->{desc}--:托盘【{input.palletBarcode}】撤销成功 ");
                    result.Success($"托盘【{input.palletBarcode}】撤销成功");

                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    msg = $"{desc}" + e.Message;
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    result.Error(msg);
                }
                return result;
            }

        }
        #endregion

        #endregion

        #region 任务重发
        /// <summary>
        /// 任务重发
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskReload(taskOperationDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务重发:";
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->任务重发--入参:{JsonConvert.SerializeObject(input)} ");
                if (input.taskId == null)
                {
                    msg = $"{desc}入参任务id为空";
                    return result.Error(msg);
                }
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.ID == input.taskId).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    if (taskInfo.taskStatus >= 90)
                    {
                        msg = $"{desc}该任务状态不可重发";
                        return result.Error(msg);
                    }
                    if (taskInfo.feedbackStatus >= 90)
                    {
                        msg = $"{desc}该任务反馈状态已反馈";
                        return result.Error(msg);
                    }

                    taskInfo.taskStatus = 0;
                    taskInfo.feedbackStatus = 0;
                    taskInfo.taskDesc = input.operationReason;
                    taskInfo.UpdateBy = input.invoker;
                    taskInfo.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    result.Success("重发成功");
                    msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:托盘【{taskInfo.palletBarcode}】,任务id【{taskInfo.ID}】：{result.msg}";
                    logger.Warn($"----->Warn----->任务重发:{msg} ");
                }
                else
                {
                    msg = $"{desc}根据入参任务id【{input.taskId}】未找到wms任务记录";
                    return result.Error(msg);
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }

            return result;
        }


        #endregion

        #region 任务完成
        /// <summary>
        /// 任务手动完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskFinish(taskOperationDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务手动完成:";
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->任务手动完成--入参:{JsonConvert.SerializeObject(input)} ");
                if (input.taskId == null)
                {
                    msg = $"{desc}入参任务id为空";
                    return result.Error(msg);
                }
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.ID == input.taskId).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    if (taskInfo.taskStatus >= 90)
                    {
                        msg = $"{desc}该任务状态不可手动完成";
                        return result.Error(msg);
                    }
                    if (taskInfo.feedbackStatus >= 90)
                    {
                        msg = $"{desc}该任务反馈状态已反馈";
                        return result.Error(msg);
                    }

                    taskFeedbackInputDto taskFeedbackInput = new taskFeedbackInputDto()
                    {
                        invoker = input.invoker,
                        palletBarcode = taskInfo.palletBarcode,
                        wmsTaskNo = taskInfo.wmsTaskNo,
                        locNo = taskInfo.toLocationNo,
                        binNo = taskInfo.toLocationNo,
                        taskFeedbackType = "END",
                        feedbackDesc = desc + input.operationReason
                    };
                    result = await TaskFeedbackWCS(taskFeedbackInput);
                    msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:托盘【{taskInfo.palletBarcode}】,任务id【{taskInfo.ID}】：{result.msg}";
                    logger.Warn($"----->Warn----->任务手动完成:{msg} ");
                }
                else
                {
                    msg = $"{desc}根据入参任务id【{input.taskId}】未找到wms任务记录";
                    return result.Error(msg);
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }

            return result;
        }
        #endregion

        #region 任务关闭
        /// <summary>
        /// 任务手动关闭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> TaskClose(taskOperationDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "任务手动关闭:";
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                if (input.taskId == null)
                {
                    msg = $"{desc}入参任务id为空";
                    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    return result.Error(msg);
                }
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.ID == input.taskId).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    //if (taskInfo.taskStatus > 0)
                    //{
                    //    msg = $"{desc}该任务状态已执行不可手动关闭";
                    //    logger.Warn($"----->Warn----->{desc}--:{msg} ");
                    //    return result.Error(msg);
                    //}
                    if (taskInfo.feedbackStatus >= 90)
                    {
                        msg = $"{desc}该任务反馈状态已反馈";
                        logger.Warn($"----->Warn----->{desc}--:{msg} ");
                        return result.Error(msg);
                    }
                    var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Task_No == taskInfo.wmsTaskNo && x.Pallet_Barcode == Convert.ToInt32(taskInfo.palletBarcode) && x.Exec_Status >= 10 && x.Exec_Status <= 90).FirstOrDefaultAsync();
                    //var srmCmd = await DC.Set<SrmCmd>().Where(x => x.Task_No == taskInfo.wmsTaskNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status >= 10 && x.Exec_Status <= 90).FirstOrDefaultAsync();
                    if (srmCmd == null)
                    {
                        msg = $"{desc}当前任务号【{taskInfo.wmsTaskNo}】,托盘【{taskInfo.palletBarcode}】反馈失败：未找到对应指令";
                        return result.Error(msg);
                    }
                    taskFeedbackInputDto taskFeedbackInput = new taskFeedbackInputDto()
                    {
                        invoker = input.invoker,
                        palletBarcode = taskInfo.palletBarcode,
                        wmsTaskNo = taskInfo.wmsTaskNo,
                        locNo = taskInfo.toLocationNo,
                        binNo = taskInfo.toLocationNo,
                        taskFeedbackType = "END",
                        exceptionFlag = "0",
                        feedbackDesc = desc + input.operationReason
                    };
                    if (taskInfo.taskTypeNo == "OUT")
                    {

                        //result = await RevokeAllotForPallet(taskFeedbackInput);
                        result = await RevokeAllotForPalletWCS(taskFeedbackInput);
                        if (result.code == ResCode.Error)
                        {
                            result.msg = desc + result.msg;
                            return result;
                        }
                    }
                    else if (taskInfo.taskTypeNo == "MOVE")
                    {
                        result = await TaskErrFeedbackForMove(taskFeedbackInput);
                        if (result.code == ResCode.Error)
                        {
                            result.msg = desc + result.msg;
                            return result;
                        }
                    }
                    else if (taskInfo.taskTypeNo == "IN")
                    {
                        result = await TaskErrFeedbackForInWCS(taskFeedbackInput);
                        if (result.code == ResCode.Error)
                        {
                            result.msg = desc + result.msg;
                            return result;
                        }
                    }
                    else
                    {
                        //result.Success($"任务类型为【{taskInfo.taskTypeNo}】,直接完成任务");
                        logger.Warn($"----->Warn----->{desc}--任务类型为【{taskInfo.taskTypeNo}】,不处理 ");
                        return result.Error($"任务类型为【{taskInfo.taskTypeNo}】,不处理");
                    }

                    taskInfo.taskStatus = 90;
                    taskInfo.taskDesc = desc + input.operationReason;
                    taskInfo.feedbackStatus = 90;
                    taskInfo.UpdateBy = input.invoker;
                    taskInfo.UpdateTime = DateTime.Now;
                    taskInfo.feedbackDesc = desc + input.operationReason;
                    WmsTaskHis wmsTaskHis = CommonHelper.Map<WmsTask, WmsTaskHis>(taskInfo, "ID");

                    //SRM指令转历史
                    SrmCmdHis his = CommonHelper.Map<SrmCmd, SrmCmdHis>(srmCmd, "ID");
                    his.Exec_Status = 90;
                    his.UpdateBy = input.invoker;
                    his.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).SingleInsertAsync(his);
                    await ((DbContext)DC).SingleDeleteAsync(srmCmd);

                    await ((DbContext)DC).Set<WmsTask>().SingleDeleteAsync(taskInfo);
                    await ((DbContext)DC).Set<WmsTaskHis>().SingleInsertAsync(wmsTaskHis);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    result.msg = desc + result.msg;
                    msg = $"时间【{DateTime.Now}】,操作人【{input.invoker}】:托盘【{taskInfo.palletBarcode}】,任务id【{taskInfo.ID}】：{result.msg}";
                    logger.Warn($"----->Warn----->任务手动关闭:{msg} ");
                }
                else
                {
                    msg = $"{desc}根据入参任务id【{input.taskId}】未找到wms任务记录";
                    return result.Error(msg);
                }
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.Error(msg);
            }

            return result;
        }
        #endregion
    }
}
