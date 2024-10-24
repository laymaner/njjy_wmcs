using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.DirectoryServices.Protocols;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Wish.ViewModel.Common;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BasWhouse.BasWPalletTypeVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using NPOI.SS.Formula.Functions;

namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayVM
    {
        /// <summary>
        /// 入库业务：上架
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> PutAway(PutAwayDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWPalletTypeVM basWPalletTypeVM = Wtm.CreateVM<BasWPalletTypeVM>();
            var hasParentTransaction = false;
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            result = await ParamValid(input);
            if (result.code == ResCode.OK)
            {
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
                    #region 通过托盘号获取上架单主表信息
                    _vpoint = $"通过托盘号获取上架单主表信息";
                    List<WmsPutaway> wmsPutaways = await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == input.palletBarcode).ToListAsync();
                    if (wmsPutaways.Count > 1)
                    {
                        throw new Exception($"根据托盘号{input.palletBarcode}，查询到多条上架单主表。");
                    }
                    var putawayNos = wmsPutaways.Select(x => x.putawayNo).ToList();
                    var wmsPutAwayEntitys = wmsPutaways.Where(x => x.putawayStatus >= 90).FirstOrDefault();
                    if (wmsPutAwayEntitys != null)
                    {
                        throw new Exception($"根据托盘号{input.palletBarcode}，查询到上架单主表已完成/删除/强制完成。");
                    }
                    var wmsPutAwayEntity = wmsPutaways.FirstOrDefault();
                    #endregion

                    #region 获取上架单明细信息
                    _vpoint = $"获取上架单明细信息";
                    List<WmsPutawayDtl> wmsPutawayDtls = await DC.Set<WmsPutawayDtl>().Where(x => putawayNos.Contains(x.putawayNo) && x.putawayDtlStatus < 90).ToListAsync();
                    if (!wmsPutawayDtls.Any())
                    {
                        throw new Exception($"根据托盘号{input.palletBarcode}，上架单号{JsonConvert.SerializeObject(putawayNos)}，查询不到未完成的上架单明细。");
                    }
                    #endregion

                    #region 库位校验
                    _vpoint = $"库位校验";
                    BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == input.binNo && x.binType == "ST" && x.virtualFlag == 0).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        throw new Exception($"根据库位号{input.binNo}，查询不到存储库位信息。");
                    }

                    _vpoint = $"库存校验";
                    var capacitySize = basWBin.capacitySize ?? 1;
                    //var stockCodeQuery= wmsPutaways.Select(x => x.stockCode).FirstOrDefault();
                    //var wmsStockList = await DC.Set<WmsStock>().Where(x => x.binNo == basWBin.binNo && x.stockCode != wmsPutAwayEntity.stockCode).ToListAsync();
                    var wmsStockList = await DC.Set<WmsStock>().Where(x => x.binNo == basWBin.binNo && x.stockCode != wmsPutaways[0].stockCode).ToListAsync();
                    if (capacitySize <= wmsStockList.Count)
                    {
                        throw new Exception($"库位{input.binNo}已达到最大容量{capacitySize}，当前容量{wmsStockList.Count}，不能上架到此库位。");
                    }
                    WmsStock wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == wmsPutAwayEntity.stockCode).FirstOrDefaultAsync();
                    if (wmsStock == null)
                    {
                        throw new Exception($"根据库存编码{wmsPutAwayEntity.stockCode}，查询不到库存主表信息。");
                    }
                    if (wmsStock.loadedType == 1)
                    {
                        _vpoint = $"ERP库位校验";
                        BasWErpWhouseBin basWErpWhouseBin = await DC.Set<BasWErpWhouseBin>().Where(x => x.binNo == basWBin.binNo).FirstOrDefaultAsync();
                        if (basWErpWhouseBin == null)
                        {
                            throw new Exception($"根据库位号{input.binNo}，查询不到ERP库位信息。");
                        }
                    }

                    #endregion

                    #region 逻辑处理
                    #region 库存处理
                    _vpoint = $"更新库存信息";

                    wmsStock.palletBarcode = input.palletBarcode;
                    wmsStock.binNo = input.binNo;
                    wmsStock.regionNo = basWBin.regionNo;
                    wmsStock.roadwayNo = basWBin.roadwayNo;
                    wmsStock.stockStatus = 50;
                    wmsStock.UpdateBy = invoker;
                    wmsStock.UpdateTime = DateTime.Now;
                    //((DbContext)DC).BulkUpdate(new WmsStock[] { wmsStock });
                    await ((DbContext)DC).BulkUpdateAsync(new WmsStock[] { wmsStock });

                    _vpoint = $"更新库存明细信息";
                    var stockDtlIds = wmsPutawayDtls.Select(x => x.stockDtlId).Distinct().ToList();
                    //List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsPutAwayEntity.stockCode).ToListAsync();
                    List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsStock.stockCode && stockDtlIds.Contains(x.ID)).ToListAsync();
                    if (wmsStockDtls.Count != stockDtlIds.Count)
                    {
                        logger.Warn($"---->上架操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,库位号：【{input.binNo}】,入参：【{inputJson}】,上架单明细中库存明细ID{JsonConvert.SerializeObject(stockDtlIds)}数量{stockDtlIds.Count}，与查询到库存明细数量不一致{wmsStockDtls.Count}");
                    }
                    foreach (var stockDtl in wmsStockDtls)
                    {
                        stockDtl.palletBarcode = input.palletBarcode;
                        stockDtl.stockDtlStatus = 50;
                        stockDtl.UpdateBy = invoker;
                        stockDtl.UpdateTime = DateTime.Now;
                    }
                    //((DbContext)DC).BulkUpdate(wmsStockDtls);
                    await ((DbContext)DC).BulkUpdateAsync(wmsStockDtls);
                    _vpoint = $"新增库存调整记录";
                    WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                    wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                    wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                    wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                    wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                    wmsStockAdjust.adjustType = "上线"; // 调整类型;新增、修改、删除
                    //wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                    wmsStockAdjust.adjustDesc = "更新库存状态更新为在库！上架库位：" + input.binNo;
                    wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                    wmsStockAdjust.CreateBy = invoker;
                    wmsStockAdjust.CreateTime = DateTime.Now;
                    //((DbContext)DC).BulkInsert(new WmsStockAdjust[] { wmsStockAdjust });
                    await ((DbContext)DC).BulkInsertAsync(new WmsStockAdjust[] { wmsStockAdjust });
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    #endregion

                    #region 上架单处理
                    //_vpoint = $"上架单处理";
                    //wmsPutAwayEntity.ptaRegionNo = basWBin.regionNo;
                    //wmsPutAwayEntity.putawayStatus = 90;
                    //wmsPutAwayEntity.updateBy = invoker;
                    //wmsPutAwayEntity.updateTime = DateTime.Now;
                    ////((DbContext)DC).BulkUpdate(new WmsPutaway[] { wmsPutAwayEntity });
                    ////((DbContext)DC).BulkSaveChanges();
                    //await ((DbContext)DC).BulkUpdateAsync(new WmsPutaway[] { wmsPutAwayEntity });
                    //await ((DbContext)DC).BulkSaveChangesAsync();
                    //_vpoint = $"上架单转历史";
                    //var his = CommonHelper.Map<WmsPutaway, WmsPutawayHis>(wmsPutAwayEntity, "ID");
                    //his.ID = Guid.NewGuid().ToString();
                    ////DC.AddEntity(his);
                    ////((DbContext)DC).BulkInsert(new WmsPutawayHis[] { his });
                    ////((DbContext)DC).BulkDelete(new WmsPutaway[] { wmsPutAwayEntity });
                    ////((DbContext)DC).BulkSaveChanges();
                    //await ((DbContext)DC).BulkInsertAsync(new WmsPutawayHis[] { his });
                    //await ((DbContext)DC).BulkDeleteAsync(new WmsPutaway[] { wmsPutAwayEntity });
                    //await ((DbContext)DC).BulkSaveChangesAsync();

                    _vpoint = $"上架单明细处理";
                    wmsPutawayDtls.ForEach(t =>
                    {
                        t.ptaBinNo = input.binNo;
                        t.putawayDtlStatus = 90;
                        t.UpdateBy = invoker;
                        t.CreateTime = DateTime.Now;
                    });
                    //((DbContext)DC).BulkUpdate(wmsPutawayDtls);
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(wmsPutawayDtls);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    _vpoint = $"上架单明细转历史";
                    List<WmsPutawayDtlHis> wmsPutawayDtlHisList = new List<WmsPutawayDtlHis>();
                    wmsPutawayDtls.ForEach(t =>
                    {
                        var his = CommonHelper.Map<WmsPutawayDtl, WmsPutawayDtlHis>(t, "ID");
                        wmsPutawayDtlHisList.Add(his);
                    });

                    #endregion
                    _vpoint = $"明细上架";
                    var sameDocTypeCount = wmsPutawayDtls.GroupBy(x => x.docTypeCode).ToList().Count();
                    if (sameDocTypeCount > 1)
                    {
                        foreach (var t in wmsPutawayDtls)
                        {
                            PutAwayByDtlDto item = new PutAwayByDtlDto
                            {
                                palletBarcode = t.palletBarcode,
                                binNo = t.binNo,
                                putawayDtlId = t.ID,
                            };
                            result = await PutAwayByDtl(item, invoker);
                            if (result.code == ResCode.Error)
                            {
                                throw new Exception(result.msg);
                            }
                        }
                        if (wmsPutawayDtlHisList.Any())
                        {
                            //((DbContext)DC).BulkInsert(wmsPutawayDtlHisList);
                            //((DbContext)DC).BulkDelete(wmsPutawayDtls);
                            //((DbContext)DC).BulkSaveChanges();
                            await ((DbContext)DC).BulkInsertAsync(wmsPutawayDtlHisList);
                            await ((DbContext)DC).BulkDeleteAsync(wmsPutawayDtls);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.CommitTransactionAsync();
                        }
                        return result;
                    }
                    else//整托上架
                    {
                        if (wmsPutawayDtlHisList.Any())
                        {
                            //((DbContext)DC).BulkInsert(wmsPutawayDtlHisList);
                            //((DbContext)DC).BulkDelete(wmsPutawayDtls);
                            //((DbContext)DC).BulkSaveChanges();
                            await ((DbContext)DC).BulkInsertAsync(wmsPutawayDtlHisList);
                            await ((DbContext)DC).BulkDeleteAsync(wmsPutawayDtls);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        _vpoint = $"整托上架-获取单据类型";
                        List<CfgDocType> cfgDocTypes = await DC.Set<CfgDocType>().Where(x => x.docTypeCode == wmsPutawayDtls[0].docTypeCode).ToListAsync();
                        //if (cfgDocTypes.Count != 1)
                        //{
                        //    throw new Exception($"单据类型{wmsPutawayDtls[0].docTypeCode}，维护信息错误，不存在或多条");
                        //}
                        var cfgDocType = cfgDocTypes.Where(x => !string.IsNullOrWhiteSpace(x.businessCode)).FirstOrDefault();
                        if (cfgDocType == null)
                        {
                            throw new Exception($"单据类型{wmsPutawayDtls[0].docTypeCode}，维护信息错误，没有维护业务代码");
                        }
                        else
                        {
                            #region 按业务类型处理--入库记录，入库唯一码，质检结果，收货明细，收货单，入库明细，入库单
                            if (cfgDocType.businessCode == "IN")
                            {
                                _vpoint = $"整托上架-入库记录";
                                var recordIds = wmsPutawayDtls.Select(x => x.recordId).Distinct().ToList();
                                var stockCodes = wmsPutawayDtls.Select(x => x.stockCode).Distinct().ToList();
                                List<WmsInReceiptRecord> wmsInReceiptRecords = await DC.Set<WmsInReceiptRecord>().Where(x => stockCodes.Contains(x.stockCode)).ToListAsync();
                                wmsInReceiptRecords.ForEach(t =>
                                {
                                    t.ptaBinNo = input.binNo;
                                    t.ptaPalletBarcode = input.palletBarcode;
                                    t.ptaRegionNo = basWBin.regionNo;
                                    t.ptaStockCode = wmsStock.stockCode;
                                    var stockId = wmsPutawayDtls.Where(x => x.recordId == t.ID).Select(x => x.stockDtlId).FirstOrDefault();
                                    t.ptaStockDtlId = stockId;
                                    t.inRecordStatus = 90;
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceiptRecords);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptRecords);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-入库唯一码";
                                List<WmsInReceiptUniicode> wmsInReceiptUniicodes = await DC.Set<WmsInReceiptUniicode>().Where(x => recordIds.Contains(x.receiptRecordId)).ToListAsync();
                                wmsInReceiptUniicodes.ForEach(t =>
                                {
                                    t.runiiStatus = 90;
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceiptUniicodes);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptUniicodes);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-质检结果";
                                var iqcResultNos = wmsInReceiptRecords.Select(x => x.iqcResultNo).Distinct().ToList();
                                List<WmsInReceiptIqcResult> wmsInReceiptIqcResults = await DC.Set<WmsInReceiptIqcResult>().Where(x => iqcResultNos.Contains(x.iqcResultNo)).ToListAsync();
                                wmsInReceiptIqcResults.ForEach(t =>
                                {
                                    var qty = wmsInReceiptRecords.Where(x => x.iqcResultNo == t.iqcResultNo).Sum(x => x.recordQty);
                                    t.putawayQty += qty;
                                    if (t.putawayQty == t.qty)
                                    {
                                        t.iqcResultStatus = 90;
                                    }
                                    else if (t.recordQty == t.qty)
                                    {
                                        t.iqcResultStatus = 41;
                                    }
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceiptIqcResults);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptIqcResults);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-收货明细";
                                var receiptDtlIds = wmsInReceiptRecords.Select(x => x.receiptDtlId).Distinct().ToList();
                                List<WmsInReceiptDtl> wmsInReceiptDts = await DC.Set<WmsInReceiptDtl>().Where(x => receiptDtlIds.Contains(x.ID)).ToListAsync();
                                wmsInReceiptDts.ForEach(t =>
                                {
                                    var qty = wmsInReceiptRecords.Where(x => x.receiptDtlId == t.ID).Sum(x => x.recordQty);
                                    t.putawayQty += qty;
                                    if (t.putawayQty + t.returnQty == t.receiptQty)
                                    {
                                        t.receiptDtlStatus = 90;
                                    }
                                    else if (t.recordQty + t.returnQty == t.receiptQty)
                                    {
                                        t.receiptDtlStatus = 41;
                                    }
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceiptDts);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptDts);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-收货单";
                                var receiptNos = wmsInReceiptDts.Select(x => x.receiptNo).Distinct().ToList();
                                List<WmsInReceipt> wmsInReceipts = await DC.Set<WmsInReceipt>().Where(x => receiptNos.Contains(x.receiptNo)).ToListAsync();
                                wmsInReceipts.ForEach(t =>
                                {
                                    var receiptDtlStatus = wmsInReceiptDts.Where(x => x.receiptNo == t.receiptNo).Min(x => x.receiptDtlStatus);
                                    if (receiptDtlStatus >= 90)
                                    {
                                        t.receiptStatus = 90;
                                    }
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceipts);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceipts);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-入库明细";
                                var inDtlIds = wmsInReceiptRecords.Select(x => x.inDtlId).Distinct().ToList();
                                List<WmsInOrderDtl> wmsInOrderDetails = await DC.Set<WmsInOrderDtl>().Where(x => inDtlIds.Contains(x.ID)).ToListAsync();
                                wmsInOrderDetails.ForEach(t =>
                                {
                                    var qty = wmsInReceiptRecords.Where(x => x.inDtlId == t.ID).Sum(x => x.recordQty);
                                    t.putawayQty += qty;
                                    if (t.putawayQty + t.returnQty == t.inQty)
                                    {
                                        t.inDtlStatus = 90;
                                    }
                                    else if (t.recordQty + t.returnQty == t.inQty)
                                    {
                                        t.inDtlStatus = 41;
                                    }
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInOrderDetails);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInOrderDetails);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-入库单";
                                var inNos = wmsInOrderDetails.Select(x => x.inNo).Distinct().ToList();
                                List<WmsInOrder> wmsInOrders = DC.Set<WmsInOrder>().Where(x => inNos.Contains(x.inNo)).ToList();
                                wmsInOrders.ForEach(t =>
                                {
                                    var inDtlStatus = wmsInOrderDetails.Where(x => x.inNo == t.inNo).Min(x => x.inDtlStatus);
                                    if (inDtlStatus >= 90)
                                    {
                                        t.inStatus = 90;
                                    }
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInOrders);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInOrders);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            else if (cfgDocType.businessCode == "EMPTY_IN")
                            {
                                _vpoint = $"整托上架-入库记录";
                                var stockCodes = wmsPutawayDtls.Select(x => x.stockCode).Distinct().ToList();
                                List<WmsInReceiptRecord> wmsInReceiptRecords = await DC.Set<WmsInReceiptRecord>().Where(x => stockCodes.Contains(x.stockCode)).ToListAsync();
                                wmsInReceiptRecords.ForEach(t =>
                                {
                                    t.ptaBinNo = input.binNo;
                                    t.ptaPalletBarcode = input.palletBarcode;
                                    t.ptaRegionNo = basWBin.regionNo;
                                    t.ptaStockCode = wmsStock.stockCode;
                                    var stockId = wmsPutawayDtls.Where(x => x.recordId == t.ID).Select(x => x.stockDtlId).FirstOrDefault();
                                    t.ptaStockDtlId = stockId;
                                    t.inRecordStatus = 90;
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsInReceiptRecords);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptRecords);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            else if (cfgDocType.businessCode == "MOVE")
                            {
                                _vpoint = $"整托上架-移库单记录";
                                var stockCodes = wmsPutawayDtls.Select(x => x.stockCode).Distinct().ToList();
                                List<WmsItnMoveRecord> wmsItnMoveRecords = await DC.Set<WmsItnMoveRecord>().Where(x => stockCodes.Contains(x.curStockCode)).ToListAsync();
                                wmsItnMoveRecords.ForEach(t =>
                                {
                                    t.curLocationNo = input.binNo;
                                    t.toStockCode = t.curStockCode;
                                    t.toStockDtlId = t.curStockDtlId;
                                    t.toLocationNo = input.binNo;
                                    t.toRegionNo = basWBin.regionNo;
                                    t.toPalletBarcode = input.palletBarcode;
                                    t.moveRecordStatus = 90;
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsItnMoveRecords);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsItnMoveRecords);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-移库单明细";
                                var moveDtlId = wmsItnMoveRecords.Select(x => x.moveDtlId).Distinct().ToList();
                                List<WmsItnMoveDtl> wmsItnMoveDtls = await DC.Set<WmsItnMoveDtl>().Where(x => moveDtlId.Contains(x.ID)).ToListAsync();
                                wmsItnMoveDtls.ForEach(t =>
                                {
                                    var qty = wmsItnMoveRecords.Where(x => x.moveDtlId == t.ID).Sum(x => x.moveQty);
                                    t.moveQty += qty;
                                    t.moveDtlStatus = 90;
                                    t.updateBy = invoker;
                                    t.updateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsItnMoveDtls);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsItnMoveDtls);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                                _vpoint = $"整托上架-移库单";
                                var moveNos = wmsItnMoveDtls.Select(x => x.moveNo).Distinct().ToList();
                                List<WmsItnMove> wmsItnMoves = await DC.Set<WmsItnMove>().Where(x => moveNos.Contains(x.moveNo)).ToListAsync();
                                wmsItnMoves.ForEach(t =>
                                {
                                    var moveDtlStatus = wmsItnMoveDtls.Where(x => x.moveNo == t.moveNo).Min(x => x.moveDtlStatus);
                                    t.moveStatus = moveDtlStatus;
                                    t.UpdateBy = invoker;
                                    t.UpdateTime = DateTime.Now;
                                });
                                //((DbContext)DC).BulkUpdate(wmsItnMoves);
                                //((DbContext)DC).BulkSaveChanges();
                                await ((DbContext)DC).BulkUpdateAsync(wmsItnMoves);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                            #endregion
                        }

                    }

                    _vpoint = $"上架单处理";
                    List<WmsPutawayDtl> wmsPutawayDtlLists = await DC.Set<WmsPutawayDtl>().Where(x => putawayNos.Contains(x.putawayNo)).ToListAsync();
                    var finishPutawayLists = wmsPutawayDtlLists.Where(x => x.putawayDtlStatus < 90).Select(x => x.putawayNo).ToList();
                    if (finishPutawayLists.Any())
                    {
                        wmsPutAwayEntity.putawayStatus = 90;
                    }
                    wmsPutAwayEntity.ptaRegionNo = basWBin.regionNo;
                    wmsPutAwayEntity.UpdateBy = invoker;
                    wmsPutAwayEntity.UpdateTime = DateTime.Now;
                    //((DbContext)DC).BulkUpdate(new WmsPutaway[] { wmsPutAwayEntity });
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkUpdateAsync(new WmsPutaway[] { wmsPutAwayEntity });
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    _vpoint = $"上架单转历史";
                    var his = CommonHelper.Map<WmsPutaway, WmsPutawayHis>(wmsPutAwayEntity, "ID");
                    //DC.AddEntity(his);
                    //((DbContext)DC).BulkInsert(new WmsPutawayHis[] { his });
                    //((DbContext)DC).BulkDelete(new WmsPutaway[] { wmsPutAwayEntity });
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkInsertAsync(new WmsPutawayHis[] { his });
                    await ((DbContext)DC).BulkDeleteAsync(new WmsPutaway[] { wmsPutAwayEntity });
                    await ((DbContext)DC).BulkSaveChangesAsync();
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

                    result.code = ResCode.Error;
                    result.msg = $"WmsPutawayVM->PutAway 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->上架操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,库位号：【{input.binNo}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }

        private async Task<BusinessResult> PutAwayByDtl(PutAwayByDtlDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            #region 校验信息

            #region 获取上架单明细信息
            _vpoint = $"获取上架单明细信息";
            WmsPutawayDtl wmsPutawayDtl = await DC.Set<WmsPutawayDtl>().Where(x => x.ID == input.putawayDtlId).FirstOrDefaultAsync();
            if (wmsPutawayDtl == null)
            {
                result.code = ResCode.Error;
                result.msg = $"根据上架单明细ID{input.putawayDtlId}，查询不到上架单明细信息";
            }
            //if (wmsPutawayDtl.putawayDtlStatus >= 90)
            //{
            //    result.code = ResCode.Error;
            //    result.msg = $"根据上架单明细ID{input.putawayDtlId}，查询到上架单明细信息已完成";
            //}
            #endregion
            #region 获取单据类型
            _vpoint = $"获取单据类型";
            List<CfgDocType> cfgDocTypes = await DC.Set<CfgDocType>().Where(x => x.docTypeCode == wmsPutawayDtl.docTypeCode).ToListAsync();
            if (cfgDocTypes.Count != 1)
            {
                result.code = ResCode.Error;
                result.msg = $"单据类型{wmsPutawayDtl.docTypeCode}，维护信息错误，不存在或多条";
            }
            var cfgDocType = cfgDocTypes.Where(x => !string.IsNullOrWhiteSpace(x.businessCode)).FirstOrDefault();
            if (cfgDocType == null)
            {
                result.code = ResCode.Error;
                result.msg = $"单据类型{wmsPutawayDtl.docTypeCode}，维护信息错误，没有维护业务代码";
            }
            BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == input.binNo && x.binType == "ST").FirstOrDefaultAsync();
            if (basWBin == null)
            {
                throw new Exception($"根据库位号{input.binNo}，查询不到存储库位信息。");
            }
            //CfgDocType_View cfgDocTypeView = cfgDocTypeVM.GetCfgDocType(cfgDocType.docTypeCode);
            //if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
            //{
            //    result.code = ResCode.Error;
            //    result.msg = $"单据类型{cfgDocType.docTypeCode},参数未配置!";
            //}
            //bool isChange = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.IsChange.GetCode()) == YesNoCode.Yes.GetCode();
            //if (isChange == false && wmsPutawayDtl.binNo == input.binNo)
            //{
            //    result.code = ResCode.Error;
            //    result.msg = $"单据类型{wmsPutawayDtl.docTypeCode}，不允许上架变更";
            //}
            #endregion

            #endregion
            #region 逻辑处理
            if (result.code == ResCode.OK)
            {
                try
                {
                    if (cfgDocType.businessCode == "IN")
                    {
                        _vpoint = $"明细上架-入库记录";
                        List<WmsInReceiptRecord> wmsInReceiptRecords = await DC.Set<WmsInReceiptRecord>().Where(x => x.stockCode == wmsPutawayDtl.skuCode).ToListAsync();
                        wmsInReceiptRecords.ForEach(t =>
                        {
                            t.ptaBinNo = input.binNo;
                            t.ptaPalletBarcode = input.palletBarcode;
                            t.ptaRegionNo = basWBin.regionNo;
                            t.ptaStockCode = wmsPutawayDtl.stockCode;
                            t.ptaStockDtlId = wmsPutawayDtl.stockDtlId;
                            t.inRecordStatus = 90;
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceiptRecords);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptRecords);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-入库唯一码";
                        List<WmsInReceiptUniicode> wmsInReceiptUniicodes = await DC.Set<WmsInReceiptUniicode>().Where(x => x.receiptRecordId == wmsPutawayDtl.recordId).ToListAsync();
                        wmsInReceiptUniicodes.ForEach(t =>
                        {
                            t.runiiStatus = 90;
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceiptUniicodes);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptUniicodes);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-质检结果";
                        var iqcResultNos = wmsInReceiptRecords.Select(x => x.iqcResultNo).Distinct().ToList();
                        List<WmsInReceiptIqcResult> wmsInReceiptIqcResults = await DC.Set<WmsInReceiptIqcResult>().Where(x => iqcResultNos.Contains(x.iqcResultNo)).ToListAsync();
                        wmsInReceiptIqcResults.ForEach(t =>
                        {
                            var qty = wmsInReceiptRecords.Where(x => x.iqcResultNo == t.iqcResultNo).Sum(x => x.recordQty);
                            t.putawayQty += qty;
                            if (t.putawayQty == t.qty)
                            {
                                t.iqcResultStatus = 90;
                            }
                            else if (t.recordQty == t.qty)
                            {
                                t.iqcResultStatus = 41;
                            }
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceiptIqcResults);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptIqcResults);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-收货明细";
                        var receiptDtlIds = wmsInReceiptRecords.Select(x => x.receiptDtlId).Distinct().ToList();
                        List<WmsInReceiptDtl> wmsInReceiptDts = await DC.Set<WmsInReceiptDtl>().Where(x => receiptDtlIds.Contains(x.ID)).ToListAsync();
                        wmsInReceiptDts.ForEach(t =>
                        {
                            var qty = wmsInReceiptRecords.Where(x => x.receiptDtlId == t.ID).Sum(x => x.recordQty);
                            t.putawayQty += qty;
                            if (t.putawayQty + t.returnQty == t.receiptQty)
                            {
                                t.receiptDtlStatus = 90;
                            }
                            else if (t.recordQty + t.returnQty == t.receiptQty)
                            {
                                t.receiptDtlStatus = 41;
                            }
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceiptDts);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptDts);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-收货单";
                        var receiptNos = wmsInReceiptDts.Select(x => x.receiptNo).Distinct().ToList();
                        List<WmsInReceipt> wmsInReceipts = await DC.Set<WmsInReceipt>().Where(x => receiptNos.Contains(x.receiptNo)).ToListAsync();
                        wmsInReceipts.ForEach(t =>
                        {
                            var receiptDtlStatus = wmsInReceiptDts.Where(x => x.receiptNo == t.receiptNo).Min(x => x.receiptDtlStatus);
                            if (receiptDtlStatus >= 90)
                            {
                                t.receiptStatus = 90;
                            }
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceipts);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceipts);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-入库明细";
                        var inDtlIds = wmsInReceiptRecords.Select(x => x.inDtlId).Distinct().ToList();
                        List<WmsInOrderDtl> wmsInOrderDetails = await DC.Set<WmsInOrderDtl>().Where(x => inDtlIds.Contains(x.ID)).ToListAsync();
                        wmsInOrderDetails.ForEach(t =>
                        {
                            var qty = wmsInReceiptRecords.Where(x => x.inDtlId == t.ID).Sum(x => x.recordQty);
                            t.putawayQty += qty;
                            if (t.putawayQty + t.returnQty == t.inQty)
                            {
                                t.inDtlStatus = 90;
                            }
                            else if (t.recordQty + t.returnQty == t.inQty)
                            {
                                t.inDtlStatus = 41;
                            }
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInOrderDetails);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInOrderDetails);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-入库单";
                        var inNos = wmsInOrderDetails.Select(x => x.inNo).Distinct().ToList();
                        List<WmsInOrder> wmsInOrders = await DC.Set<WmsInOrder>().Where(x => inNos.Contains(x.inNo)).ToListAsync();
                        wmsInOrders.ForEach(t =>
                        {
                            var inDtlStatus = wmsInOrderDetails.Where(x => x.inNo == t.inNo).Min(x => x.inDtlStatus);
                            if (inDtlStatus >= 90)
                            {
                                t.inStatus = 90;
                            }
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInOrders);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInOrders);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                    else if (cfgDocType.businessCode == "EMPTY_IN")
                    {
                        _vpoint = $"明细上架-入库记录";
                        List<WmsInReceiptRecord> wmsInReceiptRecords = await DC.Set<WmsInReceiptRecord>().Where(x => x.stockCode == wmsPutawayDtl.stockCode).ToListAsync();
                        wmsInReceiptRecords.ForEach(t =>
                        {
                            t.ptaBinNo = input.binNo;
                            t.ptaPalletBarcode = input.palletBarcode;
                            t.ptaRegionNo = basWBin.regionNo;
                            t.ptaStockCode = wmsPutawayDtl.stockCode;
                            t.ptaStockDtlId = wmsPutawayDtl.stockDtlId;
                            t.inRecordStatus = 90;
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsInReceiptRecords);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptRecords);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                    else if (cfgDocType.businessCode == "MOVE")
                    {
                        _vpoint = $"明细上架-移库单记录";
                        List<WmsItnMoveRecord> wmsItnMoveRecords = await DC.Set<WmsItnMoveRecord>().Where(x => x.curStockCode == wmsPutawayDtl.stockCode).ToListAsync();
                        wmsItnMoveRecords.ForEach(t =>
                        {
                            t.curLocationNo = input.binNo;
                            t.toStockCode = t.curStockCode;
                            t.toStockDtlId = t.curStockDtlId;
                            t.toLocationNo = input.binNo;
                            t.toRegionNo = basWBin.regionNo;
                            t.toPalletBarcode = input.palletBarcode;
                            t.moveRecordStatus = 90;
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsItnMoveRecords);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsItnMoveRecords);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-移库单明细";
                        var moveDtlId = wmsItnMoveRecords.Select(x => x.moveDtlId).Distinct().ToList();
                        List<WmsItnMoveDtl> wmsItnMoveDtls = await DC.Set<WmsItnMoveDtl>().Where(x => moveDtlId.Contains(x.ID)).ToListAsync();
                        wmsItnMoveDtls.ForEach(t =>
                        {
                            var qty = wmsItnMoveRecords.Where(x => x.moveDtlId == t.ID).Sum(x => x.moveQty);
                            t.moveQty += qty;
                            t.moveDtlStatus = 90;
                            t.updateBy = invoker;
                            t.updateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsItnMoveDtls);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsItnMoveDtls);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        _vpoint = $"明细上架-移库单";
                        var moveNos = wmsItnMoveDtls.Select(x => x.moveNo).Distinct().ToList();
                        List<WmsItnMove> wmsItnMoves = await DC.Set<WmsItnMove>().Where(x => moveNos.Contains(x.moveNo)).ToListAsync();
                        wmsItnMoves.ForEach(t =>
                        {
                            var moveDtlStatus = wmsItnMoveDtls.Where(x => x.moveNo == t.moveNo).Min(x => x.moveDtlStatus);
                            t.moveStatus = moveDtlStatus;
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //((DbContext)DC).BulkUpdate(wmsItnMoves);
                        //((DbContext)DC).BulkSaveChanges();
                        await ((DbContext)DC).BulkUpdateAsync(wmsItnMoves);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    result.code = ResCode.Error;
                    result.msg = $"WmsPutawayVM->PutAwayByDtl 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            #endregion
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->上架操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,库位号：【{input.binNo}】,上架单明细ID{input.putawayDtlId},入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }


        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<BusinessResult> ParamValid(PutAwayDto input)
        {
            BusinessResult result = new BusinessResult();
            if (input == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参input为空。";
                return result;
            }
            if (string.IsNullOrWhiteSpace(input.binNo))
            {
                result.code = ResCode.Error;
                result.msg = $"入参binNo为空；";
            }
            if (string.IsNullOrWhiteSpace(input.palletBarcode))
            {
                result.code = ResCode.Error;
                result.msg = result.msg + $"入参palletBarcode为空；";
            }
            return result;
        }

        /// <summary>
        /// 自建单上架处理
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> PutAwayByWCS(PutAwayDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWPalletTypeVM basWPalletTypeVM = Wtm.CreateVM<BasWPalletTypeVM>();
            var hasParentTransaction = false;
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            result = await ParamValid(input);
            if (result.code == ResCode.OK)
            {
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
                    #region 入库记录校验
                    WmsInReceiptRecord wmsInReceiptRecord = await DC.Set<WmsInReceiptRecord>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                    if (wmsInReceiptRecord == null)
                    {
                        throw new Exception($"根据托盘号{input.palletBarcode}，查询不到入库记录。");
                    }
                    #endregion

                    #region 库位校验
                    _vpoint = $"库位校验";
                    BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == input.binNo && x.binType == "ST" && x.virtualFlag == 0).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        throw new Exception($"根据库位号{input.binNo}，查询不到存储库位信息。");
                    }

                    _vpoint = $"库存校验";
                    var capacitySize = basWBin.capacitySize ?? 1;
                    //var stockCodeQuery= wmsPutaways.Select(x => x.stockCode).FirstOrDefault();
                    //var wmsStockList = await DC.Set<WmsStock>().Where(x => x.binNo == basWBin.binNo && x.stockCode != wmsPutAwayEntity.stockCode).ToListAsync();
                    var wmsStockList = await DC.Set<WmsStock>().Where(x => x.binNo == basWBin.binNo && x.stockCode != wmsInReceiptRecord.stockCode).ToListAsync();
                    if (capacitySize <= wmsStockList.Count)
                    {
                        throw new Exception($"库位{input.binNo}已达到最大容量{capacitySize}，当前容量{wmsStockList.Count}，不能上架到此库位。");
                    }
                    WmsStock wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == wmsInReceiptRecord.stockCode).FirstOrDefaultAsync();
                    if (wmsStock == null)
                    {
                        throw new Exception($"根据库存编码{wmsInReceiptRecord.stockCode}，查询不到库存主表信息。");
                    }

                    #endregion

                    #region 逻辑处理
                    #region 库存处理
                    _vpoint = $"更新库存信息";

                    wmsStock.palletBarcode = input.palletBarcode;
                    wmsStock.binNo = input.binNo;
                    wmsStock.regionNo = basWBin.regionNo;
                    wmsStock.roadwayNo = basWBin.roadwayNo;
                    wmsStock.stockStatus = 50;
                    wmsStock.UpdateBy = invoker;
                    wmsStock.UpdateTime = DateTime.Now;
                    //((DbContext)DC).BulkUpdate(new WmsStock[] { wmsStock });
                    await ((DbContext)DC).SingleUpdateAsync(wmsStock);

                    _vpoint = $"更新库存明细信息";
                    WmsStockDtl stockDtl = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsStock.stockCode && x.ID == wmsInReceiptRecord.stockDtlId).FirstOrDefaultAsync();
                    if (stockDtl == null)
                    {
                        throw new Exception($"根据库存编码{wmsInReceiptRecord.stockCode}和入库记录的库存明细ID{wmsInReceiptRecord.stockDtlId}，查询不到库存明细信息。");
                    }
                    stockDtl.palletBarcode = input.palletBarcode;
                    stockDtl.stockDtlStatus = 50;
                    stockDtl.UpdateBy = invoker;
                    stockDtl.UpdateTime = DateTime.Now;

                    //((DbContext)DC).BulkUpdate(wmsStockDtls);
                    await ((DbContext)DC).SingleUpdateAsync(stockDtl);
                    WmsStockUniicode wmsStockUniicode = await DC.Set<WmsStockUniicode>().Where(x => x.stockDtlId== stockDtl.ID && x.stockCode == wmsStock.stockCode).FirstOrDefaultAsync();
                    if (wmsStockUniicode == null)
                    {
                        throw new Exception($"根据库存编码{wmsStock.stockCode}和库存明细ID{wmsInReceiptRecord.stockDtlId}，查询不到库存唯一码信息。");
                    }
                    _vpoint = $"新增库存调整记录";
                    WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                    wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                    wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                    wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                    wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                    wmsStockAdjust.adjustType = "上线"; // 调整类型;新增、修改、删除
                    wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                    wmsStockAdjust.adjustDesc = "更新库存状态更新为在库！上架库位：" + input.binNo;
                    wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                    wmsStockAdjust.CreateBy = invoker;
                    wmsStockAdjust.CreateTime = DateTime.Now;
                    //((DbContext)DC).BulkInsert(new WmsStockAdjust[] { wmsStockAdjust });
                    await ((DbContext)DC).SingleInsertAsync( wmsStockAdjust );
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    #endregion

                    #region 入库记录单处理
                    _vpoint = $"入库记录单转历史";
                    WmsInReceiptRecordHis his = CommonHelper.Map<WmsInReceiptRecord, WmsInReceiptRecordHis>(wmsInReceiptRecord, "ID");
                    his.inRecordStatus = 90;
                    his.UpdateBy = invoker;
                    his.UpdateTime = DateTime.Now;
                    #endregion
                    await ((DbContext)DC).SingleInsertAsync(his);
                    await ((DbContext)DC).SingleDeleteAsync(wmsInReceiptRecord);
                    //((DbContext)DC).BulkSaveChanges();
                    await ((DbContext)DC).BulkSaveChangesAsync();
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

                    result.code = ResCode.Error;
                    result.msg = $"WmsPutawayVM->PutAway 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->上架操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,库位号：【{input.binNo}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }
    }
}
