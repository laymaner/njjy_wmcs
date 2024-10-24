using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_RACK")]
    [Index(nameof(rackNo), IsUnique = true)]
    public class BasWRack : BasePoco
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
        /// 排序号
        /// </summary>
        [Column("RACK_IDX")]
        [Required]
        public int? rackIdx { get; set; } = 1;

        /// <summary>
        /// 货架排名称
        /// </summary>
        [Column("RACK_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "货架排名称长度不能超出500字符")]
        public string rackName { get; set; }

        /// <summary>
        /// 货架排名称-其他
        /// </summary>
        [Column("RACK_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "货架排名称-其他长度不能超出500字符")]
        public string rackNameAlias { get; set; }

        /// <summary>
        /// 货架排名称-英文
        /// </summary>
        [Column("RACK_NAME_EN")]
        [StringLength(500, ErrorMessage = "货架排名称-英文长度不能超出500字符")]
        public string rackNameEn { get; set; }

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
        /// 是否启用 0：停用；1：启用
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
