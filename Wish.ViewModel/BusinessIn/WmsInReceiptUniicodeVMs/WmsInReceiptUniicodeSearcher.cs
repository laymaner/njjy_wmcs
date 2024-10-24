using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs
{
    public partial class WmsInReceiptUniicodeSearcher : BaseSearcher
    {
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "当前托盘号")]
        public String curPalletBarcode { get; set; }
        [Display(Name = "当前位置编号")]
        public String curPositionNo { get; set; }
        [Display(Name = "当前库存编码")]
        public String curStockCode { get; set; }
        [Display(Name = "当前库存明细ID")]
        public String curStockDtlId { get; set; }
        [Display(Name = "DC")]
        public String dataCode { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部入库单行号")]
        public String externalInDtlId { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "WMS入库单号")]
        public String inNo { get; set; }
        [Display(Name = "WMS检验单号")]
        public String iqcResultNo { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        [Display(Name = "WMS收货单号")]
        public String receiptNo { get; set; }
        [Display(Name = "单位")]
        public String unitCode { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "入库唯一码状态")]
        public Int32? runiiStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
