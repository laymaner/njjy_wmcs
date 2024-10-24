using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlVMs
{
    public partial class WmsOutInvoiceDtlSearcher : BaseSearcher
    {
        public String allocatResult { get; set; }
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部出库单号")]
        public String externalOutNo { get; set; }
        [Display(Name = "发货单明细状态")]
        public Int32? invoiceDtlStatus { get; set; }
        [Display(Name = "WMS发货单号")]
        public String invoiceNo { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        public String orderNo { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        [Display(Name = "序列号")]
        public String productSn { get; set; }
        [Display(Name = "WMS波次号")]
        public String waveNo { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "公司代码")]
        public String companyCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "工单号")]
        public String ticketNo { get; set; }
        [Display(Name = "单位编码")]
        public String unitCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
