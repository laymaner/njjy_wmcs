using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlHisVMs
{
    public partial class WmsItnInventoryDtlHisSearcher : BaseSearcher
    {
        public String batchNo { get; set; }
        public Decimal? confirmQty { get; set; }
        public Int32? inspectionResult { get; set; }
        public Int32? inventoryDtlStatus { get; set; }
        public String inventoryNo { get; set; }
        public String materialCode { get; set; }
        public String materialName { get; set; }
        public String materialSpec { get; set; }
        public String proprietorCode { get; set; }
        public String unitCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
