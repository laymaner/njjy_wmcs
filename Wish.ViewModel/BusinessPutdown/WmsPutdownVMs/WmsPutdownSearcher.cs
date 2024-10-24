using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public String docTypeCode { get; set; }
        public Int32? loadedType { get; set; }
        public String orderNo { get; set; }
        public String palletBarcode { get; set; }
        public String pickupMethod { get; set; }
        public String proprietorCode { get; set; }
        public String putdownNo { get; set; }
        public Int32? putdownStatus { get; set; }
        public String regionNo { get; set; }
        public String stockCode { get; set; }
        public String targetLocNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
