using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyItemVMs
{
    public partial class CfgStrategyItemSearcher : BaseSearcher
    {
        public String strategyItemGroupNo { get; set; }
        public String strategyItemName { get; set; }
        public String strategyItemNo { get; set; }
        public String strategyTypeCode { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
