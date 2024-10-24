using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBProprietorVMs
{
    public partial class BasBProprietorBatchVM : BaseBatchVM<BasBProprietor, BasBProprietor_BatchEdit>
    {
        public BasBProprietorBatchVM()
        {
            ListVM = new BasBProprietorListVM();
            LinkedVM = new BasBProprietor_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBProprietor_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
