using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterVMs
{
    public partial class SysParameterSearcher : BaseSearcher
    {
        public Int32? developFlag { get; set; }
        public String parCode { get; set; }
        public String parDescAlias { get; set; }
        public String parValue { get; set; }
        public Int32? parValueType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
