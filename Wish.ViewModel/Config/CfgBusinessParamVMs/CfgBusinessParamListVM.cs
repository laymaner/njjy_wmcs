using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamVMs
{
    public partial class CfgBusinessParamListVM : BasePagedListVM<CfgBusinessParam_View, CfgBusinessParamSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgBusinessParam_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgBusinessParam_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.businessModuleCode),
                this.MakeGridHeader(x => x.checkFlag),
                this.MakeGridHeader(x => x.paramCode),
                this.MakeGridHeader(x => x.paramDesc),
                this.MakeGridHeader(x => x.paramName),
                this.MakeGridHeader(x => x.paramNameAlias),
                this.MakeGridHeader(x => x.paramNameEn),
                this.MakeGridHeader(x => x.paramSort),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgBusinessParam_View> GetSearchQuery()
        {
            var query = DC.Set<CfgBusinessParam>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.businessModuleCode, x=>x.businessModuleCode)
                .CheckContain(Searcher.paramCode, x=>x.paramCode)
                .CheckEqual(Searcher.paramSort, x=>x.paramSort)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgBusinessParam_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    businessModuleCode = x.businessModuleCode,
                    checkFlag = x.checkFlag,
                    paramCode = x.paramCode,
                    paramDesc = x.paramDesc,
                    paramName = x.paramName,
                    paramNameAlias = x.paramNameAlias,
                    paramNameEn = x.paramNameEn,
                    paramSort = x.paramSort,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgBusinessParam_View : CfgBusinessParam{

    }
}
