using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordHisVMs
{
    public partial class WmsInReceiptIqcRecordHisSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "不良说明")]
        public String badDescription { get; set; }
        [Display(Name = "不良选项")]
        public String badOptions { get; set; }
        [Display(Name = "不良处理方式")]
        public String badSolveType { get; set; }
        [Display(Name = "库位编码")]
        public String binNo { get; set; }
        public String docTypeCode { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "WMS入库单号")]
        public String inNo { get; set; }
        public String inOutTypeNo { get; set; }
        [Display(Name = "WMS检验记录单号")]
        public String iqcRecordNo { get; set; }
        [Display(Name = "检验结果状态")]
        public Int32? iqcRecordStatus { get; set; }
        [Display(Name = "质检方式")]
        public String iqcType { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "规格")]
        public String materialSpec { get; set; }
        [Display(Name = "关联单号")]
        public String orderNo { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "WMS收货单号")]
        public String receiptNo { get; set; }
        [Display(Name = "收货完成数量")]
        public Decimal? receiptQty { get; set; }
        [Display(Name = "库区编码")]
        public String regionNo { get; set; }
        [Display(Name = "供应商名称")]
        public String supplierName { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "急料标记")]
        public Int32? urgentFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
