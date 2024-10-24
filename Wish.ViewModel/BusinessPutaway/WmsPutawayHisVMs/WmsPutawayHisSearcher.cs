using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayHisVMs
{
    public partial class WmsPutawayHisSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public Int32? loadedType { get; set; }
        public Int32? manualFlag { get; set; }
        public String onlineLocNo { get; set; }
        public String onlineMethod { get; set; }
        public String palletBarcode { get; set; }
        public String proprietorCode { get; set; }
        public String ptaRegionNo { get; set; }
        public String putawayNo { get; set; }
        public Int32? putawayStatus { get; set; }
        public String regionNo { get; set; }
        public String stockCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
