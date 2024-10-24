using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceTaskLogVMs
{
    public partial class DeviceTaskLogListVM : BasePagedListVM<DeviceTaskLog_View, DeviceTaskLogSearcher>
    {

        protected override IEnumerable<IGridColumn<DeviceTaskLog_View>> InitGridHeader()
        {
            return new List<GridColumn<DeviceTaskLog_View>>{
                this.MakeGridHeader(x => x.Device_Code),
                this.MakeGridHeader(x => x.Direct),
                this.MakeGridHeader(x => x.Task_No),
                this.MakeGridHeader(x => x.Message),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DeviceTaskLog_View> GetSearchQuery()
        {
            var query = DC.Set<DeviceTaskLog>()
                .CheckContain(Searcher.Device_Code, x=>x.Device_Code)
                .CheckContain(Searcher.Direct, x=>x.Direct)
                .CheckContain(Searcher.Task_No, x=>x.Task_No)
                .CheckContain(Searcher.Message, x=>x.Message)
                .Select(x => new DeviceTaskLog_View
                {
				    ID = x.ID,
                    Device_Code = x.Device_Code,
                    Direct = x.Direct,
                    Task_No = x.Task_No,
                    Message = x.Message,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

    }

    public class DeviceTaskLog_View : DeviceTaskLog{

    }
}
