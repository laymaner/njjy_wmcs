using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveRecordHisVMs
{
    public partial class WmsItnMoveRecordHisSearcher : BaseSearcher
    {
        [Display(Name = "码级管理")]
        public Int32? barcodeFlag { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "当前库存编码")]
        public String curStockCode { get; set; }
        public String docTypeCode { get; set; }
        [Display(Name = "来源站台/库位号")]
        public String frLocationNo { get; set; }
        [Display(Name = "来源位置类型")]
        public String frLocationType { get; set; }
        [Display(Name = "来源托盘条码")]
        public String frPalletBarcode { get; set; }
        public String frRegionNo { get; set; }
        public String frStockCode { get; set; }
        public Int32? inspectionResult { get; set; }
        public String materialCode { get; set; }
        public String moveNo { get; set; }
        public Int32? moveRecordStatus { get; set; }
        public String pickMethod { get; set; }
        public String pickType { get; set; }
        public String putDownLocNo { get; set; }
        public String skuCode { get; set; }
        public String supplierBatchNo { get; set; }
        [Display(Name = "供应商名称")]
        public String supplierName { get; set; }
        [Display(Name = "供货方类型：供应商、产线")]
        public String supplierType { get; set; }
        public String toLocationType { get; set; }
        public String toPalletBarcode { get; set; }
        [Display(Name = "目标库区")]
        public String toRegionNo { get; set; }
        [Display(Name = "目标库存编码")]
        public String toStockCode { get; set; }
        public String unitCode { get; set; }
        [Display(Name = "货主")]
        public String proprietorCode { get; set; }
        [Display(Name = "仓库号")]
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
