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
using Quartz.Util;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using WISH.Helper.Common;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using Wish.ViewModel.BusinessPutdown.WmsPutdownVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Com.Wish.Model.Base;
using Wish.Areas.Config.Model;

namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        /// <summary>
        /// 根据唯一码分配
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> OutAllocateForUniiByHand(ManualAllotInDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();

            #region 校验

            if (input == null)
            {
                return result.Error($"提交数据为空");
            }

            if (input.StockDtl == null || input.StockDtl.Count == 0)
            {
                return result.Error($"提交数据中库存集合为空");
            }

            if (input.StockDtl.Count(t => t.ID == null) > 0)
            {
                return result.Error($"提交数据中库存集合存在库存id为空的记录");
            }

            if (input.StockDtl.Count(t => t.qty <= 0) > 0)
            {
                return result.Error($"提交数据中库存集合存在库存数量不大于0的记录");
            }

            int dtlCount = input.StockDtl.GroupBy(t => new { t.ID }).Where(t => t.Count() > 1).Count();
            if (dtlCount > 0)
            {
                return result.Error($"提交数据中库存集合存在重复数据");
            }

            var invoiceDtlInfo = await DC.Set<WmsOutInvoiceDtl>().Where(t => t.ID == input.ID).FirstOrDefaultAsync();
            if (invoiceDtlInfo == null)
            {
                return result.Error($"未找到待分配的发货单明细");
            }

            if (invoiceDtlInfo.erpUndeliverQty == null || invoiceDtlInfo.erpUndeliverQty <= 0)
            {
                return result.Error($"待分配的发货单明细ERP未发数量为0，无法继续分配");
            }

            decimal allotQty = (decimal)(invoiceDtlInfo.erpUndeliverQty - (invoiceDtlInfo.allotQty - invoiceDtlInfo.completeQty));
            if (allotQty <= 0)
            {
                return result.Error($"待分配的发货单明细数量已达上限【{allotQty}】，无法继续分配");
            }


            if (Convert.ToInt32(invoiceDtlInfo.invoiceDtlStatus) > 29)
            {
                return result.Error($"待分配的发货单明细状态不是可分配状态【{invoiceDtlInfo.invoiceDtlStatus}】，无法继续分配");
            }

            var invoiceInfo = await DC.Set<WmsOutInvoice>().Where(t => t.invoiceNo == invoiceDtlInfo.invoiceNo).FirstOrDefaultAsync();
            if (invoiceInfo == null)
            {
                return result.Error($"未找到待分配发货单明细所属单据【{invoiceDtlInfo.invoiceNo}】");
            }

            if (Convert.ToInt32(invoiceInfo.invoiceStatus) >= 90)
            {
                return result.Error($"待分配发货单明细所属单据【{invoiceDtlInfo.invoiceNo}】已完成或已关闭");
            }

            if (string.IsNullOrWhiteSpace(invoiceInfo.docTypeCode))
            {
                return result.Error($"待分配发货单明细所属单据【{invoiceDtlInfo.invoiceNo}】单据类型为空");
            }

            var matInfo = await DC.Set<BasBMaterial>().Where(t => t.MaterialCode == invoiceDtlInfo.materialCode).AsNoTracking().FirstOrDefaultAsync();
            if (matInfo == null)
            {
                return result.Error($"待分配发货单明细对应物料【{invoiceDtlInfo.materialCode}】不存在");
            }

            var matCateInfo = await DC.Set<BasBMaterialCategory>().Where(t => t.materialCategoryCode == matInfo.MaterialCategoryCode).AsNoTracking().FirstOrDefaultAsync();
            if (matCateInfo == null)
            {
                return result.Error($"待分配发货单明细对应物料【{invoiceDtlInfo.materialCode}】的物料大类【{matCateInfo.materialCategoryCode}】不存在");
            }

            bool isElecFlag = false;
            if (matCateInfo.materialFlag == MaterialFlag.Electronic.GetCode())
            {
                isElecFlag = true;
            }

            #endregion

            #region 入参

            var qty = input.StockDtl.Where(t => t.isPick == false).Sum(t => t.qty);
            if (qty > allotQty && input.StockDtl.Count > 1)
            {
                return result.Error($"提交数据中非拣选库存集合汇总数量【{qty}】大于单据明细待分配数量【{allotQty}】，请返回页面勾选库存作为拣选库存，确保数量");
            }

            if (input.StockDtl.Count(t => t.isPick == true) > 1)
            {
                return result.Error($"提交数据中拣选库存只能勾选一条");
            }

            var pickStock = input.StockDtl.FirstOrDefault(t => t.isPick == true);
            if (pickStock != null)
            {
                if (pickStock.qty >= allotQty)
                {
                    var nopickStock = input.StockDtl.FirstOrDefault(t => t.isPick == false);
                    if (nopickStock != null)
                    {
                        return result.Error($"提交数据中拣选库存数量【{pickStock.qty}】满足单据明细待分配数量【{allotQty}】,无需选择其他库存，请返回页面取消其他库存，确保数量");
                    }
                }
            }

            #endregion

            #region 库存

            var stockUniiIdList = input.StockDtl.Select(t => t.ID).ToList();
            var stockUniiInfos = await DC.Set<WmsStockUniicode>().Where(t => stockUniiIdList.Contains(t.ID) && t.qty > t.occupyQty && t.delayFrozenFlag != 1 && t.exposeFrozenFlag != 1 && t.driedScrapFlag != 1).ToListAsync();
            if (stockUniiInfos.Count == 0)
            {
                return result.Error($"未找到可用包装条码或者SN号");
            }

            var stockDtlIdList = stockUniiInfos.Select(t => t.stockDtlId).ToList();
            var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => stockDtlIdList.Contains(t.ID) && t.stockDtlStatus == 50 && t.lockFlag == 0).ToListAsync();
            if (stockDtlInfos.Count == 0)
            {
                return result.Error($"未找到可用库存明细");
            }

            stockDtlIdList = stockDtlInfos.Select(t => t.ID).ToList();
            stockUniiInfos = stockUniiInfos.Where(t => stockDtlIdList.Contains(t.stockDtlId)).ToList();
            List<allotStockUniiDto> allotStockViewList = new List<allotStockUniiDto>();
            if (stockUniiInfos.Count > 0)
            {
                decimal needQty = allotQty;
                input.StockDtl = input.StockDtl.OrderBy(t => t.isPick).ToList();
                foreach (var uniiInput in input.StockDtl)
                {
                    var uniiInfo = stockUniiInfos.FirstOrDefault(t => t.ID == uniiInput.ID);
                    if (uniiInfo != null)
                    {
                        var dtlInfo = stockDtlInfos.FirstOrDefault(t => t.ID == uniiInfo.stockDtlId);
                        if (dtlInfo != null)
                        {
                            if (needQty >= uniiInfo.qty - uniiInfo.occupyQty)
                            {
                                allotStockUniiDto allotStockDtl = new allotStockUniiDto()
                                {
                                    uniicode = uniiInfo.uniicode,
                                    allotQty = uniiInfo.qty.Value - uniiInfo.occupyQty.Value,
                                    stockDtlId = uniiInfo.stockDtlId,
                                    uniiId = uniiInfo.ID,
                                };
                                allotStockViewList.Add(allotStockDtl);
                                needQty = needQty - (uniiInfo.qty.Value - uniiInfo.occupyQty.Value);
                            }
                            else
                            {
                                allotStockUniiDto allotStockDtl = new allotStockUniiDto()
                                {
                                    allotQty = needQty,
                                    uniiId = uniiInfo.ID,
                                    uniicode = uniiInfo.uniicode,
                                    stockDtlId = uniiInfo.stockDtlId,
                                };
                                allotStockViewList.Add(allotStockDtl);
                                needQty = 0;
                            }
                        }


                        if (needQty <= 0)
                        {
                            break;
                        }
                    }
                }
            }

            if (allotStockViewList.Count > 0)
            {
                List<allotStockDtlDto> allotStockDtlViews = new List<allotStockDtlDto>();
                var group = allotStockViewList.GroupBy(t => t.stockDtlId);
                foreach (var item in group)
                {
                    allotStockDtlDto dtlView = new allotStockDtlDto()
                    {
                        stockDtlId = item.Key,
                        allotStockUniiList = item.ToList()
                    };
                    allotStockDtlViews.Add(dtlView);
                }

                var regionInfos = await DC.Set<BasWRegion>().AsNoTracking().ToListAsync();
                result = await allotInvoiceForUnii(allotStockDtlViews, allotStockViewList, invoiceInfo, invoiceDtlInfo, regionInfos, invoker, isElecFlag);
            }

            #endregion

            return result;
        }



        /// <summary>
        /// 事务处理--按包装条码分配
        /// </summary>
        /// <returns></returns>
        private async Task<BusinessResult> allotInvoiceForUnii(List<allotStockDtlDto> allotStockDtlViews, List<allotStockUniiDto> allotStockViewList, WmsOutInvoice outInvoice, WmsOutInvoiceDtl outInvoiceDtl, List<BasWRegion> regionInfos, string invoker, bool isElecFlag)
        {
            BusinessResult result = new BusinessResult();
            int allotDtlCount = 0;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
            WmsPutdownVM wmsPutdownVM = Wtm.CreateVM<WmsPutdownVM>();
            var hasParentTransaction = false;
            string msg = string.Empty;
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

                List<WmsOutInvoiceRecord> addwmsOutInvoiceRecords = new List<WmsOutInvoiceRecord>();
                List<WmsOutInvoiceUniicode> addwmsOutInvoiceUniicodes = new List<WmsOutInvoiceUniicode>();
                var stockUniiids = allotStockViewList.Select(t => t.uniiId).ToList();
                var stockUniiInfos = await DC.Set<WmsStockUniicode>().Where(t => stockUniiids.Contains(t.ID)).ToListAsync();
                var stockDltids = allotStockDtlViews.Select(t => t.stockDtlId).ToList();
                var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => stockDltids.Contains(t.ID)).ToListAsync();
                var stockCodes = stockDtlInfos.Select(t => t.stockCode).ToList();
                var stockInfos = await DC.Set<WmsStock>().Where(t => stockCodes.Contains(t.stockCode)).ToListAsync();
               
                List<WmsPutdown> addwmsPutdowns = new List<WmsPutdown>();
                List<WmsPutdownDtl> addwmsPutdownDtls = new List<WmsPutdownDtl>();
                List<Int64> changeStockDtlIdList = new List<Int64>();
                List<string> changeStockcodeList = new List<string>();
                string docTypeCode = string.Empty;
                string orderNo = string.Empty;
                List<string> pickupList = new List<string>() { "PDA", "PTL" };


                WmsOutWave wmsOutWave = null;
                if (outInvoiceDtl != null)
                {
                    var needQty = outInvoiceDtl.erpUndeliverQty - (outInvoiceDtl.allotQty - outInvoiceDtl.completeQty);
                    var isBatch = outInvoiceDtl.batchNo.IsNullOrWhiteSpace() ? false : true;
                    decimal realAllotQty = 0;
                    foreach (var allotStock in allotStockDtlViews)
                    {
                        var stockDtl = stockDtlInfos.FirstOrDefault(t => t.ID == allotStock.stockDtlId && (t.qty > t.occupyQty) && t.stockDtlStatus == 50 && t.lockFlag == 0);
                        if (stockDtl != null)
                        {
                            var stock = stockInfos.FirstOrDefault(t => t.stockCode == stockDtl.stockCode);
                            var region = regionInfos.FirstOrDefault(t => t.regionNo == stock.regionNo);
                            if (region != null && region.pickupMethod == "PTL" )
                            {
                                continue;
                            }

                            string pickTaskNo = sysSequenceVM.GetSequence(SequenceCode.PickNo.GetCode());
                            decimal dtlAllotQty = 0;
                            foreach (var item in allotStock.allotStockUniiList)
                            {
                                var stockUnii = stockUniiInfos.Where(t => t.ID == item.uniiId && t.qty > t.occupyQty).FirstOrDefault();
                                decimal dtlCanUseQty = stockDtl.qty.Value - stockDtl.occupyQty.Value;
                                if (stockUnii != null)
                                {
                                    if (dtlCanUseQty >= (stockUnii.qty - stockUnii.occupyQty))
                                    {
                                        if (needQty - realAllotQty >= ((stockUnii.qty - stockUnii.occupyQty)))
                                        {
                                            item.allotQty = (stockUnii.qty.Value - stockUnii.occupyQty.Value);
                                            item.isCanAllot = true;
                                            // needQty = needQty - (stockUnii.stockQty - stockUnii.occupyQty);
                                            dtlAllotQty = dtlAllotQty + item.allotQty;
                                            realAllotQty = realAllotQty + item.allotQty;

                                            #region 库存

                                            stockDtl.occupyQty += item.allotQty;
                                            stockUnii.occupyQty = stockUnii.qty;

                                            #endregion
                                        }
                                        else
                                        {
                                            item.allotQty = needQty.Value - realAllotQty;
                                            item.isCanAllot = true;
                                            // needQty = needQty - (stockUnii.stockQty - stockUnii.occupyQty);
                                            dtlAllotQty = dtlAllotQty + item.allotQty;
                                            realAllotQty = realAllotQty + item.allotQty;

                                            #region 库存

                                            stockDtl.occupyQty += item.allotQty;
                                            stockUnii.occupyQty += item.allotQty;

                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        if (needQty - realAllotQty >= dtlCanUseQty)
                                        {
                                            item.allotQty = dtlCanUseQty;
                                            item.isCanAllot = true;
                                            // needQty = needQty - (stockUnii.stockQty - stockUnii.occupyQty);
                                            dtlAllotQty = dtlAllotQty + item.allotQty;
                                            realAllotQty = realAllotQty + item.allotQty;

                                            #region 库存

                                            stockUnii.occupyQty += item.allotQty;
                                            stockDtl.occupyQty += item.allotQty;

                                            #endregion
                                        }
                                        else
                                        {
                                            item.allotQty = needQty.Value - realAllotQty;
                                            item.isCanAllot = true;
                                            // needQty = needQty - (stockUnii.stockQty - stockUnii.occupyQty);
                                            dtlAllotQty = dtlAllotQty + item.allotQty;
                                            realAllotQty = realAllotQty + item.allotQty;

                                            #region 库存

                                            stockUnii.occupyQty += item.allotQty;
                                            stockDtl.occupyQty += item.allotQty;

                                            #endregion
                                        }
                                    }
                                }

                                if (realAllotQty >= needQty)
                                {
                                    break;
                                }
                            }

                            #region 数据处理

                            if (dtlAllotQty > 0)
                            {
                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【stockDtl.batchNo】,分配数量【{dtlAllotQty}】";
                                logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");

                                bool isPk = true;
                                if (region != null && !pickupList.Contains(region.pickupMethod))
                                {
                                    isPk = false;
                                }
                                var cfgRelationShip = await DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Roadway&Station" && x.leftCode == stock.roadwayNo).FirstOrDefaultAsync();
                                if (cfgRelationShip == null) 
                                {
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:根据库存主表中的巷道【{stock.roadwayNo}】，查询不到对应站台关系";
                                    result.code = ResCode.Error;
                                    result.msg = msg;
                                    logger.Warn($"----->info----->手动分配-按唯一码:{msg} ");
                                    return result;
                                }
                                //生成出库记录
                                WmsOutInvoiceRecord wmsOutInvoiceRecord =
                                    wmsPutdownVM.BuildOutInvoiceRecord(
                                        outInvoice,
                                        outInvoiceDtl,
                                        stock,
                                        stockDtl,
                                        pickTaskNo,
                                        AllotType.Manu.GetCode(),
                                        isBatch,
                                        "1",
                                        "0",
                                        invoker,
                                        dtlAllotQty,
                                        cfgRelationShip.rightCode,
                                        isPk);



                                addwmsOutInvoiceRecords.Add(wmsOutInvoiceRecord);
                                var allotUniiLsit = allotStock.allotStockUniiList.Where(t => t.isCanAllot).ToList();
                                foreach (var item in allotUniiLsit)
                                {
                                    var stockUnii = stockUniiInfos.Where(t => t.ID == item.uniiId).FirstOrDefault();
                                    if (stockUnii != null)
                                    {
                                        msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存唯一码【{stockUnii.uniicode}】,批次【{stockUnii.batchNo}】,分配数量【{item.allotQty}】";
                                        logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");
                                        //出库唯一码
                                        WmsOutInvoiceUniicode wmsOutReceiptUniicode = BuildOutReceiptUniicodeInfo(wmsOutInvoiceRecord, stockUnii, item.allotQty, invoker);
                                        addwmsOutInvoiceUniicodes.Add(wmsOutReceiptUniicode);
                                    }
                                }

                                if (!changeStockDtlIdList.Contains(stockDtl.ID))
                                {
                                    changeStockDtlIdList.Add(stockDtl.ID);
                                }

                                if (!changeStockcodeList.Contains(stockDtl.stockCode))
                                {
                                    changeStockcodeList.Add(stockDtl.stockCode);
                                }

                                if (string.IsNullOrWhiteSpace(docTypeCode))
                                {
                                    docTypeCode = outInvoice.docTypeCode;
                                }

                                if (string.IsNullOrWhiteSpace(orderNo))
                                {
                                    orderNo = outInvoice.invoiceNo;
                                }
                            }

                            #endregion
                        }

                        if (realAllotQty >= needQty)
                        {
                            break;
                        }
                    }

                    if (realAllotQty > 0)
                    {
                        allotDtlCount++;
                    }

                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，已分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】,本次需求数量【{needQty}】、实际分配数量【{realAllotQty}】";
                    logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");
                    outInvoiceDtl.allotQty = outInvoiceDtl.allotQty + realAllotQty;
                    outInvoiceDtl.UpdateBy = invoker;
                    outInvoiceDtl.UpdateTime = DateTime.Now;
                    if (outInvoiceDtl.allotQty > 0)
                    {
                        if (outInvoiceDtl.erpUndeliverQty > outInvoiceDtl.allotQty - outInvoiceDtl.completeQty)
                        {
                            outInvoiceDtl.invoiceDtlStatus = 21;
                            outInvoiceDtl.allocatResult = $"分配中,当前分配数量：{realAllotQty},需求数量：{needQty}";

                            outInvoice.invoiceStatus = 41;

                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量： {realAllotQty} ,需求数量： {needQty}";
                            logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");
                        }
                        else
                        {
                            outInvoiceDtl.invoiceDtlStatus = 29;
                            outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                            outInvoice.invoiceStatus = 41;

                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量： {realAllotQty} ,需求数量： {needQty}";
                            logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");
                        }

                    }
                    else
                    {
                        outInvoiceDtl.invoiceDtlStatus = 0;
                        outInvoiceDtl.allocatResult = "未分配";
                    }

                    outInvoice.UpdateBy = invoker;
                    outInvoice.UpdateTime = DateTime.Now;
                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】";
                    logger.Warn($"----->Warn----->手动分配--按唯一码:{msg} ");

                    if (!string.IsNullOrWhiteSpace(outInvoiceDtl.waveNo))
                    {
                        wmsOutWave = await DC.Set<WmsOutWave>().Where(t => t.waveNo == outInvoiceDtl.waveNo).FirstOrDefaultAsync();
                        if (wmsOutWave != null)
                        {
                            if (outInvoice.invoiceStatus == 41)
                            {
                                wmsOutWave.waveStatus = 41;
                                wmsOutWave.UpdateBy = invoker;
                                wmsOutWave.UpdateTime = DateTime.Now;
                            }
                        }
                    }
                }


                //todo:下架单

                #region 下架单和下架单明细

                //找变化的明细

                if (changeStockcodeList.Count > 0)
                {
                    foreach (var stock in changeStockcodeList)
                    {
                        var wmsStock = stockInfos.FirstOrDefault(t => t.stockCode == stock);
                        if (wmsStock != null)
                        {
                            var dtlList = stockDtlInfos.Where(t => t.stockCode == stock && changeStockDtlIdList.Contains(t.ID)).ToList();
                            var region = regionInfos.FirstOrDefault(t => t.regionNo == wmsStock.regionNo);
                            if (region != null)
                            {
                                //平库
                                if (pickupList.Contains(region.pickupMethod))
                                {
                                    if (dtlList.Count > 0)
                                    {
                                        string putdownNo = sysSequenceVM.GetSequence(SequenceCode.WmsPutdownNo.GetCode());
                                        WmsPutdown wmsPutDown = wmsPutdownVM.BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                        wmsPutDown.putdownStatus = 90;
                                        dtlList.ForEach(t =>
                                        {
                                            var downDtl = wmsPutdownVM.BuildWmsPutDownDtl(t, putdownNo, invoker);
                                            downDtl.putdownDtlStatus = 90;
                                            addwmsPutdownDtls.Add(downDtl);
                                        });
                                        addwmsPutdowns.Add(wmsPutDown);
                                    }
                                }
                                //立库
                                else
                                {
                                    var putdownInfo = await ((DbContext)DC).Set<WmsPutdown>().Where(t => t.palletBarcode == wmsStock.palletBarcode && t.stockCode == wmsStock.stockCode && t.putdownStatus < 90).FirstOrDefaultAsync();
                                    if (putdownInfo != null)
                                    {
                                        int putdownStatus = Convert.ToInt32(putdownInfo.putdownStatus);
                                        if (putdownStatus == 0)
                                        {
                                            var putdownDtlInfos = await ((DbContext)DC).Set<WmsPutawayDtl>().Where(t => t.putawayNo == putdownInfo.putdownNo && t.palletBarcode == wmsStock.palletBarcode && t.stockCode == wmsStock.stockCode).ToListAsync();
                                            dtlList.ForEach(t =>
                                            {
                                                var existPutdownDtl = putdownDtlInfos.FirstOrDefault(x => x.stockDtlId == t.ID);
                                                if (existPutdownDtl != null)
                                                {
                                                }
                                                else
                                                {
                                                    var downDtl = wmsPutdownVM.BuildWmsPutDownDtl(t, putdownInfo.putdownNo, invoker);
                                                    downDtl.putdownDtlStatus = 0;
                                                    addwmsPutdownDtls.Add(downDtl);
                                                }
                                            });
                                        }
                                        else if (putdownStatus > 0 && putdownStatus < 90)
                                        {
                                            if (hasParentTransaction == false)
                                            {
                                                if (DC.Database.CurrentTransaction != null)
                                                    await DC.Database.RollbackTransactionAsync();
                                            }

                                            result.code = ResCode.Error;
                                            result.msg = $"托盘【{wmsStock.palletBarcode}】对应下架单【{putdownInfo.putdownNo}】不是初始创建";
                                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,{result.msg}";
                                            logger.Warn($"----->info----->手动分配-按托异常:{msg} ");
                                            return result;
                                        }
                                       
                                    }
                                    else
                                    {
                                        string putdownNo = sysSequenceVM.GetSequence(SequenceCode.WmsPutdownNo.GetCode());
                                        WmsPutdown wmsPutDown = wmsPutdownVM.BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                        wmsPutDown.putdownStatus = 0;
                                        dtlList.ForEach(t =>
                                        {
                                            var downDtl = wmsPutdownVM.BuildWmsPutDownDtl(t, putdownNo, invoker);
                                            downDtl.putdownDtlStatus = 0;
                                            addwmsPutdownDtls.Add(downDtl);
                                        });
                                        addwmsPutdowns.Add(wmsPutDown);
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                await ((DbContext)DC).BulkUpdateAsync(stockDtlInfos);
                // DC.Set<WmsStockDtl>().UpdateRange(stockDtlInfos);
                // DC.SaveChanges();

                await ((DbContext)DC).BulkUpdateAsync(stockUniiInfos);
                //((DbContext)DC).BulkUpdate(new WmsOutInvoiceDtl[] { outInvoiceDtl });
                //((DbContext)DC).BulkUpdate(new WmsOutInvoice[] { outInvoice });
                await ((DbContext)DC).SingleUpdateAsync(outInvoiceDtl);
                await ((DbContext)DC).SingleUpdateAsync(outInvoice);

                if (addwmsOutInvoiceRecords.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsOutInvoiceRecords);
                if (addwmsOutInvoiceUniicodes.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsOutInvoiceUniicodes);

                if (wmsOutWave != null)
                {
                    //((DbContext)DC).BulkUpdate(new WmsOutWave[] { wmsOutWave });
                    await ((DbContext)DC).SingleUpdateAsync(wmsOutWave);
                }

                if (addwmsPutdowns.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsPutdowns);
                if (addwmsPutdownDtls.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsPutdownDtls);

                //((DbContext)DC).BulkSaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync(t => t.BatchSize = 2000);
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                        await DC.Database.CommitTransactionAsync();
                }

                result.msg = "分配结束";
                result.outParams = allotDtlCount;
            }
            catch (Exception e)
            {
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                        await DC.Database.RollbackTransactionAsync();
                }

                result.code = ResCode.Error;
                result.msg = $"异常信息: [ {e.Message} ]";
            }

            return result;
        }

    }
}
