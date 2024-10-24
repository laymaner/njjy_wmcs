﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveRecordVMs
{
    public partial class WmsItnMoveRecordTemplateVM : BaseTemplateVM
    {
        [Display(Name = "码级管理")]
        public ExcelPropety barcodeFlag_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.barcodeFlag);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.batchNo);
        [Display(Name = "CONFIRM_QTY")]
        public ExcelPropety confirmQty_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.confirmQty);
        public ExcelPropety curLocationNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.curLocationNo);
        public ExcelPropety curLocationType_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.curLocationType);
        public ExcelPropety curPalletbarCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.curPalletbarCode);
        [Display(Name = "当前库存编码")]
        public ExcelPropety curStockCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.curStockCode);
        [Display(Name = "当前库存明细ID")]
        public ExcelPropety curStockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.curStockDtlId);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.docTypeCode);
        [Display(Name = "来源站台/库位号")]
        public ExcelPropety frLocationNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frLocationNo);
        [Display(Name = "来源位置类型")]
        public ExcelPropety frLocationType_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frLocationType);
        [Display(Name = "来源托盘条码")]
        public ExcelPropety frPalletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frPalletBarcode);
        public ExcelPropety frRegionNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frRegionNo);
        public ExcelPropety frStockCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frStockCode);
        public ExcelPropety frStockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.frStockDtlId);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.inspectionResult);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.materialSpec);
        public ExcelPropety moveNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.moveNo);
        public ExcelPropety moveDtlId_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.moveDtlId);
        public ExcelPropety moveQty_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.moveQty);
        public ExcelPropety moveRecordStatus_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.moveRecordStatus);
        public ExcelPropety pickMethod_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.pickMethod);
        public ExcelPropety pickType_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.pickType);
        public ExcelPropety productDate_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.productDate);
        public ExcelPropety expDate_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.expDate);
        public ExcelPropety putDownLocNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.putDownLocNo);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.skuCode);
        public ExcelPropety stockQty_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.stockQty);
        public ExcelPropety supplierBatchNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierBatchNo);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierCode);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierName);
        [Display(Name = "供应商名称-EN")]
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierNameEn);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierNameAlias);
        [Display(Name = "供货方类型：供应商、产线")]
        public ExcelPropety supplierType_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.supplierType);
        [Display(Name = "目标库位/站台号")]
        public ExcelPropety toLocationNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toLocationNo);
        public ExcelPropety toLocationType_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toLocationType);
        public ExcelPropety toPalletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toPalletBarcode);
        [Display(Name = "目标库区")]
        public ExcelPropety toRegionNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toRegionNo);
        [Display(Name = "目标库存编码")]
        public ExcelPropety toStockCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toStockCode);
        [Display(Name = "目标库存明细ID")]
        public ExcelPropety toStockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.toStockDtlId);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.unitCode);
        [Display(Name = "货主")]
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.proprietorCode);
        [Display(Name = "仓库号")]
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveRecord>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnMoveRecordImportVM : BaseImportVM<WmsItnMoveRecordTemplateVM, WmsItnMoveRecord>
    {

    }

}
