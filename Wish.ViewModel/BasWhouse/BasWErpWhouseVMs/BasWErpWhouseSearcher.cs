using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseVMs
{
    public partial class BasWErpWhouseSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String erpWhouseName { get; set; }
        public String erpWhouseNo { get; set; }
        public String erpWhouseType { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
