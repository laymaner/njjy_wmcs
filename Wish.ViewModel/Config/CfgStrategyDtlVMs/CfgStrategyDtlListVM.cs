using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyDtlVMs
{
    public partial class CfgStrategyDtlListVM : BasePagedListVM<CfgStrategyDtl_View, CfgStrategyDtlSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgStrategyDtl_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgStrategyDtl_View>>{
                this.MakeGridHeader(x => x.strategyItemIdx),
                this.MakeGridHeader(x => x.strategyItemNo),
                this.MakeGridHeader(x => x.strategyItemValue1),
                this.MakeGridHeader(x => x.strategyItemValue2),
                this.MakeGridHeader(x => x.strategyNo),
                this.MakeGridHeader(x => x.strategyTypeCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgStrategyDtl_View> GetSearchQuery()
        {
            var query = DC.Set<CfgStrategyDtl>()
                .CheckContain(Searcher.strategyItemIdx, x=>x.strategyItemIdx)
                .CheckContain(Searcher.strategyItemNo, x=>x.strategyItemNo)
                .CheckContain(Searcher.strategyItemValue1, x=>x.strategyItemValue1)
                .CheckContain(Searcher.strategyItemValue2, x=>x.strategyItemValue2)
                .CheckContain(Searcher.strategyNo, x=>x.strategyNo)
                .CheckContain(Searcher.strategyTypeCode, x=>x.strategyTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgStrategyDtl_View
                {
				    ID = x.ID,
                    strategyItemIdx = x.strategyItemIdx,
                    strategyItemNo = x.strategyItemNo,
                    strategyItemValue1 = x.strategyItemValue1,
                    strategyItemValue2 = x.strategyItemValue2,
                    strategyNo = x.strategyNo,
                    strategyTypeCode = x.strategyTypeCode,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgStrategyDtl_View : CfgStrategyDtl{

    }
}
