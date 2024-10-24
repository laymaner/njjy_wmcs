using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockReconcileResultHisVMs
{
    public partial class WmsStockReconcileResultHisListVM : BasePagedListVM<WmsStockReconcileResultHis_View, WmsStockReconcileResultHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsStockReconcileResultHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsStockReconcileResultHis_View>>{
                this.MakeGridHeader(x => x.differQty),
                this.MakeGridHeader(x => x.erpStockNo),
                this.MakeGridHeader(x => x.erpStockQty),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.materialCategoryCode),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialTypeCode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.reconcileNo),
                this.MakeGridHeader(x => x.reconcileOperator),
                this.MakeGridHeader(x => x.reconcileTime),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.wmsStockQty),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsStockReconcileResultHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsStockReconcileResultHis>()
                .CheckEqual(Searcher.differQty, x=>x.differQty)
                .CheckContain(Searcher.erpStockNo, x=>x.erpStockNo)
                .CheckEqual(Searcher.erpStockQty, x=>x.erpStockQty)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.materialCategoryCode, x=>x.materialCategoryCode)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialTypeCode, x=>x.materialTypeCode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.reconcileNo, x=>x.reconcileNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckEqual(Searcher.wmsStockQty, x=>x.wmsStockQty)
                .Select(x => new WmsStockReconcileResultHis_View
                {
				    ID = x.ID,
                    differQty = x.differQty,
                    erpStockNo = x.erpStockNo,
                    erpStockQty = x.erpStockQty,
                    erpWhouseNo = x.erpWhouseNo,
                    materialCategoryCode = x.materialCategoryCode,
                    materialCode = x.materialCode,
                    materialTypeCode = x.materialTypeCode,
                    proprietorCode = x.proprietorCode,
                    reconcileNo = x.reconcileNo,
                    reconcileOperator = x.reconcileOperator,
                    reconcileTime = x.reconcileTime,
                    whouseNo = x.whouseNo,
                    wmsStockQty = x.wmsStockQty,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsStockReconcileResultHis_View : WmsStockReconcileResultHis{

    }
}
