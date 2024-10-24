using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseVMs
{
    public partial class BasWErpWhouseBatchVM : BaseBatchVM<BasWErpWhouse, BasWErpWhouse_BatchEdit>
    {
        public BasWErpWhouseBatchVM()
        {
            ListVM = new BasWErpWhouseListVM();
            LinkedVM = new BasWErpWhouse_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWErpWhouse_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
