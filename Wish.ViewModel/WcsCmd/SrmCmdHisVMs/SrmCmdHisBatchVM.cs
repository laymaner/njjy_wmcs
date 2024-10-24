using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;


namespace Wish.ViewModel.WcsCmd.SrmCmdHisVMs
{
    public partial class SrmCmdHisBatchVM : BaseBatchVM<SrmCmdHis, SrmCmdHis_BatchEdit>
    {
        public SrmCmdHisBatchVM()
        {
            ListVM = new SrmCmdHisListVM();
            LinkedVM = new SrmCmdHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SrmCmdHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
