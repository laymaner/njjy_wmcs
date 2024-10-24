using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyDtlVMs
{
    public partial class CfgStrategyDtlTemplateVM : BaseTemplateVM
    {
        public ExcelPropety strategyItemIdx_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyItemIdx);
        public ExcelPropety strategyItemNo_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyItemNo);
        public ExcelPropety strategyItemValue1_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyItemValue1);
        public ExcelPropety strategyItemValue2_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyItemValue2);
        public ExcelPropety strategyNo_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyNo);
        public ExcelPropety strategyTypeCode_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.strategyTypeCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgStrategyDtl>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgStrategyDtlImportVM : BaseImportVM<CfgStrategyDtlTemplateVM, CfgStrategyDtl>
    {

    }

}
