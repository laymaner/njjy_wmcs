using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_BIN")]
    [Index(nameof(binNo), IsUnique = true)]
    public class BasWBin : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Column("AREA_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "区域编码长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 最大承重
        /// </summary>
        [Column("BEAR_WEIGHT", TypeName = "decimal(18,3)")]
        public decimal? bearWeight { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        [Column("BIN_COL")]
        [Required]
        public int? binCol { get; set; }

        /// <summary>
        /// 异常标记：0：无异常，11：入库满入；21：出库空取；12：入库阻挡；22：出库阻挡；41：火警
        /// </summary>
        [Column("BIN_ERR_FLAG")]
        [Required]
        [StringLength(50, ErrorMessage = "异常标记：0：无异常，1：满入；2：空取:长度不能超出50字符")]
        public string binErrFlag { get; set; } = "0";

        /// <summary>
        /// 异常说明
        /// </summary>
        [Column("BIN_ERR_MSG")]
        [StringLength(200, ErrorMessage = "异常说明长度不能超出200字符")]
        public string binErrMsg { get; set; }

        /// <summary>
        /// 库位组内序号
        /// </summary>
        [Column("BIN_GROUP_IDX")]
        public int? binGroupIdx { get; set; }

        /// <summary>
        /// 库位组号
        /// </summary>
        [Column("BIN_GROUP_NO")]
        [StringLength(20, ErrorMessage = "库位组号长度不能超出20字符")]
        public string binGroupNo { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [Column("BIN_HEIGHT")]
        public int? binHeight { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        [Column("BIN_LAYER")]
        [Required]
        public int? binLayer { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        [Column("BIN_LENGTH")]
        public int? binLength { get; set; }

        /// <summary>
        /// 库位名称
        /// </summary>
        [Column("BIN_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "库位名称长度不能超出500字符")]
        public string binName { get; set; }

        /// <summary>
        /// 库位名称-其他
        /// </summary>
        [Column("BIN_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "库位名称-其他长度不能超出500字符")]
        public string binNameAlias { get; set; }

        /// <summary>
        /// 库位名称-英文
        /// </summary>
        [Column("BIN_NAME_EN")]
        [StringLength(500, ErrorMessage = "库位名称-英文长度不能超出500字符")]
        public string binNameEn { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        [Column("BIN_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库位编码长度不能超出100字符")]
        public string binNo { get; set; }

        /// <summary>
        /// 库位优先级（入库，从小到大，出库根据实际）
        /// </summary>
        [Column("BIN_PRIORITY")]
        [Required]
        public int? binPriority { get; set; } = 1;

        /// <summary>
        /// 排
        /// </summary>
        [Column("BIN_ROW")]
        [Required]
        public int? binRow { get; set; }

        /// <summary>
        /// 库位分类
        /// </summary>
        [Column("BIN_TYPE")]
        [StringLength(50, ErrorMessage = "库位分类长度不能超出50字符")]
        public string binType { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        [Column("BIN_WIDTH")]
        public int? binWidth { get; set; }

        /// <summary>
        /// 库位容量
        /// </summary>
        [Column("CAPACITY_SIZE")]
        [Required]
        public int? capacitySize { get; set; } = 1;

        ///// <summary>
        ///// 货位描述
        ///// </summary>
        //[Column("DESCRIPTION")]
        //[StringLength(200, ErrorMessage = "货位描述长度不能超出200字符")]
        //public string description { get; set; }

        /// <summary>
        /// 伸位组
        /// </summary>
        [Column("EXTENSION_GROUP_NO")]
        [StringLength(20, ErrorMessage = "伸位组长度不能超出20字符")]
        public string extensionGroupNo { get; set; }

        /// <summary>
        /// 伸位组内序号：靠近堆垛机为1，越远数字越大
        /// </summary>
        [Column("EXTENSION_IDX")]
        public int? extensionIdx { get; set; }

        /// <summary>
        /// 火警标志(0.无火警  1.有火警)
        /// </summary>
        [Column("FIRE_FLAG")]
        public int? fireFlag { get; set; }

        /// <summary>
        /// 是否允许入库(0.不允许 1.允许)
        /// </summary>
        [Column("IS_IN_ENABLE")]
        [Required]
        public int? isInEnable { get; set; } = 1;

        /// <summary>
        /// 是否允许出库(0.不允许 1.允许)
        /// </summary>
        [Column("IS_OUT_ENABLE")]
        [Required]
        public int? isOutEnable { get; set; } = 1;

        /// <summary>
        /// 是否有效期管理(0.非有效期管理 1.有效期管理);2.2.6需求上有这一部分
        /// </summary>
        [Column("IS_VALIDITY_PERIOD")]
        [Required]
        public int? isValidityPeriod { get; set; } = 0;

        /// <summary>
        /// 库位托盘方向 1：正向  0：反向
        /// </summary>
        [Column("PALLET_DIRECT")]
        public int? palletDirect { get; set; }

        /// <summary>
        /// 货架排号
        /// </summary>
        [Column("RACK_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "货架排号长度不能超出100字符")]
        public string rackNo { get; set; }

        /// <summary>
        /// 库区编码
        /// </summary>
        [Column("REGION_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库区编码长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 巷道编码
        /// </summary>
        [Column("ROADWAY_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "巷道编码长度不能超出100字符")]
        public string roadwayNo { get; set; }

        /// <summary>
        /// 是否启用  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 虚拟标记：0不是，1是
        /// </summary>
        [Column("VIRTUAL_FLAG")]
        [Required]
        public int? virtualFlag { get; set; } = 0;

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
