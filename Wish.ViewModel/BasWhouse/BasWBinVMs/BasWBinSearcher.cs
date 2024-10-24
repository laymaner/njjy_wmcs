using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public Int32? binCol { get; set; }
        public String binErrFlag { get; set; }
        public Int32? binHeight { get; set; }
        public Int32? binLayer { get; set; }
        public Int32? binLength { get; set; }
        public String binName { get; set; }
        public String binNo { get; set; }
        public Int32? binRow { get; set; }
        public String binType { get; set; }
        public Int32? binWidth { get; set; }
        public Int32? fireFlag { get; set; }
        public String rackNo { get; set; }
        public String regionNo { get; set; }
        public String roadwayNo { get; set; }
        public Int32? usedFlag { get; set; }
        public Int32? virtualFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
