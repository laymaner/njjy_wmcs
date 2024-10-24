using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcDtlVMs
{
    public partial class WmsItnQcDtlSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String batchNo { get; set; }
        public String erpWhouseNo { get; set; }
        public Int32? inspectionResult { get; set; }
        public Int32? itnQcDtlStatus { get; set; }
        public String itnQcNo { get; set; }
        public String materialName { get; set; }
        public String materialCode { get; set; }
        public String proprietorCode { get; set; }
        public String unitCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
