using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockAdjustVMs
{
    public partial class WmsStockAdjustListVM : BasePagedListVM<WmsStockAdjust_View, WmsStockAdjustSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsStockAdjust_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsStockAdjust_View>>{
                this.MakeGridHeader(x => x.adjustDesc),
                this.MakeGridHeader(x => x.adjustOperate),
                this.MakeGridHeader(x => x.adjustType),
                this.MakeGridHeader(x => x.packageBarcode),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsStockAdjust_View> GetSearchQuery()
        {
            var query = DC.Set<WmsStockAdjust>()
                .CheckContain(Searcher.adjustOperate, x=>x.adjustOperate)
                .CheckContain(Searcher.adjustType, x=>x.adjustType)
                .CheckContain(Searcher.packageBarcode, x=>x.packageBarcode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsStockAdjust_View
                {
				    ID = x.ID,
                    adjustDesc = x.adjustDesc,
                    adjustOperate = x.adjustOperate,
                    adjustType = x.adjustType,
                    packageBarcode = x.packageBarcode,
                    palletBarcode = x.palletBarcode,
                    proprietorCode = x.proprietorCode,
                    stockCode = x.stockCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsStockAdjust_View : WmsStockAdjust{

    }
}
