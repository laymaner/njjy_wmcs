using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocGroupVMs
{
    public partial class BasWLocGroupListVM : BasePagedListVM<BasWLocGroup_View, BasWLocGroupSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWLocGroup_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWLocGroup_View>>{
                this.MakeGridHeader(x => x.locGroupName),
                this.MakeGridHeader(x => x.locGroupNameAlias),
                this.MakeGridHeader(x => x.locGroupNameEn),
                this.MakeGridHeader(x => x.locGroupNo),
                this.MakeGridHeader(x => x.locGroupType),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWLocGroup_View> GetSearchQuery()
        {
            var query = DC.Set<BasWLocGroup>()
                .CheckContain(Searcher.locGroupName, x=>x.locGroupName)
                .CheckContain(Searcher.locGroupNo, x=>x.locGroupNo)
                .CheckContain(Searcher.locGroupType, x=>x.locGroupType)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWLocGroup_View
                {
				    ID = x.ID,
                    locGroupName = x.locGroupName,
                    locGroupNameAlias = x.locGroupNameAlias,
                    locGroupNameEn = x.locGroupNameEn,
                    locGroupNo = x.locGroupNo,
                    locGroupType = x.locGroupType,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWLocGroup_View : BasWLocGroup{

    }
}
