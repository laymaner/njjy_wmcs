using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class DoReceiptIqcRecordDto
    {
        /// <summary>
        /// 收货单明细ID
        /// </summary>
        public Int64 inReceiptDtlId { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal? passQty { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal? noPassQty { get; set; }

        /// <summary>
        /// 不良选项
        /// </summary>
        public string noPassItem { get; set; }

        /// <summary>
        /// 详细说明
        /// </summary>
        public string detailDescription { get; set; }

        /// <summary>
        /// 不良处理方式
        /// </summary>
        public string badOptions { get; set; }
        /// <summary>
        /// 回传免检标志
        /// </summary>
        public string IQCYOrN { get; set; } = "Y";

    }

    public class IQCHandleReturnDto
    {
        public List<string> receiptDtlIds { get; set; }
    }
}
