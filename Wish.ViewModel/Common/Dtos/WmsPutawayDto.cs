using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    /// <summary>
    /// 组盘上架参数
    /// </summary>
    public class DoPutawayInParamsDto
    {
        /// <summary>
        /// 质检结果单号
        /// </summary>
        public string iqcResultNo { get; set; }

        /// <summary>
        /// 载体条码/库位条码
        /// </summary>
        public string barCode { get; set; }

        /// <summary>
        /// 平库建议库位
        /// </summary>
        public string suggestBin { get; set; }

        /// <summary>
        /// 唯一码 & 数量关系
        /// </summary>
        public Dictionary<string, decimal> uniicodes { get; set; } = new Dictionary<string, decimal>();
    }
    public class DoSimpleDto
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        public string inNo { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string barCode { set; get; }
    }
    public class GroupDiskRevokeDto
    {
        /// <summary>
        /// 质检结果单号
        /// </summary>
        public string iqcResultNo { get; set; }

        /// <summary>
        /// 载体条码/库位条码
        /// </summary>
        public string barCode { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string invoker { get; set; }

        /// <summary>
        /// 唯一码 & 数量关系
        /// </summary>
        public Dictionary<string, decimal> uniicodes { get; set; } = new Dictionary<string, decimal>();
    }
    public class PutAwayOnlineDto
    {
        public string palletBarcode { get; set; }
        public string locNo { get; set; }
    }

    public class PutAwayDto
    {
        public string palletBarcode { get; set; }
        public string binNo { get; set; }
    }

    public class PutAwayByDtlDto
    {
        public string palletBarcode { get; set; }
        public string binNo { get; set; }
        public long putawayDtlId { get; set; }
    }
}
