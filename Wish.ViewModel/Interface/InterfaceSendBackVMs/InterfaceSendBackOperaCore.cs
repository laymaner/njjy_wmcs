using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;
using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.Common;
using Wish.ViewModel.Interface.InterfaceConfigVMs;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackVM : BaseCRUDVM<InterfaceSendBack>
    {
        public async Task<BusinessResult> HandleSendInfoToInter(HandleInterBackDto input)
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
                List<InterfaceConfig> interfaceConfigs = await DC.Set<InterfaceConfig>().ToListAsync();
                if (interfaceConfigs.Count == 0)
                {
                    msg = $"{desc}查不到入库反馈接口信息";
                    logger.Warn($"----->{desc}:{msg} ");
                }
                else
                {
                    List<InterfaceSendBack> interfaceSendBacks = await DC.Set<InterfaceSendBack>().Where(x=> input.ids.Contains(x.ID)).ToListAsync();
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
                            if (item.returnFlag < 2)
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
                                        item.UpdateBy = input.invoker;
                                        item.UpdateTime = DateTime.Now;
                                        //删除List
                                        delItfSendBacks.Add(item);
                                        InterfaceSendBackHis interfaceSendBackHis = new InterfaceSendBackHis();
                                        interfaceSendBackHis = CommonHelper.Map<InterfaceSendBack, InterfaceSendBackHis>(item, "ID");
                                        AddItfSendBackHiss.Add(interfaceSendBackHis);
                                        msg=$"{desc}{result.msg}:入库数据反馈成功";
                                        result.Success(msg);
                                    }
                                    else
                                    {
                                        item.returnFlag = 1;
                                        item.returnTimes++;
                                        item.interfaceResult = result.msg + $"【{item.interfaceResult}】";
                                        item.UpdateBy = input.invoker;
                                        item.UpdateTime = DateTime.Now;
                                        msg = $"{desc}{result.msg}:入库数据反馈失败";
                                        result.Error(msg);
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
                                        item.UpdateBy = input.invoker;
                                        item.UpdateTime = DateTime.Now;
                                        //删除List
                                        delItfSendBacks.Add(item);
                                        InterfaceSendBackHis interfaceSendBackHis = new InterfaceSendBackHis();
                                        interfaceSendBackHis = CommonHelper.Map<InterfaceSendBack, InterfaceSendBackHis>(item, "ID");
                                        AddItfSendBackHiss.Add(interfaceSendBackHis);
                                        msg = $"{desc}{result.msg}:出库请求成功";
                                        result.Success(msg);
                                    }
                                    else
                                    {
                                        item.returnFlag = 1;
                                        item.returnTimes++;
                                        item.interfaceResult = result.msg+$"【{item.interfaceResult}】";
                                        item.UpdateBy = input.invoker;
                                        item.UpdateTime = DateTime.Now;
                                        msg = $"{desc}{result.msg}:出库请求失败";
                                        result.Error(msg);
                                    }
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
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->Warn----->{desc}:【异常】{msg} ");
                return result.Error(msg);
            }
            return result;
        }
    }
}
