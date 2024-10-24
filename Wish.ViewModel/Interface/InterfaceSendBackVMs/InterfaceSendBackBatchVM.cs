using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackBatchVM : BaseBatchVM<InterfaceSendBack, InterfaceSendBack_BatchEdit>
    {
        public InterfaceSendBackBatchVM()
        {
            ListVM = new InterfaceSendBackListVM();
            LinkedVM = new InterfaceSendBack_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InterfaceSendBack_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
