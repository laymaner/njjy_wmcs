using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_AREA")]
    [Index(nameof(areaNo), IsUnique = true)]
    public class BasWArea : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }


        /// <summary>
        /// 区域名称
        /// </summary>
        [Column("AREA_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "区域名称长度不能超出500字符")]
        public string areaName { get; set; }

        /// <summary>
        /// 区域名称-其他
        /// </summary>
        [Column("AREA_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "区域名称-其他长度不能超出500字符")]
        public string areaNameAlias { get; set; }

        /// <summary>
        /// 区域名称-英文
        /// </summary>
        [Column("AREA_NAME_EN")]
        [StringLength(500, ErrorMessage = "区域名称-英文长度不能超出500字符")]
        public string areaNameEn { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Column("AREA_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "区域编码长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 区域类型
        /// </summary>
        [Column("AREA_TYPE")]
        [StringLength(50, ErrorMessage = "区域类型长度不能超出50字符")]
        public string areaType { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
