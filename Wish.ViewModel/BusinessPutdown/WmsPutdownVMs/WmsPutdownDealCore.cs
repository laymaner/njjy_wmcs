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
using Z.BulkOperations;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs;
using Wish.ViewModel.BusinessOut.WmsOutWaveVMs;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;

namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownVM
    {

        public async Task<List<supplyBinDto>> GetSupplyBin(List<string> supplyNoList)
        {
            List<supplyBinDto> result = new List<supplyBinDto>();
            var supplyErpBinInfos =await DC.Set<BasBSupplierBin>().Where(t => supplyNoList.Contains(t.supplierCode)).AsNoTracking().ToListAsync();
            var group = supplyErpBinInfos.GroupBy(t => t.supplierCode);
            foreach (var supplyErpBinInfo in group)
            {
                supplyBinDto supplyBinView = new supplyBinDto()
                {
                    supplierCode = supplyErpBinInfo.Key,
                    erpBinList = supplyErpBinInfo.Select(t => t.binNo).Distinct().ToList()
                };
                result.Add(supplyBinView);
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

        /// <summary>
        /// 单任务分配
        /// </summary>
        /// <param name="autoAllocateParam"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<BusinessResult> OutAllocateByInvoice(AutoAllocateParam autoAllocateParam, string invoker)
        {
            BusinessResult result = new BusinessResult();

            WmsOutInvoiceVM wmsOutInvoiceVM = Wtm.CreateVM<WmsOutInvoiceVM>();
            WmsOutWaveVM wmsOutWaveVM = Wtm.CreateVM<WmsOutWaveVM>();
            WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
            int allotDltCount = 0;
            int allAllotDltCount = 0;
            var hasParentTranslation = false;
            try
            {
                var allotType = ""; // 分配类型
                bool isAutoAllocate = true;
                if (isAutoAllocate)
                {
                    allotType = AllotType.Auto.GetCode();
                }

                #region 循环调用自动分配

                //var outWaveInfo = DC.Set<WmsOutWave>().Where(t => t.waveNo == waveAllocateParam.waveNo).FirstOrDefault();
                var outInvoices = await DC.Set<WmsOutInvoice>().Where(t => t.invoiceNo == autoAllocateParam.inVoiceNo && t.invoiceStatus < 90).ToListAsync();
                var outInvoiceDtls = await DC.Set<WmsOutInvoiceDtl>().Where(t => t.invoiceNo == autoAllocateParam.inVoiceNo && t.invoiceDtlStatus < 29).ToListAsync();

                #region 防止空值处理

                foreach (var item in outInvoiceDtls)
                {
                    if (item.erpUndeliverQty == null)
                        item.erpUndeliverQty = 0;
                    if (item.allotQty == null)
                        item.allotQty = 0;
                    if (item.completeQty == null)
                        item.completeQty = 0;
                    if (item.putdownQty == null)
                        item.putdownQty = 0;
                }

                #endregion

                outInvoiceDtls = outInvoiceDtls.Where(t => t.erpUndeliverQty > (t.allotQty - t.completeQty)).ToList();

                //var outInvoiceDtls = DC.Set<WmsOutInvoiceDtl>().Where(t => t.invoiceNo == autoAllocateParam.inVoiceNo && (t.invoiceQty > t.allotQty)).ToList().Where(x => Int32.Parse(x.invoiceDtlStatus) < 29).ToList();
                if (outInvoices.Count < 1)
                {
                    result.code = ResCode.Error;
                    result.msg = "未找到可分配的发货单据";
                    return result;
                }

                if (outInvoiceDtls.Count < 1)
                {
                    result.code = ResCode.Error;
                    result.msg = "未找到可分配的发货单明细";
                    return result;
                }

                string docTypeCode = outInvoices.FirstOrDefault().docTypeCode;

                #region 单据类型参数处理

                CfgDocTypeDtl docTypeDtl = await cfgDocTypeVM.GetDocTypeDtlAsync(docTypeCode, "IS_DESIGNATE_ERPWHOUSE"); //是否指定ERP仓库
                var isDesignateErpWhouse = "1";
                if (docTypeDtl != null)
                {
                    isDesignateErpWhouse = docTypeDtl.paramValueCode;
                }

                bool isLimitSupplyBin = false;
                docTypeDtl = await cfgDocTypeVM.GetDocTypeDtlAsync(docTypeCode, "ISLIMIT_SUPPLYBIN"); //是否卡控供应商库位
                if (docTypeDtl != null)
                {
                    isLimitSupplyBin = docTypeDtl.paramValueCode == "1" ? true : false;
                }

                #endregion

                var supplyNoList = outInvoiceDtls.Select(t => t.supplierCode).Distinct().ToList();
                var projectNoList = outInvoices.Select(t => t.projectNo).Distinct().ToList();
                var erpWhouseList = outInvoiceDtls.Select(t => t.erpWhouseNo).Distinct().ToList();
                allAllotDltCount = outInvoiceDtls.Count;
                if (outInvoiceDtls.Count > 0)
                {
                    List<string> supplyBinNoList = await GetSupplyBinList();
                    List<supplyBinDto> supplyErpBinList = await GetSupplyBin(supplyNoList);
                    List<string> supBinStockList = new List<string>();
                    var matList = outInvoiceDtls.Select(t => t.materialCode).Distinct().ToList();
                    var matInfos = await DC.Set<BasBMaterial>().Where(t => matList.Contains(t.MaterialCode)).AsNoTracking().ToListAsync();
                    var matCateList = matInfos.Select(t => t.MaterialCategoryCode).Distinct().ToList();
                    var matCateInfos = await DC.Set<BasBMaterialCategory>().Where(t => matCateList.Contains(t.materialCategoryCode)).AsNoTracking().ToListAsync();
                    var regionInfos = await DC.Set<BasWRegion>().AsNoTracking().ToListAsync();
                    List<CfgDepartmentErpWhouse> erpWhouses = await DC.Set<CfgDepartmentErpWhouse>().ToListAsync();
                    var cfgerpWhouseList = erpWhouses.Select(t => t.erpWhouseNo).ToList();
                    // List<allotSnView> allotSnViewList = outInvoiceDtls.Where(t => !string.IsNullOrWhiteSpace(t.originalSn)).Select(t => new allotSnView() { materialCode = t.materialCode, orginSn = t.originalSn }).Distinct().ToList();
                    WmsStockAllocateForMultiMatDto forMultiMatView = new WmsStockAllocateForMultiMatDto()
                    {
                        materialCodeList = matList,
                        projectNoList = projectNoList,
                        erpWhouseList = cfgerpWhouseList.Union(erpWhouseList).ToList(),
                        docTypeCode = docTypeCode,
                        supplyErpBinList = supplyBinNoList,
                        isLimitSupplyBin = isLimitSupplyBin,
                        regionNos = autoAllocateParam.regionNos,
                    };
                    //根据物料集合、项目号一次性查找库存
                    var allStockDtls = await wmsStockVM.allotNonElectronicMaterrialForMatListExtend(forMultiMatView);
                    List<WmsStockAllovateSnReturn> snStockAllotList = new List<WmsStockAllovateSnReturn>();

                    List<WmsStockAllovateElecReturn> elecStockAllotList = new List<WmsStockAllovateElecReturn>();

                    if (allStockDtls.Count > 0)
                    {
                        var stockMatList = allStockDtls.Select(t => t.materialCode).Distinct().ToList();
                        //过滤有库存的发货单明细
                        var outInvoiceDtlForhasStock = outInvoiceDtls.Where(t => stockMatList.Contains(t.materialCode)).ToList();
                        if (outInvoiceDtlForhasStock.Count > 0)
                        {
                            //筛选电子料
                            var matIsElecInfo = GetMatElecInfo(matInfos, matCateInfos, stockMatList);
                            //成品物料

                            #region 成品物料库存

                            var matIsProList = matIsElecInfo.Where(t => t.isProductFlag).Select(t => t.materialCode).ToList();
                            List<allotSnDto> allotSnViewList = outInvoiceDtlForhasStock.Where(t => matIsProList.Contains(t.materialCode) && !string.IsNullOrWhiteSpace(t.originalSn)).Select(t => new allotSnDto() { materialCode = t.materialCode, orginSn = t.originalSn }).Distinct().ToList();
                            if (allotSnViewList.Count > 0)
                            {
                                //获取sn对应库存明细id
                                snStockAllotList = await wmsStockVM.allotNonElectronicMaterrialForMatListExtendSn(allStockDtls, allotSnViewList: allotSnViewList);
                            }

                            #endregion

                            //电子料

                            #region 电子料库存排序

                            //var elecMatList = matIsElecInfo.Where(t => t.isElecFlag).Select(t => t.materialCode).ToList();
                            //if (elecMatList.Count > 0)
                            //{
                            //    //获取电子料对应库存明细id
                            //    elecStockAllotList = wmsStockVM.allotElectronicMaterrialForMatListExtend(allStockDtls, elecMatList);
                            //    //委外出库和调拨出库时校验供应商库位
                            //}

                            #endregion

                            #region 所有物料

                            elecStockAllotList = await wmsStockVM.allotUniiMaterrialForMatListExtend(allStockDtls, matIsElecInfo);

                            #endregion

                            var groupDtls = outInvoiceDtlForhasStock.GroupBy(t => new { t.projectNo, t.supplierCode, t.batchNo, t.materialCode, t.erpWhouseNo, t.originalSn, t.belongDepartment });
                            foreach (var item in groupDtls)
                            {
                                var allStockDtlForMats = allStockDtls.Where(t => t.materialCode == item.Key.materialCode).ToList();
                                if (allStockDtlForMats.Count == 0)
                                {
                                    continue;
                                }

                                //汇总数量
                                bool isElecFlag = false; //电子料标识
                                bool isProductFlag = false; //成品
                                var matInfo = matIsElecInfo.FirstOrDefault(t => t.materialCode == item.Key.materialCode);
                                if (matInfo != null)
                                {
                                    isElecFlag = matInfo.isElecFlag;
                                    isProductFlag = matInfo.isProductFlag;
                                }

                                WmsStockAllocateDto allocateView = new WmsStockAllocateDto()
                                {
                                    projectNo = item.Key.projectNo,
                                    erpWhouseNo = item.Key.erpWhouseNo,
                                    batchNo = item.Key.batchNo,
                                    materialCode = item.Key.materialCode,
                                    qty = (decimal)item.Sum(t => t.erpUndeliverQty) - ((decimal)item.Sum(t => t.allotQty.Value) - (decimal)item.Sum(t => t.completeQty)),
                                    docTypeCode = docTypeCode,
                                    isDesignateErpWhouse = isDesignateErpWhouse,
                                    isLimitSupplyBin = isLimitSupplyBin,
                                    orginSn = item.Key.originalSn,
                                    isElecFlag = isElecFlag,
                                    belongDepartment = item.Key.belongDepartment
                                };

                                #region 成品处理

                                if (!string.IsNullOrWhiteSpace(item.Key.originalSn) && isProductFlag)
                                {
                                    var dtlSn = snStockAllotList.Where(t => t.orginalSn == item.Key.originalSn).FirstOrDefault();
                                    if (dtlSn != null && dtlSn.subSnList.Count > 0)
                                    {
                                        allocateView.snStockDtlIdList = dtlSn.subSnList.Where(t => t.stockDtlId != null).Select(t => (long)t.stockDtlId).ToList();
                                    }
                                    else
                                    {
                                        //该sn号没有库存
                                        continue;
                                    }
                                }

                                #endregion

                                #region 供应商处理

                                if (supplyErpBinList.Count > 0)
                                {
                                    var curSupplyBin = supplyErpBinList.FirstOrDefault(t => t.supplierCode == item.Key.supplierCode);
                                    if (curSupplyBin != null)
                                    {
                                        if (curSupplyBin.erpBinList.Count > 0)
                                        {
                                            List<string> docTypeList = new List<string>()
                                            {
                                                BusinessCode.OutOutSourcePick.GetCode(),
                                                BusinessCode.OutTransferRequest.GetCode(),
                                            };
                                            if (isLimitSupplyBin && allocateView.erpWhouseNo == "01B")
                                            {
                                                supBinStockList =await wmsStockVM.GetSupplyErpBinStock(curSupplyBin.erpBinList);
                                                allocateView.supBinStockList.AddRange(supBinStockList);
                                            }
                                            //supBinStockList = wmsStockVM.GetSupplyErpBinStock(curSupplyBin.erpBinList);
                                            //allocateView.supBinStockList.AddRange(supBinStockList);
                                        }
                                    }
                                }

                                #endregion

                                //电子料

                                #region 电子料处理

                                //if (isElecFlag)
                                //{

                                //    var elecStockDlts = elecStockAllotList.Where(t => t.materialCode == item.Key.materialCode).FirstOrDefault();
                                //    if (elecStockDlts != null)
                                //    {
                                //        allocateView.elecStockDtlIdList = elecStockDlts.stockDtlIdList;
                                //    }
                                //    else
                                //    {
                                //        //该电子物料号没有库存
                                //        continue;
                                //    }
                                //}

                                #endregion

                                #region 所有物料

                                var elecStockDlts = elecStockAllotList.Where(t => t.materialCode == item.Key.materialCode).FirstOrDefault();
                                if (elecStockDlts != null)
                                {
                                    allocateView.elecStockDtlIdList = elecStockDlts.stockDtlIdList;
                                }
                                else
                                {
                                    //该电子物料号没有库存
                                    continue;
                                }

                                #endregion


                                var allotResult = await wmsStockVM.allotMaterrialForMemoryExtend(allStockDtlForMats, erpWhouses, allocateView);
                                if (allotResult.code == ResCode.OK)
                                {
                                    List<WmsStockAllovateReturn> allocateResultList = (List<WmsStockAllovateReturn>)allotResult.outParams;
                                    if (allocateResultList == null || allocateResultList.Count == 0)
                                    {
                                        continue;
                                    }

                                    //分配出库单，生成出库记录,占用库存
                                    var groupInvoiceDtls = item.OrderBy(t => t.batchNo).ToList();
                                    var invoiceList = groupInvoiceDtls.Select(t => t.invoiceNo).ToList();
                                    var groupInvoices = outInvoices.Where(t => invoiceList.Contains(t.invoiceNo)).ToList();
                                    if (isElecFlag)
                                    {
                                        //默认空
                                    }
                                    else
                                    {
                                        foreach (var allotItem in allocateResultList)
                                        {
                                            allotItem.pickTaskNo = sysSequenceVM.GetSequence(SequenceCode.PickNo.GetCode());
                                        }
                                    }

                                    result = await OutAllocateByInvoiceDtlsInfo(allotType, isElecFlag, groupInvoices, groupInvoiceDtls, regionInfos, allocateResultList, invoker);
                                    if (result.code == ResCode.OK)
                                    {
                                        try
                                        {
                                            if (result.outParams != null)
                                            {
                                                int curCount = Convert.ToInt32(result.outParams);
                                                allotDltCount += curCount;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            return result.Error("未找到可用库存");
                        }
                    }
                    else
                    {
                        return result.Error("未找到可用库存");
                    }
                }
                else
                {
                    result.code = ResCode.Error;
                    result.msg = "未找到可分配的发货单明细";
                    return result;
                }
                //todo更新一下波次
                //outInvoiceDtls = DC.Set<WmsOutInvoiceDtl>().Where(t => t.waveNo == waveAllocateParam.waveNo).ToList();
                //if (outInvoiceDtls.Count > 0)
                //{
                //    var dtl = outInvoiceDtls.FirstOrDefault(t => t.invoiceDtlStatus == "21" || t.invoiceDtlStatus == "0");
                //    if (dtl != null)
                //    {
                //        outWaveInfo.waveStatus = "11";
                //        outWaveInfo.updateBy = invoker;
                //        outWaveInfo.updateTime = DateTime.Now;
                //    }
                //    else
                //    {
                //        outWaveInfo.waveStatus = "22";
                //        outWaveInfo.updateBy = invoker;
                //        outWaveInfo.updateTime = DateTime.Now;
                //    }
                //    ((DbContext)DC).BulkUpdate(new WmsOutWave[] { outWaveInfo });
                //    ((DbContext)DC).BulkSaveChanges();
                //}

                #endregion
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (result.code == ResCode.OK)
                {
                    if (allAllotDltCount > 0)
                    {
                        result.msg = $"成功分配{allotDltCount}条明细，未分配{allAllotDltCount - allotDltCount}条明细";
                    }
                }
            }

            return result;
        }


        private async Task<BusinessResult> OutAllocateByInvoiceDtlsInfo(string allotType, bool isElecFlag, List<WmsOutInvoice> outInvoices, List<WmsOutInvoiceDtl> outInvoiceDtls, List<BasWRegion> regionInfos, List<WmsStockAllovateReturn> allocateResultList, string invoker)
        {
            BusinessResult result = new BusinessResult();
            List<outInvoiceAllotReturnDto> outInvoiceAllotReturnList = new List<outInvoiceAllotReturnDto>();
            try
            {
                #region 分配库存

                foreach (var dtl in outInvoiceDtls)
                {
                    var needQty = dtl.erpUndeliverQty.Value - (dtl.allotQty.Value - dtl.completeQty.Value);
                    if (needQty <= 0)
                        continue;
                    var first = allocateResultList.FirstOrDefault(t => t.qty.Value - t.allotQty > 0);
                    if (first == null)
                    {
                        break;
                    }

                    outInvoiceAllotReturnDto outInvoiceAllotReturnView = new outInvoiceAllotReturnDto()
                    {
                        invoiceDtlId = dtl.ID
                    };
                    while (needQty > 0)
                    {
                        var allotStock = allocateResultList.Where(t => t.qty > t.allotQty).FirstOrDefault();
                        if (allotStock != null)
                        {
                            allotStockDto stockView = new allotStockDto();
                            stockView.stockDtlId = allotStock.stockDtlId;
                            stockView.stockCode = allotStock.stockCode;
                            stockView.pickTaskNo = allotStock.pickTaskNo;
                            if (needQty >= allotStock.qty - allotStock.allotQty)
                            {
                                needQty = needQty - (allotStock.qty.Value - allotStock.allotQty);
                                stockView.allotQty = allotStock.qty.Value - allotStock.allotQty;
                                allotStock.allotQty = allotStock.qty.Value;
                                outInvoiceAllotReturnView.allotStocks.Add(stockView);
                                outInvoiceAllotReturnView.totalQty = outInvoiceAllotReturnView.totalQty + stockView.allotQty;
                            }
                            else
                            {
                                allotStock.allotQty = allotStock.allotQty + needQty;
                                stockView.allotQty = needQty;
                                needQty = 0;
                                outInvoiceAllotReturnView.allotStocks.Add(stockView);
                                outInvoiceAllotReturnView.totalQty = outInvoiceAllotReturnView.totalQty + stockView.allotQty;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (outInvoiceAllotReturnView.allotStocks.Count > 0)
                    {
                        outInvoiceAllotReturnList.Add(outInvoiceAllotReturnView);
                    }
                }

                #endregion

                #region 逻辑处理

                if (outInvoiceAllotReturnList.Count > 0)
                {
                    result = await allotInvoiceForUsing(allotType, isElecFlag, outInvoiceAllotReturnList, outInvoices, outInvoiceDtls, regionInfos, allocateResultList, invoker);
                }
                else
                {
                    return result.Error("未分配到库存");
                }

                #endregion
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }


        /// <summary>
        /// 事务处理
        /// </summary>
        /// <returns></returns>
        private BusinessResult allotInvoice(string allotType, bool isElecFlag, List<outInvoiceAllotReturnDto> outInvoiceAllotReturnList, List<WmsOutInvoice> outInvoices, List<WmsOutInvoiceDtl> outInvoiceDtls, List<BasWRegion> regionInfos, List<WmsStockAllovateReturn> allocateResultList, string invoker)
        {
            BusinessResult result = new BusinessResult();
            int allotDtlCount = 0;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
            WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
            var hasParentTransaction = false;
            string msg = String.Empty;
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (hasParentTransaction == false)
                {
                    DC.Database.BeginTransaction();
                }

                List<WmsOutInvoiceRecord> addwmsOutInvoiceRecords = new List<WmsOutInvoiceRecord>();
                var stockDltids = allocateResultList.Select(t => t.stockDtlId).ToList();
                var stockDtlInfos = DC.Set<WmsStockDtl>().Where(t => stockDltids.Contains(t.ID)).ToList();
                var stockCodes = stockDtlInfos.Select(t => t.stockCode).ToList();
                var stockInfos = DC.Set<WmsStock>().Where(t => stockCodes.Contains(t.stockCode)).ToList();
                WmsOutWave wmsOutWave = null;
                if (outInvoices.Any(t => !string.IsNullOrWhiteSpace(t.waveNo)))
                {
                    string waveNo = outInvoices.FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.waveNo))?.waveNo;
                    wmsOutWave = DC.Set<WmsOutWave>().Where(t => t.waveNo == waveNo).FirstOrDefault();
                }

                List<WmsStockUniicode> uniiInfos = new List<WmsStockUniicode>();
                if (isElecFlag)
                {
                    uniiInfos = DC.Set<WmsStockUniicode>().Where(t => stockDltids.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).ToList();
                }

                List<WmsPutdown> addwmsPutdowns = new List<WmsPutdown>();
                List<WmsPutdownDtl> addwmsPutdownDtls = new List<WmsPutdownDtl>();
                List<Int64> changeStockDtlIdList = new List<Int64>();
                List<string> changeStockcodeList = new List<string>();
                string docTypeCode = string.Empty;
                string orderNo = string.Empty;
                foreach (var dtl in outInvoiceAllotReturnList)
                {
                    //更新发货单据明细、单据主表和波次
                    var outInvoiceDtl = outInvoiceDtls.FirstOrDefault(t => t.ID == dtl.invoiceDtlId);
                    if (outInvoiceDtl != null)
                    {
                        var needQty = outInvoiceDtl.erpUndeliverQty - (outInvoiceDtl.allotQty - outInvoiceDtl.completeQty);
                        var isBatch = outInvoiceDtl.batchNo.IsNullOrWhiteSpace() ? false : true;
                        decimal realAllotQty = 0;

                        var outInvoice = outInvoices.FirstOrDefault(t => t.invoiceNo == outInvoiceDtl.invoiceNo);

                        foreach (var allotStock in dtl.allotStocks)
                        {
                            var stockDtl = stockDtlInfos.FirstOrDefault(t => t.ID == allotStock.stockDtlId && (t.qty > t.occupyQty) && t.stockDtlStatus == 50 && t.lockFlag == 0);
                            if (stockDtl != null)
                            {
                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【】,可分配数量【{stockDtl.qty - stockDtl.occupyQty}】,总数量【{stockDtl.qty}】,已占用数量【{stockDtl.occupyQty}】";
                                logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
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
                                        realAllotQty = realAllotQty + dtlAllotQty;
                                    }
                                }

                                if (dtlAllotQty > 0)
                                {
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【】,分配数量【{dtlAllotQty}】";
                                    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");

                                    #region 数据处理

                                    var stock = stockInfos.FirstOrDefault(t => t.stockCode == stockDtl.stockCode);
                                    //生成出库记录
                                    WmsOutInvoiceRecord wmsOutInvoiceRecord =
                                        BuildOutInvoiceRecord(
                                            outInvoice,
                                            outInvoiceDtl,
                                            stock,
                                            stockDtl,
                                            allotStock.pickTaskNo,
                                            allotType,
                                            isBatch,
                                            "1",
                                            "1",
                                            invoker,
                                            dtlAllotQty, "");
                                    if (isElecFlag)
                                    {
                                        wmsOutInvoiceRecord.pickTaskNo = sysSequenceVM.GetSequence(SequenceCode.PickNo.GetCode());
                                    }

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
                        logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
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

                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                            }
                            else
                            {
                                outInvoiceDtl.invoiceDtlStatus = 29;
                                outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                                outInvoice.invoiceStatus = 41;

                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                            }

                            //if (needQty > realAllotQty)
                            //{
                            //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.Allocating.GetCode();
                            //    outInvoiceDtl.allocatResult = $"分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";

                            //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                            //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";
                            //    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                            //}
                            //else
                            //{
                            //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.AllocateFinished.GetCode();
                            //    outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                            //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                            //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量：{realAllotQty},需求数量：{needQty}";
                            //    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
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
                        logger.Warn($"----->Warn----->发货单自动分配结束:{msg} ");
                    }
                    //todo:主表和波次主表


                    //todo:下架单

                    #region 下架单和下架单明细

                    //找变化的明细

                    if (changeStockcodeList.Count > 0)
                    {
                        foreach (var stock in changeStockcodeList)
                        {
                            var dtlList = stockDtlInfos.Where(t => t.stockCode == stock && changeStockDtlIdList.Contains(t.ID)).ToList();
                            if (dtlList.Count > 0)
                            {
                                string putdownNo = sysSequenceVM.GetSequence(SequenceCode.WmsPutdownNo.GetCode());
                                var wmsStock = stockInfos.FirstOrDefault(t => t.stockCode == stock);
                                var region = regionInfos.FirstOrDefault(t => t.regionNo == wmsStock.regionNo);

                                WmsPutdown wmsPutDown = BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);

                                //TODO：库区补充
                                if (region != null /*&& region.regionDeviceFlag == WRegionDeviceFlag.Flat.GetCode()*/)
                                {
                                    wmsPutDown.putdownStatus = 0;
                                }

                                dtlList.ForEach(t =>
                                {
                                    var downDtl = BuildWmsPutDownDtl(t, putdownNo, invoker);
                                    addwmsPutdownDtls.Add(downDtl);
                                });
                                addwmsPutdowns.Add(wmsPutDown);
                            }
                        }
                    }

                    #endregion
                }

                #region 波次处理

                if (wmsOutWave != null)
                {
                    var outInvoice = outInvoices.FirstOrDefault(t => t.waveNo == wmsOutWave.waveNo);
                    if (outInvoice != null)
                    {
                        if (outInvoice.invoiceStatus == 41)
                        {
                            wmsOutWave.waveStatus = 41;
                            wmsOutWave.UpdateBy = invoker;
                            wmsOutWave.UpdateTime = DateTime.Now;
                            ((DbContext)DC).SingleUpdate(wmsOutWave);
                        }
                    }
                }

                #endregion

                ((DbContext)DC).BulkUpdate(stockDtlInfos);
                ((DbContext)DC).BulkUpdate(outInvoiceDtls);
                ((DbContext)DC).BulkUpdate(outInvoices);
                if (addwmsOutInvoiceRecords.Count > 0)
                    ((DbContext)DC).BulkInsert(addwmsOutInvoiceRecords);


                if (addwmsPutdowns.Count > 0)
                    ((DbContext)DC).BulkInsert(addwmsPutdowns);
                if (addwmsPutdownDtls.Count > 0)
                    ((DbContext)DC).BulkInsert(addwmsPutdownDtls);


                // ((DbContext)DC).BulkSaveChanges();
                ((DbContext)DC).BulkSaveChanges(t => t.BatchSize = 1000);
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                        DC.Database.CommitTransaction();
                }

                result.msg = "分配结束";
                result.outParams = allotDtlCount;
            }
            catch (Exception e)
            {
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                        DC.Database.RollbackTransaction();
                }

                result.code = ResCode.Error;
                result.msg = $"异常信息: [ {e.Message} ]";
            }

            return result;
        }


        /// <summary>
        /// 事务处理for using
        /// </summary>
        /// <returns></returns>
        private async Task<BusinessResult> allotInvoiceForUsing(string allotType, bool isElecFlag, List<outInvoiceAllotReturnDto> outInvoiceAllotReturnList, List<WmsOutInvoice> outInvoices, List<WmsOutInvoiceDtl> outInvoiceDtls, List<BasWRegion> regionInfos, List<WmsStockAllovateReturn> allocateResultList, string invoker)
        {
            BusinessResult result = new BusinessResult();
            int allotDtlCount = 0;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
            WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();

            string msg = String.Empty;
            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {
                    List<string> pickupList = new List<string>() { "PDA", "PTL" };
                    List<WmsOutInvoiceRecord> addwmsOutInvoiceRecords = new List<WmsOutInvoiceRecord>();
                    List<WmsOutInvoiceUniicode> addwmsOutInvoiceUniicodes = new List<WmsOutInvoiceUniicode>();
                    var stockDltids = allocateResultList.Select(t => t.stockDtlId).ToList();
                    var stockDtlInfos = await ((DbContext)DC).Set<WmsStockDtl>().Where(t => stockDltids.Contains(t.ID) && t.qty > t.occupyQty && t.stockDtlStatus == 50 && t.lockFlag != 1).ToListAsync();
                    if (stockDtlInfos.Count == 0)
                    {
                        await tran.RollbackAsync();
                        return result.Error("库存不足");
                    }

                    var stockCodes = stockDtlInfos.Select(t => t.stockCode).ToList();
                    var stockInfos = await ((DbContext)DC).Set<WmsStock>().Where(t => stockCodes.Contains(t.stockCode)).ToListAsync();
                    WmsOutWave wmsOutWave = null;
                    if (outInvoices.Any(t => !string.IsNullOrWhiteSpace(t.waveNo)))
                    {
                        string waveNo = outInvoices.FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.waveNo))?.waveNo;
                        wmsOutWave = await ((DbContext)DC).Set<WmsOutWave>().Where(t => t.waveNo == waveNo).FirstOrDefaultAsync();
                    }

                    List<WmsStockUniicode> uniiInfos = new List<WmsStockUniicode>();
                    if (isElecFlag)
                    {
                        uniiInfos = await ((DbContext)DC).Set<WmsStockUniicode>().Where(t => stockDltids.Contains(t.stockDtlId) && t.delayFrozenFlag != 1 && t.driedScrapFlag != 1 && t.exposeFrozenFlag != 1 && t.qty > t.occupyQty).OrderBy(t => t.productDate).ToListAsync();
                        if (uniiInfos.Count == 0)
                        {
                            tran.Rollback();
                            return result.Error("库存不足");
                        }
                    }

                    List<WmsPutdown> addwmsPutdowns = new List<WmsPutdown>();
                    List<WmsPutdownDtl> addwmsPutdownDtls = new List<WmsPutdownDtl>();
                    List<Int64> changeStockDtlIdList = new List<Int64>();
                    List<string> changeStockcodeList = new List<string>();
                    string docTypeCode = string.Empty;
                    string orderNo = string.Empty;

                    foreach (var dtl in outInvoiceAllotReturnList)
                    {
                        //更新发货单据明细、单据主表和波次
                        var outInvoiceDtl = outInvoiceDtls.FirstOrDefault(t => t.ID == dtl.invoiceDtlId);
                        if (outInvoiceDtl != null)
                        {
                            var needQty = outInvoiceDtl.erpUndeliverQty - (outInvoiceDtl.allotQty - outInvoiceDtl.completeQty);
                            var isBatch = outInvoiceDtl.batchNo.IsNullOrWhiteSpace() ? false : true;
                            decimal realAllotQty = 0;

                            var outInvoice = outInvoices.FirstOrDefault(t => t.invoiceNo == outInvoiceDtl.invoiceNo);

                            #region 电子货架

                            bool isCanAllot = await CheckAllotElecDocCount(isElecFlag, outInvoice.invoiceNo);

                            #endregion

                            foreach (var allotStock in dtl.allotStocks)
                            {
                                var stockDtl = stockDtlInfos.FirstOrDefault(t => t.ID == allotStock.stockDtlId && (t.qty > t.occupyQty) && t.stockDtlStatus == 50 && t.lockFlag == 0);
                                if (stockDtl != null)
                                {
                                    var stock = stockInfos.FirstOrDefault(t => t.stockCode == stockDtl.stockCode);
                                    var region = regionInfos.FirstOrDefault(t => t.regionNo == stock.regionNo);
                                    if (region != null && region.pickupMethod == "PTL" && isCanAllot == false)
                                    {
                                        continue;
                                    }

                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【stockDtl.batchNo】,可分配数量【{stockDtl.qty - stockDtl.occupyQty}】,总数量【{stockDtl.qty}】,已占用数量【{stockDtl.occupyQty}】";
                                    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                                    decimal dtlAllotQty = 0;
                                    List<allotUniiDto> allotUniiList = new List<allotUniiDto>();
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
                                            foreach (var stockUnii in eleUniiInfos)
                                            {
                                                decimal dtlCanUseQty = stockDtl.qty.Value - stockDtl.occupyQty.Value;
                                                decimal curUniiAllotQty = 0;
                                                if (dtlCanUseQty >= (stockUnii.qty - stockUnii.occupyQty))
                                                {
                                                    if (needQty - realAllotQty >= ((stockUnii.qty - stockUnii.occupyQty)))
                                                    {
                                                        curUniiAllotQty = stockUnii.qty.Value - stockUnii.occupyQty.Value;
                                                        dtlAllotQty = dtlAllotQty + curUniiAllotQty;
                                                        realAllotQty = realAllotQty + curUniiAllotQty;

                                                        #region 库存

                                                        stockDtl.occupyQty += curUniiAllotQty;
                                                        stockUnii.occupyQty = stockUnii.qty;

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        curUniiAllotQty = needQty.Value - realAllotQty;
                                                        // needQty = needQty - (stockUnii.stockQty - stockUnii.occupyQty);
                                                        dtlAllotQty = dtlAllotQty + curUniiAllotQty;
                                                        realAllotQty = realAllotQty + curUniiAllotQty;

                                                        #region 库存

                                                        stockDtl.occupyQty += curUniiAllotQty;
                                                        stockUnii.occupyQty += curUniiAllotQty;

                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    if (needQty - realAllotQty >= dtlCanUseQty)
                                                    {
                                                        curUniiAllotQty = dtlCanUseQty;

                                                        #region 库存

                                                        stockUnii.occupyQty += curUniiAllotQty;
                                                        stockDtl.occupyQty += curUniiAllotQty;

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        curUniiAllotQty = needQty.Value - realAllotQty;

                                                        dtlAllotQty = dtlAllotQty + curUniiAllotQty;
                                                        realAllotQty = realAllotQty + curUniiAllotQty;

                                                        #region 库存

                                                        stockUnii.occupyQty += curUniiAllotQty;
                                                        stockDtl.occupyQty += curUniiAllotQty;

                                                        #endregion
                                                    }
                                                }

                                                if (curUniiAllotQty > 0)
                                                {
                                                    allotUniiList.Add(new allotUniiDto() { uniicodeId = stockUnii.ID, uniicode = stockUnii.uniicode, allotQty = curUniiAllotQty });
                                                }
                                            }


                                            #region old

                                            //decimal dtlRealCanQty = 0;
                                            //if (uniiCanQty >= stockDtl.qty - stockDtl.occupyQty)
                                            //{
                                            //    dtlRealCanQty = stockDtl.qty.Value - stockDtl.occupyQty.Value;
                                            //}
                                            //else
                                            //{
                                            //    dtlRealCanQty = uniiCanQty;
                                            //}

                                            //if (allotStock.allotQty >= dtlRealCanQty)
                                            //{

                                            //    dtlAllotQty = dtlRealCanQty;
                                            //    stockDtl.occupyQty += dtlRealCanQty;
                                            //    realAllotQty = realAllotQty + dtlAllotQty;
                                            //    //continue;
                                            //}
                                            //else
                                            //{
                                            //    //占用库存
                                            //    dtlAllotQty = allotStock.allotQty;
                                            //    stockDtl.occupyQty += allotStock.allotQty;
                                            //    realAllotQty = realAllotQty + allotStock.allotQty;

                                            //} 

                                            #endregion
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
                                            realAllotQty = realAllotQty + dtlAllotQty;
                                        }
                                    }

                                    stockDtl.UpdateBy = invoker;
                                    stockDtl.UpdateTime = DateTime.Now;
                                    // add by Allen 2024-04-08 17:25:56 增加的字段进行赋值 OUT 从而让库存明细表下面的触发器可以进行发现
                                    //stockDtl.businessCode = "OUT";

                                    if (dtlAllotQty > 0)
                                    {
                                        msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】:分配库存明细【{stockDtl.ID}】,批次【stockDtl.batchNo】,分配数量【{dtlAllotQty}】";
                                        logger.Warn($"----->Warn----->发货单自动分配:{msg} ");

                                        #region 数据处理

                                        bool isPk = true;
                                        if (region != null && !pickupList.Contains(region.pickupMethod))
                                        {
                                            isPk = false;
                                        }

                                        //生成出库记录
                                        WmsOutInvoiceRecord wmsOutInvoiceRecord =
                                            BuildOutInvoiceRecord(
                                                outInvoice,
                                                outInvoiceDtl,
                                                stock,
                                                stockDtl,
                                                allotStock.pickTaskNo,
                                                allotType,
                                                isBatch,
                                                "1",
                                                "1",
                                                invoker,
                                                dtlAllotQty,
                                                "",
                                                isPk);
                                        if (isElecFlag)
                                        {
                                            wmsOutInvoiceRecord.pickTaskNo = sysSequenceVM.GetSequence(SequenceCode.PickNo.GetCode());
                                        }

                                       

                                        addwmsOutInvoiceRecords.Add(wmsOutInvoiceRecord);

                                        if (allotUniiList.Count > 0)
                                        {
                                            foreach (var item in allotUniiList)
                                            {
                                                var stockUnii = uniiInfos.FirstOrDefault(t => t.uniicode == item.uniicode && t.ID == item.uniicodeId);
                                                if (stockUnii != null)
                                                {
                                                    WmsOutInvoiceUniicode wmsOutReceiptUniicode = wmsStockVM.BuildOutReceiptUniicodeInfo(wmsOutInvoiceRecord, stockUnii, item.allotQty, invoker);
                                                    addwmsOutInvoiceUniicodes.Add(wmsOutReceiptUniicode);
                                                }
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

                                        #endregion

                                        //#region 库存
                                        //await  ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                                        //await ((DbContext)DC).BulkSaveChangesAsync(t=>t.BatchSize=2000);
                                        //#endregion
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
                            logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
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

                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                                }
                                else
                                {
                                    outInvoiceDtl.invoiceDtlStatus = 29;
                                    outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                                    outInvoice.invoiceStatus = 41;

                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                                }

                                //if (needQty > realAllotQty)
                                //{
                                //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.Allocating.GetCode();
                                //    outInvoiceDtl.allocatResult = $"分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";

                                //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                                //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配中,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                //    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
                                //}
                                //else
                                //{
                                //    outInvoiceDtl.invoiceDtlStatus = OutInvoiceOrDtlStatus.AllocateFinished.GetCode();
                                //    outInvoiceDtl.allocatResult = $"分配完成,当前分配数量：{realAllotQty},需求数量：{needQty}";
                                //    outInvoice.invoiceStatus = OutInvoiceOrDtlStatus.StoreOrPickOuting.GetCode();

                                //    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,当前erp未发数量【{outInvoiceDtl.erpUndeliverQty}】，总分配数量【{outInvoiceDtl.allotQty}】，已拣选数量【{outInvoiceDtl.completeQty}】，分配完成,本次分配数量：{realAllotQty},需求数量：{needQty}";
                                //    logger.Warn($"----->Warn----->发货单自动分配:{msg} ");
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
                            logger.Warn($"----->Warn----->发货单自动分配结束:{msg} ");
                        }
                        //todo:主表和波次主表


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
                                                WmsPutdown wmsPutDown = BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                                wmsPutDown.putdownStatus = 90;
                                                dtlList.ForEach(t =>
                                                {
                                                    var downDtl = BuildWmsPutDownDtl(t, putdownNo, invoker);
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
                                                var putdownDtlInfos = await ((DbContext)DC).Set<WmsPutawayDtl>().Where(t => t.putawayNo == putdownInfo.putdownNo && t.palletBarcode == wmsStock.palletBarcode && t.stockCode == wmsStock.stockCode).ToListAsync();
                                                int putdownStatus = Convert.ToInt32(putdownInfo.putdownStatus);
                                                if (putdownStatus == 0)
                                                {
                                                    dtlList.ForEach(t =>
                                                    {
                                                        var existPutdownDtl = putdownDtlInfos.FirstOrDefault(x => x.stockDtlId == t.ID);
                                                        if (existPutdownDtl != null)
                                                        {
                                                        }
                                                        else
                                                        {
                                                            var downDtl = BuildWmsPutDownDtl(t, putdownInfo.putdownNo, invoker);
                                                            downDtl.putdownDtlStatus = 0;
                                                            addwmsPutdownDtls.Add(downDtl);
                                                        }
                                                    });
                                                }
                                                else if (putdownStatus > 0 && putdownStatus < 90)
                                                {
                                                    await tran.RollbackAsync();
                                                    result.code = ResCode.Error;
                                                    result.msg = $"托盘【{wmsStock.palletBarcode}】对应下架单【{putdownInfo.putdownNo}】不是初始创建";
                                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:外部单号【{outInvoiceDtl.externalOutNo}】,外部单行号【{outInvoiceDtl.externalOutDtlId}】,物料【{outInvoiceDtl.materialCode}】,{result.msg}";
                                                    logger.Warn($"----->Warn----->发货单自动分配异常:{msg} ");
                                                    return result;
                                                }
                                                //else
                                                //{
                                                //    string putdownNo = sysSequenceVM.GetSequence(DictonaryHelper.SequenceCode.WmsPutdownNo.GetCode());
                                                //    WmsPutdown wmsPutDown = BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                                //    wmsPutDown.putdownStatus = 0;
                                                //    dtlList.ForEach(t =>
                                                //    {
                                                //        var downDtl = BuildWmsPutDownDtl(t, putdownNo, invoker);
                                                //        addwmsPutdownDtls.Add(downDtl);
                                                //    });
                                                //    addwmsPutdowns.Add(wmsPutDown);
                                                //}
                                            }
                                            else
                                            {
                                                string putdownNo = sysSequenceVM.GetSequence(SequenceCode.WmsPutdownNo.GetCode());
                                                WmsPutdown wmsPutDown = BuildWmsPutDownForMerge(putdownNo, wmsStock, region?.pickupMethod, docTypeCode, orderNo, invoker);
                                                wmsPutDown.putdownStatus = 0;
                                                dtlList.ForEach(t =>
                                                {
                                                    var downDtl = BuildWmsPutDownDtl(t, putdownNo, invoker);
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
                    }

                    #region 波次处理

                    if (wmsOutWave != null)
                    {
                        var outInvoice = outInvoices.FirstOrDefault(t => t.waveNo == wmsOutWave.waveNo);
                        if (outInvoice != null)
                        {
                            if (outInvoice.invoiceStatus == 41)
                            {
                                wmsOutWave.waveStatus = 41;
                                wmsOutWave.UpdateBy = invoker;
                                wmsOutWave.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).SingleUpdateAsync(wmsOutWave);
                            }
                        }
                    }

                    #endregion

                    //((DbContext)DC).BulkUpdate(stockDtlInfos);
                    //((DbContext)DC).BulkUpdate(outInvoiceDtls);
                    //((DbContext)DC).BulkUpdate(outInvoices);

                    // modified by Allen 2024-03-30 对库存明细做并发冲突检测，就不能调用Bulk相关方法，而得用EF自带的SaveChanges，使用SaveChangesAsync将无法捕捉到DbUpdateConcurrencyException异常
                    await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(stockDtlInfos);
                    // DC.Set<WmsStockDtl>().UpdateRange(stockDtlInfos);
                    // DC.SaveChanges();

                    if (uniiInfos.Any())
                        await ((DbContext)DC).Set<WmsStockUniicode>().BulkUpdateAsync(uniiInfos);
                    await ((DbContext)DC).Set<WmsOutInvoiceDtl>().BulkUpdateAsync(outInvoiceDtls);
                    await ((DbContext)DC).Set<WmsOutInvoice>().BulkUpdateAsync(outInvoices);
                    if (addwmsOutInvoiceRecords.Count > 0)
                        await ((DbContext)DC).BulkInsertAsync(addwmsOutInvoiceRecords);
                    if (addwmsOutInvoiceUniicodes.Count > 0)
                        await ((DbContext)DC).BulkInsertAsync(addwmsOutInvoiceUniicodes);
                    if (addwmsPutdowns.Count > 0)
                        await ((DbContext)DC).BulkInsertAsync(addwmsPutdowns);
                    if (addwmsPutdownDtls.Count > 0)
                        await ((DbContext)DC).BulkInsertAsync(addwmsPutdownDtls);
                    // ((DbContext)DC).BulkSaveChanges();
                    //((DbContext)DC).BulkSaveChanges(t => t.BatchSize = 2000);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);

                    #region 数据一致性校验

                    #endregion

                    await tran.CommitAsync();

                    result.msg = "分配结束";
                    result.outParams = allotDtlCount;
                }
                // catch (DbUpdateConcurrencyException e)
                // {
                //     // modified by Allen 2024-03-30 捕捉数据并发冲突检测异常，作相应报错返回
                //     await tran.RollbackAsync();
                //     result.code = ResultCode.Error;
                //     result.msg = $"异常信息: [ 当前操作的库存明细数据已被其他操作更新，请重新尝试！ ]";
                // }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    result.code = ResCode.Error;
                    result.msg = $"异常信息: [ {e.Message} ]";
                }
            }

            return result;
        }

        /// <summary>
        /// 创建工单自动分配任务
        /// </summary>
        /// <param name="batchConfirmout"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public BusinessResult CreateOutInvoiceJob(string invoker)
        {
            BusinessResult result = new BusinessResult();

            List<WmsOutInvoiceJob> addOutInvoiceJobList = new List<WmsOutInvoiceJob>();
            List<WmsOutInvoiceJob> updateOutInvoiceJobList = new List<WmsOutInvoiceJob>();
            try
            {
                var outInvoices = DC.Set<WmsOutInvoice>().Where(x => x.docTypeCode == BusinessCode.OutProduceOrder.GetCode() && string.IsNullOrWhiteSpace(x.waveNo) && x.invoiceStatus < 90).ToList();
                if (outInvoices.Count > 0)
                {
                    foreach (var outInvoiceNo in outInvoices)
                    {
                        var outInvoiceJob = DC.Set<WmsOutInvoiceJob>().Where(x => x.invoiceNo == outInvoiceNo.invoiceNo).FirstOrDefault();
                        if (outInvoiceJob == null)
                        {
                            WmsOutInvoiceJob wmsOutInvoiceJob = new WmsOutInvoiceJob();
                            wmsOutInvoiceJob.invoiceNo = outInvoiceNo.invoiceNo;
                            wmsOutInvoiceJob.dealFlag = 0;
                            wmsOutInvoiceJob.projectNo = outInvoiceNo.projectNo;
                            wmsOutInvoiceJob.orderCreateTime = outInvoiceNo.CreateTime;
                            wmsOutInvoiceJob.CreateBy = invoker;
                            wmsOutInvoiceJob.CreateTime = DateTime.Now;
                            wmsOutInvoiceJob.UpdateBy = invoker;
                            wmsOutInvoiceJob.UpdateTime = DateTime.Now;
                            addOutInvoiceJobList.Add(wmsOutInvoiceJob);
                        }
                        else
                        {
                            if (outInvoiceJob.dealFlag == 0)
                            {
                                outInvoiceJob.projectNo = outInvoiceNo.projectNo;
                                outInvoiceJob.UpdateBy = invoker;
                                outInvoiceJob.UpdateTime = DateTime.Now;
                                updateOutInvoiceJobList.Add(outInvoiceJob);
                            }
                        }
                    }
                }

                if (addOutInvoiceJobList.Count > 0)
                {
                    ((DbContext)DC).BulkInsert(addOutInvoiceJobList);
                }

                if (updateOutInvoiceJobList.Count > 0)
                {
                    ((DbContext)DC).BulkUpdate(updateOutInvoiceJobList);
                }

                ((DbContext)DC).BulkSaveChanges();
            }
            catch (Exception e)
            {
                result.code = ResCode.Error;
                result.msg = "创建失败：" + e.Message;
            }

            return result;
        }


        /// <summary>
        /// 批量分配
        /// </summary>
        public async Task DealOutInvoiceJob()
        {
            List<WmsOutInvoiceJob> delOutInvoiceJobs = new List<WmsOutInvoiceJob>();
            List<WmsOutInvoiceJob> dealOutInvoiceJobs = new List<WmsOutInvoiceJob>();

            List<WmsOutInvoiceJob> outInvoiceJobs = await DC.Set<WmsOutInvoiceJob>().Where(x => x.dealFlag == 0).ToListAsync();
            if (outInvoiceJobs.Count > 0)
            {
                foreach (var job in outInvoiceJobs)
                {
                    WmsOutInvoice outInvoice = await DC.Set<WmsOutInvoice>().Where(x => x.invoiceNo == job.invoiceNo).FirstOrDefaultAsync();
                    if (outInvoice != null)
                    {
                        if (outInvoice.invoiceStatus >= 90)
                        {
                            delOutInvoiceJobs.Add(job);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(outInvoice.waveNo))
                            {
                                delOutInvoiceJobs.Add(job);
                            }
                            else
                            {
                                dealOutInvoiceJobs.Add(job);
                            }
                        }
                    }
                }

                if (delOutInvoiceJobs.Count > 0)
                {
                    await ((DbContext)DC).BulkDeleteAsync(delOutInvoiceJobs);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }

                if (dealOutInvoiceJobs.Count > 0)
                {
                    var projectNoBaks = (await DC.Set<WmsStockDtl>().Where(x => !string.IsNullOrWhiteSpace(x.projectNoBak) && "50".Equals(x.stockDtlStatus)).ToListAsync()).Select(x => x.projectNoBak).Distinct().ToList();

                    var bakDealOutInvoiceJobs = dealOutInvoiceJobs.Where(t => projectNoBaks.Contains(t.projectNo)).OrderBy(x => x.orderCreateTime).ToList();
                    var nobakDealOutInvoiceJobs = dealOutInvoiceJobs.Where(t => !projectNoBaks.Contains(t.projectNo)).OrderBy(x => x.orderCreateTime).ToList();
                    var dealJob = bakDealOutInvoiceJobs.Concat(nobakDealOutInvoiceJobs).ToList();

                    //var dealJob = dealOutInvoiceJobs.Where(x => x.dealFlag == 0)
                    //    .OrderBy(x =>
                    //    {
                    //        var index = 0;
                    //        index = Array.IndexOf(projectNoBaks, x.projectNo);
                    //        if (index != -1)
                    //        {
                    //            return index;
                    //        }
                    //        else
                    //        {
                    //            return int.MaxValue;
                    //        }
                    //    })
                    //    .OrderBy(x => x.orderCreateTime)
                    //    .ToList();

                    if (dealJob.Count > 0)
                    {
                        foreach (var outInvoiceJob in dealJob)
                        {
                            outInvoiceJob.dealFlag = 1;
                            outInvoiceJob.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).BulkUpdateAsync(new WmsOutInvoiceJob[] { outInvoiceJob });
                            await ((DbContext)DC).BulkSaveChangesAsync();

                            var outInvoice = await DC.Set<WmsOutInvoice>().Where(x => x.invoiceNo == outInvoiceJob.invoiceNo).FirstOrDefaultAsync();
                            if (outInvoice != null)
                            {
                                if (!string.IsNullOrWhiteSpace(outInvoice.waveNo))
                                {
                                    outInvoiceJob.UpdateTime = DateTime.Now;
                                    outInvoiceJob.remarks = "该单据存在波次号，不能进行分配";
                                    await ((DbContext)DC).BulkUpdateAsync(new WmsOutInvoiceJob[] { outInvoiceJob });
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                    continue;
                                }

                                if (outInvoice.invoiceStatus >= 90)
                                {
                                    outInvoiceJob.UpdateTime = DateTime.Now;
                                    outInvoiceJob.remarks = "该任务状态不允许分配";
                                    await ((DbContext)DC).BulkUpdateAsync(new WmsOutInvoiceJob[] { outInvoiceJob });
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                    continue;
                                }

                                if (outInvoice.invoiceStatus < 90)
                                {
                                    AutoAllocateParam autoAllocateParam = new AutoAllocateParam();
                                    autoAllocateParam.inVoiceNo = outInvoice.invoiceNo;
                                    autoAllocateParam.areaNo = "";
                                    BusinessResult result = await OutAllocateByInvoice(autoAllocateParam, outInvoiceJob.CreateBy);
                                    var outInvoiceDtl = await DC.Set<WmsOutInvoiceDtl>().Where(x => x.invoiceNo == outInvoiceJob.invoiceNo && x.invoiceDtlStatus < 29).ToListAsync();
                                    if (outInvoiceDtl.Count > 0)
                                    {
                                        //outInvoiceJob.dealFlag = 0;
                                        outInvoiceJob.UpdateTime = DateTime.Now;
                                        outInvoiceJob.remarks = result.msg;
                                        await ((DbContext)DC).BulkUpdateAsync(new WmsOutInvoiceJob[] { outInvoiceJob });
                                        await ((DbContext)DC).BulkSaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
