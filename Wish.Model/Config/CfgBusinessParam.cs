using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_BUSINESS_PARAM")]
    [Index(nameof(paramCode), IsUnique = true)]
    public class CfgBusinessParam : BasePoco
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
        /// 业务模块
        /// </summary>
        [Column("BUSINESS_MODULE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "业务模块长度不能超出100字符")]
        public string businessModuleCode { get; set; }

        /// <summary>
        /// 复选标识(0：单选，1：多选)
        /// </summary>
        [Column("CHECK_FLAG")]
        [Required]
        public int? checkFlag { get; set; }

        /// <summary>
        /// 参数编码
        /// </summary>
        [Column("PARAM_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数编码长度不能超出100字符")]
        public string paramCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("PARAM_DESC")]
        [StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
        public string paramDesc { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Column("PARAM_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "参数名称长度不能超出500字符")]
        public string paramName { get; set; }

        /// <summary>
        /// 参数名称-其他
        /// </summary>
        [Column("PARAM_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "参数名称-其他长度不能超出500字符")]
        public string paramNameAlias { get; set; }

        /// <summary>
        /// 参数名称-英文
        /// </summary>
        [Column("PARAM_NAME_EN")]
        [StringLength(500, ErrorMessage = "参数名称-英文长度不能超出500字符")]
        public string paramNameEn { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("PARAM_SORT")]
        [Required]
        public int paramSort { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
