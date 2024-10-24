using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyItemVMs
{
    public partial class CfgStrategyItemTemplateVM : BaseTemplateVM
    {
        public ExcelPropety strategyItemDesc_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemDesc);
        public ExcelPropety strategyItemGroupIdx_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemGroupIdx);
        public ExcelPropety strategyItemGroupNo_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemGroupNo);
        public ExcelPropety strategyItemName_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemName);
        public ExcelPropety strategyItemNameAlias_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemNameAlias);
        public ExcelPropety strategyItemNameEn_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemNameEn);
        public ExcelPropety strategyItemNo_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyItemNo);
        public ExcelPropety strategyTypeCode_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.strategyTypeCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgStrategyItem>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgStrategyItemImportVM : BaseImportVM<CfgStrategyItemTemplateVM, CfgStrategyItem>
    {

    }

}
