using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeVMs
{
    public partial class CfgDocTypeSearcher : BaseSearcher
    {
        public String businessCode { get; set; }
        public String cvType { get; set; }
        public Int32? developFlag { get; set; }
        public String docTypeCode { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
