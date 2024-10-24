using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs
{
    public partial class WmsInReceiptUniicodeTemplateVM : BaseTemplateVM
    {
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.batchNo);
        [Display(Name = "当前托盘号")]
        public ExcelPropety curPalletBarcode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.curPalletBarcode);
        [Display(Name = "当前位置编号")]
        public ExcelPropety curPositionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.curPositionNo);
        [Display(Name = "当前库存编码")]
        public ExcelPropety curStockCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.curStockCode);
        [Display(Name = "当前库存明细ID")]
        public ExcelPropety curStockDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.curStockDtlId);
        [Display(Name = "DC")]
        public ExcelPropety dataCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.dataCode);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.erpWhouseNo);
        [Display(Name = "失效日期")]
        public ExcelPropety expDate_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.expDate);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.extend9);
        [Display(Name = "外部入库单行号")]
        public ExcelPropety externalInDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.externalInDtlId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.externalInNo);
        public ExcelPropety inDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.inDtlId);
        [Display(Name = "WMS入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.inNo);
        [Display(Name = "WMS检验单号")]
        public ExcelPropety iqcResultNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.iqcResultNo);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.materialSpec);
        [Display(Name = "MSL等级")]
        public ExcelPropety mslGradeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.mslGradeCode);
        [Display(Name = "生产日期")]
        public ExcelPropety productDate_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.productDate);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.proprietorCode);
        public ExcelPropety receiptDtlId_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.receiptDtlId);
        [Display(Name = "WMS收货单号")]
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.receiptNo);
        [Display(Name = "数量")]
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.qty);
        [Display(Name = "实际上架数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.recordQty);
        public ExcelPropety receiptRecordId_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.receiptRecordId);
        [Display(Name = "供应商暴露时长")]
        public ExcelPropety supplierExposeTimeDuration_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.supplierExposeTimeDuration);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.supplierNameEn);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.supplierCode);
        [Display(Name = "SKU")]
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.skuCode);
        [Display(Name = "包装条码/SN号")]
        public ExcelPropety uniicode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.uniicode);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.whouseNo);
        [Display(Name = "入库唯一码状态")]
        public ExcelPropety runiiStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptUniicode>(x => x.runiiStatus);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptUniicodeImportVM : BaseImportVM<WmsInReceiptUniicodeTemplateVM, WmsInReceiptUniicode>
    {

    }

}
