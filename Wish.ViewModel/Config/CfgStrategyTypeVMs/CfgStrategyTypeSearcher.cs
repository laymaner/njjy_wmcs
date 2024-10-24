using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyTypeVMs
{
    public partial class CfgStrategyTypeSearcher : BaseSearcher
    {
        public String strategyTypeCategory { get; set; }
        public String strategyTypeCode { get; set; }
        public String strategyTypeName { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
