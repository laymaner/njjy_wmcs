using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderHisVMs
{
    public partial class WmsInOrderHisSearcher : BaseSearcher
    {
        [Display(Name = "区域编码")]
        public String areaNo { get; set; }
        public String cvCode { get; set; }
        public String cvName { get; set; }
        [Display(Name = "客供类型")]
        public String cvType { get; set; }
        [Display(Name = "单据类型")]
        public String docTypeCode { get; set; }
        [Display(Name = "外部入库单号")]
        public String externalInNo { get; set; }
        [Display(Name = "入库单号")]
        public String inNo { get; set; }
        [Display(Name = "入库单状态")]
        public Int32? inStatus { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "数据来源")]
        public Int32? sourceBy { get; set; }
        public String ticketNo { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
