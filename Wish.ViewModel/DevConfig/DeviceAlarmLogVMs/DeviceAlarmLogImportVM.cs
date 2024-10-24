using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceAlarmLogVMs
{
    public partial class DeviceAlarmLogTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_Code_Excel = ExcelPropety.CreateProperty<DeviceAlarmLog>(x => x.Device_Code);
        [Display(Name = "AlarmLog.AlarmInfo")]
        public ExcelPropety Message_Excel = ExcelPropety.CreateProperty<DeviceAlarmLog>(x => x.Message);
        [Display(Name = "AlarmLog.StartTime")]
        public ExcelPropety OriginTime_Excel = ExcelPropety.CreateProperty<DeviceAlarmLog>(x => x.OriginTime);

	    protected override void InitVM()
        {
        }

    }

    public class DeviceAlarmLogImportVM : BaseImportVM<DeviceAlarmLogTemplateVM, DeviceAlarmLog>
    {

    }

}
