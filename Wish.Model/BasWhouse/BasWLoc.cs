using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_LOC")]
    [Index(nameof(locNo), IsUnique = true)]
    public class BasWLoc : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 站台组编码
        /// </summary>
        [Column("LOC_GROUP_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "站台组编码长度不能超出100字符")]
        public string locGroupNo { get; set; }

        /// <summary>
        /// 站台名称
        /// </summary>
        [Column("LOC_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "站台名称长度不能超出500字符")]
        public string locName { get; set; }

        /// <summary>
        /// 站台组名称-其他
        /// </summary>
        [Column("LOC_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "站台组名称-其他长度不能超出500字符")]
        public string locNameAlias { get; set; }

        /// <summary>
        /// 站台组名称-英文
        /// </summary>
        [Column("LOC_NAME_EN")]
        [StringLength(500, ErrorMessage = "站台组名称-英文长度不能超出500字符")]
        public string locNameEn { get; set; }

        /// <summary>
        /// 站台编码
        /// </summary>
        [Column("LOC_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "站台编码长度不能超出100字符")]
        public string locNo { get; set; }

        /// <summary>
        /// 站台类型编码
        /// </summary>
        [Column("LOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "站台类型编码长度不能超出100字符")]
        public string locTypeCode { get; set; }

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
