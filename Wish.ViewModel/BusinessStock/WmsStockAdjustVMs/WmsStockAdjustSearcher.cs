using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockAdjustVMs
{
    public partial class WmsStockAdjustSearcher : BaseSearcher
    {
        [Display(Name = "调整操作")]
        public String adjustOperate { get; set; }
        [Display(Name = "调整类型")]
        public String adjustType { get; set; }
        [Display(Name = "包装条码")]
        public String packageBarcode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "库存编码")]
        public String stockCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
