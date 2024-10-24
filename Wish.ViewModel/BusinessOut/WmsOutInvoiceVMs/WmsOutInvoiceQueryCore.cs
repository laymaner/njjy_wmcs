using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Com.Wish.Model.Base;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Wish.ViewModel.Common.Dtos;
using Wish.Model.System;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceVM
    {
        /*关联表
         * 字典表：SysDictionary
         * 单据表：CfgDocType
         * 仓库表：BasWWhouse
         */
        public List<SysDictionary> dictEntities = new List<SysDictionary>();
        public List<CfgDocType> docEntitiees = new List<CfgDocType>();
        public List<BasWWhouse> whouseEntities = new List<BasWWhouse>();
        public List<BasBMaterial> materialEntities = new List<BasBMaterial>();
        public List<BasBUnit> unitsEntities = new List<BasBUnit>();
        public List<BasWErpWhouse> erpWhouseEntities = new List<BasWErpWhouse>();
        /// <summary>
        /// 新增类型为库存选取时查询库存
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ResultGetStockDtlByMaterialDto GetStockDtlByMaterial(WmsOutInvoiceSearcher Search)
        {
            ResultGetStockDtlByMaterialDto resultGetStockDtlByMaterialApi = new ResultGetStockDtlByMaterialDto();
            List<GetStockDtlByMaterialsDto> querys = new List<GetStockDtlByMaterialsDto>();
            GetStockDtlByMaterialsDto query = null;
            var paramValueCode = "";
            if (!string.IsNullOrEmpty(Search.docTypeCode))
            {
                var cfgDocDtl = DC.Set<CfgDocTypeDtl>().Where(t => t.docTypeCode == Search.docTypeCode && t.paramCode == "IS_CHECK_STOCK").FirstOrDefault();
                if (cfgDocDtl != null)
                {
                    paramValueCode = cfgDocDtl.paramValueCode;
                }
            }
            if (paramValueCode == "1")
            {
                var queryList = DC.Set<WmsStockDtl>()
                        .Where(x => x.stockDtlStatus == 50)
                        .CheckContain(Search.materialCode, x => x.materialCode)
                        .CheckContain(Search.materialName, x => x.materialName)
                        .ToList();
                if (queryList.Count > 0)
                {
                    foreach (var item in queryList)
                    {
                        query = new GetStockDtlByMaterialsDto
                        {
                            materialCode = item.materialCode,
                            materialName = item.materialName,
                            materialSpec = item.materialSpec,
                            inspectionResult = item.inspectionResult,
                            qty = item.qty,
                            occupyQty = item.occupyQty,
                            //batchNo = item.batchNo,
                            //TODO：batchNo补充
                            supplierCode = item.supplierCode,
                            supplierName = item.supplierName,
                            erpWhouseNo = item.erpWhouseNo
                        };
                        inputMapper(query);
                        querys.Add(query);
                    }
                }
            }
            resultGetStockDtlByMaterialApi.items = querys.AsQueryable().Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
            resultGetStockDtlByMaterialApi.total = querys.Count;
            return resultGetStockDtlByMaterialApi;
        }
        private GetStockDtlByMaterialsDto inputMapper(GetStockDtlByMaterialsDto dto)
        {
            dictEntities = DC.Set<SysDictionary>().Where(x => x.dictionaryCode == "INSPECTION_RESULT" && x.dictionaryItemCode == dto.inspectionResult.ToString()).ToList();
            materialEntities = DC.Set<BasBMaterial>().Where(x => x.MaterialCode == dto.materialCode).ToList();

            if (dictEntities.Any())
            {
                //dto.inspectionResultDesc = GetDicItemDisVal(dicEntities, "INSPECTION_RESULT", dto.inspectionResult);
                dto.inspectionResultDesc = dictEntities.FirstOrDefault().dictionaryItemName;
            }
            if (materialEntities.Any())
            {
                //var relEntity = materialEntities.FirstOrDefault(t => t.materialCode == dto.materialCode);
                var relEntity = materialEntities.FirstOrDefault();
                if (relEntity != null)
                {
                    unitsEntities = DC.Set<BasBUnit>().Where(x => x.unitCode == relEntity.UnitCode).ToList();
                    if (unitsEntities.Any())
                    {
                        var unitEntity = unitsEntities.FirstOrDefault();
                        if (unitEntity != null)
                        {
                            dto.unitName = unitEntity.unitName;
                        }
                    }
                }
            }
            return dto;
        }

    }
}
