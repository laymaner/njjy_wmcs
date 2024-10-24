using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceHisVMs
{
    public partial class WmsOutInvoiceHisSearcher : BaseSearcher
    {
        public String cvCode { get; set; }
        public String cvType { get; set; }
        public String docTypeCode { get; set; }
        public String externalOutNo { get; set; }
        public String inOutTypeNo { get; set; }
        public String invoiceNo { get; set; }
        public Int32? invoiceStatus { get; set; }
        public String proprietorCode { get; set; }
        public String ticketNo { get; set; }
        public String whouseNo { get; set; }
        public String projectNo { get; set; }
        public String waveNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
