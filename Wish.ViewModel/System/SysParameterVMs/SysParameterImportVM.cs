using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterVMs
{
    public partial class SysParameterTemplateVM : BaseTemplateVM
    {
        public ExcelPropety developFlag_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.developFlag);
        public ExcelPropety parCode_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parCode);
        public ExcelPropety parDesc_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parDesc);
        public ExcelPropety parDescAlias_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parDescAlias);
        public ExcelPropety parDescEn_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parDescEn);
        public ExcelPropety parValue_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parValue);
        public ExcelPropety parValueType_Excel = ExcelPropety.CreateProperty<SysParameter>(x => x.parValueType);

	    protected override void InitVM()
        {
        }

    }

    public class SysParameterImportVM : BaseImportVM<SysParameterTemplateVM, SysParameter>
    {

    }

}
