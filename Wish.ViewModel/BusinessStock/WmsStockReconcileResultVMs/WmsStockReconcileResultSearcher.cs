using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockReconcileResultVMs
{
    public partial class WmsStockReconcileResultSearcher : BaseSearcher
    {
        public Decimal? differQty { get; set; }
        public String erpStockNo { get; set; }
        public Decimal? erpStockQty { get; set; }
        public String erpWhouseNo { get; set; }
        public String materialCategoryCode { get; set; }
        public String materialCode { get; set; }
        public String materialTypeCode { get; set; }
        public String proprietorCode { get; set; }
        public String reconcileNo { get; set; }
        public String whouseNo { get; set; }
        public Decimal? wmsStockQty { get; set; }

        protected override void InitVM()
        {
        }

    }
}
