using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveDtlHisVMs
{
    public partial class WmsItnMoveDtlHisSearcher : BaseSearcher
    {
        public String whouseNo { get; set; }
        public String proprietorCode { get; set; }
        public String areaNo { get; set; }
        public String moveNo { get; set; }
        public String regionNo { get; set; }
        public String roadwayNo { get; set; }
        public String binNo { get; set; }
        public String stockCode { get; set; }
        public String palletBarcode { get; set; }
        public Int32? moveDtlStatus { get; set; }
        public Int32? loadedType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
