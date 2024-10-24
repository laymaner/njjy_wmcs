using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeDtlVMs
{
    public partial class CfgDocTypeDtlSearcher : BaseSearcher
    {
        public String businessCode { get; set; }
        public String businessModuleCode { get; set; }
        public String docTypeCode { get; set; }
        public String paramCode { get; set; }
        public String paramValueCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
