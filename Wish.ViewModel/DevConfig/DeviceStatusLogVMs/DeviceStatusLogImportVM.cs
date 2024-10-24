using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceStatusLogVMs
{
    public partial class DeviceStatusLogTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_Code_Excel = ExcelPropety.CreateProperty<DeviceStatusLog>(x => x.Device_Code);
        [Display(Name = "StatusLog.ChangeStatus")]
        public ExcelPropety Attribute_Excel = ExcelPropety.CreateProperty<DeviceStatusLog>(x => x.Attribute);
        [Display(Name = "StatusLog.StatusInfo")]
        public ExcelPropety Content_Excel = ExcelPropety.CreateProperty<DeviceStatusLog>(x => x.Content);
        [Display(Name = "StatusLog.FeedBack")]
        public ExcelPropety Message_Excel = ExcelPropety.CreateProperty<DeviceStatusLog>(x => x.Message);

	    protected override void InitVM()
        {
        }

    }

    public class DeviceStatusLogImportVM : BaseImportVM<DeviceStatusLogTemplateVM, DeviceStatusLog>
    {

    }

}
