using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterValueVMs
{
    public partial class SysParameterValueTemplateVM : BaseTemplateVM
    {
        public ExcelPropety parCode_Excel = ExcelPropety.CreateProperty<SysParameterValue>(x => x.parCode);
        public ExcelPropety parValueDesc_Excel = ExcelPropety.CreateProperty<SysParameterValue>(x => x.parValueDesc);
        public ExcelPropety parValueDescAlias_Excel = ExcelPropety.CreateProperty<SysParameterValue>(x => x.parValueDescAlias);
        public ExcelPropety parValueDescEn_Excel = ExcelPropety.CreateProperty<SysParameterValue>(x => x.parValueDescEn);
        public ExcelPropety parValueNo_Excel = ExcelPropety.CreateProperty<SysParameterValue>(x => x.parValueNo);

	    protected override void InitVM()
        {
        }

    }

    public class SysParameterValueImportVM : BaseImportVM<SysParameterValueTemplateVM, SysParameterValue>
    {

    }

}
