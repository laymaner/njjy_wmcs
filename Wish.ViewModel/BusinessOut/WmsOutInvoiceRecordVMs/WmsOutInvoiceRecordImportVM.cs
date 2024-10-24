using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordVMs
{
    public partial class WmsOutInvoiceRecordTemplateVM : BaseTemplateVM
    {
        [Display(Name = "分配结果")]
        public ExcelPropety allocatResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.allocatResult);
        [Display(Name = "分配数量")]
        public ExcelPropety allotQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.allotQty);
        public ExcelPropety allotType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.allotType);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.docTypeCode);
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.areaNo);
        [Display(Name = "装配顺序")]
        public ExcelPropety assemblyIdx_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.assemblyIdx);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.batchNo);
        [Display(Name = "归属事业部")]
        public ExcelPropety belongDepartment_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.belongDepartment);
        [Display(Name = "库位号")]
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.binNo);
        public ExcelPropety deliveryLocNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.deliveryLocNo);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.extend9);
        [Display(Name = "外部出库单行号")]
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.externalOutDtlId);
        [Display(Name = "外部出库单号")]
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.externalOutNo);
        [Display(Name = "成品名称")]
        public ExcelPropety fpName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.fpName);
        [Display(Name = "成品编码")]
        public ExcelPropety fpNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.fpNo);
        [Display(Name = "成品数量")]
        public ExcelPropety fpQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.fpQty);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.inOutTypeNo);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.inspectionResult);
        public ExcelPropety invoiceDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.invoiceDtlId);
        [Display(Name = "WMS发货单号")]
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.invoiceNo);
        public ExcelPropety isBatch_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.isBatch);
        public ExcelPropety issuedResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.issuedResult);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.orderNo);
        [Display(Name = "出库记录状态")]
        public ExcelPropety outRecordStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.outRecordStatus);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.palletBarcode);
        public ExcelPropety palletPickType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.palletPickType);
        public ExcelPropety pickLocNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.pickLocNo);
        [Display(Name = "出库数量")]
        public ExcelPropety pickQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.pickQty);
        [Display(Name = "拣选任务编号")]
        public ExcelPropety pickTaskNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.pickTaskNo);
        public ExcelPropety pickType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.pickType);
        public ExcelPropety preStockDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.preStockDtlId);
        [Display(Name = "生产部门编码")]
        public ExcelPropety productDeptCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.productDeptCode);
        [Display(Name = "生产部门名称")]
        public ExcelPropety productDeptName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.productDeptName);
        [Display(Name = "生产地点")]
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.productLocation);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.proprietorCode);
        [Display(Name = "库区编号")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.regionNo);
        public ExcelPropety reversePickFlag_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.reversePickFlag);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.skuCode);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.sourceBy);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.stockDtlId);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.supplierCode);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.supplierNameEn);
        public ExcelPropety supplyType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.supplyType);
        [Display(Name = "工单号")]
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.ticketNo);
        [Display(Name = "工单计划开始时间")]
        public ExcelPropety ticketPlanBeginTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.ticketPlanBeginTime);
        [Display(Name = "工单类型")]
        public ExcelPropety ticketType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.ticketType);
        [Display(Name = "单位编码")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.unitCode);
        [Display(Name = "WMS波次号")]
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.waveNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.whouseNo);
        public ExcelPropety operationMode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.operationMode);
        [Display(Name = "出库打印条码")]
        public ExcelPropety outBarCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.outBarCode);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.urgentFlag);
        [Display(Name = "装载状态")]
        public ExcelPropety loadedTtype_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.loadedTtype);
        [Display(Name = "序列号")]
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.productSn);
        [Display(Name = "灯光颜色")]
        public ExcelPropety lightColor_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecord>(x => x.lightColor);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceRecordImportVM : BaseImportVM<WmsOutInvoiceRecordTemplateVM, WmsOutInvoiceRecord>
    {

    }

}
