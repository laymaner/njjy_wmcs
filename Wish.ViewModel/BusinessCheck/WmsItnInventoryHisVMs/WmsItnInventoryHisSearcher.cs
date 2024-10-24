using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryHisVMs
{
    public partial class WmsItnInventoryHisSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public Int32? blindFlag { get; set; }
        public String docTypeCode { get; set; }
        public String erpWhouseNo { get; set; }
        public String inventoryLocNo { get; set; }
        public String inventoryNo { get; set; }
        public Int32? inventoryStatus { get; set; }
        public Int32? issuedFlag { get; set; }
        public String issuedOperator { get; set; }
        public DateRange issuedTime { get; set; }
        public String operationReason { get; set; }
        public String proprietorCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
