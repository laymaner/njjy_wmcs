using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysMapMonitorVMs
{
    public partial class SysMapMonitorListVM : BasePagedListVM<SysMapMonitor_View, SysMapMonitorSearcher>
    {

        protected override IEnumerable<IGridColumn<SysMapMonitor_View>> InitGridHeader()
        {
            return new List<GridColumn<SysMapMonitor_View>>{
                this.MakeGridHeader(x => x.mapConfig),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysMapMonitor_View> GetSearchQuery()
        {
            var query = DC.Set<SysMapMonitor>()
                .CheckContain(Searcher.mapConfig, x=>x.mapConfig)
                .Select(x => new SysMapMonitor_View
                {
				    ID = x.ID,
                    mapConfig = x.mapConfig,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysMapMonitor_View : SysMapMonitor{

    }
}
