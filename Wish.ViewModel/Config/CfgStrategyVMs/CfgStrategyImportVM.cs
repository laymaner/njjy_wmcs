using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyVMs
{
    public partial class CfgStrategyTemplateVM : BaseTemplateVM
    {
        public ExcelPropety strategyDesc_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyDesc);
        public ExcelPropety strategyName_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyName);
        public ExcelPropety strategyNameAlias_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyNameAlias);
        public ExcelPropety strategyNameEn_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyNameEn);
        public ExcelPropety strategyNo_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyNo);
        public ExcelPropety strategyTypeCode_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.strategyTypeCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgStrategy>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgStrategyImportVM : BaseImportVM<CfgStrategyTemplateVM, CfgStrategy>
    {

    }

}
