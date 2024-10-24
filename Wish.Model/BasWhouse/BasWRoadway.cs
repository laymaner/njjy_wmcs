using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_ROADWAY")]
    [Index(nameof(roadwayNo), IsUnique = true)]
    public class BasWRoadway : BasePoco
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

        ///// <summary>
        ///// 删除标志(0-有效,1-已删除)
        ///// </summary>
        //[Column("DEL_FLAG")]
        //[Required]
        //[StringLength(50, ErrorMessage = "删除标志(0-有效,1-已删除)长度不能超出50字符")]
        //public string delFlag { get; set; } = "0";

        /// <summary>
        /// 异常标记 0：无异常；1：有异常；
        /// </summary>
        [Column("ERR_FLAG")]
        [Required]
        public int? errFlag { get; set; } = 0;

        /// <summary>
        /// 异常说明：无异常为空
        /// </summary>
        [Column("ERR_MSG")]
        [StringLength(255, ErrorMessage = "异常说明：无异常为空长度不能超出255字符")]
        public string errMsg { get; set; }

        /// <summary>
        /// 库区编号
        /// </summary>
        [Column("REGION_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 预留空库位数量
        /// </summary>
        [Column("RESERVED_QTY")]
        [Required]
        public int? reservedQty { get; set; } = 0;

        /// <summary>
        /// 巷道名称
        /// </summary>
        [Column("ROADWAY_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "巷道名称长度不能超出500字符")]
        public string roadwayName { get; set; }

        /// <summary>
        /// 巷道名称-其他
        /// </summary>
        [Column("ROADWAY_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "巷道名称-其他长度不能超出500字符")]
        public string roadwayNameAlias { get; set; }

        /// <summary>
        /// 巷道名称-英文
        /// </summary>
        [Column("ROADWAY_NAME_EN")]
        [StringLength(500, ErrorMessage = "巷道名称-英文长度不能超出500字符")]
        public string roadwayNameEn { get; set; }

        /// <summary>
        /// 巷道编码
        /// </summary>
        [Column("ROADWAY_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "巷道编码长度不能超出100字符")]
        public string roadwayNo { get; set; }

        /// <summary>
        /// 是否启用 0：停用；1：启用；
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
