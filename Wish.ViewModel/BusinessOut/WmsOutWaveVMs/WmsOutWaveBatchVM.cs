using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutWaveVMs
{
    public partial class WmsOutWaveBatchVM : BaseBatchVM<WmsOutWave, WmsOutWave_BatchEdit>
    {
        public WmsOutWaveBatchVM()
        {
            ListVM = new WmsOutWaveListVM();
            LinkedVM = new WmsOutWave_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutWave_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
