using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockDtlVMs
{
    public partial class WmsStockDtlListVM : BasePagedListVM<WmsStockDtl_View, WmsStockDtlSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsStockDtl_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsStockDtl_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.extend1),
                this.MakeGridHeader(x => x.extend10),
                this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.chipSize),
                this.MakeGridHeader(x => x.chipThickness),
                this.MakeGridHeader(x => x.chipModel),
                this.MakeGridHeader(x => x.dafType),
                this.MakeGridHeader(x => x.extend2),
                this.MakeGridHeader(x => x.extend3),
                this.MakeGridHeader(x => x.extend4),
                this.MakeGridHeader(x => x.extend5),
                this.MakeGridHeader(x => x.extend6),
                this.MakeGridHeader(x => x.extend7),
                this.MakeGridHeader(x => x.extend8),
                this.MakeGridHeader(x => x.extend9),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.lockFlag),
                this.MakeGridHeader(x => x.lockReason),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.occupyQty),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.projectNoBak),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlStatus),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.unitCode),
                //this.MakeGridHeader(x => x.version),
                //this.MakeGridHeader(x => x.versionDirty),
                //this.MakeGridHeader(x => x.versionExtend1),
                //this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsStockDtl_View> GetSearchQuery()
        {
            var query = DC.Set<WmsStockDtl>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.projectNoBak, x=>x.projectNoBak)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                //.CheckContain(Searcher.businessCode, x=>x.businessCode)
                .Select(x => new WmsStockDtl_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    erpWhouseNo = x.erpWhouseNo,
                    extend1 = x.extend1,
                    extend10 = x.extend10,
                    extend11 = x.extend11,
                    chipSize = x.chipSize,
                    chipThickness = x.chipThickness,
                    chipModel = x.chipModel,
                    dafType = x.dafType,
                    extend2 = x.extend2,
                    extend3 = x.extend3,
                    extend4 = x.extend4,
                    extend5 = x.extend5,
                    extend6 = x.extend6,
                    extend7 = x.extend7,
                    extend8 = x.extend8,
                    extend9 = x.extend9,
                    inspectionResult = x.inspectionResult,
                    lockFlag = x.lockFlag,
                    lockReason = x.lockReason,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    occupyQty = x.occupyQty,
                    palletBarcode = x.palletBarcode,
                    projectNo = x.projectNo,
                    projectNoBak = x.projectNoBak,
                    proprietorCode = x.proprietorCode,
                    qty = x.qty,
                    skuCode = x.skuCode,
                    stockCode = x.stockCode,
                    stockDtlStatus = x.stockDtlStatus,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    whouseNo = x.whouseNo,
                    unitCode = x.unitCode,
                    //version = x.version,
                    //versionDirty = x.versionDirty,
                    //versionExtend1 = x.versionExtend1,
                    //businessCode = x.businessCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsStockDtl_View : WmsStockDtl{

    }
}
