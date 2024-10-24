using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocLocVMs
{
    public partial class CfgDocLocListVM : BasePagedListVM<CfgDocLoc_View, CfgDocLocSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgDocLoc_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgDocLoc_View>>{
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.locNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgDocLoc_View> GetSearchQuery()
        {
            var query = DC.Set<CfgDocLoc>()
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.locNo, x=>x.locNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new CfgDocLoc_View
                {
				    ID = x.ID,
                    docTypeCode = x.docTypeCode,
                    locNo = x.locNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgDocLoc_View : CfgDocLoc{

    }
}
