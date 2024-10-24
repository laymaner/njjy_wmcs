using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordHisVMs
{
    public partial class WmsInReceiptRecordHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.areaNo);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.batchNo);
        [Display(Name = "库位号")]
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.binNo);
        [Display(Name = "部门")]
        public ExcelPropety departmentName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.departmentName);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.docTypeCode);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.erpWhouseNo);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inDtlId);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inOutTypeNo);
        [Display(Name = "质量标记")]
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inspectionResult);
        [Display(Name = "质检员")]
        public ExcelPropety inspector_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inspector);
        [Display(Name = "WMS检验单号")]
        public ExcelPropety iqcResultNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.iqcResultNo);
        [Display(Name = "检验结果状态")]
        public ExcelPropety inRecordStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.inRecordStatus);
        [Display(Name = "装载类型")]
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.loadedType);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.orderNo);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.palletBarcode);
        [Display(Name = "实际上架后的库区")]
        public ExcelPropety ptaRegionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ptaRegionNo);
        [Display(Name = "实际上架后的库存编码")]
        public ExcelPropety ptaStockCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ptaStockCode);
        [Display(Name = "实际上架后的库存编码")]
        public ExcelPropety ptaStockDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ptaStockDtlId);
        [Display(Name = "包装数量")]
        public ExcelPropety minPkgQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.minPkgQty);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.proprietorCode);
        [Display(Name = "实际上架库位号")]
        public ExcelPropety ptaBinNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ptaBinNo);
        [Display(Name = "实际上架后的托盘号")]
        public ExcelPropety ptaPalletBarcode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ptaPalletBarcode);
        public ExcelPropety receiptDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.receiptDtlId);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.receiptNo);
        [Display(Name = "组盘数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.recordQty);
        [Display(Name = "库区编号")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.regionNo);
        public ExcelPropety replenishFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.replenishFlag);
        public ExcelPropety returnFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.returnFlag);
        public ExcelPropety returnResult_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.returnResult);
        public ExcelPropety returnTime_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.returnTime);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.skuCode);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.sourceBy);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.stockDtlId);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.supplierCode);
        [Display(Name = "供方名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.supplierNameEn);
        [Display(Name = "工单号")]
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.ticketNo);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.urgentFlag);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptRecordHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptRecordHisImportVM : BaseImportVM<WmsInReceiptRecordHisTemplateVM, WmsInReceiptRecordHis>
    {

    }

}
