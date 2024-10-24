using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlHisVMs
{
    public partial class WmsItnInventoryDtlHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.batchNo);
        public ExcelPropety confirmQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.confirmQty);
        public ExcelPropety difFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.difFlag);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.inspectionResult);
        public ExcelPropety inventoryDtlStatus_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.inventoryDtlStatus);
        public ExcelPropety inventoryNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.inventoryNo);
        public ExcelPropety inventoryQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.inventoryQty);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.materialCode);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.materialName);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.materialSpec);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.proprietorCode);
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.qty);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtlHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnInventoryDtlHisImportVM : BaseImportVM<WmsItnInventoryDtlHisTemplateVM, WmsItnInventoryDtlHis>
    {

    }

}
