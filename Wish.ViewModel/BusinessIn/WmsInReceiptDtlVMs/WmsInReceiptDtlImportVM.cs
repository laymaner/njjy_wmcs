using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptDtlVMs
{
    public partial class WmsInReceiptDtlTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.areaNo);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.batchNo);
        [Display(Name = "部门")]
        public ExcelPropety departmentName_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.departmentName);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.extend9);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.inDtlId);
        [Display(Name = "明细状态")]
        public ExcelPropety receiptDtlStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.receiptDtlStatus);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.inNo);
        [Display(Name = "质检员")]
        public ExcelPropety inspector_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.inspector);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.orderDtlId);
        [Display(Name = "关联单号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.orderNo);
        [Display(Name = "包装数量")]
        public ExcelPropety minPkgQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.minPkgQty);
        [Display(Name = "已回传数量")]
        public ExcelPropety postBackQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.postBackQty);
        [Display(Name = "成品序列号")]
        public ExcelPropety productSn_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.productSn);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.proprietorCode);
        [Display(Name = "上架数量")]
        public ExcelPropety putawayQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.putawayQty);
        [Display(Name = "检验合格数量")]
        public ExcelPropety qualifiedQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.qualifiedQty);
        [Display(Name = "检验特采数量")]
        public ExcelPropety qualifiedSpecialQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.qualifiedSpecialQty);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.receiptNo);
        [Display(Name = "收货完成数量")]
        public ExcelPropety receiptQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.receiptQty);
        [Display(Name = "已组盘数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.recordQty);
        [Display(Name = "直接退货数量")]
        public ExcelPropety returnQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.returnQty);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.supplierNameEn);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.supplierCode);
        [Display(Name = "检验不合格数量")]
        public ExcelPropety unqualifiedQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.unqualifiedQty);
        [Display(Name = "急料标记")]
        public ExcelPropety urgentFlag_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.urgentFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.whouseNo);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptDtl>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptDtlImportVM : BaseImportVM<WmsInReceiptDtlTemplateVM, WmsInReceiptDtl>
    {

    }

}
