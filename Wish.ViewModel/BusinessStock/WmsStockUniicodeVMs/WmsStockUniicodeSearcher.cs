using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs
{
    public partial class WmsStockUniicodeSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "货位")]
        public String binNo { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "有效期冻结")]
        public Int32? delayFrozenFlag { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "暴露冻结标志")]
        public Int32? exposeFrozenFlag { get; set; }
        [Display(Name = "库存状态")]
        public Int32? stockStatus { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "规格")]
        public String materialSpec { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "包装条码/SN码")]
        public String uniicode { get; set; }
        [Display(Name = "单位")]
        public int? unpackStatus { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
