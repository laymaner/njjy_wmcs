using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyDtlVMs
{
    public partial class CfgStrategyDtlSearcher : BaseSearcher
    {
        public String strategyItemIdx { get; set; }
        public String strategyItemNo { get; set; }
        public String strategyItemValue1 { get; set; }
        public String strategyItemValue2 { get; set; }
        public String strategyNo { get; set; }
        public String strategyTypeCode { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
