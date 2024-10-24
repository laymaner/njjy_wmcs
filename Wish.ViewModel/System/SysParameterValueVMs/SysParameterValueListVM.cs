using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterValueVMs
{
    public partial class SysParameterValueListVM : BasePagedListVM<SysParameterValue_View, SysParameterValueSearcher>
    {

        protected override IEnumerable<IGridColumn<SysParameterValue_View>> InitGridHeader()
        {
            return new List<GridColumn<SysParameterValue_View>>{
                this.MakeGridHeader(x => x.parCode),
                this.MakeGridHeader(x => x.parValueDesc),
                this.MakeGridHeader(x => x.parValueDescAlias),
                this.MakeGridHeader(x => x.parValueDescEn),
                this.MakeGridHeader(x => x.parValueNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysParameterValue_View> GetSearchQuery()
        {
            var query = DC.Set<SysParameterValue>()
                .CheckContain(Searcher.parCode, x=>x.parCode)
                .CheckEqual(Searcher.parValueNo, x=>x.parValueNo)
                .Select(x => new SysParameterValue_View
                {
				    ID = x.ID,
                    parCode = x.parCode,
                    parValueDesc = x.parValueDesc,
                    parValueDescAlias = x.parValueDescAlias,
                    parValueDescEn = x.parValueDescEn,
                    parValueNo = x.parValueNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysParameterValue_View : SysParameterValue{

    }
}
