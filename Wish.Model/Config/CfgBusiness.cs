using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_BUSINESS")]
    [Index(nameof(businessCode), IsUnique = true)]
    public class CfgBusiness : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 业务编码
        /// </summary>
        [Column("BUSINESS_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "业务编码长度不能超出100字符")]
        public string businessCode { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        [Column("BUSINESS_DESC")]
        [StringLength(200, ErrorMessage = "业务描述长度不能超出200字符")]
        public string businessDesc { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [Column("BUSINESS_NAME")]
        [Required]
        [StringLength(100, ErrorMessage = "业务名称长度不能超出100字符")]
        public string businessName { get; set; }

        /// <summary>
        /// 业务名称-其他
        /// </summary>
        [Column("BUSINESS_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "业务名称-其他长度不能超出100字符")]
        public string businessNameAlias { get; set; }

        /// <summary>
        /// 业务名称-英文
        /// </summary>
        [Column("BUSINESS_NAME_EN")]
        [StringLength(100, ErrorMessage = "业务名称-英文长度不能超出100字符")]
        public string businessNameEn { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
