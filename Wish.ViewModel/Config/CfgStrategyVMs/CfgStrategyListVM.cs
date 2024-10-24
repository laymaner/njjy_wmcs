using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyVMs
{
    public partial class CfgStrategyListVM : BasePagedListVM<CfgStrategy_View, CfgStrategySearcher>
    {

        protected override IEnumerable<IGridColumn<CfgStrategy_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgStrategy_View>>{
                this.MakeGridHeader(x => x.strategyDesc),
                this.MakeGridHeader(x => x.strategyName),
                this.MakeGridHeader(x => x.strategyNameAlias),
                this.MakeGridHeader(x => x.strategyNameEn),
                this.MakeGridHeader(x => x.strategyNo),
                this.MakeGridHeader(x => x.strategyTypeCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgStrategy_View> GetSearchQuery()
        {
            var query = DC.Set<CfgStrategy>()
                .CheckContain(Searcher.strategyName, x=>x.strategyName)
                .CheckContain(Searcher.strategyNo, x=>x.strategyNo)
                .CheckContain(Searcher.strategyTypeCode, x=>x.strategyTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgStrategy_View
                {
				    ID = x.ID,
                    strategyDesc = x.strategyDesc,
                    strategyName = x.strategyName,
                    strategyNameAlias = x.strategyNameAlias,
                    strategyNameEn = x.strategyNameEn,
                    strategyNo = x.strategyNo,
                    strategyTypeCode = x.strategyTypeCode,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgStrategy_View : CfgStrategy{

    }
}
