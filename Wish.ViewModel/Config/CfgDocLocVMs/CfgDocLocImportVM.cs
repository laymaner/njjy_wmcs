using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocLocVMs
{
    public partial class CfgDocLocTemplateVM : BaseTemplateVM
    {
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<CfgDocLoc>(x => x.docTypeCode);
        public ExcelPropety locNo_Excel = ExcelPropety.CreateProperty<CfgDocLoc>(x => x.locNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<CfgDocLoc>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class CfgDocLocImportVM : BaseImportVM<CfgDocLocTemplateVM, CfgDocLoc>
    {

    }

}
