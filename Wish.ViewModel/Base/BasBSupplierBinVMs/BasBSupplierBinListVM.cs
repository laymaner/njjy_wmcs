using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierBinVMs
{
    public partial class BasBSupplierBinListVM : BasePagedListVM<BasBSupplierBin_View, BasBSupplierBinSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBSupplierBin_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBSupplierBin_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBSupplierBin_View> GetSearchQuery()
        {
            var query = DC.Set<BasBSupplierBin>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .Select(x => new BasBSupplierBin_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    binNo = x.binNo,
                    regionNo = x.regionNo,
                    supplierCode = x.supplierCode,
                    whouseNo = x.whouseNo,
                    erpWhouseNo = x.erpWhouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBSupplierBin_View : BasBSupplierBin{

    }
}
