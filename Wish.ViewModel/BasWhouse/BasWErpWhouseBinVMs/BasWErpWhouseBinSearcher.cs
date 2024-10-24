using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs
{
    public partial class BasWErpWhouseBinSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public String delFlag { get; set; }
        public String erpWhouseNo { get; set; }
        public String regionNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
