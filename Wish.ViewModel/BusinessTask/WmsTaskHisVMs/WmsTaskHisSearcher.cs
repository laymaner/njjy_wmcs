using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessTask.WmsTaskHisVMs
{
    public partial class WmsTaskHisSearcher : BaseSearcher
    {
        public Int32? feedbackStatus { get; set; }
        public Int32? taskStatus { get; set; }
        public String frLocationNo { get; set; }
        public Int32? frLocationType { get; set; }
        public Int32? loadedType { get; set; }
        public String orderNo { get; set; }
        public String palletBarcode { get; set; }
        public String proprietorCode { get; set; }
        public String regionNo { get; set; }
        public String roadwayNo { get; set; }
        public String stockCode { get; set; }
        public String taskTypeNo { get; set; }
        public String toLocationNo { get; set; }
        public Int32? toLocationType { get; set; }
        public String whouseNo { get; set; }
        public String wmsTaskNo { get; set; }
        public String wmsTaskType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
