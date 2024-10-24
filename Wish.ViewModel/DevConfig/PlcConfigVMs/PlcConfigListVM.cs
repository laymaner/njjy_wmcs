using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.PlcConfigVMs
{
    public partial class PlcConfigListVM : BasePagedListVM<PlcConfig_View, PlcConfigSearcher>
    {

        protected override IEnumerable<IGridColumn<PlcConfig_View>> InitGridHeader()
        {
            return new List<GridColumn<PlcConfig_View>>{
                this.MakeGridHeader(x => x.Plc_Code),
                this.MakeGridHeader(x => x.Plc_Name),
                this.MakeGridHeader(x => x.IP_Address),
                this.MakeGridHeader(x => x.IP_Port),
                this.MakeGridHeader(x => x.ConnType),
                this.MakeGridHeader(x => x.IsEnabled),
                this.MakeGridHeader(x => x.Scan_Cycle),
                this.MakeGridHeader(x => x.Heartbeat_DB),
                this.MakeGridHeader(x => x.Heartbeat_Address),
                this.MakeGridHeader(x => x.Heartbeat_Enabled),
                this.MakeGridHeader(x => x.Heartbeat_WriteInterval),
                this.MakeGridHeader(x => x.Describe),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<PlcConfig_View> GetSearchQuery()
        {
            var query = DC.Set<PlcConfig>()
                .CheckContain(Searcher.Plc_Code, x=>x.Plc_Code)
                .CheckContain(Searcher.Plc_Name, x=>x.Plc_Name)
                .CheckContain(Searcher.IP_Address, x=>x.IP_Address)
                .CheckEqual(Searcher.IP_Port, x=>x.IP_Port)
                .CheckEqual(Searcher.ConnType, x=>x.ConnType)
                //.CheckEqual(Searcher.IsConnect, x=>x.IsConnect)
                .CheckEqual(Searcher.IsEnabled, x=>x.IsEnabled)
                .CheckContain(Searcher.Heartbeat_DB, x=>x.Heartbeat_DB)
                .Select(x => new PlcConfig_View
                {
				    ID = x.ID,
                    Plc_Code = x.Plc_Code,
                    Plc_Name = x.Plc_Name,
                    IP_Address = x.IP_Address,
                    IP_Port = x.IP_Port,
                    ConnType = x.ConnType,
                    IsEnabled = x.IsEnabled,
                    Scan_Cycle = x.Scan_Cycle,
                    Heartbeat_DB = x.Heartbeat_DB,
                    Heartbeat_Address = x.Heartbeat_Address,
                    Heartbeat_Enabled = x.Heartbeat_Enabled,
                    Heartbeat_WriteInterval = x.Heartbeat_WriteInterval,
                    Describe = x.Describe,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class PlcConfig_View : PlcConfig{

    }
}
