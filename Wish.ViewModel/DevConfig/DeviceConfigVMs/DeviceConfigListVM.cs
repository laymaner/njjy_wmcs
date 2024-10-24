using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;
using Wish.Model.System;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigListVM : BasePagedListVM<DeviceConfig_View, DeviceConfigSearcher>
    {

        protected override IEnumerable<IGridColumn<DeviceConfig_View>> InitGridHeader()
        {
            return new List<GridColumn<DeviceConfig_View>>{
                this.MakeGridHeader(x => x.Device_Code),
                this.MakeGridHeader(x => x.Device_Name),
                this.MakeGridHeader(x => x.WarehouseId),
                this.MakeGridHeader(x => x.Device_Name_view),
                this.MakeGridHeader(x => x.IsEnabled),
                this.MakeGridHeader(x => x.Exec_Flag),
                this.MakeGridHeader(x => x.Device_Group),
                this.MakeGridHeader(x => x.Plc_Name_view),
                this.MakeGridHeader(x => x.Plc2WcsStep),
                this.MakeGridHeader(x => x.Wcs2PlcStep),
                this.MakeGridHeader(x => x.Mode),
                this.MakeGridHeader(x => x.IsOnline),
                this.MakeGridHeader(x => x.Config),
                this.MakeGridHeader(x => x.Describe),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.Plc2WcsStepDesc),
                this.MakeGridHeader(x => x.Wcs2PlcStepDesc),
                this.MakeGridHeader(x => x.ModeDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<DeviceConfig_View> GetSearchQuery()
        {
            var query = DC.Set<DeviceConfig>()
                .CheckContain(Searcher.Device_Code, x=>x.Device_Code)
                .CheckContain(Searcher.Device_Name, x=>x.Device_Name)
                .CheckEqual(Searcher.WarehouseId, x=>x.WarehouseId)
                .CheckEqual(Searcher.Plc2WcsStep, x=>x.Plc2WcsStep)
                .CheckEqual(Searcher.Wcs2PlcStep, x=>x.Wcs2PlcStep)
                .CheckEqual(Searcher.Mode, x=>x.Mode)
                .CheckContain(Searcher.Config, x=>x.Config)
                .Select(x => new DeviceConfig_View
                {
				    ID = x.ID,
                    Device_Code = x.Device_Code,
                    Device_Name = x.Device_Name,
                    WarehouseId = x.WarehouseId,
                    Device_Name_view = x.StandardDevice.Device_Name,
                    IsEnabled = x.IsEnabled,
                    Exec_Flag = x.Exec_Flag,
                    Device_Group = x.Device_Group,
                    Plc_Name_view = x.PlcConfig.Plc_Name,
                    Plc2WcsStep = x.Plc2WcsStep,
                    Wcs2PlcStep = x.Wcs2PlcStep,
                    Mode = x.Mode,
                    IsOnline = x.IsOnline,
                    Config = x.Config,
                    Describe = x.Describe,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "PLC_STEP" && t.dictionaryItemCode == item.Plc2WcsStep.ToString());
                if (entityCmd != null)
                {
                    item.Plc2WcsStepDesc = entityCmd.dictionaryItemName;
                }
                var entityUse = dicEntities.FirstOrDefault(t => t.dictionaryCode == "WCS_SETP" && t.dictionaryItemCode == item.Wcs2PlcStep.ToString());
                if (entityUse != null)
                {
                    item.Wcs2PlcStepDesc = entityUse.dictionaryItemName;
                }
                var entityVirtual = dicEntities.FirstOrDefault(t => t.dictionaryCode == "MODE" && t.dictionaryItemCode == item.Mode.ToString());
                if (entityVirtual != null)
                {
                    item.ModeDesc = entityVirtual.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderBy(x => x.ID);
            return queryList;
        }

    }

    public class DeviceConfig_View : DeviceConfig{
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Device_Name_view { get; set; }
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Plc_Name_view { get; set; }
        public String Plc2WcsStepDesc { get; set; }
        public String Wcs2PlcStepDesc { get; set; }
        public String ModeDesc { get; set; }

    }
}
