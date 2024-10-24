using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipVMs
{
    public partial class CfgRelationshipListVM : BasePagedListVM<CfgRelationship_View, CfgRelationshipSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgRelationship_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgRelationship_View>>{
                this.MakeGridHeader(x => x.leftCode),
                this.MakeGridHeader(x => x.priority),
                this.MakeGridHeader(x => x.relationshipTypeCode),
                this.MakeGridHeader(x => x.rightCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgRelationship_View> GetSearchQuery()
        {
            var query = DC.Set<CfgRelationship>()
                .CheckContain(Searcher.leftCode, x=>x.leftCode)
                .CheckEqual(Searcher.priority, x=>x.priority)
                .CheckContain(Searcher.relationshipTypeCode, x=>x.relationshipTypeCode)
                .CheckContain(Searcher.rightCode, x=>x.rightCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new CfgRelationship_View
                {
				    ID = x.ID,
                    leftCode = x.leftCode,
                    priority = x.priority,
                    relationshipTypeCode = x.relationshipTypeCode,
                    rightCode = x.rightCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgRelationship_View : CfgRelationship{

    }
}
