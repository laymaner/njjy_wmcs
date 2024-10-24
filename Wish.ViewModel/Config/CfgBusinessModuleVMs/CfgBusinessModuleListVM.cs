using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessModuleVMs
{
    public partial class CfgBusinessModuleListVM : BasePagedListVM<CfgBusinessModule_View, CfgBusinessModuleSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgBusinessModule_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgBusinessModule_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.businessModuleCode),
                this.MakeGridHeader(x => x.businessModuleDesc),
                this.MakeGridHeader(x => x.businessModuleName),
                this.MakeGridHeader(x => x.businessModuleNameAlias),
                this.MakeGridHeader(x => x.businessModuleNameEn),
                this.MakeGridHeader(x => x.businessModuleSort),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgBusinessModule_View> GetSearchQuery()
        {
            var query = DC.Set<CfgBusinessModule>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.businessModuleCode, x=>x.businessModuleCode)
                .CheckContain(Searcher.businessModuleName, x=>x.businessModuleName)
                .CheckEqual(Searcher.businessModuleSort, x=>x.businessModuleSort)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgBusinessModule_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    businessModuleCode = x.businessModuleCode,
                    businessModuleDesc = x.businessModuleDesc,
                    businessModuleName = x.businessModuleName,
                    businessModuleNameAlias = x.businessModuleNameAlias,
                    businessModuleNameEn = x.businessModuleNameEn,
                    businessModuleSort = x.businessModuleSort,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgBusinessModule_View : CfgBusinessModule{

    }
}
