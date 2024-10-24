using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using System.DirectoryServices.Protocols;
using WISH.Helper.Common;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using WISH.ViewModels.Common.Dtos;
using Wish.ViewModel.Common.Dtos;


namespace Wish.ViewModel.BusinessIn.WmsInOrderVMs
{
    public partial class WmsInOrderVM
    {
        public async Task<BusinessResult> UpdateInOrder(WmsInOrderDto orderView, string invoker)
        {
            BusinessResult businessResult = new BusinessResult();
            // var hasParentTransaction = false;
            try
            {
                if (orderView == null)
                {
                    businessResult.code = ResCode.Error;
                    businessResult.msg = "(UpdateInOrder)入库单据更新，入参为空";
                    return businessResult;
                }

                if (orderView.wmsInOrderMain == null)
                {
                    businessResult.code = ResCode.Error;
                    businessResult.msg = "(UpdateInOrder)入库单据更新，入参中主表数据为空";
                    return businessResult;
                }

                if (string.IsNullOrWhiteSpace(orderView.wmsInOrderMain.externalInNo))
                {
                    businessResult.code = ResCode.Error;
                    businessResult.msg = "(UpdateInOrder)入库单据更新，入参中主表数据中外部单号为空";
                    return businessResult;
                }

                if (orderView.wmsInOrderDetail == null || orderView.wmsInOrderDetail.Count == 0)
                {
                    businessResult.code = ResCode.Error;
                    businessResult.msg = "(UpdateInOrder)入库单据更新，入参中明细数据为空";
                    return businessResult;
                }

                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                var oldWmsInOrder = await DC.Set<WmsInOrder>().Where(t => t.externalInNo == orderView.wmsInOrderMain.externalInNo).FirstOrDefaultAsync();
                if (oldWmsInOrder != null)
                {
                    //是否校验两次的单据类型是否一致
                    CfgDocTypeDto cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(oldWmsInOrder.docTypeCode);
                    if (cfgDocTypeView.cfgDocType == null)
                    {
                        businessResult.code = ResCode.Error;
                        businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】对应WMS单据类型【{oldWmsInOrder.docTypeCode}】未找到";
                        return businessResult;
                    }

                    if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
                    {
                        businessResult.code = ResCode.Error;
                        businessResult.msg = $"单据类型 {oldWmsInOrder.docTypeCode} 参数未配置!";
                        return businessResult;
                    }

                    // modified by Allen 2024-03-25 08:13:40 这个里面只需要查出传进来的明细行ID对应的所有明细记录，而不是查询所有的明细行数据
                    var newInDtlNoList = orderView.wmsInOrderDetail.Select(t => t.externalInDtlId).ToList();
                    var oldWmsInOrderDtlList = await DC.Set<WmsInOrderDtl>().Where(t => t.inNo == oldWmsInOrder.inNo && newInDtlNoList.Contains(t.externalInDtlId)).ToListAsync();
                    if (oldWmsInOrderDtlList.Count == 0)
                    {
                        businessResult.code = ResCode.Error;
                        businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】未找到入库单明细记录";
                        return businessResult;
                    }

                    /*
                    // 这个判断就不需要加了,因为接口里面可能只传过来需要更新的明细行数据,而并不是所有行都传进来,modified by Allen 2024-03-25 08:02:32
                    if (oldWmsInOrderDtlList.Count != orderView.wmsInOrderDetail.Count)
                    {
                        businessResult.code = ResCode.Error;
                        businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】待更新的明细行与数据库已存在明细行行数不一致";
                        return businessResult;
                    }
                    */

                    var oldInDtlNoList = oldWmsInOrderDtlList.Select(t => t.externalInDtlId).ToList();
                    if (newInDtlNoList.Count == oldInDtlNoList.Count && newInDtlNoList.Count(t => !oldInDtlNoList.Contains(t)) == 0)
                    {
                        // 自动收货且自动质检，这个状态就是应该29以下
                        bool autoReceipt = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.AutoReceipt.GetCode()) == YesNoCode.Yes.GetCode();
                        bool autoQcOK = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.AutoQC.GetCode()) == YesNoCode.Yes.GetCode();
                        if (autoReceipt && autoQcOK)
                        {
                            // var statusList = new List<int> { 31, 39, 41, 90 };
                            // 这个状态改为29以上状态就不允许再修改，因为状态90上面还有92强制删除、93强制完成 状态
                            var dtl = oldWmsInOrderDtlList.FirstOrDefault(t => t.inDtlStatus > int.Parse(InOrDtlStatus.IQCFinished.GetCode()));
                            if (dtl != null)
                            {
                                businessResult.code = ResCode.Error;
                                businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】, 外部入库单行号【{oldWmsInOrder.externalInId}】对应单据已在作业，无法修改";
                                return businessResult;
                            }

                            //数据处理
                            oldWmsInOrder.UpdateBy = invoker;
                            oldWmsInOrder.UpdateTime = DateTime.Now;
                            foreach (var item in oldWmsInOrderDtlList)
                            {
                                var newDtlItem = orderView.wmsInOrderDetail.FirstOrDefault(t => item.externalInNo == t.externalInNo && item.externalInDtlId == t.externalInDtlId);
                                if (newDtlItem != null)
                                {
                                    item.projectNo = newDtlItem.projectNo;
                                    item.erpWhouseNo = newDtlItem.erpWhouseNo;
                                    //item.docNum = newDtlItem.docNum;
                                    item.inQty = newDtlItem.inQty;
                                    item.extend1 = newDtlItem.extend1;
                                    item.extend2 = newDtlItem.extend2;
                                    item.extend3 = newDtlItem.extend3;
                                    item.extend4 = newDtlItem.extend4;
                                    item.extend5 = newDtlItem.extend5;
                                    item.extend6 = newDtlItem.extend6;
                                    item.extend7 = newDtlItem.extend7;
                                    item.extend8 = newDtlItem.extend8;
                                    item.extend9 = newDtlItem.extend9;
                                    item.extend10 = newDtlItem.extend10;
                                    item.extend11 = newDtlItem.extend11;
                                    item.extend12 = newDtlItem.extend12;
                                    item.extend13 = newDtlItem.extend13;
                                    item.extend14 = newDtlItem.extend14;
                                    item.extend15 = newDtlItem.extend15;
                                    item.UpdateBy = invoker;
                                    item.UpdateTime = DateTime.Now;
                                }
                            }

                            businessResult = await Update(oldWmsInOrder, oldWmsInOrderDtlList, invoker, true);
                        }
                        else
                        {
                            // 非自动收货且非自动质检，那状态默认就是0
                            var dtl = oldWmsInOrderDtlList.FirstOrDefault(t => t.inDtlStatus != 0);
                            if (dtl != null)
                            {
                                businessResult.code = ResCode.Error;
                                businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】, 外部入库单行号【{oldWmsInOrder.externalInId}】对应单据已在作业，无法修改";
                                return businessResult;
                            }

                            oldWmsInOrder.UpdateBy = invoker;
                            oldWmsInOrder.UpdateTime = DateTime.Now;
                            foreach (var item in oldWmsInOrderDtlList)
                            {
                                var newDtlItem = orderView.wmsInOrderDetail.FirstOrDefault(t => item.externalInNo == t.externalInNo && item.externalInDtlId == t.externalInDtlId);
                                if (newDtlItem != null)
                                {
                                    item.projectNo = newDtlItem.projectNo;
                                    item.erpWhouseNo = newDtlItem.erpWhouseNo;
                                    //item.docNum = newDtlItem.docNum;
                                    item.inQty = newDtlItem.inQty;
                                    item.extend1 = newDtlItem.extend1;
                                    item.extend2 = newDtlItem.extend2;
                                    item.extend3 = newDtlItem.extend3;
                                    item.extend4 = newDtlItem.extend4;
                                    item.extend5 = newDtlItem.extend5;
                                    item.extend6 = newDtlItem.extend6;
                                    item.extend7 = newDtlItem.extend7;
                                    item.extend8 = newDtlItem.extend8;
                                    item.extend9 = newDtlItem.extend9;
                                    item.extend10 = newDtlItem.extend10;
                                    item.extend11 = newDtlItem.extend11;
                                    item.extend12 = newDtlItem.extend12;
                                    item.extend13 = newDtlItem.extend13;
                                    item.extend14 = newDtlItem.extend14;
                                    item.extend15 = newDtlItem.extend15;
                                    item.UpdateBy = invoker;
                                    item.UpdateTime = DateTime.Now;
                                }
                            }

                            businessResult = await Update(oldWmsInOrder, oldWmsInOrderDtlList, invoker);
                            //businessResult.code = ResCode.OK;
                            //businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】更新成功";
                        }
                    }
                    else
                    {
                        businessResult.code = ResCode.Error;
                        businessResult.msg = $"外部入库单号【{oldWmsInOrder.externalInNo}】待更新的明细行号【{string.Join(',', newInDtlNoList)}】与数据库已存在明细行号【{string.Join(',', oldInDtlNoList)}】不一致";
                        return businessResult;
                    }
                }
            }
            catch (Exception ex)
            {
                //if (hasParentTransaction == false)
                //{
                //    DC.Database.RollbackTransaction();
                //}

                businessResult.code = ResCode.Error;
                businessResult.msg = $"UpdateInOrder(WmsInOrderView orderView) 执行异常!  异常信息: [ {ex.Message} ]";
            }

