using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysDictionaryVMs
{
    public partial class SysDictionaryBatchVM : BaseBatchVM<SysDictionary, SysDictionary_BatchEdit>
    {
        public SysDictionaryBatchVM()
        {
            ListVM = new SysDictionaryListVM();
            LinkedVM = new SysDictionary_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SysDictionary_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
