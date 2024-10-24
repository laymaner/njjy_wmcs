using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Wish.ViewModel.Common;
using Com.Wish.Model.Business;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{
    public partial class InterfaceConfigVM
    {
        /// <summary>
        /// 入库完成反馈WMS
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> InTaskFeedback(FeedbackInputDto input,string url)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "入库完成调用MES接口反馈:";
            string outputStr= string.Empty;
            WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.waferID))
                {
                    msg = $"{desc}入参晶圆ID为空";
                    return result.Error(msg);
                }
                ApiResponseRes returnInfo =await InterfaceHelper.SendRequest(url,input.waferID);
                logger.Warn($"----->【接口信息InTaskFeedback】----->{desc}:接口返回结果【{JsonConvert.SerializeObject(returnInfo)}】 ");
                if (returnInfo.Code == 0)
                {
                    msg= $"{desc}{returnInfo.Message}";
                    if (returnInfo.Data != null)
                    {

                        //更新库存明细和库存唯一码中的信息
                        List<FeedbackDto> feedbackDtos = JsonConvert.DeserializeObject<List<FeedbackDto>>(returnInfo.Data.ToString());
                        outputStr=JsonConvert.SerializeObject(feedbackDtos);
                        result = await wmsStockVM.UpdateStockInfoByInter(feedbackDtos);
                        if (result.code == ResCode.Error)
                        {
                            msg = $"{desc}更新库存明细和库存唯一码中的晶圆ID参数信息报错，接口返回参数【{outputStr}】处理返回数据异常：【{result.msg}】";
                            logger.Warn($"----->【接口信息错误InTaskFeedback】----->{desc}:{msg} ");
                            return result.Error(msg);
                        }
                        //if (result.code == ResCode.Error)
                        //{
                        //    msg = $"{desc}更新库存明细和库存唯一码中的信息报错，{result.msg}";
                        //    logger.Warn($"----->【接口信息错误InTaskFeedback】----->{desc}:{msg} ");
                        //    return result.Error(msg);
                        //}
                        //ItfResult Datas = JsonConvert.DeserializeObject<ItfResult>(returnInfo.Data.ToString());
                        //if (Datas.resultCode==0)
                        //{
                        //    if (Datas.data==null || Datas.data.Count==0)
                        //    {
                        //        msg = $"{desc}接口无返回数据{JsonConvert.SerializeObject(Datas.data)}";
                        //        return result.Error(msg);
                        //    }
                        //    else
                        //    {

                        //    }
                        //}
                        //else
                        //{
                        //    msg = $"{desc}接口报错{Datas.resultMsg}";
                        //    return result.Error(msg);
                        //}
                    }
                    else
                    {
                        msg = $"{desc}接口返回的data数据为空{JsonConvert.SerializeObject(returnInfo.Data)}";
                        return result.Error(msg);
                    }
                }
                else if (returnInfo.Code == -1)
                {
                    msg = $"{desc}{returnInfo.Message}";
                    return result.Error(msg);
                }

            }
            catch (Exception ex)
            {
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->【接口信息错误InTaskFeedback】----->{desc}:{msg} ");
                return result.Error(msg);
            }
            msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(input)}】，出参【{outputStr}】";
            logger.Warn($"----->【接口信息错误InTaskFeedback】----->{desc}:{msg} ");
            return result.Success(msg);
        }
        /// <summary>
        /// 出库请求MES接口同步工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> OutRequset(RequestInputDto input, string url)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "出库请求MES接口同步工单:";
            string outputStr = string.Empty;
            WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.invoiceNo))
                {
                    msg = $"{desc}入参工单号为空";
                    return result.Error(msg);
                }
                ApiResponseRes returnInfo = await InterfaceHelper.SendRequest(url, input.invoiceNo);
                logger.Warn($"----->【接口信息OutRequset】----->{desc}:接口返回结果【{JsonConvert.SerializeObject(returnInfo)}】");
                if (returnInfo.Code == 0)
                {
                    msg = $"{desc}{returnInfo.Message}";
                    if (returnInfo.Data != null)
                    {
                        //todo:根据返回数据更新对应晶圆ID的工单号，并根据更新后的信息生成发货单并生成出库任务与指令
                        List<RequestDto> requestDtos = JsonConvert.DeserializeObject<List<RequestDto>>(returnInfo.Data.ToString());
                        outputStr = JsonConvert.SerializeObject(requestDtos);
                        result = await wmsStockVM.UpdateStockProjectByInter(requestDtos);
                        if (result.code == ResCode.Error)
                        {
                            msg = $"{desc}更新库存明细和库存唯一码中的工单信息报错，接口返回参数【{outputStr}】处理返回数据异常：【{result.msg}】";
                            logger.Warn($"----->【接口信息错误OutRequset】----->{desc}:{msg} ");
                            return result.Error(msg);
                        }
                        //ItfResult Datas = JsonConvert.DeserializeObject<ItfResult>(returnInfo.Data.ToString());
                        //if (Datas.resultCode == 0)
                        //{
                        //    if (Datas.data == null || Datas.data.Count == 0)
                        //    {
                        //        msg = $"{desc}接口无返回数据{JsonConvert.SerializeObject(Datas.data)}";
                        //        return result.Error(msg);
                        //    }
                        //    else
                        //    {
                                
                        //    }
                        //}
                        //else
                        //{
                        //    msg = $"{desc}接口报错{Datas.resultMsg}";
                        //    return result.Error(msg);
                        //}
                    }
                    else
                    {
                        msg = $"{desc}接口返回的data数据为空{JsonConvert.SerializeObject(returnInfo.Data)}";
                        return result.Error(msg);
                    }
                }
                else if (returnInfo.Code == -1)
                {
                    msg = $"{desc}{returnInfo.Message}";
                    return result.Error(msg);
                }

            }
            catch (Exception ex)
            {
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->【接口信息错误OutRequset】----->{desc}:{msg} ");
                return result.Error(msg);
            }
            msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(input)}】";
            logger.Warn($"----->【接口信息错误OutRequset】----->{desc}:{msg} ");
            return result.Success(msg);
        }
    }
}
