using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessVMs
{
    public partial class CfgBusinessTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.businessCode);
        public ExcelPropety businessDesc_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.businessDesc);
        public ExcelPropety businessName_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.businessName);
        public ExcelPropety businessNameAlias_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.businessNameAlias);
        public ExcelPropety businessNameEn_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.businessNameEn);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgBusiness>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgBusinessImportVM : BaseImportVM<CfgBusinessTemplateVM, CfgBusiness>
    {

    }

}
