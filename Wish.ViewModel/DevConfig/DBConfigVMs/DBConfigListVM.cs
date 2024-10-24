using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DBConfigVMs
{
    public partial class DBConfigListVM : BasePagedListVM<DBConfig_View, DBConfigSearcher>
    {

        protected override IEnumerable<IGridColumn<DBConfig_View>> InitGridHeader()
        {
            return new List<GridColumn<DBConfig_View>>{
                this.MakeGridHeader(x => x.Block_Code),
                this.MakeGridHeader(x => x.Block_Name),
                this.MakeGridHeader(x => x.Block_Offset),
                this.MakeGridHeader(x => x.Block_Length),
                this.MakeGridHeader(x => x.Plc_Name_view),
                this.MakeGridHeader(x => x.Describe),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DBConfig_View> GetSearchQuery()
        {
            var query = DC.Set<DBConfig>()
                .CheckContain(Searcher.Block_Code, x=>x.Block_Code)
                .CheckContain(Searcher.Block_Name, x=>x.Block_Name)
                .CheckEqual(Searcher.Block_Offset, x=>x.Block_Offset)
                .CheckEqual(Searcher.Block_Length, x=>x.Block_Length)
                .CheckEqual(Searcher.PlcConfigId, x=>x.PlcConfigId)
                .Select(x => new DBConfig_View
                {
				    ID = x.ID,
                    Block_Code = x.Block_Code,
                    Block_Name = x.Block_Name,
                    Block_Offset = x.Block_Offset,
                    Block_Length = x.Block_Length,
                    Plc_Name_view = x.PlcConfig.Plc_Name,
                    Describe = x.Describe,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DBConfig_View : DBConfig{
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Plc_Name_view { get; set; }

    }
}
