using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_DOC_LOC")]
    [Index(nameof(locNo), IsUnique = true)]
    public class CfgDocLoc : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 站台编码
        /// </summary>
        [Column("LOC_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "站台编码长度不能超出100字符")]
        public string locNo { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
