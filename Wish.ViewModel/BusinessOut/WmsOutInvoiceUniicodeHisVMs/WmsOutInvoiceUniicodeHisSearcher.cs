using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeHisVMs
{
    public partial class WmsOutInvoiceUniicodeHisSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "批次号")]
        public String batchNo { get; set; }
        [Display(Name = "DC")]
        public String dataCode { get; set; }
        public String erpBinNo { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "失效日期")]
        public DateRange expDate { get; set; }
        [Display(Name = "质检结果")]
        public Int32? inspectionResult { get; set; }
        [Display(Name = "发货单号")]
        public String invoiceNo { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "供应商名称")]
        public String supplierName { get; set; }
        [Display(Name = "包装条码/SN码")]
        public String uniicode { get; set; }
        [Display(Name = "波次号")]
        public String waveNo { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "出库条码")]
        public String outBarcode { get; set; }
        public String ouniiStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
