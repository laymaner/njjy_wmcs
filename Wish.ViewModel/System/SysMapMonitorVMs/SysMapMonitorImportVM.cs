using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysMapMonitorVMs
{
    public partial class SysMapMonitorTemplateVM : BaseTemplateVM
    {
        public ExcelPropety mapConfig_Excel = ExcelPropety.CreateProperty<SysMapMonitor>(x => x.mapConfig);

	    protected override void InitVM()
        {
        }

    }

    public class SysMapMonitorImportVM : BaseImportVM<SysMapMonitorTemplateVM, SysMapMonitor>
    {

    }

}
