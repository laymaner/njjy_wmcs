using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamVMs
{
    public partial class CfgBusinessParamTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.businessCode);
        public ExcelPropety businessModuleCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.businessModuleCode);
        public ExcelPropety checkFlag_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.checkFlag);
        public ExcelPropety paramCode_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramCode);
        public ExcelPropety paramDesc_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramDesc);
        public ExcelPropety paramName_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramName);
        public ExcelPropety paramNameAlias_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramNameAlias);
        public ExcelPropety paramNameEn_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramNameEn);
        public ExcelPropety paramSort_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.paramSort);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgBusinessParam>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgBusinessParamImportVM : BaseImportVM<CfgBusinessParamTemplateVM, CfgBusinessParam>
    {

    }

}
