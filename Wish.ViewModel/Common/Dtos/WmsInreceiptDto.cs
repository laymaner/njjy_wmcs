using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.DtoModel.Common.Dtos
{
    public class WmsInReceiptInParamDto
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        public string inNo { get; set; }

        /// <summary>
        /// 入库单明细
        /// </summary>
        public List<WmsInReceiptInParamInDtlnfoDto> inDtlsInfos { get; set; } = new List<WmsInReceiptInParamInDtlnfoDto>();


    }

    public class WmsInReceiptInParamInDtlnfoDto
    {
        /// <summary>
        /// 入库单明细ID
        /// </summary>
        public Int64 inDtlId { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string batchNo { get; set; }

        /// <summary>
        /// 唯一码
        /// </summary>
        public string uniicode { get; set; }

        /// <summary>
        /// 收货数量
        /// </summary>
        public decimal receiptQty { get; set; }
    }

    public class WmsInReceiptDto
    {
        public WmsInReceipt wmsInReceipt { get; set; }

        public List<WmsInReceiptDtl> wmsInReceiptDts { get; set; }
    }
}