            return businessResult;
        }


        private async Task<BusinessResult> Update(WmsInOrder order, List<WmsInOrderDtl> dtlList, string invoker, bool hasQc = false)
        {
            BusinessResult businessResult = new BusinessResult();
            var hasParentTransaction = false;
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }

                if (hasQc)
                {
                    dtlList.ForEach(t =>
                    {
                        if (t.qualifiedQty > 0)
                        {
                            //t.qualifiedQty = t.docNum;
                            t.qualifiedQty = t.inQty;
                        }

                        if (t.qualifiedSpecialQty > 0)
                        {
                            //t.qualifiedSpecialQty = t.docNum;
                            t.qualifiedSpecialQty = t.inQty;
                        }

                        if (t.returnQty > 0)
                        {
                            //t.returnQty = t.docNum;
                            t.returnQty = t.inQty;
                        }

                        if (t.receiptQty > 0)
                        {
                            //t.receiptQty = t.docNum;
                            t.receiptQty = t.inQty;
                        }
                    });
                    var receiptInfos = await DC.Set<WmsInReceipt>().Where(t => t.inNo == order.inNo).ToListAsync();
                    receiptInfos.ForEach(t =>
                    {
                        t.UpdateBy = invoker;
                        t.UpdateTime = DateTime.Now;
                    });
                    var receiptDtlInfos = await DC.Set<WmsInReceiptDtl>().Where(t => t.inNo == order.inNo).ToListAsync();
                    var receiptIqcResultInfos = await DC.Set<WmsInReceiptIqcResult>().Where(t => t.inNo == order.inNo).ToListAsync();

                    var receiptUniiInfos = await DC.Set<WmsInReceiptUniicode>().Where(t => t.inNo == order.inNo).ToListAsync();

                    var receiptNoList = receiptInfos.Select(t => t.receiptNo).Distinct().ToList();
                    var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => receiptNoList.Contains(t.palletBarcode)).ToListAsync();
                    var uniiList = receiptUniiInfos.Select(t => t.uniicode).Distinct().ToList();
                    var stockUniiInfos = await DC.Set<WmsStockUniicode>().Where(t => uniiList.Contains(t.uniicode)).ToListAsync();

                    foreach (var item in dtlList)
                    {
                        var receiptDtlInfo = receiptDtlInfos.FirstOrDefault(t => t.inNo == item.inNo && t.inDtlId == item.ID);
                        if (receiptDtlInfo != null)
                        {
                            receiptDtlInfo.erpWhouseNo = item.erpWhouseNo;
                            receiptDtlInfo.projectNo = item.projectNo;
                            receiptDtlInfo.receiptQty = item.receiptQty;
                            receiptDtlInfo.qualifiedQty = item.qualifiedQty;
                            receiptDtlInfo.qualifiedSpecialQty = item.qualifiedSpecialQty;
                            receiptDtlInfo.returnQty = item.returnQty;
                            receiptDtlInfo.UpdateBy = invoker;
                            receiptDtlInfo.UpdateTime = DateTime.Now;

                            var receiptIqcResultInfo = receiptIqcResultInfos.FirstOrDefault(t => t.receiptNo == receiptDtlInfo.receiptNo && t.receiptDtlId == receiptDtlInfo.ID);
                            if (receiptIqcResultInfo != null)
                            {
                                receiptIqcResultInfo.erpWhouseNo = item.erpWhouseNo;
                                receiptIqcResultInfo.projectNo = item.projectNo;
                                receiptIqcResultInfo.qty = item.receiptQty;
                                receiptIqcResultInfo.returnQty = item.returnQty;
                                receiptIqcResultInfo.UpdateBy = invoker;
                                receiptIqcResultInfo.UpdateTime = DateTime.Now;
                            }

                            var receiptUniiFor = receiptUniiInfos.Where(t => t.receiptNo == receiptDtlInfo.receiptNo && t.receiptDtlId == receiptDtlInfo.ID).ToList();
                            if (receiptUniiFor.Count == 1)
                            {
                                receiptUniiFor.ForEach(unii =>
                                {
                                    unii.erpWhouseNo = item.erpWhouseNo;
                                    //unii.receiptQty = item.receiptQty;
                                    unii.qty = item.receiptQty;
                                    unii.UpdateBy = invoker;
                                    unii.UpdateTime = DateTime.Now;
                                    var stockUniiFor = stockUniiInfos.FirstOrDefault(t => t.uniicode == unii.uniicode);
                                    if (stockUniiFor != null)
                                    {
                                        stockUniiFor.erpWhouseNo = item.erpWhouseNo;
                                        stockUniiFor.occupyQty = item.receiptQty;
                                        unii.UpdateBy = invoker;
                                        unii.UpdateTime = DateTime.Now;
                                    }
                                });
                            }

                            // modified by Allen 2024-04-08 15:26:17 这块获取库存明细的时候可以带上 extend3=收货单明细行对应的ID，由这个确定唯一记录，并进行相应更新库存明细, 这样就可以确定库存明细的唯一性
                            // var stockDtl = stockDtlInfos.FirstOrDefault(t => t.palletBarcode == receiptDtlInfo.receiptNo && t.materialCode == receiptDtlInfo.materialCode);
                            var stockDtl = stockDtlInfos.FirstOrDefault(t => t.palletBarcode == receiptDtlInfo.receiptNo && t.materialCode == receiptDtlInfo.materialCode && t.extend3 == receiptDtlInfo.ID.ToString());
                            if (stockDtl != null)
                            {
                                stockDtl.erpWhouseNo = item.erpWhouseNo;
                                stockDtl.projectNo = item.projectNo;
                                stockDtl.qty = item.receiptQty;
                                receiptDtlInfo.UpdateBy = invoker;
                                receiptDtlInfo.UpdateTime = DateTime.Now;
                            }
                        }
                    }

                    //DC.Set<WmsInOrder>().Update(order);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(new WmsInOrder[] { order });
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsInOrderDetail>().UpdateRange(dtlList);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(dtlList);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsInReceipt>().UpdateRange(receiptInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(receiptInfos);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsInReceiptDt>().UpdateRange(receiptDtlInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(receiptDtlInfos);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsInReceiptUniicode>().UpdateRange(receiptUniiInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(receiptUniiInfos);
                    await ((DbContext)DC).SaveChangesAsync();

                    //DC.Set<WmsInReceiptIqcResult>().UpdateRange(receiptIqcResultInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(receiptIqcResultInfos);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsStockDtl>().UpdateRange(stockDtlInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(stockDtlInfos);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsStockUniicode>().UpdateRange(stockUniiInfos);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(stockUniiInfos);
                    await ((DbContext)DC).SaveChangesAsync();
                }
                else
                {
                    //DC.Set<WmsInOrder>().Update(order);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(new WmsInOrder[] { order });
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsInOrderDetail>().UpdateRange(dtlList);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(dtlList);
                    await ((DbContext)DC).SaveChangesAsync();
                }

                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }

                businessResult.code = ResCode.OK;
                businessResult.msg = $"外部入库单号【{order.externalInNo}】更新成功";
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                businessResult.code = ResCode.Error;
                businessResult.msg = $"UpdateInOrder(WmsInOrderView orderView) 执行异常!  异常信息: [ {ex.Message} ]";
            }

            return businessResult;
        }
    }
}
