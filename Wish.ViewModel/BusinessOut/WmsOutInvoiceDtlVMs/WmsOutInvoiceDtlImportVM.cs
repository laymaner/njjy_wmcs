using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlVMs
{
    public partial class WmsOutInvoiceDtlTemplateVM : BaseTemplateVM
    {
        public ExcelPropety allocatResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.allocatResult);
        [Display(Name = "已分配数量")]
        public ExcelPropety allotQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.allotQty);
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.areaNo);
        [Display(Name = "装配顺序")]
        public ExcelPropety assemblyIdx_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.assemblyIdx);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.batchNo);
        [Display(Name = "归属事业部")]
        public ExcelPropety belongDepartment_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.belongDepartment);
        [Display(Name = "已完成数量")]
        public ExcelPropety completeQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.completeQty);
        [Display(Name = "ERP未发数量")]
        public ExcelPropety erpUndeliverQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.erpUndeliverQty);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.extend9);
        [Display(Name = "外部出库单行号")]
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.externalOutDtlId);
        [Display(Name = "外部出库单号")]
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.externalOutNo);
        [Display(Name = "发货单明细状态")]
        public ExcelPropety invoiceDtlStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.invoiceDtlStatus);
        [Display(Name = "WMS发货单号")]
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.invoiceNo);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.orderDtlId);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.orderNo);
        [Display(Name = "生产地点")]
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.productLocation);
        [Display(Name = "生产部门编码")]
        public ExcelPropety productDeptCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.productDeptCode);
        [Display(Name = "生产部门名称")]
        public ExcelPropety productDeptName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.productDeptName);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.proprietorCode);
        [Display(Name = "单据数量")]
        public ExcelPropety invoiceQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.invoiceQty);
        [Display(Name = "已下架数量")]
        public ExcelPropety putdownQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.putdownQty);
        [Display(Name = "序列号")]
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.productSn);
        [Display(Name = "原序列号")]
        public ExcelPropety originalSn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.originalSn);
        public ExcelPropety supplyType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.supplyType);
        [Display(Name = "工单计划开始时间")]
        public ExcelPropety ticketPlanBeginTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.ticketPlanBeginTime);
        [Display(Name = "WMS波次号")]
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.waveNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.whouseNo);
        [Display(Name = "公司代码")]
        public ExcelPropety companyCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.companyCode);
        public ExcelPropety intfId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.intfId);
        public ExcelPropety intfBatchId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.intfBatchId);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.supplierCode);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.supplierName);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.supplierNameEn);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.supplierNameAlias);
        [Display(Name = "工单号")]
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.ticketNo);
        [Display(Name = "单位编码")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceDtl>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceDtlImportVM : BaseImportVM<WmsOutInvoiceDtlTemplateVM, WmsOutInvoiceDtl>
    {

    }

}
