using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocLocVMs
{
    public partial class CfgDocLocSearcher : BaseSearcher
    {
        public String docTypeCode { get; set; }
        public String locNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
