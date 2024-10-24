using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterVMs
{
    public partial class SysParameterBatchVM : BaseBatchVM<SysParameter, SysParameter_BatchEdit>
    {
        public SysParameterBatchVM()
        {
            ListVM = new SysParameterListVM();
            LinkedVM = new SysParameter_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysParameter_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
