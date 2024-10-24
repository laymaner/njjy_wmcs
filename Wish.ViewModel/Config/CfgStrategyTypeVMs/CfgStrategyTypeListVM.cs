using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyTypeVMs
{
    public partial class CfgStrategyTypeListVM : BasePagedListVM<CfgStrategyType_View, CfgStrategyTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgStrategyType_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgStrategyType_View>>{
                this.MakeGridHeader(x => x.strategyTypeCategory),
                this.MakeGridHeader(x => x.strategyTypeCode),
                this.MakeGridHeader(x => x.strategyTypeDesription),
                this.MakeGridHeader(x => x.strategyTypeName),
                this.MakeGridHeader(x => x.strategyTypeNameAlias),
                this.MakeGridHeader(x => x.strategyTypeNameEn),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgStrategyType_View> GetSearchQuery()
        {
            var query = DC.Set<CfgStrategyType>()
                .CheckContain(Searcher.strategyTypeCategory, x=>x.strategyTypeCategory)
                .CheckContain(Searcher.strategyTypeCode, x=>x.strategyTypeCode)
                .CheckContain(Searcher.strategyTypeName, x=>x.strategyTypeName)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgStrategyType_View
                {
				    ID = x.ID,
                    strategyTypeCategory = x.strategyTypeCategory,
                    strategyTypeCode = x.strategyTypeCode,
                    strategyTypeDesription = x.strategyTypeDesription,
                    strategyTypeName = x.strategyTypeName,
                    strategyTypeNameAlias = x.strategyTypeNameAlias,
                    strategyTypeNameEn = x.strategyTypeNameEn,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgStrategyType_View : CfgStrategyType{

    }
}
