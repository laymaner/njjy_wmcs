using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlHisVMs
{
    public partial class WmsOutInvoiceDtlHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety allocatResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.allocatResult);
        public ExcelPropety allotQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.allotQty);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.areaNo);
        public ExcelPropety assemblyIdx_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.assemblyIdx);
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.batchNo);
        public ExcelPropety belongDepartment_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.belongDepartment);
        public ExcelPropety completeQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.completeQty);
        public ExcelPropety erpUndeliverQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.erpUndeliverQty);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.extend9);
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.externalOutDtlId);
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.externalOutNo);
        public ExcelPropety invoiceDtlStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.invoiceDtlStatus);
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.invoiceNo);
        public ExcelPropety invoiceQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.invoiceQty);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.materialSpec);
        public ExcelPropety orderId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.orderId);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.orderNo);
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.productLocation);
        public ExcelPropety productDeptCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.productDeptCode);
        public ExcelPropety productDeptName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.productDeptName);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.proprietorCode);
        public ExcelPropety putdownQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.putdownQty);
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.productSn);
        public ExcelPropety originalSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.originalSn);
        public ExcelPropety supplyType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.supplyType);
        public ExcelPropety ticketPlanBeginTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.ticketPlanBeginTime);
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.waveNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.whouseNo);
        public ExcelPropety companyCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.companyCode);
        public ExcelPropety intfId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.intfId);
        public ExcelPropety intfBatchId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.intfBatchId);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.supplierCode);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.supplierName);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.supplierNameEn);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.supplierNameAlias);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.ticketNo);
        [Display(Name = "单位编码")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtlHis>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceDtlHisImportVM : BaseImportVM<WmsOutInvoiceDtlHisTemplateVM, WmsOutInvoiceDtlHis>
    {

    }

}
