using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordTemplateVM : BaseTemplateVM
    {
        public ExcelPropety blindFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.blindFlag);
        public ExcelPropety confirmBy_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.confirmBy);
        public ExcelPropety confirmQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.confirmQty);
        public ExcelPropety confirmReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.confirmReason);
        public ExcelPropety differenceFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.differenceFlag);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.docTypeCode);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.extend9);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inspectionResult);
        public ExcelPropety inventoryBy_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryBy);
        public ExcelPropety inventoryDtlId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryDtlId);
        public ExcelPropety inventoryNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryNo);
        public ExcelPropety inventoryQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryQty);
        public ExcelPropety inventoryReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryReason);
        public ExcelPropety inventoryRecordStatus_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.inventoryRecordStatus);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.materialSpec);
        public ExcelPropety occupyQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.occupyQty);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.palletBarcode);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.proprietorCode);
        public ExcelPropety putdownLocNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.putdownLocNo);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.stockDtlId);
        public ExcelPropety stockQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.stockQty);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.supplierCode);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.supplierNameEn);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryRecord>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnInventoryRecordImportVM : BaseImportVM<WmsItnInventoryRecordTemplateVM, WmsItnInventoryRecord>
    {

    }

}
