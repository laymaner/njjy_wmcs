using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeDtlVMs
{
    public partial class CfgDocTypeDtlTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.businessCode);
        public ExcelPropety businessModuleCode_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.businessModuleCode);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.docTypeCode);
        public ExcelPropety paramCode_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.paramCode);
        public ExcelPropety paramValueCode_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.paramValueCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<CfgDocTypeDtl>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class CfgDocTypeDtlImportVM : BaseImportVM<CfgDocTypeDtlTemplateVM, CfgDocTypeDtl>
    {

    }

}
