using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceSendBackHisVMs
{
    public partial class InterfaceSendBackHisBatchVM : BaseBatchVM<InterfaceSendBackHis, InterfaceSendBackHis_BatchEdit>
    {
        public InterfaceSendBackHisBatchVM()
        {
            ListVM = new InterfaceSendBackHisListVM();
            LinkedVM = new InterfaceSendBackHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InterfaceSendBackHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
