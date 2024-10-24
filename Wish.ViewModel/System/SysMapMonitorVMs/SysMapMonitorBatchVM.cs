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
    public partial class SysMapMonitorBatchVM : BaseBatchVM<SysMapMonitor, SysMapMonitor_BatchEdit>
    {
        public SysMapMonitorBatchVM()
        {
            ListVM = new SysMapMonitorListVM();
            LinkedVM = new SysMapMonitor_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysMapMonitor_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
