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
    public partial class SysMapMonitorSearcher : BaseSearcher
    {
        public String mapConfig { get; set; }

        protected override void InitVM()
        {
        }

    }
}
