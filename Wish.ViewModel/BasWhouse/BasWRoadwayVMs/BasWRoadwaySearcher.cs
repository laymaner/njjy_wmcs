using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRoadwayVMs
{
    public partial class BasWRoadwaySearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public Int32? errFlag { get; set; }
        public String regionNo { get; set; }
        public String roadwayName { get; set; }
        public String roadwayNo { get; set; }
        public Int32? usedFlag { get; set; }
        public Int32? virtualFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
