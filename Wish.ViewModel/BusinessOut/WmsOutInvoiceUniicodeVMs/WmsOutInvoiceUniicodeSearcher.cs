using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeVMs
{
    public partial class WmsOutInvoiceUniicodeSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "延期原因")]
        public String delayReason { get; set; }
        [Display(Name = "延期次数")]
        public Int32? delayTimes { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "质检结果")]
        public Int32? inspectionResult { get; set; }
        [Display(Name = "WMS发货单号")]
        public String invoiceNo { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "拣选任务号")]
        public String pickTaskNo { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String skuCode { get; set; }
        [Display(Name = "库存编码")]
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "WMS波次号")]
        public String waveNo { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "出库条码")]
        public String outBarcode { get; set; }
        [Display(Name = "包装条码状态")]
        public Int32? ouniiStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
