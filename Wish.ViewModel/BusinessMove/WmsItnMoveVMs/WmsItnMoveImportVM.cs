using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveVMs
{
    public partial class WmsItnMoveTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.areaNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.docTypeCode);
        public ExcelPropety moveNo_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.moveNo);
        public ExcelPropety moveStatus_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.moveStatus);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.orderDesc);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.proprietorCode);
        public ExcelPropety putdownLocNo_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.putdownLocNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnMove>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnMoveImportVM : BaseImportVM<WmsItnMoveTemplateVM, WmsItnMove>
    {

    }

}
