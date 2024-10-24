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
    public partial class WmsInReceiptIqcRecordHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.areaNo);
        [Display(Name = "不良说明")]
        public ExcelPropety badDescription_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.badDescription);
        [Display(Name = "不良选项")]
        public ExcelPropety badOptions_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.badOptions);
        [Display(Name = "不良处理方式")]
        public ExcelPropety badSolveType_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.badSolveType);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.batchNo);
        [Display(Name = "库位编码")]
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.binNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.docTypeCode);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.extend9);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.inDtlId);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.inOutTypeNo);
        [Display(Name = "质检员")]
        public ExcelPropety inspector_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.inspector);
        [Display(Name = "WMS检验记录单号")]
        public ExcelPropety iqcRecordNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.iqcRecordNo);
        [Display(Name = "检验结果状态")]
        public ExcelPropety iqcRecordStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.iqcRecordStatus);
        [Display(Name = "质检方式")]
        public ExcelPropety iqcType_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.iqcType);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.materialSpec);
        [Display(Name = "包装数量")]
        public ExcelPropety minPkgQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.minPkgQty);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.orderNo);
        [Display(Name = "备注说明")]
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.orderDesc);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.proprietorCode);
        [Display(Name = "检验合格数量")]
        public ExcelPropety qualifiedQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.qualifiedQty);
        [Display(Name = "ERP特采数量")]
        public ExcelPropety erpQualifiedSpecialQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.erpQualifiedSpecialQty);
        public ExcelPropety receiptDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.receiptDtlId);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.receiptNo);
        [Display(Name = "收货完成数量")]
        public ExcelPropety receiptQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.receiptQty);
        [Display(Name = "库区编码")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.regionNo);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.sourceBy);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.supplierNameEn);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.supplierCode);
        [Display(Name = "检验不合格数量")]
        public ExcelPropety wmsUnqualifiedQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.wmsUnqualifiedQty);
        [Display(Name = "ERP不合格数量")]
        public ExcelPropety erpUnqualifiedQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.erpUnqualifiedQty);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.urgentFlag);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptIqcRecordHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptIqcRecordHisImportVM : BaseImportVM<WmsInReceiptIqcRecordHisTemplateVM, WmsInReceiptIqcRecordHis>
    {

    }

}
