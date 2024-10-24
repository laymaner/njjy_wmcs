using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamVMs
{
    public partial class CfgBusinessParamSearcher : BaseSearcher
    {
        public String businessCode { get; set; }
        public String businessModuleCode { get; set; }
        public String paramCode { get; set; }
        public Int32? paramSort { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
