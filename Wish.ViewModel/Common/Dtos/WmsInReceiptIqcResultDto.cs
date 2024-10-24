using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class DoInReceiptIqcResultDto
    {
        /// <summary>
        /// 收货单明细ID
        /// </summary>
        public Int64 inReceiptDtID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? qty { get; set; }

        /// <summary>
        /// 质检结果
        /// </summary>
        public string qcResult { get; set; }
        /// <summary>
        /// 质检结果
        /// </summary>
        public string qcFlag { get; set; }
        /// <summary>
        /// 不良原因说明
        /// </summary>
        public string ngReason { get; set; }
        /// <summary>
        /// 回传免检标志
        /// </summary>
        public string IQCYOrN { get; set; } = "Y";
        /// <summary>
        /// 前端传参数
        /// </summary>
        public DoReceiptIqcRecordDto IQCParam { get; set; }
    }
}
