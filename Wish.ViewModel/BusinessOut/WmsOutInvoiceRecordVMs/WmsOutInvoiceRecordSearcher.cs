using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordVMs
{
    public partial class WmsOutInvoiceRecordSearcher : BaseSearcher
    {
        [Display(Name = "分配结果")]
        public String allocatResult { get; set; }
        [Display(Name = "分配数量")]
        public Decimal? allotQty { get; set; }
        public Int32? allotType { get; set; }
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "装配顺序")]
        public String assemblyIdx { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "库位号")]
        public String binNo { get; set; }
        public String deliveryLocNo { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部出库单号")]
        public String externalOutNo { get; set; }
        [Display(Name = "成品编码")]
        public String fpNo { get; set; }
        [Display(Name = "WMS发货单号")]
        public String invoiceNo { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "规格")]
        public String materialSpec { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "拣选任务编号")]
        public String pickTaskNo { get; set; }
        [Display(Name = "生产部门编码")]
        public String productDeptCode { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "库区编号")]
        public String regionNo { get; set; }
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "工单号")]
        public String ticketNo { get; set; }
        [Display(Name = "单位编码")]
        public String unitCode { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "急料标记")]
        public Int32? urgentFlag { get; set; }
        public Int32? outRecordStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
