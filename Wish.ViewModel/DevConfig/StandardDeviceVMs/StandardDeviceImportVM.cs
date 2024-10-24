using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using ASRS.WCS.Common.Enum;


namespace Wish.ViewModel.DevConfig.StandardDeviceVMs
{
    public partial class StandardDeviceTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_Code_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Device_Code);
        [Display(Name = "DeviceInfo.DeviceName")]
        public ExcelPropety Device_Name_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Device_Name);
        [Display(Name = "实现类名")]
        public ExcelPropety Device_Class_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Device_Class);
        [Display(Name = "VersionInfo.Type")]
        public ExcelPropety DeviceType_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.DeviceType);
        [Display(Name = "VersionInfo.Company")]
        public ExcelPropety Company_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Company);
        [Display(Name = "DeviceInfo.Config")]
        public ExcelPropety Config_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Config);
        [Display(Name = "DeviceInfo.Remark")]
        public ExcelPropety Describe_Excel = ExcelPropety.CreateProperty<StandardDevice>(x => x.Describe);

	    protected override void InitVM()
        {
        }

    }

    public class StandardDeviceImportVM : BaseImportVM<StandardDeviceTemplateVM, StandardDevice>
    {

    }

}
