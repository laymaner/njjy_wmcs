using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseVMs
{
    public partial class BasWErpWhouseListVM : BasePagedListVM<BasWErpWhouse_View, BasWErpWhouseSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWErpWhouse_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWErpWhouse_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.erpWhouseName),
                this.MakeGridHeader(x => x.erpWhouseNameAlias),
                this.MakeGridHeader(x => x.erpWhouseNameEn),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.erpWhouseType),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWErpWhouse_View> GetSearchQuery()
        {
            var query = DC.Set<BasWErpWhouse>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.erpWhouseName, x=>x.erpWhouseName)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.erpWhouseType, x=>x.erpWhouseType)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWErpWhouse_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    erpWhouseName = x.erpWhouseName,
                    erpWhouseNameAlias = x.erpWhouseNameAlias,
                    erpWhouseNameEn = x.erpWhouseNameEn,
                    erpWhouseNo = x.erpWhouseNo,
                    erpWhouseType = x.erpWhouseType,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWErpWhouse_View : BasWErpWhouse{

    }
}
