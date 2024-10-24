using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSkuVMs
{
    public partial class BasBSkuTemplateVM : BaseTemplateVM
    {
        public ExcelPropety pickRuleNo_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.pickRuleNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.proprietorCode);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.skuCode);
        public ExcelPropety skuRulesNo_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.skuRulesNo);
        public ExcelPropety storageRuleNo_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.storageRuleNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBSku>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasBSkuImportVM : BaseImportVM<BasBSkuTemplateVM, BasBSku>
    {

    }

}
