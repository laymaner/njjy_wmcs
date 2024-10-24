using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_Code_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Device_Code);
        [Display(Name = "DeviceInfo.DeviceName")]
        public ExcelPropety Device_Name_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Device_Name);
        [Display(Name = "DeviceInfo.HouseNo")]
        public ExcelPropety WarehouseId_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.WarehouseId);
        public ExcelPropety StandardDevice_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.StandardDeviceId);
        [Display(Name = "DeviceInfo.Effective")]
        public ExcelPropety IsEnabled_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.IsEnabled);
        [Display(Name = "DeviceInfo.Mutually")]
        public ExcelPropety Exec_Flag_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Exec_Flag);
        [Display(Name = "DeviceInfo.DeviceGroup")]
        public ExcelPropety Device_Group_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Device_Group);
        //public ExcelPropety PlcConfig_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.PlcConfigId);
        [Display(Name = "DeviceInfo.PLCStep")]
        public ExcelPropety Plc2WcsStep_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Plc2WcsStep);
        [Display(Name = "DeviceInfo.WCSStep")]
        public ExcelPropety Wcs2PlcStep_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Wcs2PlcStep);
        [Display(Name = "设备模式")]
        public ExcelPropety Mode_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Mode);
        [Display(Name = "在线状态")]
        public ExcelPropety IsOnline_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.IsOnline);
        [Display(Name = "DeviceInfo.Config")]
        public ExcelPropety Config_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Config);
        [Display(Name = "DeviceInfo.Remark")]
        public ExcelPropety Describe_Excel = ExcelPropety.CreateProperty<DeviceConfig>(x => x.Describe);

	    protected override void InitVM()
        {
            StandardDevice_Excel.DataType = ColumnDataType.ComboBox;
            StandardDevice_Excel.ListItems = DC.Set<StandardDevice>().GetSelectListItems(Wtm, y => y.Device_Name);
            //PlcConfig_Excel.DataType = ColumnDataType.ComboBox;
            //PlcConfig_Excel.ListItems = DC.Set<PlcConfig>().GetSelectListItems(Wtm, y => y.Plc_Name);
        }

    }

    public class DeviceConfigImportVM : BaseImportVM<DeviceConfigTemplateVM, DeviceConfig>
    {

    }

}
