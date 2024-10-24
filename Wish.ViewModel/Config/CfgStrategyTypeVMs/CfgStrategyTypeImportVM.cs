using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyTypeVMs
{
    public partial class CfgStrategyTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety strategyTypeCategory_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeCategory);
        public ExcelPropety strategyTypeCode_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeCode);
        public ExcelPropety strategyTypeDesription_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeDesription);
        public ExcelPropety strategyTypeName_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeName);
        public ExcelPropety strategyTypeNameAlias_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeNameAlias);
        public ExcelPropety strategyTypeNameEn_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.strategyTypeNameEn);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgStrategyType>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgStrategyTypeImportVM : BaseImportVM<CfgStrategyTypeTemplateVM, CfgStrategyType>
    {

    }

}
