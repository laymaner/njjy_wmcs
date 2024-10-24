using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysDictionaryVMs
{
    public partial class SysDictionaryListVM : BasePagedListVM<SysDictionary_View, SysDictionarySearcher>
    {

        protected override IEnumerable<IGridColumn<SysDictionary_View>> InitGridHeader()
        {
            return new List<GridColumn<SysDictionary_View>>{
                this.MakeGridHeader(x => x.developFlag),
                this.MakeGridHeader(x => x.dictionaryCode),
                this.MakeGridHeader(x => x.dictionaryItemCode),
                this.MakeGridHeader(x => x.dictionaryItemName),
                this.MakeGridHeader(x => x.dictionaryItemNameAlias),
                this.MakeGridHeader(x => x.dictionaryItemNameEn),
                this.MakeGridHeader(x => x.dictionaryName),
                this.MakeGridHeader(x => x.dictionaryNameAlias),
                this.MakeGridHeader(x => x.dictionaryNameEn),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysDictionary_View> GetSearchQuery()
        {
            var query = DC.Set<SysDictionary>()
                .CheckEqual(Searcher.developFlag, x=>x.developFlag)
                .CheckContain(Searcher.dictionaryCode, x=>x.dictionaryCode)
                .CheckContain(Searcher.dictionaryItemCode, x=>x.dictionaryItemCode)
                .CheckContain(Searcher.dictionaryName, x=>x.dictionaryName)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new SysDictionary_View
                {
				    ID = x.ID,
                    developFlag = x.developFlag,
                    dictionaryCode = x.dictionaryCode,
                    dictionaryItemCode = x.dictionaryItemCode,
                    dictionaryItemName = x.dictionaryItemName,
                    dictionaryItemNameAlias = x.dictionaryItemNameAlias,
                    dictionaryItemNameEn = x.dictionaryItemNameEn,
                    dictionaryName = x.dictionaryName,
                    dictionaryNameAlias = x.dictionaryNameAlias,
                    dictionaryNameEn = x.dictionaryNameEn,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysDictionary_View : SysDictionary{

    }
}
