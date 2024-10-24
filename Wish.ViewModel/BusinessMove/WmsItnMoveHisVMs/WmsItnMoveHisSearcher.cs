using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveHisVMs
{
    public partial class WmsItnMoveHisSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String docTypeCode { get; set; }
        public String moveNo { get; set; }
        public Int32? moveStatus { get; set; }
        public String proprietorCode { get; set; }
        public String putdownLocNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
