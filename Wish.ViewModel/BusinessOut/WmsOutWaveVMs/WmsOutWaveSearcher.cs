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
    public partial class WmsOutWaveSearcher : BaseSearcher
    {
        [Display(Name = "分配操作人")]
        public String allotOperator { get; set; }
        public String deliveryLocNo { get; set; }
        [Display(Name = "单据类型")]
        public String docTypeCode { get; set; }
        [Display(Name = "波次号")]
        public String waveNo { get; set; }
        [Display(Name = "波次单状态")]
        public Int32? waveStatus { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
