using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.PlcConfigVMs
{
    public partial class PlcConfigTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Plc_Code_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Plc_Code);
        [Display(Name = "DeviceInfo.DeviceName")]
        public ExcelPropety Plc_Name_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Plc_Name);
        [Display(Name = "PLCInfo.IP")]
        public ExcelPropety IP_Address_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.IP_Address);
        [Display(Name = "PLCInfo.Port")]
        public ExcelPropety IP_Port_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.IP_Port);
        [Display(Name = "PLCInfo.Connect")]
        public ExcelPropety ConnType_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.ConnType);
        [Display(Name = "DeviceInfo.Effective")]
        public ExcelPropety IsEnabled_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.IsEnabled);
        [Display(Name = "PLCInfo.Scan")]
        public ExcelPropety Scan_Cycle_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Scan_Cycle);
        [Display(Name = "PLCInfo.CheckHeartDB")]
        public ExcelPropety Heartbeat_DB_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Heartbeat_DB);
        [Display(Name = "PLCInfo.CheckHeartOffset")]
        public ExcelPropety Heartbeat_Address_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Heartbeat_Address);
        [Display(Name = "PLCInfo.CheckHeart")]
        public ExcelPropety Heartbeat_Enabled_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Heartbeat_Enabled);
        [Display(Name = "PLCInfo.WriteHeart")]
        public ExcelPropety Heartbeat_WriteInterval_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Heartbeat_WriteInterval);
        [Display(Name = "DeviceInfo.Remark")]
        public ExcelPropety Describe_Excel = ExcelPropety.CreateProperty<PlcConfig>(x => x.Describe);

	    protected override void InitVM()
        {
        }

    }

    public class PlcConfigImportVM : BaseImportVM<PlcConfigTemplateVM, PlcConfig>
    {

    }

}
