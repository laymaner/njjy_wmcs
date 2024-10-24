using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterValueVMs
{
    public partial class SysParameterValueSearcher : BaseSearcher
    {
        public String parCode { get; set; }
        public Int32? parValueNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
