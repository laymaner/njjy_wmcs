using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlVMs
{
    public partial class WmsItnInventoryDtlSearcher : BaseSearcher
    {
        [Display(Name = "批次号")]
        public String batchNo { get; set; }
        [Display(Name = "是否差异")]
        public Int32? difFlag { get; set; }
        public Int32? inspectionResult { get; set; }
        [Display(Name = "盘点明细状态")]
        public Int32? inventoryDtlStatus { get; set; }
        [Display(Name = "盘点单号")]
        public String inventoryNo { get; set; }
        [Display(Name = "盘点数量")]
        public Decimal? inventoryQty { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "物料名称")]
        public String materialName { get; set; }
        [Display(Name = "物料规格")]
        public String materialSpec { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "计划数量")]
        public Decimal? qty { get; set; }
        [Display(Name = "单位")]
        public String unitCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
