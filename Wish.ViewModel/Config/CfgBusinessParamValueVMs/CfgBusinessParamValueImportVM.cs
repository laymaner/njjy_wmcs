using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamValueVMs
{
    public partial class CfgBusinessParamValueTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.businessCode);
        public ExcelPropety businessModuleCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.businessModuleCode);
        public ExcelPropety defaultFlag_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.defaultFlag);
        public ExcelPropety paramCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramCode);
        public ExcelPropety paramValueCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramValueCode);
        public ExcelPropety paramValueDesc_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramValueDesc);
        public ExcelPropety paramValueName_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramValueName);
        public ExcelPropety paramValueNameAlias_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramValueNameAlias);
        public ExcelPropety paramValueNameEn_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.paramValueNameEn);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgBusinessParamValue>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgBusinessParamValueImportVM : BaseImportVM<CfgBusinessParamValueTemplateVM, CfgBusinessParamValue>
    {

    }

}
