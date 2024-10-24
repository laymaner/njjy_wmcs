using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{
    public partial class InterfaceConfigListVM : BasePagedListVM<InterfaceConfig_View, InterfaceConfigSearcher>
    {

        protected override IEnumerable<IGridColumn<InterfaceConfig_View>> InitGridHeader()
        {
            return new List<GridColumn<InterfaceConfig_View>>{
                this.MakeGridHeader(x => x.interfaceCode),
                this.MakeGridHeader(x => x.interfaceName),
                this.MakeGridHeader(x => x.interfaceUrl),
                this.MakeGridHeader(x => x.retryMaxTimes),
                this.MakeGridHeader(x => x.retryInterval),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InterfaceConfig_View> GetSearchQuery()
        {
            var query = DC.Set<InterfaceConfig>()
                .CheckContain(Searcher.interfaceCode, x=>x.interfaceCode)
                .CheckContain(Searcher.interfaceName, x=>x.interfaceName)
                .CheckContain(Searcher.interfaceUrl, x=>x.interfaceUrl)
                .CheckEqual(Searcher.retryMaxTimes, x=>x.retryMaxTimes)
                .CheckEqual(Searcher.retryInterval, x=>x.retryInterval)
                .Select(x => new InterfaceConfig_View
                {
				    ID = x.ID,
                    interfaceCode = x.interfaceCode,
                    interfaceName = x.interfaceName,
                    interfaceUrl = x.interfaceUrl,
                    retryMaxTimes = x.retryMaxTimes,
                    retryInterval = x.retryInterval,
                    CreateBy = x.CreateBy,
                    CreateTime= x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime= x.UpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InterfaceConfig_View : InterfaceConfig{

    }
}
