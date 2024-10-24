using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class AssignBinInParmaDto
    {

        /// <summary>
		/// 质检结果id
		/// </summary>
		public string iqcResultId { get; set; }

        /// <summary>
        /// 包装条码
        /// </summary>
        public string packageBarcode { get; set; }
        /// <summary>
        /// 载体条码
        /// </summary>
        public string palletBarcode { get; set; }
        /// <summary>
        /// ERP仓库
        /// </summary>
        public string erpWhouseNo { get; set; }
        /// <summary>
        /// 库区
        /// </summary>
		public string regionNo { get; set; }
        /// <summary>
        /// 巷道集合，逗号隔开
        /// </summary>

        public string roadwayNos { get; set; }

        public string WcsAllotType { get; set; }

    }

    /// <summary>
    /// 货位信息,供货位分配使用
    /// </summary>
    public class WBinDto
    {
        /// <summary>
		/// 载体条码
		/// </summary>
		public string palletBarcode { get; set; }
        /// <summary>
		/// 列
		/// </summary>
        public int? binCol { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public int? binLayer { get; set; }

        /// <summary>
        /// 库位名称
        /// </summary>
        public string binName { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string binNo { get; set; }

        /// <summary>
        /// 巷道号
        /// </summary>
        public string roadwayNo { get; set; }

        /// <summary>
        /// 排
        /// </summary>
        public int? binRow { get; set; }

        /// <summary>
        /// 伸位组
        /// </summary>
        public string extensionGroupNo { get; set; }

        /// <summary>
        /// 伸位组内序号：靠近堆垛机为1，越远数字越大
        /// </summary>
        public int? extensionIdx { get; set; }

        /// <summary>
        /// 是否启用  0：禁用；1：启用
        /// </summary>
        public int usedFlag { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool isCheck { get; set; }
    }


    public class WRackBinSaveDto
    {
        public string areaNo { get; set; }

        public string regionNo { get; set; }

        public string rackNo { get; set; }

        public List<string> binList { get; set; } = new List<string>();
    }

    public class WSupBinSaveDto
    {
        public string areaNo { get; set; }

        public string regionNo { get; set; }

        public string rackNo { get; set; }
        public string erpWhouseNo { get; set; }

        public List<string> binList { get; set; } = new List<string>();
    }
    /// <summary>
    /// 巷道信息,供货位分配使用
    /// </summary>
	public class WRackBinDto
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        public string areaNo { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? rackIdx { get; set; }

        /// <summary>
        /// 货架排名称
        /// </summary>
        public string rackName { get; set; }

        /// <summary>
        /// 货架排号
        /// </summary>
        public string rackNo { get; set; }

        /// <summary>
        /// 库区编码
        /// </summary>
        public string regionNo { get; set; }

        /// <summary>
        /// 巷道编码
        /// </summary>
        public string roadwayNo { get; set; }

        /// <summary>
        /// 货位信息
        /// </summary>
        public List<WBinDto> binList { get; set; } = new List<WBinDto>();
    }

    public class BinStatisticsDto
    {
        public string name { get; set; }

        public int value { get; set; }
    }

    public class RoadwayStatisticsDto
    {
        public string name { get; set; }

        public int value1 { get; set; }
        public int value2 { get; set; }
    }
}
