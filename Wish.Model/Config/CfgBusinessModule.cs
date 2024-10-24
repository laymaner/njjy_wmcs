using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_BUSINESS_MODULE")]
    [Index(nameof(businessModuleCode), IsUnique = true)]
    public class CfgBusinessModule : BasePoco
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
        /// 业务模块编码
        /// </summary>
        [Column("BUSINESS_MODULE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "业务模块编码长度不能超出100字符")]
        public string businessModuleCode { get; set; }

        /// <summary>
        /// 业务模块说明
        /// </summary>
        [Column("BUSINESS_MODULE_DESC")]
        [StringLength(200, ErrorMessage = "业务模块说明长度不能超出200字符")]
        public string businessModuleDesc { get; set; }

        /// <summary>
        /// 业务模块名称
        /// </summary>
        [Column("BUSINESS_MODULE_NAME")]
        [Required]
        [StringLength(100, ErrorMessage = "业务模块名称长度不能超出100字符")]
        public string businessModuleName { get; set; }

        /// <summary>
        /// 业务模块名称-其他
        /// </summary>
        [Column("BUSINESS_MODULE_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "业务模块名称-其他长度不能超出100字符")]
        public string businessModuleNameAlias { get; set; }

        /// <summary>
        /// 业务模块名称-英文
        /// </summary>
        [Column("BUSINESS_MODULE_NAME_EN")]
        [StringLength(100, ErrorMessage = "业务模块名称-英文长度不能超出100字符")]
        public string businessModuleNameEn { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("BUSINESS_MODULE_SORT")]
        [Required]
        public int businessModuleSort { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
