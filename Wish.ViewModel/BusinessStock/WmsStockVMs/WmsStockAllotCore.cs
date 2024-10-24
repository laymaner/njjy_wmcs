using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Wish.Model.System;
using Com.Wish.Model.Base;
using Microsoft.EntityFrameworkCore;
using Quartz.Util;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using Wish.ViewModel.BusinessPutdown.WmsPutdownVMs;

namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        public List<BasWWhouse> whouseEntities = new List<BasWWhouse>();
        public List<BasWArea> areaEntities = new List<BasWArea>();
        public List<BasWRegion> regionEntities = new List<BasWRegion>();
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public List<BasWRoadway> roadwayEntities = new List<BasWRoadway>();
        public List<BasWBin> binEntities = new List<BasWBin>();
        public List<BasBProprietor> proEntities = new List<BasBProprietor>();
        public List<BasWErpWhouse> erpWhouseEntities = new List<BasWErpWhouse>();
        public List<BasBMaterial> matEntities = new List<BasBMaterial>();
        public List<BasBMaterialType> matTypeEntities = new List<BasBMaterialType>();
        public List<BasBMaterialCategory> matCategoryEntities = new List<BasBMaterialCategory>();
        public List<BasBUnit> unitEntities = new List<BasBUnit>();

        #region 仓库优先级

        private List<CfgDepartmentErpWhouse> GetDepartmentErpWhouses(List<CfgDepartmentErpWhouse> allWhouses, string departmentCode = "默认", string docTypeCode = "2014")
        {
            List<CfgDepartmentErpWhouse> result = allWhouses.Where(t => t.departmentCode == departmentCode && t.docTypeCode == docTypeCode).ToList();
            return result;
        }

        private async Task<List<CfgDepartmentErpWhouse>> GetDepartmentErpWhouses(string departmentCode = "默认", string docTypeCode = "2014")
        {
            List<CfgDepartmentErpWhouse> result = await DC.Set<CfgDepartmentErpWhouse>().Where(t => t.departmentCode == departmentCode && t.docTypeCode == docTypeCode).AsNoTracking().ToListAsync();
            return result;
        }

        #endregion

        #region 手动分配按托查询

        /// <summary>
        /// 按拖查询库存
        /// </summary>
        /// <param name="input"></param>
        public async Task<BusinessResult> GatAvailableStockForPallet(GetStockByAllotDto input)
        {
            BusinessResult result = new BusinessResult();

            #region 校验

            var invoiceDtlInfo = DC.Set<WmsOutInvoiceDtl>().Where(t => t.ID == input.invoiceDtlId).FirstOrDefault();
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

            #endregion

            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
            CfgDocTypeDtl docTypeDtl = await cfgDocTypeVM.GetDocTypeDtlAsync(invoiceInfo.docTypeCode, "IS_DESIGNATE_ERPWHOUSE"); //是否指定ERP仓库
            var isDesignateErpWhouse = "1";
            if (docTypeDtl != null)
            {
                isDesignateErpWhouse = docTypeDtl.paramValueCode;
            }

            bool isLimitSupplyBin = false;
            docTypeDtl = await cfgDocTypeVM.GetDocTypeDtlAsync(invoiceInfo.docTypeCode, "ISLIMIT_SUPPLYBIN"); // 是否卡控供应商库位
            if (docTypeDtl != null)
            {
                isLimitSupplyBin = docTypeDtl.paramValueCode == "1" ? true : false;
            }

            //汇总数量
            bool isElecFlag = false; //电子料标识
            bool isProductFlag = false; //成品
            List<string> supplyBinList = new List<string>();
            List<string> stockCodeList = new List<string>();
            string materailName = matInfo.MaterialName;
            if (matCateInfo.materialFlag == MaterialFlag.Electronic.GetCode())
            {
                isElecFlag = true;
            }
            else if (matCateInfo.materialFlag == MaterialFlag.Product.GetCode())
            {
                isProductFlag = true;
            }

            List<WmsStock> wmsStockInfos = new List<WmsStock>();

            List<string> docTypeList = new List<string>()
            {
                BusinessCode.OutOutSourcePick.GetCode(),
                BusinessCode.OutTransferRequest.GetCode(),
            };

            if (isLimitSupplyBin)
            {
                //if (isElecFlag)
                //{
                if (invoiceDtlInfo.erpWhouseNo == "01B" && !string.IsNullOrWhiteSpace(invoiceDtlInfo.supplierCode))
                {
                    List<BasBSupplierBin> supplyBinInfos = await DC.Set<BasBSupplierBin>().Where(t => t.supplierCode == invoiceDtlInfo.supplierCode).ToListAsync();
                    supplyBinList = supplyBinInfos.Select(t => t.binNo).Distinct().ToList();
                }

                //}
                var stockQuery = DC.Set<WmsStock>().Where(t => t.stockStatus == 50 && t.errFlag == 0)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.palletBarcode), t => (t.palletBarcode.Contains(input.palletBarcode) || t.binNo.Contains(input.palletBarcode)))
                    .WhereIf(supplyBinList.Count > 0, t => supplyBinList.Contains(t.binNo));
                wmsStockInfos = await stockQuery.ToListAsync();
            }
            else
            {
                supplyBinList = await GetSupplyBinList();
                var stockQuery = DC.Set<WmsStock>().Where(t => t.stockStatus == 50 && t.errFlag == 0)
                    .Where(t => !supplyBinList.Contains(t.binNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.palletBarcode), t => (t.palletBarcode.Contains(input.palletBarcode) || t.binNo.Contains(input.palletBarcode)));
                wmsStockInfos = await stockQuery.ToListAsync();
            }


            if (wmsStockInfos.Any())
            {
                stockCodeList = wmsStockInfos.Select(t => t.stockCode).Distinct().ToList();
                WmsStockAllocateForHandDto allotView = new WmsStockAllocateForHandDto()
                {
                    materialCode = invoiceDtlInfo.materialCode,
                    erpWhouseNo = invoiceDtlInfo.erpWhouseNo,
                    docBatchNo = invoiceDtlInfo.batchNo,
                    docTypeCode = invoiceInfo.docTypeCode,
                    isDesignateErpWhouse = isDesignateErpWhouse,
                    projectNo = invoiceDtlInfo.projectNo,
                    stockCodeList = stockCodeList,
                    belongDepartment = invoiceDtlInfo.belongDepartment,
                    batchNo = input.batchNo,
                    isProductFlag = isProductFlag,
                    isElecFlag = isElecFlag
                };
                if (isProductFlag && !string.IsNullOrWhiteSpace(invoiceDtlInfo.originalSn))
                {
                    allotView.snList = invoiceDtlInfo.originalSn.Split(',').ToList();
                }

                var stockDtls = await GetStockDtlsForPallet(allotView, input);

                #region 电子料

                if (stockDtls.Any())
                {
                    if (isElecFlag)
                    {
                        var dtlList = stockDtls.Select(t => t.ID).ToList();
                        List<Int64> filterDtlList = new List<Int64>();
                        var uniiInfos = await DC.Set<WmsStockUniicode>().Where(t => dtlList.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).ToListAsync();
                        filterDtlList = uniiInfos.Select(t => t.stockDtlId).ToList();
                        stockDtls = stockDtls.Where(t => filterDtlList.Contains(t.ID)).ToList();
                    }
                }

                #endregion

                if (stockDtls.Any())
                {
                    result.outParams = await GetStockDtlsForFill(stockDtls, wmsStockInfos, input, materailName);
                }
                else
                {
                    return result.Error($"待分配的发货单明细未找到可用在库库存");
                }
            }
            else
            {
                return result.Error($"待分配的发货单明细未找到可用在库库存");
            }

            return result;
        }

        
        #endregion


        


        #region 手动分配提交

        public async Task<BusinessResult> OutAllocateByHand(ManualAllotInDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            if (input.allotType == "0")
            {
                //按托分配
                result = await OutAllocateForPalletByHand(input, invoker);
            }
            else if (input.allotType == "1")
            {
                //按唯一码分配
                result = await OutAllocateForUniiByHand(input, invoker);
            }

            return result;
        }

        public async Task<List<string>> GetSupplyBinList()
        {
            List<string> result = new List<string>();
            var supplyErpBinInfos = await DC.Set<BasBSupplierBin>().AsNoTracking().ToListAsync();
            result = supplyErpBinInfos.Select(t => t.binNo).Distinct().ToList();
            return result;
        }

        public async Task<BusinessResult> OutAllocateForPalletByHand(ManualAllotInDto input, string invoker)
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

            if (input.StockDtl.Count(t => t.ID==null) > 0)
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


            if (invoiceDtlInfo.invoiceDtlStatus > 29)
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

            var stockDtlIdList = input.StockDtl.Select(t => t.ID).ToList();
            var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => stockDtlIdList.Contains(t.ID) && t.stockDtlStatus == 50 && t.lockFlag == 0 && t.qty > t.occupyQty).ToListAsync();
            if (stockDtlInfos.Count == 0)
            {
                return result.Error($"未找到可用库存");
            }

            List<allotStockDto> allotStockViewList = new List<allotStockDto>();
            decimal needQty = allotQty;
            input.StockDtl = input.StockDtl.OrderBy(t => t.isPick).ToList();

            foreach (var dtlInput in input.StockDtl)
            {
                var dtlInfo = stockDtlInfos.FirstOrDefault(t => t.ID == dtlInput.ID);
                if (dtlInfo != null)
                {
                    if (needQty >= dtlInfo.qty - dtlInfo.occupyQty)
                    {
                        allotStockDto allotStockDtl = new allotStockDto()
                        {
                            allotQty = dtlInfo.qty.Value - dtlInfo.occupyQty.Value,
                            stockDtlId = dtlInfo.ID,
                            isPick = "0"
                        };
                        allotStockViewList.Add(allotStockDtl);
                        needQty = needQty - (dtlInfo.qty.Value - dtlInfo.occupyQty.Value);
                    }
                    else
                    {
                        allotStockDto allotStockDtl = new allotStockDto()
                        {
                            allotQty = needQty,
                            stockDtlId = dtlInfo.ID,
                            isPick = "1"
                        };
                        allotStockViewList.Add(allotStockDtl);
                        needQty = 0;
                    }

                    if (needQty <= 0)
                    {
                        break;
                    }
                }
            }

            if (allotStockViewList.Count > 0)
            {
                var regionInfos = await DC.Set<BasWRegion>().AsNoTracking().ToListAsync();
                result = await allotInvoiceForPallet(allotStockViewList, invoiceInfo, invoiceDtlInfo, regionInfos, invoker, isElecFlag);
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 事务处理--按托分配
        /// </summary>
        /// <returns></returns>
        private async Task<BusinessResult> allotInvoiceForPallet(List<allotStockDto> allotStockViewList, WmsOutInvoice outInvoice, WmsOutInvoiceDtl outInvoiceDtl, List<BasWRegion> regionInfos, string invoker, bool isElecFlag)
        {
            BusinessResult result = new BusinessResult();
            int allotDtlCount = 0;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
            WmsPutdownVM wmsPutdownVM = Wtm.CreateVM<WmsPutdownVM>();
            string msg = string.Empty;
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

                List<WmsOutInvoiceRecord> addwmsOutInvoiceRecords = new List<WmsOutInvoiceRecord>();
                var stockDltids = allotStockViewList.Select(t => t.stockDtlId).ToList();
                var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => stockDltids.Contains(t.ID)).ToListAsync();
                var stockCodes = stockDtlInfos.Select(t => t.stockCode).ToList();
                var stockInfos = await DC.Set<WmsStock>().Where(t => stockCodes.Contains(t.stockCode)).ToListAsync();
                List<WmsStockUniicode> uniiInfos = new List<WmsStockUniicode>();
                if (isElecFlag)
                {
                    uniiInfos = await DC.Set<WmsStockUniicode>().Where(t => stockDltids.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).ToListAsync();
                }

                List<WmsPutdown> addwmsPutdowns = new List<WmsPutdown>();
                List<WmsPutdownDtl> addwmsPutdownDtls = new List<WmsPutdownDtl>();
                List<Int64> changeStockDtlIdList = new List<Int64>();
                List<string> changeStockcodeList = new List<string>();
                string docTypeCode = string.Empty;
                string orderNo = string.Empty;
                List<string> pickupList = new List<string>() { "PDA", "PTL" };

                #region 电子货架

                //bool isCanAllot = await wmsPutdownVM.CheckAllotElecDocCount(isElecFlag, outInvoice.invoiceNo);

                #endregion

                WmsOutWave wmsOutWave = null;
                if (outInvoiceDtl != null)
                {
                    var needQty = outInvoiceDtl.erpUndeliverQty - (outInvoiceDtl.allotQty - outInvoiceDtl.completeQty);
                    var isBatch = outInvoiceDtl.batchNo.IsNullOrWhiteSpace() ? false : true;
                    decimal realAllotQty = 0;
                    foreach (var allotStock in allotStockViewList)
                    {
                        var stockDtl = stockDtlInfos.FirstOrDefault(t => t.ID == allotStock.stockDtlId && (t.qty > t.occupyQty) && t.stockDtlStatus == 50 && t.lockFlag == 0);
                        if (stockDtl != null)
                        {
                            var stock = stockInfos.FirstOrDefault(t => t.stockCode == stockDtl.stockCode);
                            var region = regionInfos.FirstOrDefault(t => t.regionNo == stock.regionNo);
                            if (region != null && region.pickupMethod == "PTL" /*&& isCanAllot == false*/)
                            {
                                continue;
                            }

                            decimal dtlAllotQty = 0;
                            if (isElecFlag)
                            {
                                var eleUniiInfos = uniiInfos.Where(t => t.stockDtlId == stockDtl.ID).ToList();
                                decimal uniiCanQty = eleUniiInfos.Sum(t => t.qty.Value) - eleUniiInfos.Sum(t => t.occupyQty.Value);
                                if (uniiCanQty <= 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    decimal dtlRealCanQty = 0;
                                    if (uniiCanQty >= stockDtl.qty - stockDtl.occupyQty)
                                    {
                                        dtlRealCanQty = stockDtl.qty.Value - stockDtl.occupyQty.Value;
                                    }
                                    else
                                    {
                                        dtlRealCanQty = uniiCanQty;
                                    }

                                    if (allotStock.allotQty >= dtlRealCanQty)
                                    {
                                        dtlAllotQty = dtlRealCanQty;
                                        stockDtl.occupyQty += dtlRealCanQty;
                                        realAllotQty = realAllotQty + dtlAllotQty;
                                        //continue;
                                    }
                                    else
                                    {
                                        //占用库存
                                        dtlAllotQty = allotStock.allotQty;
                                        stockDtl.occupyQty += allotStock.allotQty;
                                        realAllotQty = realAllotQty + allotStock.allotQty;
                                    }
                                }
                            }
                            else
                            {
                                if (allotStock.allotQty >= stockDtl.qty - stockDtl.occupyQty)
                                {
                                    dtlAllotQty = stockDtl.qty.Value - stockDtl.occupyQty.Value;
                                    stockDtl.occupyQty = stockDtl.qty.Value;
                                    realAllotQty = realAllotQty + dtlAllotQty;
                                    //continue;
                                }
                                else
                                {
                                    //占用库存
                                    dtlAllotQty = allotStock.allotQty;
                                    stockDtl.occupyQty = stockDtl.occupyQty + allotStock.allotQty;
                                    realAllotQty = realAllotQty + allotStock.allotQty;
                                }
                            }


                            if (dtlAllotQty > 0)
                            {
                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【/*stockDtl.batchNo*/】,分配数量【{dtlAllotQty}】";
                                logger.Warn($"----->Warn----->手动分配-按托:{msg} ");

                                #region 数据处理

                                bool isPk = true;
                                if (region != null && !pickupList.Contains(region.pickupMethod))
                                {
                                    isPk = false;
                                }

                                //生成出库记录
                                WmsOutInvoiceRecord wmsOutInvoiceRecord =
                                    wmsPutdownVM.BuildOutInvoiceRecord(
                                        outInvoice,
                                        outInvoiceDtl,
                                        stock,
                                        stockDtl,
                                        allotStock.pickTaskNo,
                                        AllotType.Manu.GetCode(),
                                        isBatch,
                                        allotStock.isPick,
                                        "1",
                                        invoker,
                                        dtlAllotQty,
                                        "",
                                        isPk);
                                wmsOutInvoiceRecord.pickTaskNo = sysSequenceVM.GetSequence(SequenceCode.PickNo.GetCode());


                                addwmsOutInvoiceRecords.Add(wmsOutInvoiceRecord);

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

                                #endregion
                            }
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
                    logger.Warn($"----->Warn----->手动分配-按托:{msg} ");
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
                            logger.Warn($"----->Warn----->手动分配-按托:{msg} ");
                        }
                        else
                        {
                            outInvoiceDtl.invoiceDtlStatus = 29;
                            outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                            outInvoice.invoiceStatus = 41;

                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量： {realAllotQty} ,需求数量： {needQty}";
                            logger.Warn($"----->Warn----->手动分配-按托:{msg} ");
                        }

                        //if (needQty > realAllotQty)
                        //{
                        //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.Allocating.GetCode();
                        //    outInvoiceDtl.allocatResult = $"分配中,当前分配数量：{realAllotQty},需求数量：{needQty}";

                        //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                        //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量： {realAllotQty} ,需求数量： {needQty}";
                        //    logger.Warn($"----->Warn----->手动分配-按托:{msg} ");
                        //}
                        //else
                        //{
                        //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.AllocateFinished.GetCode();
                        //    outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                        //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                        //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量： {realAllotQty} ,需求数量： {needQty}";
                        //    logger.Warn($"----->Warn----->手动分配-按托:{msg} ");
                        //}
                    }
                    else
                    {
                        outInvoiceDtl.invoiceDtlStatus = 0;
                        outInvoiceDtl.allocatResult = "未分配";
                    }

                    outInvoice.UpdateBy = invoker;
                    outInvoice.UpdateTime = DateTime.Now;
                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】";
                    logger.Warn($"----->info----->手动分配-按托结束:{msg} ");
                }

                //todo:主表和波次主表
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
                                        //else
                                        //{
                                        //    string putdownNo = sysSequenceVM.GetSequence(DictonaryHelper.SequenceCode.WmsPutdownNo.GetCode());
                                        //    WmsPutdown wmsPutDown = wmsPutdownVM.BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                        //    wmsPutDown.putdownStatus = 90;
                                        //    dtlList.ForEach(t =>
                                        //    {
                                        //        var downDtl = wmsPutdownVM.BuildWmsPutDownDtl(t, putdownNo, invoker);
                                        //        addwmsPutdownDtls.Add(downDtl);
                                        //    });
                                        //    addwmsPutdowns.Add(wmsPutDown);
                                        //}
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


                //((DbContext)DC).BulkUpdate(new WmsOutInvoiceDtl[] {outInvoiceDtl});
                //((DbContext)DC).BulkUpdate(new WmsOutInvoice[] { outInvoice });
                //((DbContext)DC).SingleUpdate(outInvoiceDtl);
                //((DbContext)DC).SingleUpdate(outInvoice);

                // modified by Allen 2024-03-30 对库存明细做并发冲突检测，就不能调用Bulk相关方法，而得用EF自带的SaveChanges，使用SaveChangesAsync将无法捕捉到DbUpdateConcurrencyException异常
                await ((DbContext)DC).BulkUpdateAsync(stockDtlInfos);
                // DC.Set<WmsStockDtl>().UpdateRange(stockDtlInfos);
                // DC.SaveChanges();

                if (addwmsOutInvoiceRecords.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsOutInvoiceRecords);
                await ((DbContext)DC).SingleUpdateAsync(outInvoiceDtl);
                await ((DbContext)DC).SingleUpdateAsync(outInvoice);
                if (wmsOutWave != null)
                {
                    await ((DbContext)DC).SingleUpdateAsync(wmsOutWave);
                }

                if (addwmsPutdowns.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsPutdowns);
                if (addwmsPutdownDtls.Count > 0)
                    await ((DbContext)DC).BulkInsertAsync(addwmsPutdownDtls);
                // ((DbContext)DC).BulkSaveChanges();
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

        
        /// <summary>
        /// 创建出库唯一码表
        /// </summary>
        /// <param name="outInvoiceRecord"></param>
        /// <param name="stockUniicode"></param>
        /// <returns></returns>
        public WmsOutInvoiceUniicode BuildOutReceiptUniicodeInfo(WmsOutInvoiceRecord outInvoiceRecord, WmsStockUniicode stockUniicode, decimal allotQty, string invoker)
        {
            WmsOutInvoiceUniicode outReceiptUniicode = new WmsOutInvoiceUniicode();
            outReceiptUniicode.whouseNo = stockUniicode.whouseNo;
            outReceiptUniicode.erpWhouseNo = stockUniicode.erpWhouseNo;
            outReceiptUniicode.areaNo = stockUniicode.areaNo;
            outReceiptUniicode.proprietorCode = stockUniicode.proprietorCode;
            outReceiptUniicode.pickTaskNo = outInvoiceRecord.pickTaskNo;
            outReceiptUniicode.invoiceNo = outInvoiceRecord.invoiceNo;
            outReceiptUniicode.invoiceDtlId = outInvoiceRecord.invoiceDtlId;
            outReceiptUniicode.waveNo = outInvoiceRecord.waveNo;
            outReceiptUniicode.invoiceRecordId = outInvoiceRecord.ID;
            outReceiptUniicode.stockCode = stockUniicode.stockCode;
            outReceiptUniicode.palletBarcode = stockUniicode.palletBarcode;
            outReceiptUniicode.stockDtlId = stockUniicode.stockDtlId;
            outReceiptUniicode.uniicode = stockUniicode.uniicode;
            outReceiptUniicode.projectNo = stockUniicode.projectNo;
            outReceiptUniicode.skuCode = stockUniicode.skuCode;
            outReceiptUniicode.materialCode = stockUniicode.materialCode;
            outReceiptUniicode.materialName = stockUniicode.materialName;
            outReceiptUniicode.materialSpec = stockUniicode.materialSpec;
            outReceiptUniicode.supplierCode = stockUniicode.supplierCode;
            outReceiptUniicode.supplierName = stockUniicode.supplierName;
            outReceiptUniicode.supplierNameEn = stockUniicode.supplierNameEn;
            outReceiptUniicode.supplierNameAlias = stockUniicode.supplierNameAlias;
            outReceiptUniicode.batchNo = stockUniicode.batchNo;
            outReceiptUniicode.inspectionResult = stockUniicode.inspectionResult;
            outReceiptUniicode.erpBinNo = stockUniicode.erpWhouseNo;
            outReceiptUniicode.allotQty = allotQty;
            outReceiptUniicode.pickQty = 0;
            outReceiptUniicode.mslGradeCode = stockUniicode.mslGradeCode;
            outReceiptUniicode.dataCode = stockUniicode.dataCode;
            outReceiptUniicode.productDate = stockUniicode.productDate;
            outReceiptUniicode.expDate = stockUniicode.expDate;
            outReceiptUniicode.delayToEndDate = stockUniicode.delayToEndDate;
            outReceiptUniicode.delayTimes = stockUniicode.delayTimes;
            outReceiptUniicode.delayReason = stockUniicode.delayReason;
            outReceiptUniicode.supplierExposeTimes = stockUniicode.supplierExposeTimes;
            outReceiptUniicode.realExposeTimes = stockUniicode.realExposeTimes;
            outReceiptUniicode.unpackTime = stockUniicode.unpackTime;
            outReceiptUniicode.packageTime = stockUniicode.packageTime;
            outReceiptUniicode.leftMslTimes = stockUniicode.leftMslTimes;
            outReceiptUniicode.driedTimes = stockUniicode.driedTimes;
            outReceiptUniicode.driedScrapFlag = stockUniicode.driedScrapFlag;
            outReceiptUniicode.CreateBy = invoker;
            outReceiptUniicode.CreateTime = DateTime.Now;
            outReceiptUniicode.UpdateBy = invoker;
            outReceiptUniicode.UpdateTime = DateTime.Now;
            outReceiptUniicode.extend1 = stockUniicode.extend1;
            outReceiptUniicode.extend2 = stockUniicode.extend2;
            outReceiptUniicode.extend3 = stockUniicode.extend3;
            outReceiptUniicode.extend4 = stockUniicode.extend4;
            outReceiptUniicode.extend5 = stockUniicode.extend5;
            outReceiptUniicode.extend6 = stockUniicode.extend6;
            outReceiptUniicode.extend7 = stockUniicode.extend7;
            outReceiptUniicode.extend8 = stockUniicode.extend8;
            outReceiptUniicode.extend9 = stockUniicode.extend9;
            outReceiptUniicode.extend10 = stockUniicode.extend10;
            outReceiptUniicode.extend11 = stockUniicode.extend11;
            outReceiptUniicode.extend12 = stockUniicode.chipSize;
            outReceiptUniicode.extend13 = stockUniicode.chipThickness;
            outReceiptUniicode.extend14 = stockUniicode.chipModel;
            outReceiptUniicode.extend15 = stockUniicode.dafType;
            outReceiptUniicode.ouniiStatus = 0;
            return outReceiptUniicode;
        }

        #endregion
    }
}
