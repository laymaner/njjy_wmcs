using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class BinDto
    {
        public string binNo { get; set; }
        public string extensionGroupNo { get; set; }

        public string roadwayNo { get; set; }

        public string regionNo { get; set; }
    }
    public class WmsStockDto
    {
        public WmsStock wmsStock { get; set; }

        public List<WmsStockDtl> wmsStockDtls { get; set; }
    }
    /// <summary>
    /// 多物料库存
    /// </summary>
    public class WmsStockAllocateForMultiMatDto
    {
        /// <summary>
        /// 项目号
        /// </summary>
        public List<string> projectNoList { get; set; }

        public List<string> supplyErpBinList { get; set; }

        public string isDesignateErpWhouse { get; set; }
        /// <summary>
        /// 是否卡控供应商库位
        /// </summary>
        public bool isLimitSupplyBin { get; set; }

        /// <summary>
        /// 单据类型    
        /// </summary>
        public string docTypeCode { get; set; }
        /// <summary>
        /// 库区
        /// </summary>
        public List<string> regionNos { get; set; } = new List<string>();
        public List<string> materialCodeList { get; set; }

        public List<string> erpWhouseList { get; set; }

        public List<WmsStockAllocateDto> singleMatAllot { get; set; } = new List<WmsStockAllocateDto>();

    }

    public class supplyBinDto

    {
        public string supplierCode { get; set; }

        public List<string> erpBinList { get; set; } = new List<string>();
    }

    public class EmptyStockAllotDto
    {
        public string palletTypeCode { get; set; }

        public decimal? qty { get; set; }
        /// <summary>
        /// 0：自动分配；1手动分配
        /// </summary>
        public int allotType { get; set; }

        public List<string> palletBarcodeList { get; set; } = new List<string>();

        public string roadwayNos { get; set; }

        public string locNo { get; set; }

        public string invoker { get; set; }
    }

    public class AllotStockandBinDto
    {
        public string docTypeCode { get; set; }

        public int docPriority { get; set; }

        public string businessCode { get; set; }
        public string stockCode { get; set; }

        public string palletBarcode { get; set; }

        public int loadedType { get; set; }

        public string binNo { get; set; }

        /// <summary>
        /// 伸位组
        /// </summary>
        public string extensionGroupNo { get; set; }

        /// <summary>
        /// 伸位组内序号：靠近堆垛机为1，越远数字越大
        /// </summary>
        public int? extensionIdx { get; set; }

        public int? binPriority { get; set; } = 1;

        public string roadwayNo { get; set; }


        public string regionNo { get; set; }
        /// <summary>
        /// 0：自动分配；1手动分配
        /// </summary>
        public int allotType { get; set; }
        public string locNo { get; set; }
        public string invoker { get; set; }

    }


    /// <summary>
    /// 分配库存-平库 入参
    /// </summary>
    public class WmsStockAllocateDto
    {
        //public string supplierCode { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string projectNo { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string materialCode { get; set; }

        public List<string> supBinStockList { get; set; } = new List<string> { };
        /// <summary>
        /// 电子料标识
        /// </summary>
        public bool isElecFlag { get; set; } = false;

        public List<string> supplyBinStockList { get; set; } = new List<string>();

        /// <summary>
        /// originalSn源sn
        /// </summary>
        public string orginSn { get; set; }

        public List<Int64> snStockDtlIdList { get; set; } = new List<Int64>();

        public List<Int64> elecStockDtlIdList { get; set; } = new List<Int64>();

        /// <summary>
        /// 批次号
        /// </summary>
        public string batchNo { get; set; }

        /// <summary>
        /// 区域编号(楼号)
        /// </summary>
        public string areaNo { get; set; }

        /// <summary>
        /// ERP仓库号
        /// </summary>
        public string erpWhouseNo { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string supplierCode { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string docTypeCode { get; set; }
        /// <summary>
        /// 是否指定erp仓库
        /// </summary>

        public string isDesignateErpWhouse { get; set; }
        /// <summary>
        /// 是否卡控供应商库位
        /// </summary>
        public bool isLimitSupplyBin { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal? qty { get; set; }

        public string belongDepartment { get; set; }

        public List<string> palletBarcodeList { get; set; } = new List<string>();

        public List<string> roadwayList { get; set; } = new List<string>();
        /// <summary>
        /// 0:自动分配，1：手动分配
        /// </summary>
        public int allotType { get; set; } = 0;
    }
    /// <summary>
    /// sn分配
    /// </summary>
    public class WmsStockAllovateSnReturn
    {
        public string orginalSn { get; set; }

        public List<WmsStockAllovateSubSnReturn> subSnList { get; set; } = new List<WmsStockAllovateSubSnReturn>();
    }
    public class WmsStockAllovateSubSnReturn
    {

        /// <summary>
        /// sn即唯一码
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 库存明细id--非电子料(对应的是库存明细)
        /// </summary>
        public Int64? stockDtlId { get; set; }
    }


    public class WmsStockAllovateElecReturn
    {
        /// <summary>
        /// sn即唯一码
        /// </summary>
        public string materialCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string batchNo { get; set; }
        /// <summary>
        /// 库存明细id--电子料(对应的是库存明细)
        /// </summary>
        public List<Int64> stockDtlIdList { get; set; }
    }

    /// <summary>
    /// 分配库存-平库 出参
    /// </summary>
    public class WmsStockAllovateReturn
    {
        /// <summary>
        /// 库存明细id--非电子料(对应的是库存明细)
        /// </summary>
        public Int64? stockDtlId { get; set; }

        /// <summary>
        /// 库存编码
        /// </summary>
        public string stockCode { get; set; }

        public string batchNo { get; set; }

        /// <summary>
        /// sn即唯一码
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 数量 = 包装条码数量/库存明细分配数量
        /// </summary>
        /// <returns></returns>
        public decimal? qty { get; set; }

        /// <summary>
        /// 是否需要拣选标记(true：需要拣选, false：不需要拣选(其实就是类似整出))
        /// </summary>
        public bool isNeedPick { get; set; }

        /// <summary>
        /// 拣选任务号
        /// </summary>
        public string pickTaskNo { get; set; }


        public List<StockUniicode> uniicodes;

        public decimal allotQty { get; set; } = 0;
    }

    public class StockUniicode
    {
        /// <summary>
        /// 包装条码--电子料(对应的是库存唯一码)
        /// </summary>
        public string uniicode { get; set; }

        /// <summary>
        /// 数量 = 包装条码数量（这个里面有可能存在拣选数量）
        /// </summary>
        /// <returns></returns>
        public decimal? uniicodeQty { get; set; }

        /// <summary>
        /// 是否需要拣选标记(true：需要拣选, false：不需要拣选(其实就是类似整出))
        /// </summary>
        public bool isNeedPick { get; set; }
    }

    /// <summary>
    /// 推荐拣选条码入参
    /// </summary>
    public class RecommendStockDtl
    {
        public string stockDtlId { get; set; }
    }

    /// <summary>
    /// 推荐拣选条码出参
    /// </summary>
    public class RecommendUniicodes
    {
        public string stockDtlId { get; set; }
        public string uniicode { get; set; }
        public string binNo { get; set; }
        public DateTime inwhTime { get; set; }
    }


    public class WmsStockAllocateForHandDto
    {
        public string batchNo { get; set; }

        public string materialCode { get; set; }

        public string docBatchNo { get; set; }

        public string projectNo { get; set; }

        public string erpWhouseNo { get; set; }

        public string docTypeCode { get; set; }

        public string isDesignateErpWhouse { get; set; }

        public string belongDepartment { get; set; }

        public bool isProductFlag { get; set; }

        public bool isElecFlag { get; set; }

        public List<string> snList { get; set; } = new List<string>();

        public string uniicode { get; set; }

        public List<string> stockCodeList { get; set; } = new List<string>();
    }

    public class erpWhousePrirityDto
    {
        public string erpWhouseNo { get; set; }

        public int priority { get; set; } = 100;
    }

    public class allotStockDtlDto
    {
        public Int64? stockDtlId { get; set; }

        public string pickTaskNo { get; set; }

        public decimal allotQty { get; set; }

        public List<allotStockUniiDto> allotStockUniiList = new List<allotStockUniiDto>();
    }

    public class allotStockUniiDto
    {
        public Int64? stockDtlId { get; set; }

        public Int64? uniiId { get; set; }
        public string uniicode { get; set; }

        public decimal allotQty { get; set; }

        public bool isCanAllot { get; set; } = false;
    }
    public class GetStockByAllotDto
    {

        public Int64 invoiceDtlId { get; set; }

        //public string supplyCode { get; set; }

        //public string docTypeCode { get; set; }

        public string erpWhouseNo { get; set; }
        public string materialCode { get; set; }
        public string batchNo { get; set; }
        public string allotType { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string palletBarcode { get; set; }
        public string uniicode { get; set; }


    }
}
