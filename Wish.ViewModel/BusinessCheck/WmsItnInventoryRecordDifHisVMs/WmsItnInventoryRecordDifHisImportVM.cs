using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordDifHisVMs
{
    public partial class WmsItnInventoryRecordDifHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.batchNo);
        public ExcelPropety dataCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.dataCode);
        public ExcelPropety delayFrozenFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delayFrozenFlag);
        public ExcelPropety delayFrozenReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delayFrozenReason);
        public ExcelPropety delayReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delayReason);
        public ExcelPropety delayTimes_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delayTimes);
        public ExcelPropety delayToEndDate_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delayToEndDate);
        public ExcelPropety difQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.difQty);
        public ExcelPropety driedScrapFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.driedScrapFlag);
        public ExcelPropety driedTimes_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.driedTimes);
        public ExcelPropety expDate_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.expDate);
        public ExcelPropety exposeFrozenFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.exposeFrozenFlag);
        public ExcelPropety exposeFrozenReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.exposeFrozenReason);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.extend9);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inspectionResult);
        public ExcelPropety inventoryDtlId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inventoryDtlId);
        public ExcelPropety inventoryNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inventoryNo);
        public ExcelPropety inventoryQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inventoryQty);
        public ExcelPropety inventoryRecordId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inventoryRecordId);
        public ExcelPropety leftMslTimes_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.leftMslTimes);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.materialSpec);
        public ExcelPropety mslGradeCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.mslGradeCode);
        public ExcelPropety packageTime_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.packageTime);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.palletBarcode);
        public ExcelPropety positionNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.positionNo);
        public ExcelPropety productDate_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.productDate);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.proprietorCode);
        public ExcelPropety realExposeTimes_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.realExposeTimes);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.stockDtlId);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.supplierCode);
        public ExcelPropety supplierExposeTimes_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.supplierExposeTimes);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.supplierNameEn);
        public ExcelPropety uniicode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.uniicode);
        public ExcelPropety unpackTime_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.unpackTime);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.whouseNo);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.areaNo);
        public ExcelPropety delFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.delFlag);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.erpWhouseNo);
        [Display(Name = "入库时间")]
        public ExcelPropety inwhTime_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.inwhTime);
        public ExcelPropety occupyQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.occupyQty);
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.qty);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.unitCode);
        public ExcelPropety fileedId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.fileedId);
        [Display(Name = "附件名称")]
        public ExcelPropety fileedName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.fileedName);
        public ExcelPropety oldStockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.oldStockDtlId);
        [Display(Name = "备用项目号")]
        public ExcelPropety projectNoBak_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.projectNoBak);
        [Display(Name = "拆封状态")]
        public ExcelPropety unpackStatus_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecordDifHis>(x => x.unpackStatus);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnInventoryRecordDifHisImportVM : BaseImportVM<WmsItnInventoryRecordDifHisTemplateVM, WmsItnInventoryRecordDifHis>
    {

    }

}
