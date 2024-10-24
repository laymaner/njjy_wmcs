using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyItemVMs
{
    public partial class CfgStrategyItemListVM : BasePagedListVM<CfgStrategyItem_View, CfgStrategyItemSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgStrategyItem_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgStrategyItem_View>>{
                this.MakeGridHeader(x => x.strategyItemDesc),
                this.MakeGridHeader(x => x.strategyItemGroupIdx),
                this.MakeGridHeader(x => x.strategyItemGroupNo),
                this.MakeGridHeader(x => x.strategyItemName),
                this.MakeGridHeader(x => x.strategyItemNameAlias),
                this.MakeGridHeader(x => x.strategyItemNameEn),
                this.MakeGridHeader(x => x.strategyItemNo),
                this.MakeGridHeader(x => x.strategyTypeCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgStrategyItem_View> GetSearchQuery()
        {
            var query = DC.Set<CfgStrategyItem>()
                .CheckContain(Searcher.strategyItemGroupNo, x=>x.strategyItemGroupNo)
                .CheckContain(Searcher.strategyItemName, x=>x.strategyItemName)
                .CheckContain(Searcher.strategyItemNo, x=>x.strategyItemNo)
                .CheckContain(Searcher.strategyTypeCode, x=>x.strategyTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgStrategyItem_View
                {
				    ID = x.ID,
                    strategyItemDesc = x.strategyItemDesc,
                    strategyItemGroupIdx = x.strategyItemGroupIdx,
                    strategyItemGroupNo = x.strategyItemGroupNo,
                    strategyItemName = x.strategyItemName,
                    strategyItemNameAlias = x.strategyItemNameAlias,
                    strategyItemNameEn = x.strategyItemNameEn,
                    strategyItemNo = x.strategyItemNo,
                    strategyTypeCode = x.strategyTypeCode,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgStrategyItem_View : CfgStrategyItem{

    }
}
