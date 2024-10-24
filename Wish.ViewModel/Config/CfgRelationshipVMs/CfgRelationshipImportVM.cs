using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipVMs
{
    public partial class CfgRelationshipTemplateVM : BaseTemplateVM
    {
        public ExcelPropety leftCode_Excel = ExcelPropety.CreateProperty<CfgRelationship>(x => x.leftCode);
        public ExcelPropety priority_Excel = ExcelPropety.CreateProperty<CfgRelationship>(x => x.priority);
        public ExcelPropety relationshipTypeCode_Excel = ExcelPropety.CreateProperty<CfgRelationship>(x => x.relationshipTypeCode);
        public ExcelPropety rightCode_Excel = ExcelPropety.CreateProperty<CfgRelationship>(x => x.rightCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<CfgRelationship>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class CfgRelationshipImportVM : BaseImportVM<CfgRelationshipTemplateVM, CfgRelationship>
    {

    }

}
