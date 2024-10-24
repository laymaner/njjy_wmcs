using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish.Areas.BasWhouse.Model;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;

namespace Wish.ViewModel.Common.Dtos
{
    public class MergeAllotDto
    {
        public string projectNo { get; set; }

        public string batchNo { get; set; }

        public string id { get; set; }


        public string materialCode { get; set; }

        public string erpWhouseNo { get; set; }

        public decimal totalQty { get; set; }

        public List<string> invoiceDtlIds { get; set; }

    }
    public class outInvoiceAllotReturnDto
    {
        public string projectNo { get; set; }

        public string batchNo { get; set; }



        public string materialCode { get; set; }

        public string erpWhouseNo { get; set; }

        public decimal totalQty { get; set; }

        public Int64? invoiceDtlId { get; set; }

        public List<allotStockDto> allotStocks = new List<allotStockDto>();


    }

    public class allotStockDto
    {
        public string pickTaskNo { get; set; }
        public Int64? stockDtlId { get; set; }

        public string stockCode { get; set; }

        public decimal allotQty { get; set; }
        /// <summary>
        /// 物料整出还是拣选
        /// </summary>
        public string isPick { get; set; } = "1";

    }

    public class allotMatDto
    {
        public string materialCode { get; set; }
        public bool isElecFlag { get; set; } = false;

        public bool isProductFlag { get; set; } = false;

    }

    public class PalletAllotDto
    {
        public string allotType { get; set; }

        public bool isElecFlag { get; set; }

        public List<WmsOutInvoice> outInvoices { get; set; }

        public List<WmsOutInvoiceDtl> outInvoiceDtls { get; set; }

        public List<BasWRegion> regionInfos { get; set; }

        public List<WmsStockAllovateReturn> allocateResultList { get; set; }

        public string invoker { get; set; }
    }

    public class allotSnDto

    {
        /// <summary>
        /// originalSn源sn
        /// </summary>
        public string orginSn { get; set; }

        public string materialCode { get; set; }

        public List<string> snList { get; set; } = new List<string>();

    }

    public class allotUniiDto
    {
        public Int64 uniicodeId { get; set; }
        public string uniicode { get; set; }

        public decimal allotQty { get; set; }
    }

    public class AutoAllocateParam
    {
        /// <summary>
        /// 出库单号
        /// </summary>
        public string inVoiceNo { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string areaNo { get; set; }
        /// <summary>
        /// 库区
        /// </summary>
        public List<string> regionNos { get; set; } = new List<string>();

        public Dictionary<string, string> dictTaskNo = new Dictionary<string, string>();
    }
    public class BatchConfirmout
    {
        public string areaNo { get; set; }
        public List<string> inVoiceNo { get; set; }
        /// <summary>
        /// 库区
        /// </summary>
        public List<string> regionNos { get; set; } = new List<string>();
    }

    public class ManuAllocateParam
    {
        /// <summary>
        /// 出库单明细ID
        /// </summary>
        public string inVoiceDtlId { get; set; }

        /// <summary>
        /// 手动分配类型
        /// </summary>
        public OutInvoiceManuAllocateType allocateType { get; set; }

        /// <summary>
        /// 分配库存列表
        /// </summary>
        public List<StockAllocateInfo> stockAllocateInfos { get; set; }
    }

    public class StockAllocateInfo
    {
        /// <summary>
        /// 库存明细ID/包装条码
        /// </summary>
        public string stockDtlId_Uniicode { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal qty { get; set; }

        /// <summary>
        /// 是否拣选
        /// </summary>
        public bool isPick { get; set; }
    }


    public class WaveAllocateDto
    {
        /// <summary>
        /// 波次号
        /// </summary>
        public string waveNo { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string areaNo { get; set; }
        /// <summary>
        /// 库区编号
        /// </summary>
        public List<string> regionNos { get; set; } = new List<string>();
    }

    public class ManualAllotInDto
    {
        public Int64? ID { get; set; }
        public string allotType { get; set; }
        public List<ManualAllocateDto> StockDtl { get; set; } = new List<ManualAllocateDto> { };
    }

    public class ManualAllocateDto
    {
        public Int64? ID { get; set; }
        public string uniicode { get; set; }
        public decimal qty { get; set; }
        public bool isPick { get; set; }
    }

    public class PutdownDto
    {
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }
        /// <summary>
        /// 站台编号
        /// </summary>
        public string locNo { get; set; }

        public string invoker { get; set; }
    }

}
