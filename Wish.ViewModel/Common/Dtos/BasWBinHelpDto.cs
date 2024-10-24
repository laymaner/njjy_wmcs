using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class AllotBinForMatDto
    {

        public int loadedType { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }
        public string erpWhouseNo { get; set; }

        public string areaNo { get; set; }
        public string materialCode { get; set; }

        public string skuCode { get; set; }

        public string batchNo { get; set; }

        public List<string> roadwayList { get; set; } = new List<string>();

        public string regionNo { get; set; }

        public string sdType { get; set; }

        public int? height { get; set; }
        public int? layer { get; set; }
        /// <summary>
        /// 是否移库
        /// </summary>
        public bool isMove { get; set; } = false;

    }
    public class CanUseBinForRoadwayDto
    {
        public string roadwayNo { get; set; }

        public int emptyBinCount { get; set; }

        public int taskCount { get; set; }

        public List<CanUseBinDto> canUseBinList { get; set; }

        public decimal rate { get; set; }

    }


    public class CanUseBinDto
    {
        public string binNo { get; set; }


        public string erpWhouseNo { get; set; }
        public string roadwayNo { get; set; }


        public string regionNo { get; set; }

        public string areaNo { get; set; }

        /// <summary>
        /// 库位组内序号
        /// </summary>
        public int? binGroupIdx { get; set; }

        /// <summary>
        /// 库位组号
        /// </summary>
        public string binGroupNo { get; set; }

        /// <summary>
		/// 伸位组
		/// </summary>
        public string extensionGroupNo { get; set; }

        /// <summary>
        /// 伸位组内序号：靠近堆垛机为1，越远数字越大
        /// </summary>
        public int? extensionIdx { get; set; }


        public int? binPriority { get; set; } = 1;
        /// <summary>
        /// 排
        /// </summary>
        public int? binRow { get; set; }

        /// <summary>
		/// 列
		/// </summary>
        public int? binCol { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public int? binLayer { get; set; }

        public int? binHeight { get; set; }

    }


    public class AllotBinInputDto
    {
        /// <summary>
        /// 质检结果id
        /// </summary>
        public Int64? iqcResultId { get; set; }

        /// <summary>
        /// 包装条码
        /// </summary>
        public string packageBarcode { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }

        //      /// <summary>
        //      /// ERP仓库
        //      /// </summary>
        //      public string erpWhouseNo { get; set; }
        //      /// <summary>
        //      /// 库区
        //      /// </summary>
        //public string regionNo { get; set; }
        /// <summary>
        /// 巷道集合，逗号隔开
        /// </summary>

        public string roadwayNos { get; set; }

        public int? height { get; set; }

        public int? layer { get; set; }

        public string invoker { get; set; }

        /// <summary>
        /// 是否移库
        /// </summary>
        public bool isMove { get; set; } = false;
        /// <summary>
        /// false:正常分配；true：异常重新分配
        /// </summary>
        public bool isAllotAgain { get; set; } = false;
        ///// <summary>
        ///// 0:正常分配；1：置换分配（两个托盘号）；2：异常重新分配
        ///// </summary>
        //public string wcsAllotType { get; set; }

        //public string wmsAllotType { get; set; }

        //public string wmsAllotTypeDesc { get; set; }

    }


    public class AllotBinResultDto
    {
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }

        public string erpWhouseNo { get; set; }

        public string areaNo { get; set; }

        public string binNo { get; set; }

        public int? binPriority { get; set; }

        /// <summary>
        /// 库区
        /// </summary>
        public string regionNo { get; set; }


        public string roadwayNo { get; set; }

        public bool isAllotBin { get; set; }

        //public bool isExchangeBin { get; set; }
    }


    public class AlootBinStrategyDto
    {
        /// <summary>
        /// 均布策略：逐层，逐列
        /// </summary>
        public string distributed { get; set; }

        /// <summary>
        /// 巷道分布策略,靠近1列还是远离
        /// </summary>
        public string storeNearby { get; set; }
    }

    public class EmptyInDto
    {
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }
        /// <summary>
        /// 堆叠数量
        /// </summary>
        public decimal qty { get; set; }

        public string invoker { get; set; }

        public int? height { get; set; }

        public string locNo { get; set; }

    }


    #region Wcs
    public class WcsAllotRoadwayInputDto
    {
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }

        /// <summary>
        /// 巷道集合，逗号隔开
        /// </summary>

        public string roadwayNos { get; set; }
    }
    public class WcsAllotBinInputDto
    {

        ///<summary>
        ///站台号
        ///</summary>
        public string locNo1 { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode1 { get; set; }

        ///<summary>
        ///站台号
        ///</summary>
        public string locNo2 { get; set; }

        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode2 { get; set; }


        /// <summary>
        /// 0:正常分配；1：置换分配（两个托盘号）；2：异常重新分配
        /// </summary>
        public string wcsAllotType { get; set; }

        /// <summary>
        /// 巷道集合，逗号隔开
        /// </summary>

        public string roadwayNos { get; set; }

        /// <summary>
        /// 自定义数据集
        /// </summary>
        public string data { get; set; } = "";
        /// <summary>
        /// 11 满入,12近伸阻挡
        /// </summary>
        public string errFlag { get; set; }

        public string errMsg { get; set; }

        public string invoker { get; set; }

        public int? height { get; set; }
    }

    public class WcsAllotBinResultDto
    {

        ///<summary>
        ///库位1
        ///</summary>
        public string binNo1 { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode1 { get; set; }

        ///<summary>
        ///库位1
        ///</summary>
        public string binNo2 { get; set; }

        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode2 { get; set; }
        /// <summary>
        /// 巷道号
        /// </summary>
        public string roadwayNo { get; set; }
        /// <summary>
        /// 0:正常分配；1：置换分配（两个托盘号）；2：异常重新分配
        /// </summary>
        public string wcsAllotType { get; set; }

        /// <summary>
        /// 自定义数据集
        /// </summary>
        public string Data { get; set; } = "";

    }
    public class BinExceptionDto
    {
        public string binNo { get; set; }
        public int? operationType { get; set; }
    }
    public class OpenOrCloseBinDto
    {
        public string binNo { get; set; }
        public int? operationType { get; set; }
        public string invoker { get; set; }
    }
    #endregion

}
