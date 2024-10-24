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
    public partial class WmsInReceiptRecordTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.areaNo);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.batchNo);
        [Display(Name = "库位号")]
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.binNo);
        [Display(Name = "部门")]
        public ExcelPropety departmentName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.departmentName);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.docTypeCode);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.erpWhouseNo);
        //public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend1);
        //public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend10);
        //public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend11);
        //public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend12);
        //public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend13);
        //public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend14);
        //public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend15);
        //public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend2);
        //public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend3);
        //public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend4);
        //public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend5);
        //public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend6);
        //public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend7);
        //public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend8);
        //public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.extend9);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inDtlId);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inOutTypeNo);
        [Display(Name = "质量标记")]
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inspectionResult);
        [Display(Name = "质检员")]
        public ExcelPropety inspector_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inspector);
        [Display(Name = "WMS检验结果单号")]
        public ExcelPropety iqcResultNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.iqcResultNo);
        [Display(Name = "检验结果状态")]
        public ExcelPropety inRecordStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.inRecordStatus);
        [Display(Name = "装载类型")]
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.loadedType);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.orderNo);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.palletBarcode);
        [Display(Name = "实际上架后的库区")]
        public ExcelPropety ptaRegionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ptaRegionNo);
        [Display(Name = "实际上架后的库存编码")]
        public ExcelPropety ptaStockCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ptaStockCode);
        [Display(Name = "实际上架后的库存编码")]
        public ExcelPropety ptaStockDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ptaStockDtlId);
        [Display(Name = "包装数量")]
        public ExcelPropety minPkgQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.minPkgQty);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.proprietorCode);
        [Display(Name = "实际上架库位号")]
        public ExcelPropety ptaBinNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ptaBinNo);
        [Display(Name = "实际上架后的托盘号")]
        public ExcelPropety ptaPalletBarcode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ptaPalletBarcode);
        public ExcelPropety receiptDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.receiptDtlId);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.receiptNo);
        [Display(Name = "组盘数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.recordQty);
        [Display(Name = "库区编号")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.regionNo);
        public ExcelPropety replenishFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.replenishFlag);
        public ExcelPropety returnFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.returnFlag);
        public ExcelPropety returnResult_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.returnResult);
        public ExcelPropety returnTime_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.returnTime);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.skuCode);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.sourceBy);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.stockDtlId);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.supplierCode);
        [Display(Name = "供方名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.supplierNameEn);
        [Display(Name = "工单号")]
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.ticketNo);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.urgentFlag);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecord>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptRecordImportVM : BaseImportVM<WmsInReceiptRecordTemplateVM, WmsInReceiptRecord>
    {

    }

}
