using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceSearcher : BaseSearcher
    {
        public String cvCode { get; set; }
        [Display(Name = "客供名称")]
        public String cvName { get; set; }
        public String cvType { get; set; }
        [Display(Name = "单据类型")]
        public String docTypeCode { get; set; }
        [Display(Name = "外部出库单号")]
        public String externalOutNo { get; set; }
        [Display(Name = "成品编码")]
        public String fpNo { get; set; }
        public String inOutTypeNo { get; set; }
        [Display(Name = "发货单号")]
        public String invoiceNo { get; set; }
        [Display(Name = "发货单状态")]
        public Int32? invoiceStatus { get; set; }
        public String proprietorCode { get; set; }
        [Display(Name = "数据来源")]
        public Int32? sourceBy { get; set; }
        [Display(Name = "工单号")]
        public String ticketNo { get; set; }
        public String whouseNo { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        public String materialCode { get; set; }
        public String materialName { get; set; }
        public String waveNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
