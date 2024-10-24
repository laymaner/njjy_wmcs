using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceStatusLogVMs
{
    public partial class DeviceStatusLogListVM : BasePagedListVM<DeviceStatusLog_View, DeviceStatusLogSearcher>
    {

        protected override IEnumerable<IGridColumn<DeviceStatusLog_View>> InitGridHeader()
        {
            return new List<GridColumn<DeviceStatusLog_View>>{
                this.MakeGridHeader(x => x.Device_Code),
                this.MakeGridHeader(x => x.Attribute),
                this.MakeGridHeader(x => x.Content),
                this.MakeGridHeader(x => x.Message),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DeviceStatusLog_View> GetSearchQuery()
        {
            var query = DC.Set<DeviceStatusLog>()
                .CheckContain(Searcher.Device_Code, x=>x.Device_Code)
                .CheckContain(Searcher.Attribute, x=>x.Attribute)
                .CheckContain(Searcher.Content, x=>x.Content)
                .CheckContain(Searcher.Message, x=>x.Message)
                .Select(x => new DeviceStatusLog_View
                {
				    ID = x.ID,
                    Device_Code = x.Device_Code,
                    Attribute = x.Attribute,
                    Content = x.Content,
                    Message = x.Message,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DeviceStatusLog_View : DeviceStatusLog{

    }
}
