using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.Base;


namespace Wish.ViewModel.Base.BasBDepartmentVMs
{
    public partial class BasBDepartmentTemplateVM : BaseTemplateVM
    {
        public ExcelPropety DepartmentCode_Excel = ExcelPropety.CreateProperty<BasBDepartment>(x => x.DepartmentCode);
        public ExcelPropety DepartmentName_Excel = ExcelPropety.CreateProperty<BasBDepartment>(x => x.DepartmentName);
        public ExcelPropety DepartmentNameAlias_Excel = ExcelPropety.CreateProperty<BasBDepartment>(x => x.DepartmentNameAlias);
        public ExcelPropety DepartmentNameEn_Excel = ExcelPropety.CreateProperty<BasBDepartment>(x => x.DepartmentNameEn);
        public ExcelPropety UsedFlag_Excel = ExcelPropety.CreateProperty<BasBDepartment>(x => x.UsedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class BasBDepartmentImportVM : BaseImportVM<BasBDepartmentTemplateVM, BasBDepartment>
    {

    }

}
