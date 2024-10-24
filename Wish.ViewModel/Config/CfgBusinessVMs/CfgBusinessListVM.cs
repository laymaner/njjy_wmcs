using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessVMs
{
    public partial class CfgBusinessListVM : BasePagedListVM<CfgBusiness_View, CfgBusinessSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgBusiness_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgBusiness_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.businessDesc),
                this.MakeGridHeader(x => x.businessName),
                this.MakeGridHeader(x => x.businessNameAlias),
                this.MakeGridHeader(x => x.businessNameEn),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgBusiness_View> GetSearchQuery()
        {
            var query = DC.Set<CfgBusiness>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.businessName, x=>x.businessName)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgBusiness_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    businessDesc = x.businessDesc,
                    businessName = x.businessName,
                    businessNameAlias = x.businessNameAlias,
                    businessNameEn = x.businessNameEn,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgBusiness_View : CfgBusiness{

    }
}
