using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysEmailVMs
{
    public partial class SysEmailTemplateVM : BaseTemplateVM
    {
        public ExcelPropety alertType_Excel = ExcelPropety.CreateProperty<SysEmail>(x => x.alertType);
        public ExcelPropety email_Excel = ExcelPropety.CreateProperty<SysEmail>(x => x.email);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<SysEmail>(x => x.usedFlag);
        public ExcelPropety userName_Excel = ExcelPropety.CreateProperty<SysEmail>(x => x.userName);

	    protected override void InitVM()
        {
        }

    }

    public class SysEmailImportVM : BaseImportVM<SysEmailTemplateVM, SysEmail>
    {

    }

}
