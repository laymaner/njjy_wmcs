using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using WISH.Helper.Common;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptVMs;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;
using Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordVMs;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{
    public partial class WmsInReceiptIqcResultVM
    {
        /// <summary>
        /// 质检确认
        /// </summary>
        /// <param name="doItnQcConfirmInParamView"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DoIDoItnQcConfirm(DoInReceiptIqcResultDto doItnQcConfirmInParamView, string invoker)
        {
            BusinessResult result = new BusinessResult();
            BusinessResult itfResult = null;
            string inParam = "";
            string outParam = "";
            string success = "";
            string _vpoint = "";
            string inputJson = JsonConvert.SerializeObject(doItnQcConfirmInParamView);
            var hasParentTransaction = false;
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (!hasParentTransaction)
                {
                    await DC.Database.BeginTransactionAsync();
                }

                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
                WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
                WmsInReceiptVM wmsInReceiptVM = Wtm.CreateVM<WmsInReceiptVM>();
                WmsInOrderVM wmsInOrderVM = Wtm.CreateVM<WmsInOrderVM>();
                WmsStockUniicodeVM wmsStockUniicodeVM = Wtm.CreateVM<WmsStockUniicodeVM>();
                WmsInReceiptIqcRecordVM wmsInReceiptIqcRecordVM = Wtm.CreateVM<WmsInReceiptIqcRecordVM>();
                CfgDocTypeDto cfgDocTypeView = null;

                #region 1  校验

                _vpoint = "检验 收货单明细ID获取收货单信息 单据状态是否正确";
                // 1-1 收货单明细ID获取收货单信息 单据状态是否正确：质检录入
                WmsInReceiptDtl wmsInReceiptDt = await wmsInReceiptVM.GetWmsInReceiptDtAsync(doItnQcConfirmInParamView.inReceiptDtID);

                WmsInOrder wmsInOrder = await wmsInOrderVM.GetWmsInOrderAsync(wmsInReceiptDt.inNo);

                if (wmsInReceiptDt == null)
                {
                    throw new Exception($"收货单明细数据 {doItnQcConfirmInParamView.inReceiptDtID}不存在!");
                }

                WmsInReceipt wmsInReceipt = await wmsInReceiptVM.GetWmsInReceiptAsync(wmsInReceiptDt.receiptNo);

                WmsInOrderDtl wmsInOrderDetail = await wmsInOrderVM.GetWmsInOrderDtlAsync(wmsInReceiptDt.inDtlId);

                if (wmsInReceipt == null)
                {
                    throw new Exception($"收货单明细数据 {doItnQcConfirmInParamView.inReceiptDtID} 对应的收货单不存在!");
                }

                cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(wmsInOrder.docTypeCode);
                bool checkFlag = cfgDocTypeView != null;
                if (cfgDocTypeView != null)
                {
                    if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
                    {
                        checkFlag = false;
                    }
                }

                // 单据类型是 委外入库 或 采购入库
                var poVsOemDocType =
                    (cfgDocTypeView.cfgDocType.docTypeCode == InOrderDocType.PO.GetCode())
                    || (cfgDocTypeView.cfgDocType.docTypeCode == InOrderDocType.OEM.GetCode());
                if (checkFlag == false)
                {
                    throw new Exception($"单据类型 {wmsInOrder.docTypeCode} 参数未配置!");
                }

                if ((wmsInReceipt.receiptStatus == Convert.ToInt32(ReceiptOrDtlStatus.QCRecord.GetCode()) || wmsInReceipt.receiptStatus == Convert.ToInt32(ReceiptOrDtlStatus.IQCing.GetCode()) || wmsInReceipt.receiptStatus == Convert.ToInt32(ReceiptOrDtlStatus.PutAwaying.GetCode())) == false)
                {
                    //ERP特采或者退货时，多次调用，根据合格数量判断
                    if (wmsInReceiptDt.qualifiedQty == 0 && wmsInReceiptDt.qualifiedSpecialQty == 0)
                    {
                        throw new Exception($"收货单 {wmsInReceipt.receiptNo} 不是质检录入/质检中状态!");
                    }
                }

                // 查找唯一码信息
                var wmsInReceiptUniicode = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeReceiptDtlIdAsync(wmsInReceiptDt.ID);
                if (!wmsInReceiptUniicode.Any())
                {
                    throw new Exception($"入库唯一码根据收货明细ID：【 {wmsInReceiptDt.ID} 】查询为空!");
                }

                //根据唯一码表中的库存编码查询库存
                //var wmsStock = DC.Set<WmsStock>().Where(x => x.stockCode == wmsInReceiptUniicode[0].stockCode).FirstOrDefault();
                var wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == wmsInReceiptUniicode[0].curStockCode).FirstOrDefaultAsync();
                if (wmsStock == null)
                {
                    throw new Exception($"根据唯一码表中的库存编码查询库存：【 {wmsInReceiptUniicode[0].curStockCode} 】为空!");
                }

                //根据唯一码表中的库存编码库存明细ID查询库存明细
                //var wmsStockDtl = DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsInReceiptUniicode[0].stockCode && x.ID == wmsInReceiptUniicode[0].stockDtlId).FirstOrDefault();
                var wmsStockDtl = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsInReceiptUniicode[0].curStockCode && x.ID == wmsInReceiptUniicode[0].curStockDtlId).FirstOrDefaultAsync();
                if (wmsStockDtl == null)
                {
                    throw new Exception($"根据唯一码表中的库存编码库位明细ID查询库存明细：【 {wmsStock.stockCode} 】为空!");
                }

                _vpoint = "检验 质检录入表信息";
                // 1-2 获取质检录入表信息
                WmsInReceiptIqcRecord wmsInReceiptIqcRecord = await wmsInReceiptIqcRecordVM.GetWmsInReceiptIqcRecordByInDtlIdAsync(wmsInReceiptDt.inDtlId);
                if (wmsInReceiptIqcRecord == null)
                {
                    throw new Exception($"收货单明细ID{doItnQcConfirmInParamView.inReceiptDtID} 对应的质检录入记录不存在!");
                }

                // 1-2-1 状态是否正确
                if ((wmsInReceiptIqcRecord.iqcRecordStatus == Convert.ToInt32(IqcRecordStatus.Init.GetCode()) ||
                     wmsInReceiptIqcRecord.iqcRecordStatus == Convert.ToInt32(IqcRecordStatus.IQCing.GetCode())) == false)
                {
                    throw new Exception($"检录入记录单不是 [初始创建] 或者 [质检中] 的状态!");
                }

                // 1-2-2 数量是否匹配  特采(ERP)/不合格(ERP)<=WMS不合格-ERP特采-ERP不合格    合格数量=录入合格
                decimal qualifiedQty = (decimal)(wmsInReceiptIqcRecord.qualifiedQty ?? 0);
                //decimal unqualifiedQty = (decimal)(wmsInReceiptIqcRecord.unqualifiedQty ?? 0);
                decimal unqualifiedQty = (decimal)(wmsInReceiptIqcRecord.wmsUnqualifiedQty ?? 0);
                //decimal unqualifiedSpecialQty = (decimal)(wmsInReceiptIqcRecord.unqualifiedSpecialQty ?? 0);
                decimal unqualifiedSpecialQty = (decimal)(wmsInReceiptIqcRecord.erpUnqualifiedQty ?? 0);
                //decimal qualifiedSpecialQty = (decimal)(wmsInReceiptIqcRecord.qualifiedSpecialQty ?? 0);
                decimal qualifiedSpecialQty = (decimal)(wmsInReceiptIqcRecord.erpQualifiedSpecialQty ?? 0);

                if (doItnQcConfirmInParamView.qcFlag == QcFlag.Pass.GetCode())
                {
                    if (qualifiedQty != doItnQcConfirmInParamView.qty)
                    {
                        throw new Exception($"检录入记录单中的合格数量与本次提交的合格数量不一致!");
                    }
                }
                else if (doItnQcConfirmInParamView.qcFlag == QcFlag.ErpAod.GetCode() || doItnQcConfirmInParamView.qcFlag == QcFlag.ErpNg.GetCode())
                {
                    if (doItnQcConfirmInParamView.qty > unqualifiedQty - unqualifiedSpecialQty - qualifiedSpecialQty)
                    {
                        throw new Exception($" 特采(ERP)数量 或者 不合格(ERP)数量 > WMS不合格数量 - ERP特采数量 - ERP不合格数量 !");
                    }
                }

                #endregion

                #region 2  业务处理

                _vpoint = "生成质检结果信息";

                #region 2-1 生成质检结果信息
                /* 修改质检逻辑2023-10-28
                 * 质检记录和质检结果应该一对一
                 * ERP特采数量更新在质检记录中，增加在质检结果数量上，退货数量更新到质检结果上
                 * 判断该
                 */
                bool isIqcResult = true;//存在质检结果数据
                var InReceiptQCResultNo = string.Empty;
                WmsInReceiptIqcResult wmsInReceiptIqcResult = new WmsInReceiptIqcResult();
                wmsInReceiptIqcResult = await DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcRecordNo == wmsInReceiptIqcRecord.iqcRecordNo && x.receiptDtlId == wmsInReceiptIqcRecord.receiptDtlId && x.materialCode == wmsInReceiptIqcRecord.materialCode && (x.iqcResultStatus == 0 || x.iqcResultStatus == 31)).FirstOrDefaultAsync();
                if (wmsInReceiptIqcResult == null || doItnQcConfirmInParamView.qcFlag == QcFlag.ErpNg.GetCode())
                {
                    isIqcResult = false;//不存在质检结果数据
                    InReceiptQCResultNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InReceiptQCRecordNo.GetCode());
                    wmsInReceiptIqcResult = BuildWmsInReceiptIqcResult(wmsInReceiptIqcRecord, doItnQcConfirmInParamView, wmsInOrderDetail, wmsInOrder, invoker);
                    wmsInReceiptIqcResult.iqcResultNo = InReceiptQCResultNo;
                    //DC.AddEntity(wmsInReceiptIqcResult);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkInsertAsync(new WmsInReceiptIqcResult[] { wmsInReceiptIqcResult });
                    await ((DbContext)DC).SaveChangesAsync();
                }
                InReceiptQCResultNo = wmsInReceiptIqcResult.iqcResultNo;

                #endregion

                _vpoint = "更新质检录入信息";

                #region 2-2 更新质检录入信息

                // ERP特采
                if (doItnQcConfirmInParamView.qcFlag == QcFlag.ErpAod.GetCode())
                {
                    //if (qualifiedSpecialQty==0)
                    //{
                    //wmsInReceiptIqcRecord.qualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    wmsInReceiptIqcRecord.erpQualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    wmsInReceiptDt.qualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    wmsInOrderDetail.qualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    //}
                    //else//第二次特采
                    //{
                    //    wmsInReceiptIqcRecord.qualifiedSpecialQty = (wmsInReceiptIqcRecord.qualifiedSpecialQty?? 0)+doItnQcConfirmInParamView.qty;
                    //    wmsInReceiptDt.qualifiedSpecialQty = (wmsInReceiptDt.qualifiedSpecialQty ?? 0)+doItnQcConfirmInParamView.qty;
                    //    wmsInOrderDetail.qualifiedSpecialQty = (wmsInOrderDetail.qualifiedSpecialQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //}
                    if (isIqcResult)
                    {
                        wmsInReceiptIqcResult.UpdateBy = invoker;
                        wmsInReceiptIqcResult.UpdateTime = DateTime.Now;
                        wmsInReceiptIqcResult.qty = (wmsInReceiptIqcResult.qty ?? 0) + doItnQcConfirmInParamView.qty; // 默认合格

                        //DC.UpdateEntity(wmsInReceiptIqcResult);
                        await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptIqcResult[] { wmsInReceiptIqcResult });
                    }

                }

                // ERP不合格
                if (doItnQcConfirmInParamView.qcFlag == QcFlag.ErpNg.GetCode())
                {
                    //ERP退货
                    //if (unqualifiedSpecialQty == 0)
                    //{
                    //wmsInReceiptIqcRecord.unqualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    wmsInReceiptIqcRecord.erpQualifiedSpecialQty = doItnQcConfirmInParamView.qty;
                    wmsInReceiptDt.unqualifiedQty = doItnQcConfirmInParamView.qty;
                    wmsInReceiptDt.returnQty = doItnQcConfirmInParamView.qty;
                    wmsInOrderDetail.unqualifiedQty = doItnQcConfirmInParamView.qty;
                    wmsInOrderDetail.returnQty = doItnQcConfirmInParamView.qty;
                    //}
                    //else//第二次ERP退货
                    //{
                    //    wmsInReceiptIqcRecord.unqualifiedSpecialQty = (wmsInReceiptIqcRecord.unqualifiedSpecialQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //    wmsInReceiptDt.unqualifiedQty = (wmsInReceiptDt.unqualifiedQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //    wmsInReceiptDt.returnQty = (wmsInReceiptDt.returnQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //    wmsInOrderDetail.unqualifiedQty = (wmsInOrderDetail.unqualifiedQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //    wmsInOrderDetail.returnQty = (wmsInOrderDetail.returnQty ?? 0) + doItnQcConfirmInParamView.qty;
                    //}

                    if (isIqcResult)
                    {
                        wmsInReceiptIqcResult.UpdateBy = invoker;
                        wmsInReceiptIqcResult.UpdateTime = DateTime.Now;
                        wmsInReceiptIqcResult.returnQty = (wmsInReceiptIqcResult.returnQty ?? 0) + doItnQcConfirmInParamView.qty;

                        //DC.UpdateEntity(wmsInReceiptIqcResult);
                        await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptIqcResult[] { wmsInReceiptIqcResult });
                    }
                }

                // 合格
                if (doItnQcConfirmInParamView.qcFlag == QcFlag.Pass.GetCode())
                {
                    wmsInReceiptIqcRecord.qualifiedQty = doItnQcConfirmInParamView.qty;
                    //if (wmsInReceiptIqcRecord.receiptQty - doItnQcConfirmInParamView.qty>0)
                    //{
                    //    wmsInReceiptIqcRecord.unqualifiedQty = wmsInReceiptIqcRecord.receiptQty - doItnQcConfirmInParamView.qty;
                    //    wmsInReceiptDt.unqualifiedQty = wmsInReceiptIqcRecord.receiptQty - doItnQcConfirmInParamView.qty;
                    //}
                    //else if (wmsInReceiptIqcRecord.receiptQty - doItnQcConfirmInParamView.qty == 0)
                    //{
                    //    wmsInReceiptIqcRecord.unqualifiedQty = 0;
                    //    wmsInReceiptDt.unqualifiedQty = 0;
                    //}
                    wmsInReceiptDt.qualifiedQty = doItnQcConfirmInParamView.qty;
                    wmsInOrderDetail.qualifiedQty = doItnQcConfirmInParamView.qty;
                    if (isIqcResult)
                    {
                        wmsInReceiptIqcResult.UpdateBy = invoker;
                        wmsInReceiptIqcResult.UpdateTime = DateTime.Now;
                        wmsInReceiptIqcResult.qty = (wmsInReceiptIqcResult.qty ?? 0) + doItnQcConfirmInParamView.qty; // 默认合格

                        //DC.UpdateEntity(wmsInReceiptIqcResult);
                        await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptIqcResult[] { wmsInReceiptIqcResult });
                    }
                }

                // 不合格数量 = 特采数量 + ERP不合格数量
                //var noPassQtyOK = (wmsInReceiptIqcRecord.unqualifiedSpecialQty ?? 0) + (wmsInReceiptIqcRecord.qualifiedSpecialQty ?? 0) ==
                //                  (wmsInReceiptIqcRecord.unqualifiedQty ?? 0);
                var noPassQtyOK = (wmsInReceiptIqcRecord.erpUnqualifiedQty ?? 0) + (wmsInReceiptIqcRecord.erpQualifiedSpecialQty ?? 0) ==
                                  (wmsInReceiptIqcRecord.wmsUnqualifiedQty ?? 0);
                //var passQtyOK = (wmsInReceiptIqcRecord.receiptQty - wmsInReceiptIqcRecord.qualifiedQty) == (wmsInReceiptIqcRecord.unqualifiedQty ?? 0);
                var passQtyOK = (wmsInReceiptIqcRecord.receiptQty - wmsInReceiptIqcRecord.qualifiedQty) == (wmsInReceiptIqcRecord.wmsUnqualifiedQty ?? 0);
                if (noPassQtyOK && passQtyOK)
                {
                    wmsInReceiptIqcRecord.iqcRecordStatus = Convert.ToInt32(IqcRecordStatus.IQCFinished.GetCode());
                    wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.IQCFinished.GetCode());
                    wmsInOrderDetail.inDtlStatus = Convert.ToInt32(InOrDtlStatus.IQCFinished.GetCode());
                }
                else
                {
                    wmsInReceiptIqcRecord.iqcRecordStatus = Convert.ToInt32(IqcRecordStatus.IQCing.GetCode());
                    wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.IQCing.GetCode());
                    wmsInOrderDetail.inDtlStatus = Convert.ToInt32(InOrDtlStatus.IQCing.GetCode());
                }

                foreach (var item in wmsInReceiptUniicode)
                {
                    //更新入库唯一码
                    if (string.IsNullOrWhiteSpace(item.iqcResultNo))
                    {
                        item.iqcResultNo = wmsInReceiptIqcResult.iqcResultNo;
                    }
                    item.UpdateBy = invoker; /// LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    item.UpdateTime = DateTime.Now;
                }


                //更新库存主表状态为入库中
                wmsStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                wmsStock.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsStock.UpdateTime = DateTime.Now;

                //更新库存明细表状态为入库中
                if (doItnQcConfirmInParamView.qcFlag != QcFlag.ErpNg.GetCode())
                {
                    wmsStockDtl.inspectionResult = Convert.ToInt32(doItnQcConfirmInParamView.qcResult);
                }
                wmsStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                wmsStockDtl.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsStockDtl.UpdateTime = DateTime.Now;

                wmsInReceiptIqcRecord.UpdateBy = LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInReceiptIqcRecord.UpdateTime = DateTime.Now;
                wmsInReceiptDt.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInReceiptDt.UpdateTime = DateTime.Now;
                wmsInOrderDetail.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInOrderDetail.UpdateTime = DateTime.Now;

                //foreach (var item in wmsStockUniicode)
                //{
                //    item.updateBy = invoker;
                //    item.updateTime = DateTime.Now;
                //    item.inspectionResult = Convert.ToInt32(doItnQcConfirmInParamView.qcResult);
                //}

                //DC.UpdateEntity(wmsInReceiptUniicode);
                //DC.Set<WmsInReceiptUniicode>().UpdateRange(wmsInReceiptUniicode);
                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptUniicode);
                //DC.UpdateEntity(wmsStock);
                await ((DbContext)DC).BulkUpdateAsync(new WmsStock[] { wmsStock });
                await ((DbContext)DC).BulkUpdateAsync(new WmsStockDtl[] { wmsStockDtl });
                //DC.UpdateEntity(wmsStockDtl);

                //DC.UpdateEntity(wmsInReceiptIqcRecord);
                //DC.UpdateEntity(wmsInReceiptDt);
                //DC.UpdateEntity(wmsInOrderDetail);
                await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptIqcRecord[] { wmsInReceiptIqcRecord });
                await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptDtl[] { wmsInReceiptDt });
                await ((DbContext)DC).BulkUpdateAsync(new WmsInOrderDtl[] { wmsInOrderDetail });
                //DC.Set<WmsStockUniicode>().UpdateRange(wmsStockUniicode);
                await ((DbContext)DC).SaveChangesAsync();
                //DC.SaveChanges();

                #endregion


                _vpoint = "更新收单信息";

                #region 2-4 更新收货单、入库单信息

                // 2-4-1 收货单明细信息 在2-2中已处理
                //          合格/不合格/特采数量
                //          状态：质检中/质检完成

                // 2-4-2 收货单主表信息
                //          状态：质检中/质检完成
                var _temp = await wmsInReceiptVM.IsExistInReceiptDtlStatusMinAsync(wmsInReceiptDt.receiptNo, ReceiptOrDtlStatus.IQCFinished.GetCode());

                _vpoint = "更新入库单信息";
                // 2-5 更新入库单信息

                // 2-5-1 入库单明细信息
                //          合格/不合格/特采数量
                //          状态：质检中/质检完成

                // 2-5-2 入库单主表信息
                //          状态：质检中/质检完成

                if (_temp == false)
                {
                    wmsInReceipt.receiptStatus = Convert.ToInt32(ReceiptOrDtlStatus.IQCFinished.GetCode());
                    wmsInOrder.inStatus = Convert.ToInt32(ReceiptOrDtlStatus.IQCFinished.GetCode());
                }
                else
                {
                    wmsInReceipt.receiptStatus = Convert.ToInt32(ReceiptOrDtlStatus.IQCing.GetCode());
                    wmsInOrder.inStatus = Convert.ToInt32(ReceiptOrDtlStatus.IQCing.GetCode());
                }

                wmsInReceipt.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInReceipt.UpdateTime = DateTime.Now;

                wmsInOrder.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInOrder.UpdateTime = DateTime.Now;


                //DC.UpdateEntity(wmsInReceipt);
                //DC.UpdateEntity(wmsInOrder);
                //await ((DbContext)DC).BulkUpdateAsync(new WmsInReceipt[] { wmsInReceipt });
                await ((DbContext)DC).Set<WmsInReceipt>().SingleUpdateAsync(wmsInReceipt);
                await ((DbContext)DC).Set<WmsInOrder>().SingleUpdateAsync(wmsInOrder);
                //await ((DbContext)DC).BulkUpdateAsync(new WmsInOrder[] { wmsInOrder });
                await ((DbContext)DC).SaveChangesAsync();
                //DC.SaveChanges();

                #endregion

                result.outParams = wmsInReceiptIqcResult;

                #region 2-5 处理单据类型为采购入库/委外入库  ---WMS采购检验导入ERP


                #endregion

                #endregion
                #region 操作日志
                string inparams = JsonConvert.SerializeObject(doItnQcConfirmInParamView);
                string outparams = JsonConvert.SerializeObject(result);
                #endregion
                if (!hasParentTransaction)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        await DC.Database.CommitTransactionAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!hasParentTransaction)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        await DC.Database.RollbackTransactionAsync();
                    }
                }

                result.code = ResCode.Error;
                result.msg = $"WmsItnQcRecordVM->DoIDoItnQcConfirm 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }
            finally
            {
                // 如果存在接口调用，那就把参数和结果存起来，放finally里面是因为上面的代码会将接口日记录也回滚掉
                if (null != itfResult)
                {
                    if (null != itfResult.outParams)
                    {
                        outParam = JsonConvert.SerializeObject(itfResult.outParams);
                    }

                    success = itfResult.code == ResCode.OK ? "成功" : "失败";
                }
                #region 操作日志
                string inparams = JsonConvert.SerializeObject(doItnQcConfirmInParamView);
                string outparams = JsonConvert.SerializeObject(result);
                logger.Warn($"--->质检结果，操作人：{invoker}-->入参{inputJson}--->出参{outparams}-->接口入参{inParam}-->接口出参{outParam}");
                #endregion
            }

            return result;
        }

    }
}
