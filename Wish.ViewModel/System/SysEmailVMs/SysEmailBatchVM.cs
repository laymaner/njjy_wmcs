using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysEmailVMs
{
    public partial class SysEmailBatchVM : BaseBatchVM<SysEmail, SysEmail_BatchEdit>
    {
        public SysEmailBatchVM()
        {
            ListVM = new SysEmailListVM();
            LinkedVM = new SysEmail_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysEmail_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
