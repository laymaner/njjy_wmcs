using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptVMs
{
    public partial class WmsInReceiptSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public String cvCode { get; set; }
        public String cvName { get; set; }
        public String cvType { get; set; }
        public String docTypeCode { get; set; }
        public String externalInNo { get; set; }
        public String inNo { get; set; }
        public String inOutTypeNo { get; set; }
        public String proprietorCode { get; set; }
        public String receiptNo { get; set; }
        public Int32? receiptStatus { get; set; }
        public String regionNo { get; set; }
        public String ticketNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
