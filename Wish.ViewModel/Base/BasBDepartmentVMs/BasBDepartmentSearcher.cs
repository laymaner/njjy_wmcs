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
    public partial class BasBDepartmentSearcher : BaseSearcher
    {
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public Int32? UsedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
