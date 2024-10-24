using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs
{
    public partial class BasWErpWhouseBinListVM : BasePagedListVM<BasWErpWhouseBin_View, BasWErpWhouseBinSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWErpWhouseBin_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWErpWhouseBin_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.delFlag),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWErpWhouseBin_View> GetSearchQuery()
        {
            var query = DC.Set<BasWErpWhouseBin>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.delFlag, x=>x.delFlag)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWErpWhouseBin_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    binNo = x.binNo,
                    delFlag = x.delFlag,
                    erpWhouseNo = x.erpWhouseNo,
                    regionNo = x.regionNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWErpWhouseBin_View : BasWErpWhouseBin{

    }
}
