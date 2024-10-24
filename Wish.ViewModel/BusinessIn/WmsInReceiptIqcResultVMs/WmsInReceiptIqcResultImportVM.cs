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
    public partial class WmsInReceiptIqcResultTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.areaNo);
        [Display(Name = "不良说明")]
        public ExcelPropety badDescription_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.badDescription);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.batchNo);
        [Display(Name = "库位号")]
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.binNo);
        [Display(Name = "部门名称")]
        public ExcelPropety departmentName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.departmentName);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.docTypeCode);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.extend9);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inDtlId);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inOutTypeNo);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inspectionResult);
        [Display(Name = "质检员")]
        public ExcelPropety inspector_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.inspector);
        [Display(Name = "WMS检验记录单号")]
        public ExcelPropety iqcRecordNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.iqcRecordNo);
        [Display(Name = "WMS检验结果单号")]
        public ExcelPropety iqcResultNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.iqcResultNo);
        [Display(Name = "检验结果状态")]
        public ExcelPropety iqcResultStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.iqcResultStatus);
        [Display(Name = "质检方式")]
        public ExcelPropety iqcType_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.iqcType);
        public ExcelPropety isReturnFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.isReturnFlag);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.materialSpec);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.orderDesc);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.orderNo);
        [Display(Name = "包装数量")]
        public ExcelPropety minPkgQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.minPkgQty);
        [Display(Name = "已回传数量")]
        public ExcelPropety postBackQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.postBackQty);
        [Display(Name = "成品序列号")]
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.productSn);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.proprietorCode);
        [Display(Name = "上架数量")]
        public ExcelPropety putawayQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.putawayQty);
        [Display(Name = "数量")]
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.qty);
        public ExcelPropety receiptDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.receiptDtlId);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.receiptNo);
        [Display(Name = "已组盘数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.recordQty);
        [Display(Name = "库区编号")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.regionNo);
        [Display(Name = "直接退货数量")]
        public ExcelPropety returnQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.returnQty);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.skuCode);
        [Display(Name = "数据来源")]
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.sourceBy);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.supplierNameEn);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.supplierCode);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.ticketNo);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.unitCode);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.urgentFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcResult>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptIqcResultImportVM : BaseImportVM<WmsInReceiptIqcResultTemplateVM, WmsInReceiptIqcResult>
    {

    }

}
