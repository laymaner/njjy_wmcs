using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockDtlVMs
{
    public partial class WmsStockDtlSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "质量标记")]
        public Int32? inspectionResult { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        [Display(Name = "备用项目号")]
        public String projectNoBak { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "单位")]
        public String unitCode { get; set; }
        [Display(Name = "版本计数")]
        public String businessCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
