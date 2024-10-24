using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionVMs
{
    public partial class BasWRegionSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public Int32? manualFlag { get; set; }
        public String pickupMethod { get; set; }
        public String regionName { get; set; }
        public String regionNo { get; set; }
        public String regionTypeCode { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
