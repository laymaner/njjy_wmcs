using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocVMs
{
    public partial class BasWLocListVM : BasePagedListVM<BasWLoc_View, BasWLocSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWLoc_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWLoc_View>>{
                this.MakeGridHeader(x => x.locGroupNo),
                this.MakeGridHeader(x => x.locName),
                this.MakeGridHeader(x => x.locNameAlias),
                this.MakeGridHeader(x => x.locNameEn),
                this.MakeGridHeader(x => x.locNo),
                this.MakeGridHeader(x => x.locTypeCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWLoc_View> GetSearchQuery()
        {
            var query = DC.Set<BasWLoc>()
                .CheckContain(Searcher.locGroupNo, x=>x.locGroupNo)
                .CheckContain(Searcher.locName, x=>x.locName)
                .CheckContain(Searcher.locNo, x=>x.locNo)
                .CheckContain(Searcher.locTypeCode, x=>x.locTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWLoc_View
                {
				    ID = x.ID,
                    locGroupNo = x.locGroupNo,
                    locName = x.locName,
                    locNameAlias = x.locNameAlias,
                    locNameEn = x.locNameEn,
                    locNo = x.locNo,
                    locTypeCode = x.locTypeCode,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWLoc_View : BasWLoc{

    }
}
