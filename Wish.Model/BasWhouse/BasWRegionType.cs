using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_REGION_TYPE")]
    [Index(nameof(regionTypeCode), IsUnique = true)]
    public class BasWRegionType : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 库区类型编码
        /// </summary>
        [Column("REGION_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "库区类型编码长度不能超出100字符")]
        public string regionTypeCode { get; set; }

        /// <summary>
        /// 库区类型标记：暂存、存储、拣选、存拣一体
        /// </summary>
        [Column("REGION_TYPE_FLAG")]
        [Required]
        [StringLength(100, ErrorMessage = "库区类型标记：暂存、存储、拣选、存拣一体长度不能超出100字符")]
        public string regionTypeFlag { get; set; }

        /// <summary>
        /// 库区类型名称
        /// </summary>
        [Column("REGION_TYPE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "库区类型名称长度不能超出500字符")]
        public string regionTypeName { get; set; }

        /// <summary>
        /// 库区类型名称其他
        /// </summary>
        [Column("REGION_TYPE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "库区类型名称其他长度不能超出500字符")]
        public string regionTypeNameAlias { get; set; }

        /// <summary>
        /// 库区类型名称英文
        /// </summary>
        [Column("REGION_TYPE_NAME_EN")]
        [StringLength(500, ErrorMessage = "库区类型名称英文长度不能超出500字符")]
        public string regionTypeNameEn { get; set; }


    }
}
