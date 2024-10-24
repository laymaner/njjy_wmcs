using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcRecordHisVMs
{
    public partial class WmsItnQcRecordHisListVM : BasePagedListVM<WmsItnQcRecordHis_View, WmsItnQcRecordHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnQcRecordHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnQcRecordHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.extend1),
                this.MakeGridHeader(x => x.extend10),
                this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.extend12),
                this.MakeGridHeader(x => x.extend13),
                this.MakeGridHeader(x => x.extend14),
                this.MakeGridHeader(x => x.extend15),
                this.MakeGridHeader(x => x.extend2),
                this.MakeGridHeader(x => x.extend3),
                this.MakeGridHeader(x => x.extend4),
                this.MakeGridHeader(x => x.extend5),
                this.MakeGridHeader(x => x.extend6),
                this.MakeGridHeader(x => x.extend7),
                this.MakeGridHeader(x => x.extend8),
                this.MakeGridHeader(x => x.extend9),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.itnQcDtlId),
                this.MakeGridHeader(x => x.itnQcLocNo),
                this.MakeGridHeader(x => x.itnQcNo),
                this.MakeGridHeader(x => x.itnQcStatus),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.stockQty),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnQcRecordHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnQcRecordHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.itnQcLocNo, x=>x.itnQcLocNo)
                .CheckContain(Searcher.itnQcNo, x=>x.itnQcNo)
                .CheckEqual(Searcher.itnQcStatus, x=>x.itnQcStatus)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnQcRecordHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    erpWhouseNo = x.erpWhouseNo,
                    extend1 = x.extend1,
                    extend10 = x.extend10,
                    extend11 = x.extend11,
                    extend12 = x.extend12,
                    extend13 = x.extend13,
                    extend14 = x.extend14,
                    extend15 = x.extend15,
                    extend2 = x.extend2,
                    extend3 = x.extend3,
                    extend4 = x.extend4,
                    extend5 = x.extend5,
                    extend6 = x.extend6,
                    extend7 = x.extend7,
                    extend8 = x.extend8,
                    extend9 = x.extend9,
                    inspectionResult = x.inspectionResult,
                    itnQcDtlId = x.itnQcDtlId,
                    itnQcLocNo = x.itnQcLocNo,
                    itnQcNo = x.itnQcNo,
                    itnQcStatus = x.itnQcStatus,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    palletBarcode = x.palletBarcode,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    stockCode = x.stockCode,
                    stockDtlId = x.stockDtlId,
                    stockQty = x.stockQty,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnQcRecordHis_View : WmsItnQcRecordHis{

    }
}
