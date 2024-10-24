using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Com.Wish.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Z.BulkOperations;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Base.BasBSupplierBinVMs;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using Wish.ViewModel.BusinessPutdown.WmsPutdownVMs;
using Wish.ViewModel.System.SysSequenceVMs;

namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        public async Task<List<string>> GetErrBin()
        {
            List<string> filterBinList = new List<string>();
            List<string> regionList = new List<string>() { "LXK", "TPK" };
            var binInfos = await DC.Set<BasWBin>().AsNoTracking().Where(t => regionList.Contains(t.regionNo) && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
            if (binInfos.Count > 0)
            {
                var filterBinInfos = binInfos.Where(t => t.isOutEnable == 0 || t.binErrFlag != "0").ToList();
                var list = filterBinInfos.Select(t => t.binNo).ToList();
                filterBinList.AddRange(list);

                filterBinInfos = binInfos.Where(t => t.binErrFlag != "0" && t.extensionIdx == 1).ToList();
                List<BinDto> binViews = filterBinInfos.Select(t => new BinDto() { extensionGroupNo = t.extensionGroupNo, regionNo = t.regionNo, roadwayNo = t.regionNo }).ToList();

                var query = from bin in binInfos
                            join ext in binViews on new { bin.extensionGroupNo, bin.regionNo, bin.roadwayNo } equals new { ext.extensionGroupNo, ext.regionNo, ext.roadwayNo }
                            select bin.binNo;
                filterBinList.AddRange(query.ToList());
                filterBinList = filterBinList.Distinct().ToList();
            }
            return filterBinList;
        }

        private async Task<List<WmsStockDtl>> GetAllStockQuery(WmsStockAllocateForMultiMatDto input, List<string> stockCodes)
        {


            List<WmsStockDtl> stockDtls = new List<WmsStockDtl>();
            if (input.docTypeCode == DictonaryHelper.BusinessCode.OutProduceOrder.GetCode())//
            {
                //备用项目号为空，然后找备用项目号等于该项目号
                //找项目号为空
                //批次
                //入库时间
                var dtlQuery = DC.Set<WmsStockDtl>()
                      .Where(x => stockCodes.Contains(x.stockCode))
                      .Where(x => x.lockFlag == 0)
                      .Where(x => x.stockDtlStatus == 50)
                      .Where(x => input.materialCodeList.Contains(x.materialCode))
                      .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || input.projectNoList.Contains(x.projectNoBak) || string.IsNullOrWhiteSpace(x.projectNo))
                      .Where(x => x.inspectionResult == Convert.ToInt32(DictonaryHelper.InspectionResult.Qualitified.GetCode()) || x.inspectionResult == Convert.ToInt32(DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode()))
                      .Where(x => x.qty > x.occupyQty)
                      .WhereIf(input.erpWhouseList != null && input.erpWhouseList.Count > 0, x => input.erpWhouseList.Contains(x.erpWhouseNo))
                      //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.batchNo), t => t.batchNo == stockAllocateView.batchNo)
                      .OrderBy(x => x.projectNoBak)
                      .ThenBy(x => x.projectNo);
                stockDtls = await dtlQuery.ToListAsync();
            }
            else
            {
                var dtlQuery = DC.Set<WmsStockDtl>()
                     .Where(x => stockCodes.Contains(x.stockCode))
                     .Where(x => x.lockFlag == 0)
                     .Where(x => x.stockDtlStatus == 50)
                     .Where(x => input.materialCodeList.Contains(x.materialCode))
                     .Where(x => x.inspectionResult == Convert.ToInt32(DictonaryHelper.InspectionResult.Qualitified.GetCode()) || x.inspectionResult == Convert.ToInt32(DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode()))
                     .Where(x => x.qty > x.occupyQty)
                     .WhereIf(input.erpWhouseList != null && input.erpWhouseList.Count > 0, x => input.erpWhouseList.Contains(x.erpWhouseNo))
                     //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.batchNo), t => t.batchNo == stockAllocateView.batchNo)
                     .OrderBy(x => x.projectNoBak)
                     .ThenBy(x => x.projectNo);
                stockDtls = await dtlQuery.ToListAsync();
            }

            return stockDtls;
        }
        /// <summary>
        /// 非电子料库存分配-平库
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public async Task<List<WmsStockDtl>> allotNonElectronicMaterrialForMatListExtend(WmsStockAllocateForMultiMatDto input)
        {
            // 用到的变量
            BusinessResult result = new BusinessResult();
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            List<string> stockCodes = new List<string>();
            List<WmsStock> wmsStocks = new List<WmsStock>();
            //#region 是否卡控供应商库位
            //bool isLimitSupplyBin = false;
            //var docTpyeDtlInfo = DC.Set<CfgDocTypeDtl>().Where(t => t.docTypeCode == input.docTypeCode && t.paramCode == "ISLIMIT_SUPPLYBIN" && t.paramValueCode == "1").FirstOrDefault();
            //if (docTpyeDtlInfo != null)
            //{
            //    isLimitSupplyBin = true;
            //} 
            //#endregion
            //List<string> docTypeList = new List<string>()
            //{
            //    DictonaryHelper.BusinessCode.OutOutSourcePick.GetCode(),//委外出库
            //    DictonaryHelper.BusinessCode.OutTransferRequest.GetCode(),//调拨申请出

            //};
            var errBinList = await GetErrBin();
            if (input.isLimitSupplyBin)
            {
                // 先找出所有库存
                var stockQuery = DC.Set<WmsStock>()
                    .Where(x => x.errFlag == Convert.ToInt32(DictonaryHelper.YesNoCode.No.GetCode()))
                    .Where(x => x.stockStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .WhereIf(input.regionNos.Any(), x => input.regionNos.Contains(x.regionNo))
                    .WhereIf(errBinList.Any(), t => !errBinList.Contains(t.binNo));
                wmsStocks = await stockQuery.ToListAsync();
                //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.areaNo), x => x.areaNo == stockAllocateView.areaNo).ToList();
                stockCodes = wmsStocks.Select(t => t.stockCode).ToList();
            }
            else
            {
                // 先找出所有库存
                var stockQuery = DC.Set<WmsStock>()
                    .Where(x => x.errFlag == Convert.ToInt32(DictonaryHelper.YesNoCode.No.GetCode()))
                    .Where(x => x.stockStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .WhereIf(input.regionNos.Any(), x => input.regionNos.Contains(x.regionNo))
                    .WhereIf(errBinList.Any(), t => !errBinList.Contains(t.binNo))
                    .WhereIf(input.supplyErpBinList.Any(), t => !input.supplyErpBinList.Contains(t.binNo));
                wmsStocks = await stockQuery.ToListAsync();
                //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.areaNo), x => x.areaNo == stockAllocateView.areaNo).ToList();
                stockCodes = wmsStocks.Select(t => t.stockCode).ToList();
            }

            List<WmsStockDtl> wmsStockDtls = new List<WmsStockDtl>();
            wmsStockDtls = await GetAllStockQuery(input, stockCodes);
            return wmsStockDtls;
        }
        /// <summary>
        /// 成品库存分配
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public async Task<List<WmsStockAllovateSnReturn>> allotNonElectronicMaterrialForMatListExtendSn(List<WmsStockDtl> allStockDtls, List<allotSnDto> allotSnViewList)
        {
            // 用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            List<string> stockCodes = new List<string>();
            List<WmsStockAllovateSnReturn> result = new List<WmsStockAllovateSnReturn>();
            var orginSnList = allotSnViewList.Select(t => t.orginSn).ToList();
            List<string> allSnList = new List<string>();

            foreach (var snView in allotSnViewList)
            {
                var snList = snView.orginSn.Split(',').ToList();
                if (snList.Count > 0)
                {
                    snView.snList = snList;
                    allSnList.AddRange(snList);
                }

            }

            var uniiInfos = await DC.Set<WmsStockUniicode>().Where(t => allSnList.Contains(t.uniicode) && t.qty > t.occupyQty).ToListAsync();
            foreach (var allotSn in allotSnViewList)
            {
                var snUniiInfos = uniiInfos.Where(t => allotSn.snList.Contains(t.uniicode)).ToList();
                if (snUniiInfos.Count > 0)
                {
                    WmsStockAllovateSnReturn allovateSnReturn = new WmsStockAllovateSnReturn();
                    allovateSnReturn.orginalSn = allotSn.orginSn;
                    foreach (var item in snUniiInfos)
                    {
                        WmsStockAllovateSubSnReturn snReturn = new WmsStockAllovateSubSnReturn()
                        {
                            sn = item.uniicode,
                            stockDtlId = item.stockDtlId
                        };
                        allovateSnReturn.subSnList.Add(snReturn);
                    }
                    result.Add(allovateSnReturn);
                }

            }


            return result;
        }

        public async Task<List<string>> GetSupplyErpBinStock(List<string> supplyErpBinList)
        {
            List<string> result = new List<string>();
            var wmsStocks =await DC.Set<WmsStock>()
                .Where(x => x.errFlag == Convert.ToInt32(DictonaryHelper.YesNoCode.No.GetCode()))
                .Where(x => supplyErpBinList.Contains(x.binNo))
                .Where(x => x.stockStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode()).ToListAsync();
            if (wmsStocks.Count > 0)
            {
                result = wmsStocks.Select(t => t.stockCode).ToList();
            }
            return result;
        }

        /// <summary>
        /// 电子料库存分配
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public List<WmsStockAllovateElecReturn> allotElectronicMaterrialForMatListExtend(List<WmsStockDtl> allStockDtls, List<string> elecMatList)
        {
            // 用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            List<string> stockCodes = new List<string>();
            List<WmsStockAllovateElecReturn> result = new List<WmsStockAllovateElecReturn>();
            var dtlList = allStockDtls.Where(t => elecMatList.Contains(t.materialCode)).Select(t => t.ID).ToList();
            if (dtlList.Count > 0)
            {
                var uniiInfos = DC.Set<WmsStockUniicode>().Where(t => dtlList.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).ToList();
                var groupUnii = uniiInfos.GroupBy(t => t.materialCode);
                foreach (var item in groupUnii)
                {
                    var stockdtlList = item.OrderBy(t => t.productDate).ThenBy(t => t.inwhTime).Select(t => t.stockDtlId).Distinct().ToList();
                    WmsStockAllovateElecReturn allovateElecReturn = new WmsStockAllovateElecReturn()
                    {
                        materialCode = item.Key,
                        stockDtlIdList = stockdtlList,

                    };
                    result.Add(allovateElecReturn);
                }
            }
            return result;
        }


        /// <summary>
        /// 唯一码入库时间或者生产日期
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public async Task<List<WmsStockAllovateElecReturn>> allotUniiMaterrialForMatListExtend(List<WmsStockDtl> allStockDtls, List<allotMatDto> allotMats)
        {
            // 用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            List<string> stockCodes = new List<string>();
            List<WmsStockAllovateElecReturn> result = new List<WmsStockAllovateElecReturn>();
            var elecMatList = allotMats.Select(t => t.materialCode).Distinct().ToList();
            var dtlList = allStockDtls.Where(t => elecMatList.Contains(t.materialCode)).Select(t => t.ID).ToList();
            if (dtlList.Count > 0)
            {
                var uniiInfos = await DC.Set<WmsStockUniicode>().Where(t => dtlList.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).ToListAsync();
                var groupUnii = uniiInfos.GroupBy(t => t.materialCode);
                foreach (var item in groupUnii)
                {
                    var matInfo = allotMats.FirstOrDefault(t => t.materialCode == item.Key);
                    List<Int64> stockdtlList = new List<Int64>();
                    if (matInfo != null)
                    {
                        if (matInfo.isElecFlag)
                        {
                            stockdtlList = item.OrderBy(t => t.productDate).ThenBy(t => t.inwhTime).Select(t => t.stockDtlId).Distinct().ToList();

                        }
                        else
                        {
                            stockdtlList = item.OrderBy(t => t.inwhTime).Select(t => t.stockDtlId).Distinct().ToList();
                        }
                    }
                    WmsStockAllovateElecReturn allovateElecReturn = new WmsStockAllovateElecReturn()
                    {
                        materialCode = item.Key,
                        stockDtlIdList = stockdtlList,

                    };
                    result.Add(allovateElecReturn);
                }
            }
            return result;
        }
        #region 不是发料单（工单）
        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="wmsStockDtls"></param>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        private List<WmsStockDtl> GetWmsStockDtlsForBatchNoProduct(List<WmsStockDtl> wmsStockDtls, WmsStockAllocateDto stockAllocateView, List<erpWhousePrirityDto> erpWhouses)
        {

            #region 相同字段多个值排序
            //string[] list = new string[] { null, "pb2" };
            //var orderBy8 = testList.OrderBy(x =>
            //{
            //    var index = 0;
            //    index = Array.IndexOf(list, x.projectNobak);
            //    if (index != -1) { return index; }
            //    else
            //    {
            //        return int.MaxValue;
            //    }
            //}).ToList();
            #endregion
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            result = FilterWmsStockDtlsForBatchForNoProduct(wmsStockDtls, stockAllocateView);

            if (result.Count > 0)
            {
                result = GetWmsStockDtlsForSort(result, stockAllocateView, erpWhouses);
            }


            return result;
        }

        private List<WmsStockDtl> FilterWmsStockDtlsForBatchForNoProduct(List<WmsStockDtl> wmsStockDtls, WmsStockAllocateDto stockAllocateView)
        {
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            if (string.IsNullOrWhiteSpace(stockAllocateView.batchNo))
            {
                result = wmsStockDtls
              .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
              .Where(x => x.lockFlag == 0)
              .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
              .Where(x => x.materialCode == stockAllocateView.materialCode)
              .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
              .Where(x => x.qty > x.occupyQty).ToList();
            }
            else
            {
                result = wmsStockDtls
              .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
              .Where(x => x.lockFlag == 0)
              .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
              .Where(x => x.materialCode == stockAllocateView.materialCode)
              .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
              .Where(x => x.qty > x.occupyQty)
              //.Where(x => x.batchNo == stockAllocateView.batchNo)
              .ToList();
            }
            return result;
        }

        #endregion

        #region 工单

        private List<WmsStockDtl> FilterWmsStockDtlsForBatch(List<WmsStockDtl> wmsStockDtls, WmsStockAllocateDto stockAllocateView)
        {
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            result = wmsStockDtls
               .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
               .Where(x => x.lockFlag == 0)
               .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
               .Where(x => x.materialCode == stockAllocateView.materialCode)
               // .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
               .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
               .Where(x => x.qty > x.occupyQty).ToList();

            #region old
            //if (string.IsNullOrWhiteSpace(stockAllocateView.batchNo))
            //{
            //    result = wmsStockDtls
            //  .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //  .Where(x => x.lockFlag == 0)
            //  .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
            //  .Where(x => x.materialCode == stockAllocateView.materialCode)
            //  .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
            //  .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            //  .Where(x => x.qty > x.occupyQty).ToList();
            //}
            //else
            //{
            //    result = wmsStockDtls
            //  .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //  .Where(x => x.lockFlag == 0)
            //  .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
            //  .Where(x => x.materialCode == stockAllocateView.materialCode)
            //  .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
            //  .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            //  .Where(x => x.qty > x.occupyQty)
            //  //.Where(x => x.batchNo == stockAllocateView.batchNo)
            //  .ToList();
            //} 
            #endregion
            return result;
        }
        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="wmsStockDtls"></param>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        private List<WmsStockDtl> GetWmsStockDtlsForBatch(List<WmsStockDtl> wmsStockDtls, WmsStockAllocateDto stockAllocateView, List<erpWhousePrirityDto> erpWhouses)
        {

            #region 相同字段多个值排序
            //string[] list = new string[] { null, "pb2" };
            //var orderBy8 = testList.OrderBy(x =>
            //{
            //    var index = 0;
            //    index = Array.IndexOf(list, x.projectNobak);
            //    if (index != -1) { return index; }
            //    else
            //    {
            //        return int.MaxValue;
            //    }
            //}).ToList();
            #endregion
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            result = FilterWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView);

            if (result.Count > 0)
            {
                result = GetWmsStockDtlsForSort(result, stockAllocateView, erpWhouses);
            }


            return result;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="wmsStockDtls"></param>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        private List<WmsStockDtl> GetWmsStockDtlsForSort(List<WmsStockDtl> wmsStockDtls, WmsStockAllocateDto stockAllocateView, List<erpWhousePrirityDto> erpWhouses)
        {
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            if (wmsStockDtls.Count > 0)
            {
                string[] erpWhouseArr = erpWhouses.Select(t => t.erpWhouseNo).ToArray();
                if (!string.IsNullOrWhiteSpace(stockAllocateView.projectNo))
                {
                    string[] prolist = new string[] { null, stockAllocateView.projectNo };
                    if (stockAllocateView.elecStockDtlIdList.Count > 0)
                    {
                        Int64[] elecSortDltLIst = stockAllocateView.elecStockDtlIdList.ToArray();

                        result = wmsStockDtls.OrderBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(erpWhouseArr, x.erpWhouseNo);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                        .ThenBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(prolist, x.projectNoBak);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                          .ThenBy(x => x.projectNo)
                          .ThenBy(x =>
                          {
                              var index = 0;
                              index = Array.IndexOf(elecSortDltLIst, x.ID);
                              if (index != -1)
                              {
                                  return index;
                              }
                              else
                              {
                                  return int.MaxValue;
                              }
                          })
                           .ToList();

                    }
                    else
                    {

                        result = wmsStockDtls.OrderBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(erpWhouseArr, x.erpWhouseNo);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                        .ThenBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(prolist, x.projectNoBak);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                          .ThenBy(x => x.projectNo)
                          .ToList();


                    }

                }
                else
                {
                    if (stockAllocateView.elecStockDtlIdList.Count > 0)
                    {
                        Int64[] elecSortDltLIst = stockAllocateView.elecStockDtlIdList.ToArray();
                        result = wmsStockDtls.OrderBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(erpWhouseArr, x.erpWhouseNo);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                        .ThenBy(x => x.projectNoBak)
                          .ThenBy(x => x.projectNo)
                          .ThenBy(x =>
                          {
                              var index = 0;
                              index = Array.IndexOf(elecSortDltLIst, x.ID);
                              if (index != -1)
                              {
                                  return index;
                              }
                              else
                              {
                                  return int.MaxValue;
                              }
                          })
                           .ToList();
                    }
                    else
                    {
                        result = wmsStockDtls.OrderBy(x =>
                        {
                            var index = 0;
                            index = Array.IndexOf(erpWhouseArr, x.erpWhouseNo);
                            if (index != -1)
                            {
                                return index;
                            }
                            else
                            {
                                return int.MaxValue;
                            }
                        })
                        .ThenBy(x => x.projectNoBak)
                       .ThenBy(x => x.projectNo)
                        .ToList();
                    }
                }
            }
            return result;
        }
        #endregion

        #region 批次
        /// <summary>
        /// 库存分配-有批次
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public async Task<BusinessResult> allotMaterrialForMemoryExtendForAll(List<WmsStockDtl> wmsStockDtls, List<CfgDepartmentErpWhouse> erpWhouses, WmsStockAllocateDto stockAllocateView)
        {
            // 用到的变量
            // BusinessResult result = new BusinessResult();
            WmsStockAllovateReturn allotResult = new WmsStockAllovateReturn();

            BasBSupplierBinVM basBSupplierBinVm = Wtm.CreateVM<BasBSupplierBinVM>();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();

            // 需要用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();

            List<WmsStockDtl> resultDtlList = new List<WmsStockDtl>();

            List<erpWhousePrirityDto> erpWhousePririties = new List<erpWhousePrirityDto>()
                {
                    new erpWhousePrirityDto(){erpWhouseNo=stockAllocateView.erpWhouseNo}
                };

            #region 优先级
            // 根据库存找出库存明细
            //wmsStockDtls = DC.Set<WmsStockDtl>()
            //.Where(x => stockCodes.Contains(x.stockCode))
            //.Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //.Where(x => x.lockFlag == "0")
            //.Where(x => x.inspectionResult == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            ////.Where(x => x.IsValid == true)
            //.Where(x => x.qty > x.occupyQty).ToList();


            // 4、批次处理
            //_vpoint = "批次处理";
            //if (null != stockAllocateView.batchNo)
            //{
            //    // 4.1 指定批次，按物料编码+批次：
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode && x.batchNo == stockAllocateView.batchNo).ToList();
            //}
            //else
            //{
            //    // 4.2 不指定批次，按物料编码
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode).ToList();
            //}

            // 库存明细表需要添加一个入库时间 inwhTime
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选需检验批次
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选不强检验入库时间，只要是此物料即可
            //wmsStockDtls.OrderBy(x => x.inwhTime);

            #endregion

            #region 是否有符合入参批次的库存唯一码
            if (!string.IsNullOrWhiteSpace(stockAllocateView.batchNo))
            {
                List<Int64> hasBatchStockDtlList = new List<Int64>();
                var stockUniiInfos = await DC.Set<WmsStockUniicode>().Where(t => t.batchNo == stockAllocateView.batchNo && t.materialCode == stockAllocateView.materialCode && t.qty > t.occupyQty && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1).ToListAsync();
                if (stockUniiInfos.Count > 0)
                {
                    hasBatchStockDtlList = stockUniiInfos.Select(t => t.stockDtlId).Distinct().ToList();
                    wmsStockDtls = wmsStockDtls.Where(t => hasBatchStockDtlList.Contains(t.ID)).ToList();
                }
            }
            #endregion

            // 单据类型为工单发料时按ERP仓库优先级查找库存
            if (stockAllocateView.docTypeCode == DictonaryHelper.BusinessCode.OutProduceOrder.GetCode())
            {
                //var totalStockQty = wmsStockDtls.Sum(t => t.qty) - wmsStockDtls.Sum(t => t.occupyQty);
                if (stockAllocateView.isDesignateErpWhouse == "0")
                {
                    #region 仓库
                    if (string.IsNullOrWhiteSpace(stockAllocateView.belongDepartment))
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses);
                    }
                    else
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses, stockAllocateView.belongDepartment);
                    }
                    foreach (var item in erpWhouses)
                    {
                        var erp = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == item.erpWhouseNo);
                        if (erp != null)
                        {
                            erp.priority = item.priority;
                        }
                        else
                        {
                            erpWhousePririties.Add(new erpWhousePrirityDto() { erpWhouseNo = item.erpWhouseNo, priority = item.priority });
                        }

                    }

                    //if (!string.IsNullOrWhiteSpace(stockAllocateView.departMent) && stockAllocateView.departMent.Contains("机器人"))
                    //{
                    //    var Whouse = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == "01B");
                    //    if (Whouse == null)
                    //    {
                    //        erpWhousePririties.Add(new erpWhousePrirityView() { erpWhouseNo = "01B", priority = 99 });
                    //    }
                    //}
                    #endregion


                    decimal totalStockQty = 0;
                    var wmsStockDtlsForOther = (from dtl in wmsStockDtls
                                                join erpWhouse in erpWhouses on dtl.erpWhouseNo equals erpWhouse.erpWhouseNo
                                                //where dtl.erpWhouseNo != stockAllocateView.erpWhouseNo
                                                orderby erpWhouse.priority ascending, dtl.projectNoBak ascending, dtl.projectNo descending
                                                select dtl)
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.materialCode == stockAllocateView.materialCode)
                    // .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.Where(x => x.batchNo == stockAllocateView.batchNo)
                    //.OrderBy(x => x.projectNoBak)
                    //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                    //.ThenBy(x => x.projectNo)
                    //.ThenBy(x => x.inwhTime)
                    .ToList();


                    if (wmsStockDtlsForOther.Any())
                    {
                        #region 排序
                        wmsStockDtlsForOther = GetWmsStockDtlsForSort(wmsStockDtlsForOther, stockAllocateView, erpWhousePririties);
                        #endregion
                        resultDtlList = resultDtlList.Union(wmsStockDtlsForOther).ToList();
                        totalStockQty = resultDtlList.Sum(t => t.qty.Value) - resultDtlList.Sum(t => t.occupyQty.Value);
                    }
                    if (stockAllocateView.qty > totalStockQty)
                    {
                        //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                        var stockDtls = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                        resultDtlList = resultDtlList.Union(stockDtls).ToList();
                    }
                }
                else
                {
                    //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                    resultDtlList = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                }
            }
            else
            {
                //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                resultDtlList = GetWmsStockDtlsForBatchNoProduct(wmsStockDtls, stockAllocateView, erpWhousePririties);
            }
            //过滤成品
            if (stockAllocateView.snStockDtlIdList.Count > 0)
            {
                resultDtlList = resultDtlList.Where(t => stockAllocateView.snStockDtlIdList.Contains(t.ID)).ToList();
            }

            if (resultDtlList.Any())
            {
                foreach (var wmsStockDtl in resultDtlList)
                {
                    if (stockAllocateView.qty <= 0)
                    {
                        break;
                    }
                    WmsStockAllovateReturn allocateResult = new WmsStockAllovateReturn();
                    // 取库存明细列表的第一条记录
                    if (stockAllocateView.qty >= (wmsStockDtl.qty - wmsStockDtl.occupyQty))
                    {
                        // 不需要拣选, 该库存明细的可用数量<= 需求数量, 那就是该库存明细全分掉
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = wmsStockDtl.qty - wmsStockDtl.occupyQty;
                        allocateResult.isNeedPick = false;
                        allocateResultList.Add(allocateResult);
                    }
                    else
                    {
                        // 这个是需要拣选的，可用数量 > 需求数量, 分配结果数量 = 需求数量
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = stockAllocateView.qty;
                        allocateResult.isNeedPick = true;
                        allocateResultList.Add(allocateResult);
                    }
                    stockAllocateView.qty = (decimal)(stockAllocateView.qty - (wmsStockDtl.qty - wmsStockDtl.occupyQty.Value));

                }
                // 出参：非电子料：库存明细ID，数量，uniicodes传null，不传[]
                //allocateResult.stockDtlId = wmsStockDtl.ID;
                //allocateResult.uniicodes = null;

                businessResult.code = ResCode.OK;
                businessResult.msg = "库存分配成功";
                businessResult.outParams = allocateResultList;
            }
            else
            {
                businessResult.code = ResCode.Error;
                businessResult.msg = "库存分配失败：未找到库存明细";
                businessResult.outParams = null;
            }

            return businessResult;
        }

        #region old
        /// <summary>
        /// 库存分配-有批次
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public BusinessResult allotMaterrialForMemoryExtendBatch(List<WmsStockDtl> wmsStockDtls, List<CfgDepartmentErpWhouse> erpWhouses, WmsStockAllocateDto stockAllocateView)
        {
            // 用到的变量
            // BusinessResult result = new BusinessResult();
            WmsStockAllovateReturn allotResult = new WmsStockAllovateReturn();

            BasBSupplierBinVM basBSupplierBinVm = Wtm.CreateVM<BasBSupplierBinVM>();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();

            // 需要用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();

            List<WmsStockDtl> resultDtlList = new List<WmsStockDtl>();

            List<erpWhousePrirityDto> erpWhousePririties = new List<erpWhousePrirityDto>()
                {
                    new erpWhousePrirityDto(){erpWhouseNo=stockAllocateView.erpWhouseNo}
                };

            #region 优先级
            // 根据库存找出库存明细
            //wmsStockDtls = DC.Set<WmsStockDtl>()
            //.Where(x => stockCodes.Contains(x.stockCode))
            //.Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //.Where(x => x.lockFlag == "0")
            //.Where(x => x.inspectionResult == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            ////.Where(x => x.IsValid == true)
            //.Where(x => x.qty > x.occupyQty).ToList();


            // 4、批次处理
            //_vpoint = "批次处理";
            //if (null != stockAllocateView.batchNo)
            //{
            //    // 4.1 指定批次，按物料编码+批次：
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode && x.batchNo == stockAllocateView.batchNo).ToList();
            //}
            //else
            //{
            //    // 4.2 不指定批次，按物料编码
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode).ToList();
            //}

            // 库存明细表需要添加一个入库时间 inwhTime
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选需检验批次
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选不强检验入库时间，只要是此物料即可
            //wmsStockDtls.OrderBy(x => x.inwhTime);

            #endregion

            #region 是否有符合入参批次的库存唯一码
            if (!string.IsNullOrWhiteSpace(stockAllocateView.batchNo))
            {
                List<Int64> hasBatchStockDtlList = new List<Int64>();
                var stockUniiInfos = DC.Set<WmsStockUniicode>().Where(t => t.batchNo == stockAllocateView.batchNo && t.materialCode == stockAllocateView.materialCode && t.qty > t.occupyQty && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1).ToList();
                if (stockUniiInfos.Count > 0)
                {
                    hasBatchStockDtlList = stockUniiInfos.Select(t => t.stockDtlId).Distinct().ToList();
                    wmsStockDtls = wmsStockDtls.Where(t => hasBatchStockDtlList.Contains(t.ID)).ToList();
                }
            }
            #endregion

            // 单据类型为工单发料时按ERP仓库优先级查找库存
            if (stockAllocateView.docTypeCode == DictonaryHelper.BusinessCode.OutProduceOrder.GetCode())
            {
                //var totalStockQty = wmsStockDtls.Sum(t => t.qty) - wmsStockDtls.Sum(t => t.occupyQty);
                if (stockAllocateView.isDesignateErpWhouse == "0")
                {
                    #region 仓库
                    if (string.IsNullOrWhiteSpace(stockAllocateView.belongDepartment))
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses);
                    }
                    else
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses, stockAllocateView.belongDepartment);
                    }
                    foreach (var item in erpWhouses)
                    {
                        var erp = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == item.erpWhouseNo);
                        if (erp != null)
                        {
                            erp.priority = item.priority;
                        }
                        else
                        {
                            erpWhousePririties.Add(new erpWhousePrirityDto() { erpWhouseNo = item.erpWhouseNo, priority = item.priority });
                        }

                    }

                    //if (!string.IsNullOrWhiteSpace(stockAllocateView.departMent) && stockAllocateView.departMent.Contains("机器人"))
                    //{
                    //    var Whouse = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == "01B");
                    //    if (Whouse == null)
                    //    {
                    //        erpWhousePririties.Add(new erpWhousePrirityView() { erpWhouseNo = "01B", priority = 99 });
                    //    }
                    //}
                    #endregion


                    decimal totalStockQty = 0;
                    var wmsStockDtlsForOther = (from dtl in wmsStockDtls
                                                join erpWhouse in erpWhouses on dtl.erpWhouseNo equals erpWhouse.erpWhouseNo
                                                //where dtl.erpWhouseNo != stockAllocateView.erpWhouseNo
                                                orderby erpWhouse.priority ascending, dtl.projectNoBak ascending, dtl.projectNo descending
                                                select dtl)
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.materialCode == stockAllocateView.materialCode)
                    .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.Where(x => x.batchNo == stockAllocateView.batchNo)
                    //.OrderBy(x => x.projectNoBak)
                    //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                    //.ThenBy(x => x.projectNo)
                    //.ThenBy(x => x.inwhTime)
                    .ToList();


                    if (wmsStockDtlsForOther.Any())
                    {
                        #region 排序
                        wmsStockDtlsForOther = GetWmsStockDtlsForSort(wmsStockDtlsForOther, stockAllocateView, erpWhousePririties);
                        #endregion
                        resultDtlList = resultDtlList.Union(wmsStockDtlsForOther).ToList();
                        totalStockQty = resultDtlList.Sum(t => t.qty.Value) - resultDtlList.Sum(t => t.occupyQty.Value);
                    }
                    if (stockAllocateView.qty > totalStockQty)
                    {
                        //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                        var stockDtls = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                        resultDtlList = resultDtlList.Union(stockDtls).ToList();
                    }
                }
                else
                {
                    //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                    resultDtlList = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                }
            }
            else
            {
                //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                resultDtlList = GetWmsStockDtlsForBatchNoProduct(wmsStockDtls, stockAllocateView, erpWhousePririties);
            }
            //过滤成品
            if (stockAllocateView.snStockDtlIdList.Count > 0)
            {
                resultDtlList = resultDtlList.Where(t => stockAllocateView.snStockDtlIdList.Contains(t.ID)).ToList();
            }

            if (resultDtlList.Any())
            {
                foreach (var wmsStockDtl in resultDtlList)
                {
                    if (stockAllocateView.qty <= 0)
                    {
                        break;
                    }
                    WmsStockAllovateReturn allocateResult = new WmsStockAllovateReturn();
                    // 取库存明细列表的第一条记录
                    if (stockAllocateView.qty >= (wmsStockDtl.qty - wmsStockDtl.occupyQty))
                    {
                        // 不需要拣选, 该库存明细的可用数量<= 需求数量, 那就是该库存明细全分掉
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = wmsStockDtl.qty - wmsStockDtl.occupyQty;
                        allocateResult.isNeedPick = false;
                        allocateResultList.Add(allocateResult);
                    }
                    else
                    {
                        // 这个是需要拣选的，可用数量 > 需求数量, 分配结果数量 = 需求数量
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = stockAllocateView.qty;
                        allocateResult.isNeedPick = true;
                        allocateResultList.Add(allocateResult);
                    }
                    stockAllocateView.qty = (decimal)(stockAllocateView.qty - (wmsStockDtl.qty - wmsStockDtl.occupyQty.Value));

                }
                // 出参：非电子料：库存明细ID，数量，uniicodes传null，不传[]
                //allocateResult.stockDtlId = wmsStockDtl.ID;
                //allocateResult.uniicodes = null;

                businessResult.code = ResCode.OK;
                businessResult.msg = "库存分配成功";
                businessResult.outParams = allocateResultList;
            }
            else
            {
                businessResult.code = ResCode.Error;
                businessResult.msg = "库存分配失败：未找到库存明细";
                businessResult.outParams = null;
            }

            return businessResult;
        }

        /// <summary>
        /// 库存分配--无批次
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public BusinessResult allotMaterrialForMemoryExtendNoBatch(List<WmsStockDtl> wmsStockDtls, List<CfgDepartmentErpWhouse> erpWhouses, WmsStockAllocateDto stockAllocateView)
        {
            // 用到的变量
            // BusinessResult result = new BusinessResult();
            WmsStockAllovateReturn allotResult = new WmsStockAllovateReturn();

            BasBSupplierBinVM basBSupplierBinVm = Wtm.CreateVM<BasBSupplierBinVM>();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();

            // 需要用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            List<erpWhousePrirityDto> erpWhousePririties = new List<erpWhousePrirityDto>()
                {
                    new erpWhousePrirityDto(){erpWhouseNo=stockAllocateView.erpWhouseNo}
                };

            List<WmsStockDtl> resultDtlList = new List<WmsStockDtl>();

            #region 注释
            // 根据库存找出库存明细
            //wmsStockDtls = DC.Set<WmsStockDtl>()
            //.Where(x => stockCodes.Contains(x.stockCode))
            //.Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //.Where(x => x.lockFlag == "0")
            //.Where(x => x.inspectionResult == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            ////.Where(x => x.IsValid == true)
            //.Where(x => x.qty > x.occupyQty).ToList();


            // 4、批次处理
            //_vpoint = "批次处理";
            //if (null != stockAllocateView.batchNo)
            //{
            //    // 4.1 指定批次，按物料编码+批次：
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode && x.batchNo == stockAllocateView.batchNo).ToList();
            //}
            //else
            //{
            //    // 4.2 不指定批次，按物料编码
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode).ToList();
            //}

            // 库存明细表需要添加一个入库时间 inwhTime
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选需检验批次
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选不强检验入库时间，只要是此物料即可
            //wmsStockDtls.OrderBy(x => x.inwhTime); 
            #endregion


            // 单据类型为工单发料时按ERP仓库优先级查找库存
            if (stockAllocateView.docTypeCode == DictonaryHelper.BusinessCode.OutProduceOrder.GetCode())
            {
                //var totalStockQty = wmsStockDtls.Sum(t => t.qty) - wmsStockDtls.Sum(t => t.occupyQty);
                if (stockAllocateView.isDesignateErpWhouse == "0")
                {
                    #region 仓库优先级
                    if (string.IsNullOrWhiteSpace(stockAllocateView.belongDepartment))
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses);
                    }
                    else
                    {
                        erpWhouses = GetDepartmentErpWhouses(erpWhouses, stockAllocateView.belongDepartment);
                    }

                    foreach (var item in erpWhouses)
                    {
                        var erp = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == item.erpWhouseNo);
                        if (erp != null)
                        {
                            erp.priority = item.priority;
                        }
                        else
                        {
                            erpWhousePririties.Add(new erpWhousePrirityDto() { erpWhouseNo = item.erpWhouseNo, priority = item.priority });
                        }
                    }
                    //if (!string.IsNullOrWhiteSpace(stockAllocateView.belongDepartment) && stockAllocateView.belongDepartment.Contains("机器人"))
                    //{
                    //    var Whouse = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == "01B");
                    //    if (Whouse == null)
                    //    {
                    //        erpWhousePririties.Add(new erpWhousePrirityView() { erpWhouseNo = "01B", priority = 99 });
                    //    }
                    //}
                    #endregion

                    decimal totalStockQty = 0;
                    var wmsStockDtlsForOther = (from dtl in wmsStockDtls
                                                join erpWhouse in erpWhouses on dtl.erpWhouseNo equals erpWhouse.erpWhouseNo
                                                //where dtl.erpWhouseNo != stockAllocateView.erpWhouseNo
                                                orderby erpWhouse.priority ascending, dtl.projectNoBak ascending, dtl.projectNo descending
                                                select dtl)
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.materialCode == stockAllocateView.materialCode)
                    .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.OrderBy(x => x.projectNoBak)
                    //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                    //.ThenBy(x => x.projectNo)
                    //.ThenBy(x => x.inwhTime)
                    .ToList();


                    if (wmsStockDtlsForOther.Any())
                    {
                        #region 排序
                        wmsStockDtlsForOther = GetWmsStockDtlsForSort(wmsStockDtlsForOther, stockAllocateView, erpWhousePririties);
                        #endregion
                        resultDtlList = resultDtlList.Union(wmsStockDtlsForOther).ToList();
                        totalStockQty = resultDtlList.Sum(t => t.qty.Value) - resultDtlList.Sum(t => t.occupyQty.Value);
                    }
                    if (stockAllocateView.qty > totalStockQty)
                    {
                        //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                        var stockDtls = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                        resultDtlList = resultDtlList.Union(stockDtls).ToList();
                    }
                }
                else
                {
                    //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                    resultDtlList = GetWmsStockDtlsForBatch(wmsStockDtls, stockAllocateView, erpWhousePririties);
                }
            }
            else
            {
                //todo：这里指定了批次只查对应有批次的，后续数量不足时是否可分配其他批次
                resultDtlList = GetWmsStockDtlsForBatchNoProduct(wmsStockDtls, stockAllocateView, erpWhousePririties);
            }

            if (stockAllocateView.snStockDtlIdList.Count > 0)
            {
                resultDtlList = resultDtlList.Where(t => stockAllocateView.snStockDtlIdList.Contains(t.ID)).ToList();
            }


            // 非电子料用库存明细
            if (resultDtlList.Any())
            {
                foreach (var wmsStockDtl in resultDtlList)
                {
                    if (stockAllocateView.qty <= 0)
                    {
                        break;
                    }
                    WmsStockAllovateReturn allocateResult = new WmsStockAllovateReturn();
                    // 取库存明细列表的第一条记录
                    if (stockAllocateView.qty >= (wmsStockDtl.qty - wmsStockDtl.occupyQty))
                    {
                        // 不需要拣选, 该库存明细的可用数量<= 需求数量, 那就是该库存明细全分掉
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = wmsStockDtl.qty - wmsStockDtl.occupyQty;
                        allocateResult.isNeedPick = false;
                        allocateResultList.Add(allocateResult);
                    }
                    else
                    {
                        // 这个是需要拣选的，可用数量 > 需求数量, 分配结果数量 = 需求数量
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.stockCode = wmsStockDtl.stockCode;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = stockAllocateView.qty;
                        allocateResult.isNeedPick = true;
                        allocateResultList.Add(allocateResult);
                    }
                    stockAllocateView.qty = (decimal)(stockAllocateView.qty - (wmsStockDtl.qty - wmsStockDtl.occupyQty.Value));

                }
                // 出参：非电子料：库存明细ID，数量，uniicodes传null，不传[]
                //allocateResult.stockDtlId = wmsStockDtl.ID;
                //allocateResult.uniicodes = null;

                businessResult.code = ResCode.OK;
                businessResult.msg = "库存分配成功";
                businessResult.outParams = allocateResultList;
            }
            else
            {
                businessResult.code = ResCode.Error;
                businessResult.msg = "库存分配失败：未找到库存明细";
                businessResult.outParams = null;
            }

            return businessResult;
        }
        #endregion
        #endregion
        /// <summary>
        /// 库存分配
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public async Task<BusinessResult> allotMaterrialForMemoryExtend(List<WmsStockDtl> wmsStockDtls, List<CfgDepartmentErpWhouse> erpWhouses, WmsStockAllocateDto stockAllocateView)
        {
            BusinessResult businessResult = new BusinessResult();
            if (stockAllocateView.erpWhouseNo == "01B" && stockAllocateView.supBinStockList.Count > 0)
            {
                wmsStockDtls = wmsStockDtls.Where(t => stockAllocateView.supBinStockList.Contains(t.stockCode)).ToList();
            }
            if (stockAllocateView.isElecFlag && stockAllocateView.elecStockDtlIdList.Count > 0)
            {
                wmsStockDtls = wmsStockDtls.Where(t => stockAllocateView.elecStockDtlIdList.Contains(t.ID)).ToList();
            }
            //if (stockAllocateView.isElecFlag && stockAllocateView.elecStockDtlIdList.Count > 0)
            //{
            //    wmsStockDtls = wmsStockDtls.Where(t => stockAllocateView.elecStockDtlIdList.Contains(t.ID)).ToList();
            //}

            businessResult = await allotMaterrialForMemoryExtendForAll(wmsStockDtls, erpWhouses, stockAllocateView);
            return businessResult;
        }
        /// <summary>
        /// 非电子料库存分配-平库
        /// </summary>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        public BusinessResult allotNonElectronicMaterrialForExtend(WmsStockAllocateDto stockAllocateView)
        {
            // 用到的变量
            BusinessResult result = new BusinessResult();
            WmsStockAllovateReturn allotResult = new WmsStockAllovateReturn();

            BasBSupplierBinVM basBSupplierBinVm = Wtm.CreateVM<BasBSupplierBinVM>();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();

            // 需要用到的变量
            BusinessResult businessResult = new BusinessResult();
            List<WmsStockAllovateReturn> allocateResultList = new List<WmsStockAllovateReturn>();
            string _vpoint = "";
            List<string> stockCodes = new List<string>();
            List<string> stockDtlIds = new List<string>();

            // 非电子料
            // 1、排除占用库存（WMS_STOCK_DTL）
            // 区域为空不作限制

            // 先找出所有库存
            var wmsStocks = DC.Set<WmsStock>()
                .Where(x => x.errFlag.ToString() == DictonaryHelper.YesNoCode.No.GetCode())
                .Where(x => x.stockStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode()).ToList();
            //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.areaNo), x => x.areaNo == stockAllocateView.areaNo).ToList();
            wmsStocks.ForEach(x => stockCodes.Add(x.stockCode));
            if (!stockCodes.Any())
            {
                businessResult.code = ResCode.Error;
                businessResult.msg = "库存分配失败：未找到可分配库存";
                businessResult.outParams = null;
                return businessResult;
            }


            #region MyRegion
            // 根据库存找出库存明细
            //wmsStockDtls = DC.Set<WmsStockDtl>()
            //.Where(x => stockCodes.Contains(x.stockCode))
            //.Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
            //.Where(x => x.lockFlag == "0")
            //.Where(x => x.inspectionResult == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            ////.Where(x => x.IsValid == true)
            //.Where(x => x.qty > x.occupyQty).ToList();


            // 4、批次处理
            //_vpoint = "批次处理";
            //if (null != stockAllocateView.batchNo)
            //{
            //    // 4.1 指定批次，按物料编码+批次：
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode && x.batchNo == stockAllocateView.batchNo).ToList();
            //}
            //else
            //{
            //    // 4.2 不指定批次，按物料编码
            //    wmsStockDtls = wmsStockDtls.Where(x => x.materialCode == stockAllocateView.materialCode).ToList();
            //}

            // 库存明细表需要添加一个入库时间 inwhTime
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选需检验批次
            //      非电子料：  按入库时间先进先出（精确到天）
            //                分配到库存明细 暂定最早库存唯一对应的明细，拣选不强检验入库时间，只要是此物料即可
            //wmsStockDtls.OrderBy(x => x.inwhTime);

            #endregion


            List<WmsStockDtl> wmsStockDtls = new List<WmsStockDtl>();
            List<CfgErpWhouse> erpWhouses = new List<CfgErpWhouse>();
            // 单据类型为工单发料时按ERP仓库优先级查找库存
            if (stockAllocateView.docTypeCode == DictonaryHelper.BusinessCode.OutProduceOrder.GetCode())
            {
                // 是否指定ERP仓库  0：不指定 1：指定
                CfgDocTypeDtl docTypeDtl = cfgDocTypeVM.GetDocTypeDtl(stockAllocateView.docTypeCode, "IS_DESIGNATE_ERPWHOUSE");
                var param = "1";
                if (docTypeDtl != null)
                {
                    param = docTypeDtl.paramValueCode;
                }
                //var totalStockQty = wmsStockDtls.Sum(t => t.qty) - wmsStockDtls.Sum(t => t.occupyQty);
                if (param == "0")
                {
                    decimal totalStockQty = 0;


                    var wmsStockDtlsForOther = (from dtl in DC.Set<WmsStockDtl>()
                                                join erpWhouse in DC.Set<CfgErpWhouse>() on dtl.erpWhouseNo equals erpWhouse.erpWhouseNo
                                                //where dtl.erpWhouseNo != stockAllocateView.erpWhouseNo
                                                orderby erpWhouse.priority ascending, dtl.projectNoBak ascending, dtl.projectNo descending
                                                select dtl)
                    .Where(x => stockCodes.Contains(x.stockCode))
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.materialCode == stockAllocateView.materialCode)
                    .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                    .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    .OrderBy(x => x.projectNoBak)
                    .ThenBy(x => x.projectNo)
                    //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.batchNo) && x.batchNo == stockAllocateView.batchNo ? 0 : 1)
                    .ToList();
                    if (wmsStockDtlsForOther.Any())
                    {
                        wmsStockDtls = wmsStockDtls.Union(wmsStockDtlsForOther).ToList();
                        totalStockQty = wmsStockDtls.Sum(t => t.qty.Value) - wmsStockDtls.Sum(t => t.occupyQty.Value);
                    }

                    if (stockAllocateView.qty > totalStockQty)
                    {
                        var stockDtls = DC.Set<WmsStockDtl>()
                        .Where(x => stockCodes.Contains(x.stockCode))
                        .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
                        .Where(x => x.lockFlag == 0)
                        .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                        .Where(x => x.materialCode == stockAllocateView.materialCode)
                        .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                        .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                        .Where(x => x.qty > x.occupyQty)
                        //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.batchNo), t => t.batchNo == stockAllocateView.batchNo)
                        .OrderBy(x => x.projectNoBak)
                        .ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                        .ThenBy(x => x.projectNo)
                        //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.batchNo) && x.batchNo == stockAllocateView.batchNo ? 0 : 1)
                        .ToList();
                        if (stockDtls.Any())
                        {
                            wmsStockDtls = wmsStockDtls.Union(stockDtls).ToList();
                        }
                    }
                }
                else
                {
                    wmsStockDtls = DC.Set<WmsStockDtl>()
                    .Where(x => stockCodes.Contains(x.stockCode))
                    .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.materialCode == stockAllocateView.materialCode)
                    .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.batchNo), t => t.batchNo == stockAllocateView.batchNo)
                    .OrderBy(x => x.projectNoBak)
                    .ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                    .ThenBy(x => x.projectNo)
                    //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.batchNo) && x.batchNo == stockAllocateView.batchNo ? 0 : 1)
                    .ToList();
                }
            }
            else
            {
                wmsStockDtls = DC.Set<WmsStockDtl>()
                .Where(x => stockCodes.Contains(x.stockCode))
                .Where(x => x.erpWhouseNo == stockAllocateView.erpWhouseNo)
                .Where(x => x.lockFlag == 0)
                .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
                .Where(x => x.materialCode == stockAllocateView.materialCode)
                .WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.projectNo), x => x.projectNoBak == stockAllocateView.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                .Where(x => x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
                .Where(x => x.qty > x.occupyQty)
                //.WhereIf(!string.IsNullOrWhiteSpace(stockAllocateView.batchNo), t => t.batchNo == stockAllocateView.batchNo)
                .OrderBy(x => x.projectNoBak)
                .ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.projectNo) && x.projectNoBak == stockAllocateView.projectNo ? 0 : 1)
                .ThenBy(x => x.projectNo)
                //.ThenBy(x => !string.IsNullOrWhiteSpace(stockAllocateView.batchNo) && x.batchNo == stockAllocateView.batchNo ? 0 : 1)
                .ToList();
            }

            // 非电子料用库存明细
            if (wmsStockDtls.Any())
            {
                foreach (var wmsStockDtl in wmsStockDtls)
                {
                    if (stockAllocateView.qty <= 0)
                    {
                        break;
                    }
                    WmsStockAllovateReturn allocateResult = new WmsStockAllovateReturn();
                    // 取库存明细列表的第一条记录
                    if (stockAllocateView.qty >= (wmsStockDtl.qty - wmsStockDtl.occupyQty))
                    {
                        // 不需要拣选, 该库存明细的可用数量<= 需求数量, 那就是该库存明细全分掉
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = wmsStockDtl.qty - wmsStockDtl.occupyQty;
                        allocateResult.isNeedPick = false;
                        allocateResultList.Add(allocateResult);
                    }
                    else
                    {
                        // 这个是需要拣选的，可用数量 > 需求数量, 分配结果数量 = 需求数量
                        allocateResult.stockDtlId = wmsStockDtl.ID;
                        allocateResult.batchNo = stockAllocateView.batchNo;
                        allocateResult.qty = stockAllocateView.qty;
                        allocateResult.isNeedPick = true;
                        allocateResultList.Add(allocateResult);
                    }
                    stockAllocateView.qty = (decimal)(stockAllocateView.qty - (wmsStockDtl.qty - wmsStockDtl.occupyQty.Value));

                }


                // 出参：非电子料：库存明细ID，数量，uniicodes传null，不传[]
                //allocateResult.stockDtlId = wmsStockDtl.ID;
                //allocateResult.uniicodes = null;

                businessResult.code = ResCode.OK;
                businessResult.msg = "库存分配成功";
                businessResult.outParams = allocateResultList;
            }
            else
            {
                businessResult.code = ResCode.Error;
                businessResult.msg = "库存分配失败：未找到库存明细";
                businessResult.outParams = null;
            }

            return businessResult;
        }
        /// <summary>
        /// 获取近伸不是该物料的库位库存信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="allotinput"></param>
        /// <returns></returns>
        private (List<AllotStockandBinDto>, List<AllotStockandBinDto>) QueryNearStock(List<AllotStockandBinDto> input, WmsStockAllocateDto allotinput)
        {
            List<AllotStockandBinDto> filterList = new List<AllotStockandBinDto>();
            if (input.Count > 0)
            {
                var extenGroupList = input.Select(t => t.extensionGroupNo).Distinct().ToList();
                var binInfos = DC.Set<BasWBin>().AsNoTracking().Where(t => extenGroupList.Contains(t.extensionGroupNo)).ToList();
                var binList = binInfos.Select(t => t.binNo).ToList();
                var stockquery = from dtl in DC.Set<WmsStockDtl>()
                                 join stock in DC.Set<WmsStock>() on dtl.stockCode equals stock.stockCode
                                 where dtl.materialCode != allotinput.materialCode && binList.Contains(stock.binNo)
                                 select stock.binNo;
                var stockqueryResult = stockquery.ToList();
                var filterExtensonList = binInfos.Where(t => stockquery.Contains(t.binNo)).Select(t => t.extensionGroupNo).ToList();
                filterList = input.Where(t => filterExtensonList.Contains(t.extensionGroupNo)).ToList();
                input = input.Except(filterList).ToList();
            }

            return (input, filterList);



        }

        public async Task<List<AllotStockandBinDto>> QueryEmptyStock(WmsStockAllocateDto input)
        {
            List<AllotStockandBinDto> result = new List<AllotStockandBinDto>();

            if (input.allotType == 0)
            {
                var filterBinList = await GetErrBin();

                int count = Convert.ToInt32(input.qty.Value);
                int remandCount = count;
                var query = (from stockDtl in DC.Set<WmsStockDtl>()
                             join stock in DC.Set<WmsStock>() on stockDtl.stockCode equals stock.stockCode
                             join bin in DC.Set<BasWBin>() on stock.binNo equals bin.binNo
                             join roadway in DC.Set<BasWRoadway>() on bin.roadwayNo equals roadway.roadwayNo
                             where stockDtl.stockDtlStatus == 50 && stockDtl.occupyQty == 0 && stockDtl.materialCode == input.materialCode && stockDtl.lockFlag != 1
                                   && stock.stockStatus == 50 && stock.loadedType == 0
                                   && bin.binErrFlag == "0" && bin.usedFlag == 1 && bin.isOutEnable == 1 && bin.binType == "ST" && !filterBinList.Contains(bin.binNo)
                                   && roadway.usedFlag == 1
                             select new AllotStockandBinDto()
                             {
                                 stockCode = stock.stockCode,
                                 palletBarcode = stock.palletBarcode,
                                 binNo = stock.binNo,
                                 roadwayNo = stock.roadwayNo,
                                 extensionGroupNo = bin.extensionGroupNo,
                                 binPriority = bin.binPriority,
                                 extensionIdx = bin.extensionIdx,
                                 regionNo = stock.regionNo,
                                 loadedType = stock.loadedType.Value
                             }).WhereIf(input.roadwayList.Any(), t => input.roadwayList.Contains(t.roadwayNo)).OrderBy(t => t.extensionIdx);
                //是否考虑内存和速度
                #region 直接取
                // result = query.Skip(0).Take(count).ToList();
                #endregion

                #region 清库位原则
                var queryResult = await query.ToListAsync();
                if (queryResult.Count > 0)
                {

                    var groupStock = queryResult.GroupBy(t => new { t.extensionGroupNo });
                    //找近伸不为空，远伸为空的
                    var Stock = groupStock.Where(t => t.Count() == 1 && t.Any(t => t.extensionIdx == 1)).SelectMany(t => t).ToList();
                    if (Stock.Any())
                    {
                        if (Stock.Count > remandCount)
                        {
                            Stock = Stock.Skip(0).Take(remandCount).ToList();
                            result.AddRange(Stock);
                        }
                        else
                        {
                            result.AddRange(Stock);
                        }
                        remandCount = count - result.Count;
                        if (result.Count >= count)
                        {
                            return result;
                        }
                    }
                    //近伸是其他物料的
                    List<AllotStockandBinDto> filterList = new List<AllotStockandBinDto>();

                    //找近伸为空，远伸有库存的
                    Stock = groupStock.Where(t => t.Count() == 1 && t.Any(t => t.extensionIdx == 2)).SelectMany(t => t).ToList();
                    if (Stock.Any())
                    {
                        var filterResult = QueryNearStock(Stock, input);
                        Stock = filterResult.Item1;
                        filterList = filterResult.Item2;
                        if (Stock.Count > remandCount)
                        {
                            Stock = Stock.Skip(0).Take(remandCount).ToList();
                            result.AddRange(Stock);
                        }
                        else
                        {
                            result.AddRange(Stock);
                        }
                        remandCount = count - result.Count;
                        if (result.Count >= count)
                        {
                            return result;
                        }

                    }


                    //找近伸的
                    Stock = groupStock.Where(t => t.Count() == 2).SelectMany(t => t).ToList();
                    if (Stock.Any())
                    {
                        var nearStock = Stock.Where(t => t.extensionIdx == 1).ToList();
                        if (nearStock.Any())
                        {
                            foreach (var item in nearStock)
                            {
                                if (nearStock.Count > remandCount)
                                {
                                    nearStock = nearStock.Skip(0).Take(remandCount).ToList();
                                    result.AddRange(nearStock);
                                }
                                else
                                {
                                    result.AddRange(nearStock);
                                }
                                remandCount = count - result.Count;
                                if (result.Count >= count)
                                {
                                    return result;
                                }
                            }
                        }

                        var farStock = Stock.Where(t => t.extensionIdx == 2).ToList();
                        if (farStock.Any())
                        {
                            foreach (var item in farStock)
                            {
                                if (farStock.Count > remandCount)
                                {
                                    farStock = farStock.Skip(0).Take(remandCount).ToList();
                                    result.AddRange(farStock);
                                }
                                else
                                {
                                    result.AddRange(farStock);
                                }
                                remandCount = count - result.Count;
                                if (result.Count >= count)
                                {
                                    return result;
                                }
                            }
                        }

                    }

                    if (filterList.Any())
                    {

                        foreach (var item in filterList)
                        {
                            if (filterList.Count > remandCount)
                            {
                                filterList = filterList.Skip(0).Take(remandCount).ToList();
                                result.AddRange(filterList);
                            }
                            else
                            {
                                result.AddRange(filterList);
                            }
                            remandCount = count - result.Count;
                            if (result.Count >= count)
                            {
                                return result;
                            }
                        }
                    }
                    #region old
                    //var farStock = queryResult.Where(t => t.extensionIdx == 2).ToList();
                    //if (farStock.Count > 0)
                    //{
                    //    //把浅位有库存的过滤掉
                    //    farStock = QueryNearStock(farStock);
                    //    if (farStock.Count > 0)
                    //    {
                    //        if (farStock.Count > remandCount)
                    //        {
                    //            farStock = farStock.Skip(0).Take(remandCount).ToList();
                    //            result.AddRange(farStock);
                    //        }
                    //        else
                    //        {
                    //            result.AddRange(farStock);
                    //        }

                    //        remandCount = count - result.Count;
                    //        if (result.Count >= count)
                    //        {
                    //            return result;
                    //        }
                    //    }

                    //}

                    //var nearStock = queryResult.Where(t => t.extensionIdx == 1).ToList();
                    //if (nearStock.Count > 0)
                    //{
                    //    if (nearStock.Count > remandCount)
                    //    {
                    //        nearStock = nearStock.Skip(0).Take(remandCount).ToList();
                    //        result.AddRange(nearStock);
                    //    }
                    //    else
                    //    {
                    //        result.AddRange(nearStock);
                    //    }
                    //    remandCount = count - result.Count;
                    //    if (result.Count >= count)
                    //    {
                    //        return result;
                    //    }
                    //}

                    //if (queryResult.Count > remandCount)
                    //{
                    //    queryResult = queryResult.Skip(0).Take(remandCount).ToList();
                    //    result.AddRange(nearStock);
                    //}
                    //else
                    //{
                    //    result.AddRange(queryResult);
                    //}
                    //remandCount = count - result.Count;
                    //if (result.Count >= count)
                    //{
                    //    return result;
                    //} 
                    #endregion

                }
                #endregion

            }
            else
            {
                var query = (from stockDtl in DC.Set<WmsStockDtl>()
                             join stock in DC.Set<WmsStock>() on stockDtl.stockCode equals stock.stockCode
                             join bin in DC.Set<BasWBin>() on stock.binNo equals bin.binNo
                             join roadway in DC.Set<BasWRoadway>() on bin.roadwayNo equals roadway.roadwayNo
                             where stockDtl.stockDtlStatus == 50 && stockDtl.occupyQty == 0 && stockDtl.materialCode == input.materialCode && stockDtl.lockFlag != 1
                                   && stock.stockStatus == 50 && stock.loadedType == 0 && input.palletBarcodeList.Contains(stock.palletBarcode)
                                   && bin.binErrFlag == "0" && bin.usedFlag == 1 && bin.isOutEnable == 1 && bin.binType == "ST"
                                   && roadway.usedFlag == 1
                             select new AllotStockandBinDto()
                             {
                                 stockCode = stock.stockCode,
                                 palletBarcode = stock.palletBarcode,
                                 binNo = stock.binNo,
                                 roadwayNo = stock.roadwayNo,
                                 extensionGroupNo = bin.extensionGroupNo,
                                 extensionIdx = bin.extensionIdx,
                                 regionNo = stock.regionNo,
                                 loadedType = stock.loadedType.Value
                             }).WhereIf(input.roadwayList.Any(), t => input.roadwayList.Contains(t.roadwayNo));

                result = await query.ToListAsync();
            }
            return result;

        }

        public async Task<(bool, bool, WmsStock)> isNeedMove(AllotStockandBinDto input)
        {
            bool isCanMove = true;
            bool isNeedMove = false;
            WmsStock stockInfo = null;
            try
            {
                //if (input.extensionIdx == 1)
                //{
                //    return (true, false);
                //}
                var nearBinInfo = await DC.Set<BasWBin>().Where(t => t.roadwayNo == input.roadwayNo && t.regionNo == input.regionNo && t.extensionGroupNo == input.extensionGroupNo && t.extensionIdx == 1).FirstOrDefaultAsync();
                if (nearBinInfo != null)
                {
                    stockInfo = await DC.Set<WmsStock>().Where(t => t.binNo == nearBinInfo.binNo).FirstOrDefaultAsync();
                    if (stockInfo != null)
                    {
                        if (stockInfo.stockStatus < 50)
                        {
                            isCanMove = false;
                        }
                        else if (stockInfo.stockStatus == 50)
                        {
                            var stockDtlInfo = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == stockInfo.stockCode && t.occupyQty > 0).FirstOrDefaultAsync();
                            if (stockDtlInfo == null)
                            {
                                isNeedMove = true;
                            }
                            else
                            {
                                isCanMove = false;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return (isCanMove, isNeedMove, stockInfo);
        }

        /// <summary>
        /// 空托出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<BusinessResult> AllotEmptyStock(EmptyStockAllotDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "空托/空箱出库:";
            try
            {
                #region 校验
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.palletTypeCode))
                {
                    msg = $"{desc}入参载体类型为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.locNo))
                {
                    msg = $"{desc}入参目标站台为空";
                    return result.Error(msg);
                }
                if (input.allotType != 0 && input.allotType != 1)
                {
                    msg = $"{desc}入参分配类型【{input.allotType}】不在取值范围内【0，1】";
                    return result.Error(msg);
                }
                if (input.allotType == 0)
                {
                    if (input.qty == null || input.qty <= 0)
                    {
                        msg = $"{desc}入参分配类型【{input.allotType}】为自动分配，需求数量不能为空或者小于0";
                        return result.Error(msg);
                    }
                }
                else
                {
                    if (input.palletBarcodeList == null || input.palletBarcodeList.Count <= 0)
                    {
                        msg = $"{desc}入参分配类型【{input.allotType}】为手动分配，勾选载体集合不能为空";
                        return result.Error(msg);
                    }
                    input.palletBarcodeList = input.palletBarcodeList.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t).Distinct().ToList();
                    if (input.palletBarcodeList.Count == 0)
                    {
                        msg = $"{desc}入参分配类型【{input.allotType}】为手动分配，勾选载体集合经过数据筛选后不能为空";
                        return result.Error(msg);
                    }

                }
                List<string> roadwayList = new List<string>();
                if (!string.IsNullOrWhiteSpace(input.roadwayNos))
                    roadwayList = input.roadwayNos.Split(',').ToList();

                var docTypeInfo = await DC.Set<CfgDocType>().Where(t => t.businessCode == "EMPTY_OUT").FirstOrDefaultAsync();
                if (docTypeInfo == null)
                {
                    msg = $"{desc}未配置空托（空箱）出库单据类型";
                    return result.Error(msg);
                }

                var locInfo = await DC.Set<BasWLoc>().Where(t => t.locNo == input.locNo && t.locTypeCode.Contains("OUT")).FirstOrDefaultAsync();
                if (locInfo == null)
                {
                    msg = $"{desc}请求站台号【{input.locNo}】未找到对应出库站台记录数据";
                    return result.Error(msg);
                }
                var palletTypeInfo = await DC.Set<BasWPalletType>().AsNoTracking().Where(t => t.palletTypeCode == input.palletTypeCode).FirstOrDefaultAsync();
                if (palletTypeInfo == null)
                {
                    msg = $"{desc}入参载体类型【{input.palletTypeCode}】未找到对应载体类型信息";
                    return result.Error(msg);
                }
                var matInfo = await DC.Set<BasBMaterial>().AsNoTracking().Where(t => t.MaterialCode == input.palletTypeCode).FirstOrDefaultAsync();
                if (matInfo == null)
                {
                    msg = $"{desc}入参载体类型【{input.palletTypeCode}】未找到对应物料记录信息";
                    return result.Error(msg);
                }

                #region 卡控数量
                var outTaskInfos = await DC.Set<WmsTask>().Where(t => t.wmsTaskType == "EMPTY_OUT" && t.taskStatus < 90).ToListAsync();
                outTaskInfos = outTaskInfos.Where(t => Regex.IsMatch(t.palletBarcode, palletTypeInfo.checkFormula) == true).ToList();
                if (docTypeInfo.taskMaxQty == null)
                {
                    docTypeInfo.taskMaxQty = 5;
                }
                int remainQty = docTypeInfo.taskMaxQty.Value - outTaskInfos.Count;
                if (input.allotType == 0)
                {
                    if (remainQty < input.qty)
                    {
                        msg = $"{desc}入参载体类型【{input.palletTypeCode}】可出库数量为【{docTypeInfo.taskMaxQty}】(根据空托出库单据类型任务数量计算，未设置默认5)，已有任务数量【{outTaskInfos.Count}】,实际需求数量为【{input.qty}】";
                        return result.Error(msg);
                    }
                }
                else if (input.allotType == 1)
                {
                    if (remainQty < input.palletBarcodeList.Count)
                    {
                        msg = $"{desc}入参载体类型【{input.palletTypeCode}】可出库数量为【{docTypeInfo.taskMaxQty}】(根据空托出库单据类型任务数量计算，未设置默认5)，已有任务数量【{outTaskInfos.Count}】,实际需求数量为【{input.palletBarcodeList.Count}】";
                        return result.Error(msg);
                    }
                }
                #endregion
                #endregion
                //查找库存
                WmsStockAllocateDto allocateView = new WmsStockAllocateDto()
                {

                    allotType = input.allotType,
                    materialCode = matInfo.MaterialCode,
                    qty = input.qty,
                    palletBarcodeList = input.palletBarcodeList,
                    roadwayList = roadwayList,
                };

                var stockInfo = await QueryEmptyStock(allocateView);
                if (stockInfo == null || stockInfo.Count == 0)
                {
                    msg = $"{desc}入参载体类型【{input.palletTypeCode}】对应物料【{matInfo.MaterialCode}】库存不足";
                    return result.Error(msg);
                }
                stockInfo.ForEach(t =>
                {
                    t.docTypeCode = docTypeInfo.docTypeCode;
                    t.allotType = input.allotType;
                    t.invoker = input.invoker;
                    t.locNo = input.locNo;
                    t.docPriority = docTypeInfo.docPriority ?? 99;
                    t.businessCode = docTypeInfo.businessCode;
                });
                stockInfo = stockInfo.OrderBy(t => t.extensionGroupNo).ThenBy(t => t.extensionIdx).ToList();
                //事务处理
                //占用库存，生成出库记录、下架单和任务
                foreach (var item in stockInfo)
                {

                    if (item.extensionIdx == 1)
                    {
                        var itemResult = await AllotEmptyStockForPallet(item);
                        msg = msg + itemResult.msg;
                    }
                    else
                    {
                        //todo:是否需要移库
                        (bool, bool, WmsStock) check = await isNeedMove(item);
                        if (check.Item1 == false)
                        {
                            continue;
                        }
                        else
                        {
                            if (check.Item2 == false)
                            {
                                var itemResult = await AllotEmptyStockForPallet(item);
                                msg = msg + itemResult.msg;
                            }
                            else
                            {
                                if (check.Item3 != null)
                                {
                                    WmsTaskVM wmsTaskVM = Wtm.CreateVM<WmsTaskVM>();
                                    var moveResult = await wmsTaskVM.MoveHandle(check.Item3, input.invoker);
                                    if (moveResult.code == ResCode.OK)
                                    {
                                        var itemResult = await AllotEmptyStockForPallet(item);
                                        msg = msg + itemResult.msg;
                                    }
                                    else
                                    {
                                        msg = msg + moveResult.msg;
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                }
                result.code = ResCode.OK;
                result.msg = $"执行完成：" + msg;
            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;
        }

        public async Task<BusinessResult> AllotEmptyStockForPallet(AllotStockandBinDto input)
        {
            BusinessResult result = new BusinessResult();

            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {
                    var stockInfo = await ((DbContext)DC).Set<WmsStock>().Where(t => t.stockCode == input.stockCode && t.palletBarcode == input.palletBarcode && t.loadedType == input.loadedType && t.stockStatus == 50).FirstOrDefaultAsync();
                    if (stockInfo == null)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】可用在库库存；");
                    }
                    var regionInfo = await ((DbContext)DC).Set<BasWRegion>().Where(t => t.regionNo == stockInfo.regionNo).FirstOrDefaultAsync();
                    var stockDtlInfos = await ((DbContext)DC).Set<WmsStockDtl>().Where(t => t.stockDtlStatus == 50 && t.stockCode == stockInfo.stockCode).ToListAsync();
                    if (stockDtlInfos.Count == 0)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】可用在库库存明细；");
                    }
                    var taskInfo = await ((DbContext)DC).Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                    if (taskInfo != null)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】存在未完成任务【{taskInfo.wmsTaskNo}】；");
                    }
                    var putdownInfo = await ((DbContext)DC).Set<WmsPutdown>().Where(t => t.palletBarcode == input.palletBarcode && t.putdownStatus < 90).FirstOrDefaultAsync();
                    if (putdownInfo != null)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】存在未完成下架单【{putdownInfo.putdownNo}】；");
                    }
                    WmsPutdownVM wmsPutdownVM = Wtm.CreateVM<WmsPutdownVM>();
                    SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                    List<string> pickupList = new List<string>() { "PDA", "PTL" };
                    List<WmsPutdown> addwmsPutdowns = new List<WmsPutdown>();
                    List<WmsPutdownDtl> addwmsPutdownDtls = new List<WmsPutdownDtl>();
                    //占用库存
                    #region 占用库存
                    stockInfo.stockStatus = 70;
                    stockInfo.UpdateBy = input.invoker;
                    stockInfo.UpdateTime = DateTime.Now;

                    stockDtlInfos.ForEach(t =>
                    {
                        t.occupyQty = t.qty;
                        t.stockDtlStatus = 70;
                        t.UpdateBy = input.invoker;
                        t.UpdateTime = DateTime.Now;
                    });

                    #endregion

                    #region 生成出库记录
                    bool isPk = true;
                    if (regionInfo != null && !pickupList.Contains(regionInfo.pickupMethod))
                    {
                        isPk = false;
                    }
                    //生成出库记录
                    WmsOutInvoiceRecord wmsOutInvoiceRecord =
                    BuildOutInvoiceRecord(
                        stockInfo,
                        stockDtlInfos.FirstOrDefault(),
                        "",
                        input.allotType,
                        0,
                      0,
                        input.invoker,
                        1,
                        input.docTypeCode,
                        false,
                        isPk,
                        input.locNo);

                    #endregion

                    #region 生成下架单
                    string putdownNo = sysSequenceVM.GetSequence(DictonaryHelper.SequenceCode.WmsPutdownNo.GetCode());
                    WmsPutdown wmsPutDown = wmsPutdownVM.BuildWmsPutDownForMerge(putdownNo, stockInfo, regionInfo?.pickupMethod, input.docTypeCode, "", input.invoker);
                    wmsPutDown.putdownStatus = 31;
                    stockDtlInfos.ForEach(t =>
                    {
                        var downDtl = wmsPutdownVM.BuildWmsPutDownDtl(t, putdownNo, input.invoker);
                        downDtl.putdownDtlStatus = 31;
                        addwmsPutdownDtls.Add(downDtl);
                    });
                    #endregion

                    #region 生成任务
                    string wmstaskNo = sysSequenceVM.GetSequence(DictonaryHelper.SequenceCode.wmsTaskNo.GetCode());
                    WmsTask wmsTask = BuildEmptyWmsTask(wmstaskNo, stockInfo, input.locNo, input.businessCode, "OUT", input.invoker, input.docPriority);
                    #endregion

                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                    await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(stockDtlInfos);
                    await ((DbContext)DC).Set<WmsOutInvoiceRecord>().AddAsync(wmsOutInvoiceRecord);
                    await ((DbContext)DC).Set<WmsPutdown>().AddAsync(wmsPutDown);
                    await ((DbContext)DC).Set<WmsPutdownDtl>().AddRangeAsync(addwmsPutdownDtls);
                    await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTask);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    await tran.CommitAsync();
                    result.code = ResCode.OK;
                    result.msg = $"托盘【{input.palletBarcode}】出库成功；";

                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return result.Error($"{ex.Message};");
                }
            }
            return result;
        }
    }
}
