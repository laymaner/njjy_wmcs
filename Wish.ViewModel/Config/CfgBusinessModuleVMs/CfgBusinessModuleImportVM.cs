using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessModuleVMs
{
    public partial class CfgBusinessModuleTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessCode);
        public ExcelPropety businessModuleCode_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleCode);
        public ExcelPropety businessModuleDesc_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleDesc);
        public ExcelPropety businessModuleName_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleName);
        public ExcelPropety businessModuleNameAlias_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleNameAlias);
        public ExcelPropety businessModuleNameEn_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleNameEn);
        public ExcelPropety businessModuleSort_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.businessModuleSort);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgBusinessModule>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgBusinessModuleImportVM : BaseImportVM<CfgBusinessModuleTemplateVM, CfgBusinessModule>
    {

    }

}
