using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.System.Model;

namespace Wish.ViewModel.System.SysSequenceVMs
{
    public partial class SysSequenceBatchVM : BaseBatchVM<SysSequence, SysSequence_BatchEdit>
    {
        public SysSequenceBatchVM()
        {
            ListVM = new SysSequenceListVM();
            LinkedVM = new SysSequence_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysSequence_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
