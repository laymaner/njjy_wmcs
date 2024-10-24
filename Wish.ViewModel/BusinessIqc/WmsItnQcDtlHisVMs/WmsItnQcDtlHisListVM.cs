using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcDtlHisVMs
{
    public partial class WmsItnQcDtlHisListVM : BasePagedListVM<WmsItnQcDtlHis_View, WmsItnQcDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnQcDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnQcDtlHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.confirmQty),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.itnQcDtlStatus),
                this.MakeGridHeader(x => x.itnQcNo),
                this.MakeGridHeader(x => x.itnQcQty),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnQcDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnQcDtlHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckEqual(Searcher.itnQcDtlStatus, x=>x.itnQcDtlStatus)
                .CheckContain(Searcher.itnQcNo, x=>x.itnQcNo)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnQcDtlHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    batchNo = x.batchNo,
                    confirmQty = x.confirmQty,
                    erpWhouseNo = x.erpWhouseNo,
                    inspectionResult = x.inspectionResult,
                    itnQcDtlStatus = x.itnQcDtlStatus,
                    itnQcNo = x.itnQcNo,
                    itnQcQty = x.itnQcQty,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    proprietorCode = x.proprietorCode,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnQcDtlHis_View : WmsItnQcDtlHis{

    }
}
