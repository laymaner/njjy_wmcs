using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterVMs
{
    public partial class SysParameterListVM : BasePagedListVM<SysParameter_View, SysParameterSearcher>
    {

        protected override IEnumerable<IGridColumn<SysParameter_View>> InitGridHeader()
        {
            return new List<GridColumn<SysParameter_View>>{
                this.MakeGridHeader(x => x.developFlag),
                this.MakeGridHeader(x => x.parCode),
                this.MakeGridHeader(x => x.parDesc),
                this.MakeGridHeader(x => x.parDescAlias),
                this.MakeGridHeader(x => x.parDescEn),
                this.MakeGridHeader(x => x.parValue),
                this.MakeGridHeader(x => x.parValueType),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysParameter_View> GetSearchQuery()
        {
            var query = DC.Set<SysParameter>()
                .CheckEqual(Searcher.developFlag, x=>x.developFlag)
                .CheckContain(Searcher.parCode, x=>x.parCode)
                .CheckContain(Searcher.parDescAlias, x=>x.parDescAlias)
                .CheckContain(Searcher.parValue, x=>x.parValue)
                .CheckEqual(Searcher.parValueType, x=>x.parValueType)
                .Select(x => new SysParameter_View
                {
				    ID = x.ID,
                    developFlag = x.developFlag,
                    parCode = x.parCode,
                    parDesc = x.parDesc,
                    parDescAlias = x.parDescAlias,
                    parDescEn = x.parDescEn,
                    parValue = x.parValue,
                    parValueType = x.parValueType,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysParameter_View : SysParameter{

    }
}
