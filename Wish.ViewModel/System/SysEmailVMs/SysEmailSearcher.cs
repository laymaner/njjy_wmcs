using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysEmailVMs
{
    public partial class SysEmailSearcher : BaseSearcher
    {
        public String alertType { get; set; }
        public String email { get; set; }
        public Int32? usedFlag { get; set; }
        public String userName { get; set; }

        protected override void InitVM()
        {
        }

    }
}
