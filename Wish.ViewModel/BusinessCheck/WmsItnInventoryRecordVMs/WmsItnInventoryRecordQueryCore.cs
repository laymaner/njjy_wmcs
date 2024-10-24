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
using Wish.Model.System;
using Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs;
using Wish.ViewModel.Common.Dtos;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordVM
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
        public async  Task<ResultGetStockDtlByMaterialDto> GetStockDtlByMaterial(WmsItnInventoryRecordSearcher Search)
        {
            ResultGetStockDtlByMaterialDto resultGetStockDtlByMaterialApi = new ResultGetStockDtlByMaterialDto();
            List<GetStockDtlByMaterialsDto> querys = new List<GetStockDtlByMaterialsDto>();
            var queryList = (from stockDtl in DC.Set<WmsStockDtl>() 
                            join stockUniicode in DC.Set<WmsStockUniicode>() on stockDtl.ID equals stockUniicode.stockDtlId into tempStockUniicode
                            from stockUniicode in tempStockUniicode.DefaultIfEmpty()
                            select new GetStockDtlByMaterialsDto
                            {
                                materialCode = stockUniicode.materialCode,
                                materialName = stockUniicode.materialName,
                                materialSpec = stockUniicode.materialSpec,
                                inspectionResult = stockUniicode.inspectionResult,
                                qty = stockUniicode.qty,
                                occupyQty = stockUniicode.occupyQty,
                                batchNo = stockUniicode.batchNo,
                                //TODO：batchNo补充
                                supplierCode = stockUniicode.supplierCode,
                                supplierName = stockUniicode.supplierName,
                                erpWhouseNo = stockUniicode.erpWhouseNo,
                                projectNo = stockUniicode.projectNo,
                                palletBarcode=stockUniicode.palletBarcode,
                                uniiCode=stockUniicode.uniicode,
                                stockDtlStatus=stockDtl.stockDtlStatus,
                            })
                    .Where(x => x.stockDtlStatus == 50)
                    .CheckContain(Search.uniiCode, x => x.uniiCode)
                    .CheckContain(Search.projectNo, x => x.projectNo)
                    .ToList();
            dictEntities =await DC.Set<SysDictionary>().AsNoTracking().ToListAsync();
            materialEntities =await DC.Set<BasBMaterial>().AsNoTracking().ToListAsync();
            unitsEntities =await DC.Set<BasBUnit>().AsNoTracking().ToListAsync();
            if (queryList.Count > 0)
            {
                foreach (var item in queryList)
                {
                    inputMapper(item, dictEntities, materialEntities, unitsEntities);
                    querys.Add(item);
                }
            }

            resultGetStockDtlByMaterialApi.items = querys.AsQueryable().Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
            resultGetStockDtlByMaterialApi.total = querys.Count;
            return resultGetStockDtlByMaterialApi;
        }
        private GetStockDtlByMaterialsDto inputMapper(GetStockDtlByMaterialsDto dto,List<SysDictionary> dictionaries,List<BasBMaterial> basBMaterials,List<BasBUnit> basBUnits)
        {
            var dictEntity = dictionaries.Where(x => x.dictionaryCode == "INSPECTION_RESULT" && x.dictionaryItemCode == dto.inspectionResult.ToString()).ToList();
            var materialEntity = basBMaterials.Where(x => x.MaterialCode == dto.materialCode).ToList();

            if (dictEntity.Any())
            {
                //dto.inspectionResultDesc = GetDicItemDisVal(dicEntities, "INSPECTION_RESULT", dto.inspectionResult);
                dto.inspectionResultDesc = dictEntity.FirstOrDefault().dictionaryItemName;
            }
            if (materialEntity.Any())
            {
                //var relEntity = materialEntities.FirstOrDefault(t => t.materialCode == dto.materialCode);
                var relEntity = materialEntity.FirstOrDefault();
                if (relEntity != null)
                {
                    var unitsEntity = basBUnits.Where(x => x.unitCode == relEntity.UnitCode).ToList();
                    if (unitsEntity.Any())
                    {
                        var unitEntity = unitsEntity.FirstOrDefault();
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
