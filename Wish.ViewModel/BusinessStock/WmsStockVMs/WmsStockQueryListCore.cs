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
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BusinessStock.WmsStockDtlVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.Common;


namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        private async Task<PagedResultDto<WmsStockDtlDto>> GetStockDtlsForFill(List<WmsStockDtl> wmsStockDtlInfos, List<WmsStock> wmsStockInfos, GetStockByAllotDto input, string materailName)
        {
            PagedResultDto<WmsStockDtlDto> result = new PagedResultDto<WmsStockDtlDto>();

            try
            {
                var queryResult = wmsStockDtlInfos.Skip((input.page - 1) * input.limit).Take(input.limit).ToList();
                if (queryResult.Any())
                {
                    #region 实体转换

                    var viewResult = (from dtl in wmsStockDtlInfos
                                      join main in wmsStockInfos on dtl.stockCode equals main.stockCode
                                      select new { dtl, main })
                        .Select(x => new WmsStockDtlDto
                        {
                            ID = x.dtl.ID,
                            whouseNo = x.dtl.whouseNo,
                            areaNo = x.dtl.areaNo,
                            erpWhouseNo = x.dtl.erpWhouseNo,
                            proprietorCode = x.dtl.proprietorCode,
                            stockCode = x.dtl.stockCode,
                            palletBarcode = x.dtl.palletBarcode,
                            projectNo = x.dtl.projectNo,
                            projectNoBak = x.dtl.projectNoBak,
                            materialCode = x.dtl.materialCode,
                            materialName = x.dtl.materialName,
                            supplierCode = x.dtl.supplierCode,
                            supplierName = x.dtl.supplierName,
                            supplierNameEn = x.dtl.supplierNameEn,
                            supplierNameAlias = x.dtl.supplierNameAlias,
                            //batchNo = x.dtl.batchNo,
                            materialSpec = x.dtl.materialSpec,
                            qty = x.dtl.qty,
                            occupyQty = x.dtl.occupyQty,
                            stockDtlStatus = x.dtl.stockDtlStatus,
                            inspectionResult = x.dtl.inspectionResult,
                            lockFlag = x.dtl.lockFlag,
                            lockReason = x.dtl.lockReason,
                            skuCode = x.dtl.skuCode,
                            //delFlag = x.dtl.delFlag,
                            extend1 = x.dtl.extend1,
                            extend2 = x.dtl.extend2,
                            extend3 = x.dtl.extend3,
                            extend4 = x.dtl.extend4,
                            extend5 = x.dtl.extend5,
                            extend6 = x.dtl.extend6,
                            extend7 = x.dtl.extend7,
                            extend8 = x.dtl.extend8,
                            extend9 = x.dtl.extend9,
                            extend10 = x.dtl.extend10,
                            extend11 = x.dtl.extend11,
                            chipSize = x.dtl.chipSize,
                            chipThickness = x.dtl.chipThickness,
                            chipModel = x.dtl.chipModel,
                            dafType = x.dtl.dafType,
                            regionNo = x.main.regionNo,
                            roadwayNo = x.main.roadwayNo,
                            binNo = x.main.binNo,
                            loadedType = x.main.loadedType,
                            locAllotGroup = x.main.locAllotGroup,
                            locNo = x.main.locNo,
                            specialFlag = x.main.specialFlag,
                            specialMsg = x.main.specialMsg,
                            errFlag = x.main.errFlag,
                            errMsg = x.main.errMsg,
                            invoiceNo = x.main.invoiceNo,
                            weight = x.main.weight,
                            stockStatus = x.main.stockStatus,
                            CreateBy = x.dtl.CreateBy,
                            CreateTime = x.dtl.CreateTime,
                            UpdateBy = x.dtl.UpdateBy,
                            UpdateTime = x.dtl.UpdateTime,
                            canQty = x.dtl.qty - x.dtl.occupyQty
                            //materialTypeCode = x.material.materialTypeCode,
                            //materialCategoryCode = x.material.materialCategoryCode,
                            //unitCode = x.material.unitCode
                        }).ToList();

                    #endregion

                    var matList = queryResult.Select(t => t.materialCode).Distinct().ToList();
                    whouseEntities = await DC.Set<BasWWhouse>().AsNoTracking().ToListAsync();
                    areaEntities = await DC.Set<BasWArea>().AsNoTracking().ToListAsync();
                    regionEntities = await DC.Set<BasWRegion>().AsNoTracking().ToListAsync();
                    dicEntities = await DC.Set<SysDictionary>().AsNoTracking().ToListAsync();
                    // roadwayEntities = DC.Set<BasWRoadway>().AsNoTracking().ToList();
                    // binEntities = DC.Set<BasWBin>().AsNoTracking().ToList();
                    proEntities = await DC.Set<BasBProprietor>().AsNoTracking().ToListAsync();
                    erpWhouseEntities = await DC.Set<BasWErpWhouse>().AsNoTracking().ToListAsync();
                    unitEntities = await DC.Set<BasBUnit>().AsNoTracking().ToListAsync();
                    foreach (WmsStockDtlDto item in viewResult)
                    {
                        DtlMapper(item);
                        item.materialName = materailName;
                    }

                    var count = viewResult.Count;
                    var viewRes = viewResult.Skip((input.page - 1) * input.limit).Take(input.limit).ToList();
                    result = new PagedResultDto<WmsStockDtlDto>()
                    {
                        items = viewResult,
                        total = count,
                    };
                }
            }
            catch
            {
            }

            return result;
        }

        private WmsStockDtlDto DtlMapper(WmsStockDtlDto dto)
        {
            //if (matEntities.Any())
            //{
            //    var relEntity = matEntities.FirstOrDefault(t => t.materialCode == dto.materialCode);
            //    if (relEntity != null)
            //    {
            //        dto.materialCategoryCode = relEntity.materialCategoryCode;
            //        dto.materialTypeCode = relEntity.materialTypeCode;
            //        dto.unitCode = relEntity.unitCode;
            //    }
            //}
            if (whouseEntities.Any())
            {
                var relEntity = whouseEntities.FirstOrDefault(t => t.whouseNo == dto.whouseNo);
                if (relEntity != null)
                {
                    dto.whouseName = relEntity.whouseName;
                }
            }

            if (areaEntities.Any())
            {
                var relEntity = areaEntities.FirstOrDefault(t => t.areaNo == dto.areaNo);
                if (relEntity != null)
                {
                    dto.areaName = relEntity.areaName;
                }
            }

            if (regionEntities.Any())
            {
                var relEntity = regionEntities.FirstOrDefault(t => t.regionNo == dto.regionNo);
                if (relEntity != null)
                {
                    dto.regionName = relEntity.regionName;
                }
            }

            if (roadwayEntities.Any())
            {
                var relEntity = roadwayEntities.FirstOrDefault(t => t.roadwayNo == dto.roadwayNo);
                if (relEntity != null)
                {
                    dto.roadwayName = relEntity.roadwayName;
                }
            }

            if (binEntities.Any())
            {
                var relEntity = binEntities.FirstOrDefault(t => t.binNo == dto.binNo);
                if (relEntity != null)
                {
                    dto.binName = relEntity.binName;
                }
            }

            if (proEntities.Any())
            {
                var relEntity = proEntities.FirstOrDefault(t => t.proprietorCode == dto.proprietorCode);
                if (relEntity != null)
                {
                    dto.proprietorName = relEntity.proprietorName;
                }
            }

            if (erpWhouseEntities.Any())
            {
                var relEntity = erpWhouseEntities.FirstOrDefault(t => t.erpWhouseNo == dto.erpWhouseNo);
                if (relEntity != null)
                {
                    dto.erpWhouseName = relEntity.erpWhouseName;
                }
            }

            //if (matTypeEntities.Any())
            //{
            //    var relEntity = matTypeEntities.FirstOrDefault(t => t.materialTypeCode == dto.materialTypeCode);
            //    if (relEntity != null)
            //    {
            //        dto.materialTypeName = relEntity.materialTypeName;
            //    }
            //}
            //if (matCategoryEntities.Any())
            //{
            //    var relEntity = matCategoryEntities.FirstOrDefault(t => t.materialCategoryCode == dto.materialCategoryCode);
            //    if (relEntity != null)
            //    {
            //        dto.materialCategoryName = relEntity.materialCategoryName;
            //        dto.electronicMaterialFlag = relEntity.materialFlag;
            //    }
            //}
            if (unitEntities.Any())
            {
                var relEntity = unitEntities.FirstOrDefault(t => t.unitCode == dto.unitCode);
                if (relEntity != null)
                {
                    dto.unitName = relEntity.unitName;
                }
            }

            if (dicEntities.Any())
            {
                dto.loadedTypeDesc = GetDicItemDisVal(dicEntities, "LOADED_TYPE", dto.loadedType);
                dto.inspectionResultDesc = GetDicItemDisVal(dicEntities, "INSPECTION_RESULT", dto.inspectionResult);
                dto.stockDtlStatusDesc = GetDicItemDisVal(dicEntities, "STOCK_DTL_STATUS", dto.stockDtlStatus);
                dto.stockStatusDesc = GetDicItemDisVal(dicEntities, "STOCK_STATUS", dto.stockStatus);
                dto.lockFlagDesc = GetDicItemDisVal(dicEntities, "LOCK_FLAG", dto.lockFlag);
                dto.errFlagDesc = GetDicItemDisVal(dicEntities, "ERR_FLAG", dto.errFlag);
                dto.materialFlagDesc = GetDicItemDisVal(dicEntities, "MATERIAL_FLAG", dto.materialFlag);
                if (dto.specialFlag != null)
                {
                    dto.specialFlagDesc = GetDicItemDisVal(dicEntities, "SPECIAL_FLAG", dto.specialFlag);
                }
            }

            return dto;
        }

        private string GetDicItemDisVal(List<SysDictionary> sysDictionaries, string code, object itemCode, string lanType = "Zh")
        {
            string result = "";
            if (itemCode != null)
            {
                var entity = sysDictionaries.FirstOrDefault(t => t.dictionaryCode == code && t.dictionaryItemCode == itemCode.ToString());
                if (entity != null)
                {
                    result = entity.dictionaryItemName;
                }
            }

            return result;
        }

        private async Task<List<WmsStockDtl>> GetStockDtlsForPallet(WmsStockAllocateForHandDto input, GetStockByAllotDto srarchInput)
        {
            List<WmsStockDtl> stockDtls = new List<WmsStockDtl>();
            bool isProductDoc = false;
            if (input.docTypeCode == BusinessCode.OutProduceOrder.GetCode())
            {
                isProductDoc = true;
            }

            List<Int64> stockDtlIdList = new List<Int64>();
            if (input.snList.Count > 0)
            {
                var uniiInfo = await DC.Set<WmsStockUniicode>().Where(t => input.snList.Contains(t.uniicode) && t.delayFrozenFlag != 1 && t.exposeFrozenFlag != 1 && t.driedScrapFlag != 1 && t.qty > t.occupyQty).ToListAsync();
                stockDtlIdList = uniiInfo.Select(t => t.stockDtlId).Distinct().ToList();
            }

            //可用库存
            if (string.IsNullOrWhiteSpace(input.docBatchNo))
            {
                var dtlQuery = DC.Set<WmsStockDtl>()
                    .Where(x => input.stockCodeList.Contains(x.stockCode))
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.stockDtlStatus == 50)
                    .Where(x => x.materialCode == input.materialCode)
                    //.Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak==input.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.inspectionResult.ToString() == InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.WhereIf(!string.IsNullOrWhiteSpace(srarchInput.batchNo), t => t.batchNo.Contains(srarchInput.batchNo))
                    //.WhereIf(!string.IsNullOrWhiteSpace(srarchInput.erpWhouseNo), t => t.erpWhouseNo.Contains(srarchInput.erpWhouseNo))
                    .WhereIf(stockDtlIdList.Any(), t => stockDtlIdList.Contains(t.ID));
                // .WhereIf(isProductDoc, x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.projectNo));
                stockDtls = await dtlQuery.ToListAsync();
            }
            else
            {
                var dtlQuery = DC.Set<WmsStockDtl>()
                    .Where(x => input.stockCodeList.Contains(x.stockCode))
                    .Where(x => x.lockFlag == 0)
                    .Where(x => x.stockDtlStatus == 50)
                    .Where(x => x.materialCode == input.materialCode)
                    //.Where(x => x.batchNo == input.docBatchNo)
                    // .Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                    .Where(x => x.inspectionResult.ToString() == InspectionResult.Qualitified.GetCode() || x.inspectionResult.ToString() == InspectionResult.AcceptOnDeviation.GetCode())
                    .Where(x => x.qty > x.occupyQty)
                    //.WhereIf(!string.IsNullOrWhiteSpace(input.batchNo), t => t.batchNo.Contains(input.batchNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(srarchInput.erpWhouseNo), t => t.erpWhouseNo.Contains(srarchInput.erpWhouseNo))
                    .WhereIf(stockDtlIdList.Any(), t => stockDtlIdList.Contains(t.ID));
                //  .WhereIf(isProductDoc, x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
                stockDtls = await dtlQuery.ToListAsync();
            }


            //筛选排序
            stockDtls = await GetStockDtlsForPalletForProduct(stockDtls, input);
            //关联

            return stockDtls;
        }

        private async Task<List<WmsStockDtl>> GetStockDtlsForPalletForProduct(List<WmsStockDtl> stockDtls, WmsStockAllocateForHandDto input)
        {
            List<WmsStockDtl> resultDtlList = new List<WmsStockDtl>();
            List<erpWhousePrirityDto> erpWhousePririties = new List<erpWhousePrirityDto>()
            {
                new erpWhousePrirityDto() { erpWhouseNo = input.erpWhouseNo }
            };
            if (input.docTypeCode == BusinessCode.OutProduceOrder.GetCode())
            {
                if (input.isDesignateErpWhouse == "0")
                {
                    List<CfgDepartmentErpWhouse> erpWhouses = new List<CfgDepartmentErpWhouse>();
                    if (string.IsNullOrWhiteSpace(input.belongDepartment))
                    {
                        erpWhouses = await GetDepartmentErpWhouses();
                    }
                    else
                    {
                        erpWhouses = await GetDepartmentErpWhouses(input.belongDepartment);
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

                    //if (!string.IsNullOrWhiteSpace(input.belongDepartment) && input.belongDepartment.Contains("机器人"))
                    //{
                    //    var Whouse = erpWhousePririties.FirstOrDefault(t => t.erpWhouseNo == "01B");
                    //    if (Whouse == null)
                    //    {
                    //        erpWhousePririties.Add(new erpWhousePrirityView() { erpWhouseNo = "01B", priority = 99 });
                    //    }
                    //}
                }
            }

            erpWhousePririties = erpWhousePririties.OrderBy(t => t.priority).ToList();
            //resultDtlList = (from dtl in stockDtls
            //                 join erpWhouse in erpWhousePririties on dtl.erpWhouseNo equals erpWhouse.erpWhouseNo
            //                 //where dtl.erpWhouseNo != stockAllocateView.erpWhouseNo
            //                 orderby erpWhouse.priority ascending
            //                 select dtl)
            //     .Where(x => x.lockFlag == "0")
            //     .Where(x => x.materialCode == input.materialCode)
            //     //.Where(x => string.IsNullOrWhiteSpace(x.projectNoBak) || x.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.projectNo))
            //     .Where(x => x.stockDtlStatus.ToString() == DictonaryHelper.StockStatus.InStore.GetCode())
            //     .Where(x => x.inspectionResult == DictonaryHelper.InspectionResult.Qualitified.GetCode() || x.inspectionResult == DictonaryHelper.InspectionResult.AcceptOnDeviation.GetCode())
            //     .Where(x => x.qty > x.occupyQty)
            //     .ToList();
            resultDtlList = GetWmsStockDtlsForSorts(stockDtls, erpWhousePririties, input);
            return resultDtlList;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="wmsStockDtls"></param>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        private List<WmsStockDtl> GetWmsStockDtlsForSorts(List<WmsStockDtl> wmsStockDtls, List<erpWhousePrirityDto> erpWhouses, WmsStockAllocateForHandDto stockAllocateView)
        {
            List<WmsStockDtl> result = new List<WmsStockDtl>();
            if (wmsStockDtls.Count > 0)
            {
                string[] erpWhouseArr = erpWhouses.Select(t => t.erpWhouseNo).ToArray();
                if (!string.IsNullOrWhiteSpace(stockAllocateView.projectNo))
                {
                    string[] prolist = new string[] { null, stockAllocateView.projectNo };
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
                        //.ThenBy(x => x.inwhTime)
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
                    }).ThenBy(x => x.projectNoBak)
                        .ThenBy(x => x.projectNo)
                        //.ThenBy(x => x.inwhTime)
                        .ToList();
                }
            }

            return result;
        }




        #region 手动分配按唯一码查询

        /// <summary>
        /// 唯一码查询库存
        /// </summary>
        /// <param name="input"></param>
        public async Task<BusinessResult> GatAvailableStockForUnii(GetStockByAllotDto input)
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

            var invoiceInfo = DC.Set<WmsOutInvoice>().Where(t => t.invoiceNo == invoiceDtlInfo.invoiceNo).FirstOrDefault();
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

            var matInfo = DC.Set<BasBMaterial>().Where(t => t.MaterialCode == invoiceDtlInfo.materialCode).AsNoTracking().FirstOrDefault();
            if (matInfo == null)
            {
                return result.Error($"待分配发货单明细对应物料【{invoiceDtlInfo.materialCode}】不存在");
            }

            var matCateInfo = DC.Set<BasBMaterialCategory>().Where(t => t.materialCategoryCode == matInfo.MaterialCategoryCode).AsNoTracking().FirstOrDefault();
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
                    uniicode = input.uniicode,
                    materialCode = invoiceDtlInfo.materialCode,
                    erpWhouseNo = invoiceDtlInfo.erpWhouseNo,
                    docBatchNo = invoiceDtlInfo.batchNo,
                    docTypeCode = invoiceInfo.docTypeCode,
                    isDesignateErpWhouse = isDesignateErpWhouse,
                    projectNo = invoiceDtlInfo.projectNo,
                    stockCodeList = stockCodeList,
                    batchNo = input.batchNo,
                    isProductFlag = isProductFlag,
                    isElecFlag = isElecFlag
                };
                if (isProductFlag && !string.IsNullOrWhiteSpace(invoiceDtlInfo.originalSn))
                {
                    allotView.snList = invoiceDtlInfo.originalSn.Split(',').ToList();
                }

                var stockUniis = await GetStockUniisForUnii(allotView, input);
                if (stockUniis.Any())
                {
                    result.outParams = await GetStockUniisForFill(stockUniis, wmsStockInfos, input, materailName);
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

        private async Task<List<WmsStockUniicodeDto>> GetStockUniisForUnii(WmsStockAllocateForHandDto input, GetStockByAllotDto searchInput)
        {
            bool isProductDoc = false;
            List<erpWhousePrirityDto> erpWhousePririties = new List<erpWhousePrirityDto>()
    {
        new erpWhousePrirityDto() { erpWhouseNo = input.erpWhouseNo }
    };
            if (input.docTypeCode == BusinessCode.OutProduceOrder.GetCode())
            {
                isProductDoc = true;
                if (input.isDesignateErpWhouse == "0")
                {
                    List<CfgDepartmentErpWhouse> erpWhouses = new List<CfgDepartmentErpWhouse>();
                    if (string.IsNullOrWhiteSpace(input.belongDepartment))
                    {
                        erpWhouses = await GetDepartmentErpWhouses();
                    }
                    else
                    {
                        erpWhouses = await GetDepartmentErpWhouses(input.belongDepartment);
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
                }
            }

            erpWhousePririties = erpWhousePririties.OrderBy(t => t.priority).ToList();
            List<string> erpWhouseList = erpWhousePririties.Select(t => t.erpWhouseNo).ToList();
            List<WmsStockUniicodeDto> stockDtls = new List<WmsStockUniicodeDto>();
            var query = from uniicode in DC.Set<WmsStockUniicode>()
                        join dtl in DC.Set<WmsStockDtl>() on uniicode.stockDtlId equals dtl.ID into dtlTemp
                        from dtl in dtlTemp.DefaultIfEmpty()
                        join main in DC.Set<WmsStock>() on uniicode.stockCode equals main.stockCode into mainTemp
                        from main in mainTemp.DefaultIfEmpty()
                        where uniicode.materialCode == input.materialCode && input.stockCodeList.Contains(uniicode.stockCode)
                                                                          && uniicode.qty > uniicode.occupyQty && uniicode.delayFrozenFlag != 1 && uniicode.exposeFrozenFlag != 1
                                                                          && uniicode.driedScrapFlag != 1
                                                                          && dtl.stockDtlStatus == 50 && dtl.lockFlag == 0
                                                                          && dtl.qty > dtl.occupyQty
                        select new { uniicode, dtl, main };


            if (string.IsNullOrWhiteSpace(input.docBatchNo))
            {
                query = query
                    //.Where(t => erpWhouseList.Contains(t.uniicode.erpWhouseNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(searchInput.batchNo), t => t.uniicode.batchNo.Contains(searchInput.batchNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(searchInput.erpWhouseNo), t => t.uniicode.erpWhouseNo.Contains(searchInput.erpWhouseNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.uniicode), t => t.uniicode.uniicode.Contains(input.uniicode));
                // .WhereIf(input.snList.Any(), t => input.snList.Contains(t.uniicode.uniicode))
                //.WhereIf(isProductDoc, x => string.IsNullOrWhiteSpace(x.dtl.projectNoBak) || x.dtl.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.dtl.projectNo));
            }
            else
            {
                query = query
                    //.Where(t => erpWhouseList.Contains(t.uniicode.erpWhouseNo))
                    .Where(t => t.uniicode.batchNo == input.docBatchNo)
                    .WhereIf(!string.IsNullOrWhiteSpace(searchInput.batchNo), t => t.uniicode.batchNo.Contains(searchInput.batchNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(searchInput.erpWhouseNo), t => t.uniicode.erpWhouseNo.Contains(searchInput.erpWhouseNo))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.uniicode), t => t.uniicode.uniicode.Contains(input.uniicode));
                //.WhereIf(input.snList.Any(), t => input.snList.Contains(t.uniicode.uniicode))
                //.WhereIf(isProductDoc, x => string.IsNullOrWhiteSpace(x.dtl.projectNoBak) || x.dtl.projectNoBak == input.projectNo || string.IsNullOrWhiteSpace(x.dtl.projectNo));
            }

            #region 实体转换

            var viewQuery = query.Select(x => new WmsStockUniicodeDto
            {
                ID = x.uniicode.ID,
                dataCode = x.uniicode.dataCode,
                delayFrozenFlag = x.uniicode.delayFrozenFlag,
                delayFrozenReason = x.uniicode.delayFrozenReason,
                delayReason = x.uniicode.delayReason,
                stockDtlId = x.uniicode.stockDtlId,
                delayTimes = x.uniicode.delayTimes,
                delayToEndDate = x.uniicode.delayToEndDate,
                driedScrapFlag = x.uniicode.driedScrapFlag,
                driedTimes = x.uniicode.driedTimes,
                productDate = x.uniicode.productDate,
                expDate = x.uniicode.expDate,
                exposeFrozenFlag = x.uniicode.exposeFrozenFlag,
                exposeFrozenReason = x.uniicode.exposeFrozenReason,
                realExposeTimes = x.uniicode.realExposeTimes,
                inwhTime = x.uniicode.inwhTime,
                leftMslTimes = x.uniicode.leftMslTimes,
                packageTime = x.uniicode.packageTime,
                positionNo = x.uniicode.positionNo,
                unpackStatus = x.uniicode.unpackStatus,
                mslGradeCode = x.uniicode.mslGradeCode,
                uniicode = x.uniicode.uniicode,
                unpackTime = x.uniicode.unpackTime,
                whouseNo = x.uniicode.whouseNo,
                areaNo = x.uniicode.areaNo,
                erpWhouseNo = x.uniicode.erpWhouseNo,
                proprietorCode = x.uniicode.proprietorCode,
                stockCode = x.uniicode.stockCode,
                palletBarcode = x.uniicode.palletBarcode,
                projectNo = x.uniicode.projectNo,
                materialCode = x.uniicode.materialCode,
                materialName = x.uniicode.materialName,
                supplierCode = x.uniicode.supplierCode,
                supplierName = x.uniicode.supplierName,
                supplierNameEn = x.uniicode.supplierNameEn,
                supplierNameAlias = x.uniicode.supplierNameAlias,
                batchNo = x.uniicode.batchNo,
                materialSpec = x.uniicode.materialSpec,
                //stockQty = x.uniicode.stockQty,
                qty = x.uniicode.qty,
                occupyQty = x.uniicode.occupyQty,
                inspectionResult = x.uniicode.inspectionResult,
                stockDtlStatus = x.dtl.stockDtlStatus,
                lockFlag = x.dtl.lockFlag,
                lockReason = x.dtl.lockReason,
                skuCode = x.uniicode.skuCode,
                //delFlag = x.uniicode.delFlag,
                extend1 = x.uniicode.extend1,
                extend2 = x.uniicode.extend2,
                extend3 = x.uniicode.extend3,
                extend4 = x.uniicode.extend4,
                extend5 = x.uniicode.extend5,
                extend6 = x.uniicode.extend6,
                extend7 = x.uniicode.extend7,
                extend8 = x.uniicode.extend8,
                extend9 = x.uniicode.extend9,
                extend10 = x.uniicode.extend10,
                extend11 = x.uniicode.extend11,
                chipSize = x.uniicode.chipSize,
                chipThickness = x.uniicode.chipThickness,
                chipModel = x.uniicode.chipModel,
                dafType = x.uniicode.dafType,
                regionNo = x.main.regionNo,
                roadwayNo = x.main.roadwayNo,
                binNo = x.main.binNo,
                loadedType = x.main.loadedType,
                locAllotGroup = x.main.locAllotGroup,
                locNo = x.main.locNo,
                specialFlag = x.main.specialFlag,
                specialMsg = x.main.specialMsg,
                errFlag = x.main.errFlag,
                errMsg = x.main.errMsg,
                invoiceNo = x.main.invoiceNo,
                weight = x.main.weight,
                stockStatus = x.main.stockStatus,
                CreateTime = x.uniicode.CreateTime,
                CreateBy = x.uniicode.CreateBy,
                UpdateTime = x.uniicode.UpdateTime,
                UpdateBy = x.uniicode.UpdateBy,
                canQty = x.uniicode.qty - x.uniicode.occupyQty,
                dtlQty = x.dtl.qty,
                dtlOccupyQty = x.dtl.occupyQty,
                dtlCanQty = x.dtl.qty - x.dtl.occupyQty
                //materialCategoryCode = x.material.materialCategoryCode,
                //materialTypeCode = x.material.materialTypeCode
            });
            stockDtls = await viewQuery.ToListAsync();

            #endregion


            //筛选排序
            stockDtls = GetWmsStockUniisForSort(stockDtls, erpWhousePririties, input);
            return stockDtls;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="wmsStockDtls"></param>
        /// <param name="stockAllocateView"></param>
        /// <returns></returns>
        private List<WmsStockUniicodeDto> GetWmsStockUniisForSort(List<WmsStockUniicodeDto> wmsStockUniis, List<erpWhousePrirityDto> erpWhouses, WmsStockAllocateForHandDto stockAllocateView)
        {
            List<WmsStockUniicodeDto> result = new List<WmsStockUniicodeDto>();
            if (wmsStockUniis != null)
            {
                string[] erpWhouseArr = erpWhouses.Select(t => t.erpWhouseNo).ToArray();
                if (!string.IsNullOrWhiteSpace(stockAllocateView.projectNo))
                {
                    string[] prolist = new string[] { null, stockAllocateView.projectNo };
                    result = wmsStockUniis.OrderBy(x =>
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
                        .OrderBy(x =>
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
                        .ThenBy(x => x.inwhTime)
                        .ToList();
                }
                else
                {
                    result = wmsStockUniis.OrderBy(x =>
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
                        .OrderBy(x => x.projectNoBak)
                        .ThenBy(x => x.projectNo)
                        .ThenBy(x => x.inwhTime)
                        .ToList();
                }
            }

            return result;
        }


        private async Task<PagedResultDto<WmsStockUniicodeDto>> GetStockUniisForFill(List<WmsStockUniicodeDto> wmsStockDtlInfos, List<WmsStock> wmsStockInfos, GetStockByAllotDto input, string materailName)
        {
            PagedResultDto<WmsStockUniicodeDto> result = new PagedResultDto<WmsStockUniicodeDto>();

            try
            {
                var count = wmsStockDtlInfos.Count;
                var queryResult = wmsStockDtlInfos.Skip((input.page - 1) * input.limit).Take(input.limit).ToList();
                if (queryResult.Any())
                {
                    // var matList = queryResult.Select(t => t.materialCode).Distinct().ToList();
                    whouseEntities = await DC.Set<BasWWhouse>().AsNoTracking().ToListAsync();
                    areaEntities = await DC.Set<BasWArea>().AsNoTracking().ToListAsync();
                    regionEntities = await DC.Set<BasWRegion>().AsNoTracking().ToListAsync();
                    dicEntities = await DC.Set<SysDictionary>().AsNoTracking().ToListAsync();
                    //roadwayEntities = DC.Set<BasWRoadway>().AsNoTracking().ToList();
                    //binEntities = DC.Set<BasWBin>().AsNoTracking().ToList();
                    proEntities = await DC.Set<BasBProprietor>().AsNoTracking().ToListAsync();
                    erpWhouseEntities = await DC.Set<BasWErpWhouse>().AsNoTracking().ToListAsync();
                    unitEntities = await DC.Set<BasBUnit>().ToListAsync();
                    foreach (WmsStockUniicodeDto item in queryResult)
                    {
                        UniiMapper(item);
                        item.materialName = materailName;
                    }

                    result = new PagedResultDto<WmsStockUniicodeDto>()
                    {
                        items = queryResult,
                        total = count,
                    };
                }
            }
            catch
            {
            }

            return result;
        }

        private WmsStockUniicodeDto UniiMapper(WmsStockUniicodeDto dto)
        {
            //if (matEntities.Any())
            //{
            //    var relEntity = matEntities.FirstOrDefault(t => t.materialCode == dto.materialCode);
            //    if (relEntity != null)
            //    {
            //        dto.materialCategoryCode = relEntity.materialCategoryCode;
            //        dto.materialTypeCode = relEntity.materialTypeCode;
            //        dto.unitCode = relEntity.unitCode;
            //    }
            //}
            if (whouseEntities.Any())
            {
                var relEntity = whouseEntities.FirstOrDefault(t => t.whouseNo == dto.whouseNo);
                if (relEntity != null)
                {
                    dto.whouseName = relEntity.whouseName;
                }
            }

            if (areaEntities.Any())
            {
                var relEntity = areaEntities.FirstOrDefault(t => t.areaNo == dto.areaNo);
                if (relEntity != null)
                {
                    dto.areaName = relEntity.areaName;
                }
            }

            if (regionEntities.Any())
            {
                var relEntity = regionEntities.FirstOrDefault(t => t.regionNo == dto.regionNo);
                if (relEntity != null)
                {
                    dto.regionName = relEntity.regionName;
                }
            }

            if (roadwayEntities.Any())
            {
                var relEntity = roadwayEntities.FirstOrDefault(t => t.roadwayNo == dto.roadwayNo);
                if (relEntity != null)
                {
                    dto.roadwayName = relEntity.roadwayName;
                }
            }

            if (binEntities.Any())
            {
                var relEntity = binEntities.FirstOrDefault(t => t.binNo == dto.binNo);
                if (relEntity != null)
                {
                    dto.binName = relEntity.binName;
                }
            }

            if (proEntities.Any())
            {
                var relEntity = proEntities.FirstOrDefault(t => t.proprietorCode == dto.proprietorCode);
                if (relEntity != null)
                {
                    dto.proprietorName = relEntity.proprietorName;
                }
            }

            if (erpWhouseEntities.Any())
            {
                var relEntity = erpWhouseEntities.FirstOrDefault(t => t.erpWhouseNo == dto.erpWhouseNo);
                if (relEntity != null)
                {
                    dto.erpWhouseName = relEntity.erpWhouseName;
                }
            }

            if (matTypeEntities.Any())
            {
                var relEntity = matTypeEntities.FirstOrDefault(t => t.materialTypeCode == dto.materialTypeCode);
                if (relEntity != null)
                {
                    dto.materialTypeName = relEntity.materialTypeName;
                }
            }

            //if (matCategoryEntities.Any())
            //{
            //    var relEntity = matCategoryEntities.FirstOrDefault(t => t.materialCategoryCode == dto.materialCategoryCode);
            //    if (relEntity != null)
            //    {
            //        dto.materialCategoryName = relEntity.materialCategoryName;
            //        dto.electronicMaterialFlag = relEntity.materialFlag;
            //    }
            //}
            if (unitEntities.Any())
            {
                var relEntity = unitEntities.FirstOrDefault(t => t.unitCode == dto.unitCode);
                if (relEntity != null)
                {
                    dto.unitName = relEntity.unitName;
                }
            }

            if (dicEntities.Any())
            {
                dto.loadedTypeDesc = GetDicItemDisVal(dicEntities, "LOADED_TYPE", dto.loadedType);
                dto.inspectionResultDesc = GetDicItemDisVal(dicEntities, "INSPECTION_RESULT", dto.inspectionResult);
                dto.stockStatusDesc = GetDicItemDisVal(dicEntities, "STOCK_STATUS", dto.stockStatus);
                dto.stockDtlStatusDesc = GetDicItemDisVal(dicEntities, "STOCK_DTL_STATUS", dto.stockDtlStatus);
                dto.lockFlagDesc = GetDicItemDisVal(dicEntities, "LOCK_FLAG", dto.lockFlag);
                dto.errFlagDesc = GetDicItemDisVal(dicEntities, "ERR_FLAG", dto.errFlag);
                dto.specialFlagDesc = GetDicItemDisVal(dicEntities, "SPECIAL_FLAG", dto.specialFlag);
                dto.delayFrozenFlagDesc = GetDicItemDisVal(dicEntities, "DELAY_FROZEN_FLAG", dto.delayFrozenFlag);
                dto.driedScrapFlagDesc = GetDicItemDisVal(dicEntities, "DIRED_SCRAP_FLAG", dto.driedScrapFlag);
                dto.exposeFrozenFlagDesc = GetDicItemDisVal(dicEntities, "EXPOSE_FROZEN_FLAG", dto.exposeFrozenFlag);
                dto.unpackStatusDesc = GetDicItemDisVal(dicEntities, "UNPACK_STATUS", dto.unpackStatus);
                dto.materialFlagDesc = GetDicItemDisVal(dicEntities, "MATERIAL_FLAG", dto.materialFlag);
            }

            return dto;
        }

        #endregion
    }
}
