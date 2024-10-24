using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderDtlVMs
{
    public partial class WmsInOrderDtlSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "公司代码")]
        public String companyCode { get; set; }
        [Display(Name = "部门名称")]
        public String departmentName { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "入库单明细状态")]
        public Int32? inDtlStatus { get; set; }
        [Display(Name = "WMS入库单号")]
        public String inNo { get; set; }
        [Display(Name = "质检员")]
        public String inspector { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "规格")]
        public String materialSpec { get; set; }
        [Display(Name = "关联单号")]
        public String orderNo { get; set; }
        [Display(Name = "成品序列号")]
        public String productSn { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "采购订单行号")]
        public String purchaseOrderId { get; set; }
        [Display(Name = "采购订单号")]
        public String purchaseOrderNo { get; set; }
        [Display(Name = "供应商名称")]
        public String supplierName { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "急料标记")]
        public Int32? urgentFlag { get; set; }
        [Display(Name = "单位")]
        public String unitCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
