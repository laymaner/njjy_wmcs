using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;
using ASRS.WCS.Common.Enum;


namespace Wish.ViewModel.DevConfig.StandardDeviceVMs
{
    public partial class StandardDeviceListVM : BasePagedListVM<StandardDevice_View, StandardDeviceSearcher>
    {

        protected override IEnumerable<IGridColumn<StandardDevice_View>> InitGridHeader()
        {
            return new List<GridColumn<StandardDevice_View>>{
                this.MakeGridHeader(x => x.Device_Code),
                this.MakeGridHeader(x => x.Device_Name),
                this.MakeGridHeader(x => x.Device_Class),
                this.MakeGridHeader(x => x.DeviceType),
                this.MakeGridHeader(x => x.Company),
                this.MakeGridHeader(x => x.Config),
                this.MakeGridHeader(x => x.Describe),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<StandardDevice_View> GetSearchQuery()
        {
            var query = DC.Set<StandardDevice>()
                .CheckContain(Searcher.Device_Code, x=>x.Device_Code)
                .CheckContain(Searcher.Device_Name, x=>x.Device_Name)
                .CheckContain(Searcher.Device_Class, x=>x.Device_Class)
                .CheckEqual(Searcher.DeviceType, x=>x.DeviceType)
                .CheckContain(Searcher.Company, x=>x.Company)
                .CheckContain(Searcher.Config, x=>x.Config)
                .Select(x => new StandardDevice_View
                {
				    ID = x.ID,
                    Device_Code = x.Device_Code,
                    Device_Name = x.Device_Name,
                    Device_Class = x.Device_Class,
                    DeviceType = x.DeviceType,
                    Company = x.Company,
                    Config = x.Config,
                    Describe = x.Describe,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class StandardDevice_View : StandardDevice{

    }
}
