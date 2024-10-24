using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcHisVMs
{
    public partial class WmsItnQcHisSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String docTypeCode { get; set; }
        public String erpWhouseNo { get; set; }
        public String itnQcLocNo { get; set; }
        public String itnQcNo { get; set; }
        public Int32? itnQcStatus { get; set; }
        public String proprietorCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
