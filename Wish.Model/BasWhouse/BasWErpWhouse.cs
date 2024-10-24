using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_ERP_WHOUSE")]
    [Index(nameof(erpWhouseNo), IsUnique = true)]
    public class BasWErpWhouse : BasePoco
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
        /// ERP仓库名称
        /// </summary>
        [Column("ERP_WHOUSE_NAME")]
        [Required]
        [StringLength(100, ErrorMessage = "ERP仓库名称长度不能超出100字符")]
        public string erpWhouseName { get; set; }

        /// <summary>
        /// ERP仓库名称-其他
        /// </summary>
        [Column("ERP_WHOUSE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "ERP仓库名称-其他长度不能超出500字符")]
        public string erpWhouseNameAlias { get; set; }

        /// <summary>
        /// ERP仓库名称-英文
        /// </summary>
        [Column("ERP_WHOUSE_NAME_EN")]
        [StringLength(500, ErrorMessage = "ERP仓库名称-英文长度不能超出500字符")]
        public string erpWhouseNameEn { get; set; }

        /// <summary>
        /// ERP仓库编码
        /// </summary>
        [Column("ERP_WHOUSE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "ERP仓库编码长度不能超出100字符")]
        public string erpWhouseNo { get; set; }

        /// <summary>
        /// ERP仓库类别
        /// </summary>
        [Column("ERP_WHOUSE_TYPE")]
        [StringLength(100, ErrorMessage = "ERP仓库类别长度不能超出100字符")]
        public string erpWhouseType { get; set; }

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
