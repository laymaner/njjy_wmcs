using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgErpWhouseVMs
{
    public partial class CfgErpWhouseSearcher : BaseSearcher
    {
        public String erpWhouseNo { get; set; }
        public String erpWhouseName { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
