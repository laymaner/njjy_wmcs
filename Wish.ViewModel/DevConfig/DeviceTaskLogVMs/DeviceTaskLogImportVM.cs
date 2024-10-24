using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceTaskLogVMs
{
    public partial class DeviceTaskLogTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_Code_Excel = ExcelPropety.CreateProperty<DeviceTaskLog>(x => x.Device_Code);
        [Display(Name = "TaskLog.Direction")]
        public ExcelPropety Direct_Excel = ExcelPropety.CreateProperty<DeviceTaskLog>(x => x.Direct);
        [Display(Name = "TaskLog.TaskNo")]
        public ExcelPropety Task_No_Excel = ExcelPropety.CreateProperty<DeviceTaskLog>(x => x.Task_No);
        [Display(Name = "TaskLog.Massage")]
        public ExcelPropety Message_Excel = ExcelPropety.CreateProperty<DeviceTaskLog>(x => x.Message);

	    protected override void InitVM()
        {
        }

    }

    public class DeviceTaskLogImportVM : BaseImportVM<DeviceTaskLogTemplateVM, DeviceTaskLog>
    {

    }

}
