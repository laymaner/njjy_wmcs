using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWWhouseVMs
{
    public partial class BasWWhouseListVM : BasePagedListVM<BasWWhouse_View, BasWWhouseSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWWhouse_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWWhouse_View>>{
                this.MakeGridHeader(x => x.contacts),
                this.MakeGridHeader(x => x.maxTaskQty),
                this.MakeGridHeader(x => x.telephone),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseAddress),
                this.MakeGridHeader(x => x.whouseName),
                this.MakeGridHeader(x => x.whouseNameAlias),
                this.MakeGridHeader(x => x.whouseNameEn),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.whouseType),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWWhouse_View> GetSearchQuery()
        {
            var query = DC.Set<BasWWhouse>()
                .CheckContain(Searcher.contacts, x=>x.contacts)
                .CheckContain(Searcher.telephone, x=>x.telephone)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseAddress, x=>x.whouseAddress)
                .CheckContain(Searcher.whouseName, x=>x.whouseName)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.whouseType, x=>x.whouseType)
                .Select(x => new BasWWhouse_View
                {
				    ID = x.ID,
                    contacts = x.contacts,
                    maxTaskQty = x.maxTaskQty,
                    telephone = x.telephone,
                    usedFlag = x.usedFlag,
                    whouseAddress = x.whouseAddress,
                    whouseName = x.whouseName,
                    whouseNameAlias = x.whouseNameAlias,
                    whouseNameEn = x.whouseNameEn,
                    whouseNo = x.whouseNo,
                    whouseType = x.whouseType,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWWhouse_View : BasWWhouse{

    }
}
