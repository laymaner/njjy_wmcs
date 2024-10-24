using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysEmailVMs
{
    public partial class SysEmailListVM : BasePagedListVM<SysEmail_View, SysEmailSearcher>
    {

        protected override IEnumerable<IGridColumn<SysEmail_View>> InitGridHeader()
        {
            return new List<GridColumn<SysEmail_View>>{
                this.MakeGridHeader(x => x.alertType),
                this.MakeGridHeader(x => x.email),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.userName),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysEmail_View> GetSearchQuery()
        {
            var query = DC.Set<SysEmail>()
                .CheckContain(Searcher.alertType, x=>x.alertType)
                .CheckContain(Searcher.email, x=>x.email)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.userName, x=>x.userName)
                .Select(x => new SysEmail_View
                {
				    ID = x.ID,
                    alertType = x.alertType,
                    email = x.email,
                    usedFlag = x.usedFlag,
                    userName = x.userName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysEmail_View : SysEmail{

    }
}
