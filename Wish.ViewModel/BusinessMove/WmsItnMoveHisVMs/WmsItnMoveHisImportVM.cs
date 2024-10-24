using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveHisVMs
{
    public partial class WmsItnMoveHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.areaNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.docTypeCode);
        public ExcelPropety moveNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.moveNo);
        public ExcelPropety moveStatus_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.moveStatus);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.orderDesc);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.proprietorCode);
        public ExcelPropety putdownLocNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.putdownLocNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnMoveHisImportVM : BaseImportVM<WmsItnMoveHisTemplateVM, WmsItnMoveHis>
    {

    }

}
