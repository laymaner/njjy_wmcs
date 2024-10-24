using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;


namespace Wish.ViewModel.WcsCmd.SrmCmdVMs
{
    public partial class SrmCmdBatchVM : BaseBatchVM<SrmCmd, SrmCmd_BatchEdit>
    {
        public SrmCmdBatchVM()
        {
            ListVM = new SrmCmdListVM();
            LinkedVM = new SrmCmd_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SrmCmd_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
