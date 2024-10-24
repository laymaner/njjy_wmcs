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
    public partial class BasBDepartmentBatchVM : BaseBatchVM<BasBDepartment, BasBDepartment_BatchEdit>
    {
        public BasBDepartmentBatchVM()
        {
            ListVM = new BasBDepartmentListVM();
            LinkedVM = new BasBDepartment_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBDepartment_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
