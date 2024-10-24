using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutWaveHisVMs
{
    public partial class WmsOutWaveHisSearcher : BaseSearcher
    {
        public String allotOperator { get; set; }
        public String deliveryLocNo { get; set; }
        public String docTypeCode { get; set; }
        public String issuedResult { get; set; }
        public String operationReason { get; set; }
        public String proprietorCode { get; set; }
        public String waveNo { get; set; }
        public Int32? waveStatus { get; set; }
        public Int32? waveType { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
