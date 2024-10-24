using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysParameterValueVMs
{
    public partial class SysParameterValueBatchVM : BaseBatchVM<SysParameterValue, SysParameterValue_BatchEdit>
    {
        public SysParameterValueBatchVM()
        {
            ListVM = new SysParameterValueListVM();
            LinkedVM = new SysParameterValue_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysParameterValue_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
