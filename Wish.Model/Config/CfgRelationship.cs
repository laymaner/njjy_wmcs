using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_RELATIONSHIP")]
    public class CfgRelationship : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 左表编码
        /// </summary>
        [Column("LEFT_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "左表编码长度不能超出100字符")]
        public string leftCode { get; set; }

        /// <summary>
        /// 优先级 默认0（特定的对应关系使用，数值小的优先级高）
        /// </summary>
        [Column("PRIORITY")]
        [Required]
        public int? priority { get; set; } = 0;

        /// <summary>
        /// 对应关系类型
        /// </summary>
        [Column("RELATIONSHIP_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "对应关系类型长度不能超出100字符")]
        public string relationshipTypeCode { get; set; }

        /// <summary>
        /// 右表编码
        /// </summary>
        [Column("RIGHT_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "右表编码长度不能超出100字符")]
        public string rightCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
