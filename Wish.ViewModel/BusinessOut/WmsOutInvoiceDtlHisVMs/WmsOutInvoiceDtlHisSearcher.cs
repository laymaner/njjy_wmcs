using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlHisVMs
{
    public partial class WmsOutInvoiceDtlHisSearcher : BaseSearcher
    {
        public String allocatResult { get; set; }
        public Decimal? allotQty { get; set; }
        public String areaNo { get; set; }
        public String batchNo { get; set; }
        public String erpWhouseNo { get; set; }
        public String externalOutNo { get; set; }
        public Int32? invoiceDtlStatus { get; set; }
        public String invoiceNo { get; set; }
        public String materialCode { get; set; }
        public String orderNo { get; set; }
        public String productDeptCode { get; set; }
        public String productDeptName { get; set; }
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        public String productSn { get; set; }
        public String supplyType { get; set; }
        public String waveNo { get; set; }
        public String whouseNo { get; set; }
        public String companyCode { get; set; }
        public String supplierCode { get; set; }
        public String ticketNo { get; set; }
        [Display(Name = "单位编码")]
        public String unitCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
