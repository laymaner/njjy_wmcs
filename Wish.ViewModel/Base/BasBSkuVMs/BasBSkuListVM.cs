using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSkuVMs
{
    public partial class BasBSkuListVM : BasePagedListVM<BasBSku_View, BasBSkuSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBSku_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBSku_View>>{
                this.MakeGridHeader(x => x.pickRuleNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.skuRulesNo),
                this.MakeGridHeader(x => x.storageRuleNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBSku_View> GetSearchQuery()
        {
            var query = DC.Set<BasBSku>()
                .CheckContain(Searcher.pickRuleNo, x=>x.pickRuleNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasBSku_View
                {
				    ID = x.ID,
                    pickRuleNo = x.pickRuleNo,
                    proprietorCode = x.proprietorCode,
                    skuCode = x.skuCode,
                    skuRulesNo = x.skuRulesNo,
                    storageRuleNo = x.storageRuleNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBSku_View : BasBSku{

    }
}
