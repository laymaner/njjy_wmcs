using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class UniicodeDto
    {

        public string uniicode { get; set; }

        public string batchNo { get; set; }

        public decimal qty { get; set; }
    }
    /// <summary>
    /// 唯一码生成条件
    /// </summary>
    public class WmsInReceiptUniicodeGenerateDto
    {
        /// <summary>
        /// 入库单明细ID
        /// </summary>
        public Int64? inDtlID { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string materialCode { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string batchNo { set; get; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string supplierCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal qty { get; set; }

        /// <summary>
        /// 包装数量
        /// </summary>
        public decimal packageQty { get; set; }
    }

    public class WmsInReceiptUniicodeGenerateDtoForW
    {
        /// <summary>
        /// 入库单明细ID
        /// </summary>
        public string inDtlID { get; set; }

        /// <summary>
        /// 外部单行号
        /// </summary>
        public string ExternalID { get; set; }

        /// <summary>
        /// 外部单单号
        /// </summary>
        public string ExternalNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string materialCode { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string batchNo { set; get; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string supplierCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal qty { get; set; }

        /// <summary>
        /// 包装数量
        /// </summary>
        public decimal packageQty { get; set; }
    }
}
