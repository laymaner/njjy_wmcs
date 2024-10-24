using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.DevConfig;


namespace Wish.ViewModel.DevConfig.MotDevPartsHisVMs
{
    public partial class MotDevPartsHisBatchVM : BaseBatchVM<MotDevPartsHis, MotDevPartsHis_BatchEdit>
    {
        public MotDevPartsHisBatchVM()
        {
            ListVM = new MotDevPartsHisListVM();
            LinkedVM = new MotDevPartsHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class MotDevPartsHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
