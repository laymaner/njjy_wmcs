using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgErpWhouseVMs
{
    public partial class CfgErpWhouseListVM : BasePagedListVM<CfgErpWhouse_View, CfgErpWhouseSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgErpWhouse_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgErpWhouse_View>>{
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.priority),
                this.MakeGridHeader(x => x.erpWhouseName),
                this.MakeGridHeader(x => x.erpWhouseNameEn),
                this.MakeGridHeader(x => x.erpWhouseNameAlias),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgErpWhouse_View> GetSearchQuery()
        {
            var query = DC.Set<CfgErpWhouse>()
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.erpWhouseName, x=>x.erpWhouseName)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgErpWhouse_View
                {
				    ID = x.ID,
                    erpWhouseNo = x.erpWhouseNo,
                    priority = x.priority,
                    erpWhouseName = x.erpWhouseName,
                    erpWhouseNameEn = x.erpWhouseNameEn,
                    erpWhouseNameAlias = x.erpWhouseNameAlias,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgErpWhouse_View : CfgErpWhouse{

    }
}
