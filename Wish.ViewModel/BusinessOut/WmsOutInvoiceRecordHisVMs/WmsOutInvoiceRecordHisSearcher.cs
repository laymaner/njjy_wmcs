using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordHisVMs
{
    public partial class WmsOutInvoiceRecordHisSearcher : BaseSearcher
    {
        public String allocatResult { get; set; }
        public Int32? allotType { get; set; }
        public String docTypeCode { get; set; }
        public String areaNo { get; set; }
        public String batchNo { get; set; }
        public String belongDepartment { get; set; }
        public String binNo { get; set; }
        public String deliveryLocNo { get; set; }
        public String erpWhouseNo { get; set; }
        public String externalOutNo { get; set; }
        public String fpNo { get; set; }
        public Int32? inspectionResult { get; set; }
        public String invoiceNo { get; set; }
        public String materialCode { get; set; }
        public String orderNo { get; set; }
        public Int32? outRecordStatus { get; set; }
        public String pickLocNo { get; set; }
        public String pickTaskNo { get; set; }
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        public String regionNo { get; set; }
        public String skuCode { get; set; }
        public Int32? sourceBy { get; set; }
        public String stockCode { get; set; }
        public String supplierCode { get; set; }
        public String ticketNo { get; set; }
        [Display(Name = "单位编码")]
        public String unitCode { get; set; }
        public String waveNo { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "装载状态")]
        public Int32? loadedTtype { get; set; }
        protected override void InitVM()
        {
        }

    }
}
