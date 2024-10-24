using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgErpWhouseVMs
{
    public partial class CfgErpWhouseTemplateVM : BaseTemplateVM
    {
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.erpWhouseNo);
        public ExcelPropety priority_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.priority);
        public ExcelPropety erpWhouseName_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.erpWhouseName);
        public ExcelPropety erpWhouseNameEn_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.erpWhouseNameEn);
        public ExcelPropety erpWhouseNameAlias_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.erpWhouseNameAlias);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgErpWhouse>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgErpWhouseImportVM : BaseImportVM<CfgErpWhouseTemplateVM, CfgErpWhouse>
    {

    }

}
