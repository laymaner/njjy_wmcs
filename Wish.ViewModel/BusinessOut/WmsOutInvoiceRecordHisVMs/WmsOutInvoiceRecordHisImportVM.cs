using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordHisVMs
{
    public partial class WmsOutInvoiceRecordHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety allocatResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.allocatResult);
        public ExcelPropety allotQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.allotQty);
        public ExcelPropety allotType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.allotType);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.docTypeCode);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.areaNo);
        public ExcelPropety assemblyIdx_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.assemblyIdx);
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.batchNo);
        public ExcelPropety belongDepartment_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.belongDepartment);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.binNo);
        public ExcelPropety deliveryLocNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.deliveryLocNo);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.extend9);
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.externalOutDtlId);
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.externalOutNo);
        public ExcelPropety fpName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.fpName);
        public ExcelPropety fpNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.fpNo);
        public ExcelPropety fpQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.fpQty);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.inOutTypeNo);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.inspectionResult);
        public ExcelPropety invoiceDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.invoiceDtlId);
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.invoiceNo);
        public ExcelPropety isBatch_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.isBatch);
        public ExcelPropety issuedResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.issuedResult);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.orderDtlId);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.orderNo);
        public ExcelPropety outRecordStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.outRecordStatus);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.palletBarcode);
        public ExcelPropety palletPickType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.palletPickType);
        public ExcelPropety pickLocNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.pickLocNo);
        public ExcelPropety pickQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.pickQty);
        public ExcelPropety pickTaskNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.pickTaskNo);
        public ExcelPropety pickType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.pickType);
        public ExcelPropety preStockDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.preStockDtlId);
        public ExcelPropety productDeptCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.productDeptCode);
        public ExcelPropety productDeptName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.productDeptName);
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.productLocation);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.proprietorCode);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.regionNo);
        public ExcelPropety reversePickFlag_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.reversePickFlag);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.skuCode);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.sourceBy);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.stockDtlId);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.supplierCode);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.supplierNameEn);
        public ExcelPropety supplyType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.supplyType);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.ticketNo);
        public ExcelPropety ticketPlanBeginTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.ticketPlanBeginTime);
        public ExcelPropety ticketType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.ticketType);
        [Display(Name = "单位编码")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.unitCode);
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.waveNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.whouseNo);
        public ExcelPropety operationMode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.operationMode);
        public ExcelPropety outBarCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.outBarCode);
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.urgentFlag);
        [Display(Name = "装载状态")]
        public ExcelPropety loadedTtype_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.loadedTtype);
        [Display(Name = "序列号")]
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.productSn);
        [Display(Name = "灯光颜色")]
        public ExcelPropety lightColor_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceRecordHis>(x => x.lightColor);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceRecordHisImportVM : BaseImportVM<WmsOutInvoiceRecordHisTemplateVM, WmsOutInvoiceRecordHis>
    {

    }

}
