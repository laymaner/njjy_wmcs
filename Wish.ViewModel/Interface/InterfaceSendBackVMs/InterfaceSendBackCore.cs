using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;
using Elsa.Models;
using Microsoft.EntityFrameworkCore;
using Wish.ViewModel.Common.Dtos;
using Newtonsoft.Json;
using Wish.ViewModel.Interface.InterfaceConfigVMs;
using WISH.Helper.Common;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.TaskConfig.Model;
using Wish.ViewModel.Common;
using Org.BouncyCastle.Asn1.Tsp;
using Wish.Areas.BasWhouse.Model;
using NPOI.SS.Formula.Functions;
using System.Text.RegularExpressions;
using Wish.Model.System;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackVM
    {
        /// <summary>
        /// 定时回传
        /// </summary>
        /// <returns></returns>
        public async Task SendInfoToInter()
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "接口定时任务:";
            var hasParentTransaction = false;
            InterfaceConfigVM interfaceConfigVM = Wtm.CreateVM<InterfaceConfigVM>();
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }
                var paramValue=await DC.Set<SysParameter>().Where(x=>x.parCode=="INTER").FirstOrDefaultAsync();
                if (paramValue == null)
                {
                    msg = $"{desc}当前接口未配置开关";
                    logger.Warn($"----->{desc}:{msg} ");
                }
                else
                {
                    if (paramValue.parValue.Equals("1"))
                    {
                        List<InterfaceConfig> interfaceConfigs = await DC.Set<InterfaceConfig>().ToListAsync();
                        if (interfaceConfigs.Count == 0)
                        {
                            msg = $"{desc}查不到入库反馈接口信息";
                            logger.Warn($"----->{desc}:{msg} ");
                        }
                        else
                        {
                            List<InterfaceSendBack> interfaceSendBacks = await DC.Set<InterfaceSendBack>().ToListAsync();
                            List<InterfaceSendBack> delItfSendBacks = new List<InterfaceSendBack>();
                            List<InterfaceSendBackHis> AddItfSendBackHiss = new List<InterfaceSendBackHis>();

                            if (interfaceSendBacks.Any())
                            {
                                if (hasParentTransaction == false)
                                {
                                    await DC.Database.BeginTransactionAsync();
                                }
                                foreach (var item in interfaceSendBacks)
                                {
                                    var itfConfig = interfaceConfigs.Where(x => x.interfaceCode == item.interfaceCode).FirstOrDefault();
                                    if (itfConfig == null)
                                    {
                                        msg = $"{desc}ID【{item.ID}】,接口编码【{item.interfaceCode}】,接口名称【{item.interfaceName}】找不到接口配置";
                                        logger.Warn($"----->{desc}:{msg} ");
                                        continue;
                                    }
                                    if (item.returnFlag < 2 && item.returnTimes < itfConfig.retryMaxTimes)
                                    {
                                        if (item.interfaceCode.Contains("IN"))
                                        {
                                            FeedbackInputDto feedbackInputDto = JsonConvert.DeserializeObject<FeedbackInputDto>(item.interfaceSendInfo);
                                            result = await interfaceConfigVM.InTaskFeedback(feedbackInputDto, itfConfig.interfaceUrl);
                                            if (result.code == ResCode.OK)
                                            {
                                                item.returnFlag = 2;
                                                item.returnTimes++;
                                                item.interfaceResult = result.msg;
                                                item.UpdateBy = "MES";
                                                item.UpdateTime = DateTime.Now;
                                                //删除List
                                                delItfSendBacks.Add(item);
                                                InterfaceSendBackHis interfaceSendBackHis = new InterfaceSendBackHis();
                                                interfaceSendBackHis = CommonHelper.Map<InterfaceSendBack, InterfaceSendBackHis>(item, "ID");
                                                AddItfSendBackHiss.Add(interfaceSendBackHis);
                                            }
                                            else
                                            {
                                                item.returnFlag = 1;
                                                item.returnTimes++;
                                                item.interfaceResult = result.msg;
                                                item.UpdateBy = "MES";
                                                item.UpdateTime = DateTime.Now;
                                            }
                                        }
                                        else if (item.interfaceCode.Contains("OUT"))
                                        {
                                            RequestInputDto RequestInputDto = JsonConvert.DeserializeObject<RequestInputDto>(item.interfaceSendInfo);
                                            result = await interfaceConfigVM.OutRequset(RequestInputDto, itfConfig.interfaceUrl);
                                            if (result.code == ResCode.OK)
                                            {
                                                item.returnFlag = 2;
                                                item.returnTimes++;
                                                item.interfaceResult = result.msg;
                                                item.UpdateBy = "MES";
                                                item.UpdateTime = DateTime.Now;
                                                //删除List
                                                delItfSendBacks.Add(item);
                                                InterfaceSendBackHis interfaceSendBackHis = new InterfaceSendBackHis();
                                                interfaceSendBackHis = CommonHelper.Map<InterfaceSendBack, InterfaceSendBackHis>(item, "ID");
                                                AddItfSendBackHiss.Add(interfaceSendBackHis);
                                            }
                                            else
                                            {
                                                item.returnFlag = 1;
                                                item.returnTimes++;
                                                item.interfaceResult = result.msg;
                                                item.UpdateBy = "MES";
                                                item.UpdateTime = DateTime.Now;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (item.returnTimes <= itfConfig.retryMaxTimes)
                                        {
                                            item.interfaceResult = $"回传已达最大次数{item.returnTimes++},如需回传可手动调用【{item.interfaceResult}】";
                                            item.UpdateBy = "WMS_JOB";
                                            item.UpdateTime = DateTime.Now;
                                        }
                                    }

                                }
                                await ((DbContext)DC).Set<InterfaceSendBack>().BulkUpdateAsync(interfaceSendBacks);
                                await ((DbContext)DC).BulkSaveChangesAsync();

                                //删除转历史
                                if (delItfSendBacks.Any())
                                {
                                    await ((DbContext)DC).Set<InterfaceSendBack>().BulkDeleteAsync(delItfSendBacks);
                                }
                                if (AddItfSendBackHiss.Any())
                                {
                                    await ((DbContext)DC).Set<InterfaceSendBackHis>().BulkInsertAsync(AddItfSendBackHiss);
                                }
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                if (hasParentTransaction == false)
                                {
                                    await DC.Database.CommitTransactionAsync();
                                }
                            }
                        }
                    }
                    else{
                        msg = $"{desc}当前接口未配置开关";
                        logger.Warn($"----->{desc}:{msg} ");
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->Warn----->{desc}:【异常】{msg} ");
            }
            
        }

        /// <summary>
        /// 创建出库请求接口信息
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateOutRequest(string invoiceNo,string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "接口定时任务:";
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                var palletTypes =await DC.Set<BasWPalletType>().Where(x=>x.palletTypeCode=="OUT" ).ToListAsync();
                if (palletTypes.Any())
                {
                    var palletType = palletTypes.Where(x => Regex.IsMatch(invoiceNo, x.checkFormula) == true).FirstOrDefault();
                    if (palletType == null)
                    {
                        msg = desc + $"当前存在出库单校验数据，{invoiceNo}校验不通过";
                        logger.Warn(msg);
                        return result.Error(msg);
                    }
                }
                var interfaceSendBackList= await DC.Set<InterfaceSendBack>().Where(x=>x.interfaceCode== "OUT_REQUEST").ToListAsync();
                if (interfaceSendBackList.Any())
                {
                    bool isEqualInvoice=false;
                    interfaceSendBackList.ForEach(x => {
                        RequestInputDto deserializedDto = JsonConvert.DeserializeObject<RequestInputDto>(x.interfaceSendInfo);
                        if (deserializedDto != null)
                        {
                            if (deserializedDto.invoiceNo.Equals(invoiceNo))
                            {
                                isEqualInvoice=true;
                            }
                        }
                    });
                    if (isEqualInvoice)
                    {
                        msg = desc + $"当前存在与{invoiceNo}一致的出库请求记录";
                        logger.Warn(msg);
                        return result.Error(msg);
                    }
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                RequestInputDto requestInputDto = new RequestInputDto()
                {
                    invoiceNo = invoiceNo
                };
                string sendInfo = JsonConvert.SerializeObject(requestInputDto);
                InterfaceSendBack interfaceSendBack = new InterfaceSendBack()
                {
                    interfaceCode = "OUT_REQUEST",
                    interfaceName = "出库请求",
                    interfaceSendInfo = sendInfo,
                    interfaceResult = "",
                    returnFlag = 0,
                    returnTimes = 0,
                    CreateBy = invoker,
                    CreateTime = DateTime.Now,
                    UpdateBy = invoker,
                    UpdateTime = DateTime.Now,
                };
                await ((DbContext)DC).Set<InterfaceSendBack>().SingleInsertAsync(interfaceSendBack);
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
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->Warn----->{desc}:【异常】{msg} ");
            }
            return result;
        }
    }
}
