using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionTypeVMs
{
    public partial class BasWRegionTypeListVM : BasePagedListVM<BasWRegionType_View, BasWRegionTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWRegionType_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWRegionType_View>>{
                this.MakeGridHeader(x => x.regionTypeCode),
                this.MakeGridHeader(x => x.regionTypeFlag),
                this.MakeGridHeader(x => x.regionTypeName),
                this.MakeGridHeader(x => x.regionTypeNameAlias),
                this.MakeGridHeader(x => x.regionTypeNameEn),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWRegionType_View> GetSearchQuery()
        {
            var query = DC.Set<BasWRegionType>()
                .CheckContain(Searcher.regionTypeCode, x=>x.regionTypeCode)
                .CheckContain(Searcher.regionTypeFlag, x=>x.regionTypeFlag)
                .CheckContain(Searcher.regionTypeName, x=>x.regionTypeName)
                .Select(x => new BasWRegionType_View
                {
				    ID = x.ID,
                    regionTypeCode = x.regionTypeCode,
                    regionTypeFlag = x.regionTypeFlag,
                    regionTypeName = x.regionTypeName,
                    regionTypeNameAlias = x.regionTypeNameAlias,
                    regionTypeNameEn = x.regionTypeNameEn,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWRegionType_View : BasWRegionType{

    }
}
