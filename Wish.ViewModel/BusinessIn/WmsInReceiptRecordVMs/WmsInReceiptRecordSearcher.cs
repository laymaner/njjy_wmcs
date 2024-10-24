using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs
{
    public partial class WmsInReceiptRecordSearcher : BaseSearcher
    {
        [Display(Name = "楼号")]
        public String areaNo { get; set; }
        [Display(Name = "批次")]
        public String batchNo { get; set; }
        [Display(Name = "库位号")]
        public String binNo { get; set; }
        [Display(Name = "部门")]
        public String departmentName { get; set; }
        public String docTypeCode { get; set; }
        [Display(Name = "ERP仓库")]
        public String erpWhouseNo { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "WMS入库单号")]
        public String inNo { get; set; }
        public String inOutTypeNo { get; set; }
        [Display(Name = "质量标记")]
        public Int32? inspectionResult { get; set; }
        [Display(Name = "质检员")]
        public String inspector { get; set; }
        [Display(Name = "WMS检验结果单号")]
        public String iqcResultNo { get; set; }
        [Display(Name = "检验结果状态")]
        public Int32? inRecordStatus { get; set; }
        [Display(Name = "装载类型")]
        public Int32? loadedType { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "实际上架后的库存编码")]
        public String ptaStockCode { get; set; }
        [Display(Name = "实际上架后的库存编码")]
        public String ptaStockDtlId { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "实际上架库位号")]
        public String ptaBinNo { get; set; }
        [Display(Name = "实际上架后的托盘号")]
        public String ptaPalletBarcode { get; set; }
        [Display(Name = "WMS收货单号")]
        public String receiptNo { get; set; }
        [Display(Name = "组盘数量")]
        public Decimal? recordQty { get; set; }
        [Display(Name = "库区编号")]
        public String regionNo { get; set; }
        public String returnResult { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        [Display(Name = "供应商编码")]
        public String supplierCode { get; set; }
        [Display(Name = "供方名称")]
        public String supplierName { get; set; }
        [Display(Name = "工单号")]
        public String ticketNo { get; set; }
        [Display(Name = "急料标记")]
        public Int32? urgentFlag { get; set; }
        [Display(Name = "单位")]
        public String unitCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
