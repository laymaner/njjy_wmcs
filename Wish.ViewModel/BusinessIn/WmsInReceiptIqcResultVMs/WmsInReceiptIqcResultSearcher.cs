using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{
    public partial class WmsInReceiptIqcResultSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "不良说明")]
        public String badDescription { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "库位号")]
        public String binNo { get; set; }
        [Display(Name = "部门名称")]
        public String departmentName { get; set; }
        public String docTypeCode { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "WMS入库单号")]
        public String inNo { get; set; }
        public String inOutTypeNo { get; set; }
        public Int32? inspectionResult { get; set; }
        [Display(Name = "质检员")]
        public String inspector { get; set; }
        [Display(Name = "WMS检验记录单号")]
        public String iqcRecordNo { get; set; }
        [Display(Name = "WMS检验结果单号")]
        public String iqcResultNo { get; set; }
        [Display(Name = "检验结果状态")]
        public Int32? iqcResultStatus { get; set; }
        [Display(Name = "质检方式")]
        public String iqcType { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "关联单号")]
        public String orderNo { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        [Display(Name = "库区编号")]
        public String regionNo { get; set; }
        [Display(Name = "数据来源")]
        public Int32? sourceBy { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        public String ticketNo { get; set; }
        public String unitCode { get; set; }
        [Display(Name = "急料标记")]
        public Int32? urgentFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
