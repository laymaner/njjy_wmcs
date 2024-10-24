using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryHisVMs
{
    public partial class WmsItnInventoryHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.areaNo);
        public ExcelPropety blindFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.blindFlag);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.docTypeCode);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.erpWhouseNo);
        public ExcelPropety inventoryLocNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.inventoryLocNo);
        public ExcelPropety inventoryNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.inventoryNo);
        public ExcelPropety inventoryStatus_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.inventoryStatus);
        public ExcelPropety issuedFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.issuedFlag);
        public ExcelPropety issuedOperator_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.issuedOperator);
        public ExcelPropety issuedTime_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.issuedTime);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.operationReason);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.orderDesc);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.proprietorCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnInventoryHisImportVM : BaseImportVM<WmsItnInventoryHisTemplateVM, WmsItnInventoryHis>
    {

    }

}
