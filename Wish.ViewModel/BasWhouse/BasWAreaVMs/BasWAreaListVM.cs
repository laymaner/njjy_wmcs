using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWAreaVMs
{
    public partial class BasWAreaListVM : BasePagedListVM<BasWArea_View, BasWAreaSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWArea_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWArea_View>>{
                this.MakeGridHeader(x => x.areaName),
                this.MakeGridHeader(x => x.areaNameAlias),
                this.MakeGridHeader(x => x.areaNameEn),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.areaType),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWArea_View> GetSearchQuery()
        {
            var query = DC.Set<BasWArea>()
                .CheckContain(Searcher.areaName, x=>x.areaName)
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.areaType, x=>x.areaType)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWArea_View
                {
				    ID = x.ID,
                    areaName = x.areaName,
                    areaNameAlias = x.areaNameAlias,
                    areaNameEn = x.areaNameEn,
                    areaNo = x.areaNo,
                    areaType = x.areaType,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWArea_View : BasWArea{

    }
}
