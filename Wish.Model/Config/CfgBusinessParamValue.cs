using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_BUSINESS_PARAM_VALUE")]
    public class CfgBusinessParamValue : BasePoco
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
        /// 是否为默认值（1：默认值）
        /// </summary>
        [Column("DEFAULT_FLAG")]
        public int defaultFlag { get; set; }

        /// <summary>
        /// 参数编码
        /// </summary>
        [Column("PARAM_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数编码长度不能超出100字符")]
        public string paramCode { get; set; }

        /// <summary>
        /// 参数值编码
        /// </summary>
        [Column("PARAM_VALUE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数值编码长度不能超出100字符")]
        public string paramValueCode { get; set; }

        /// <summary>
        /// 参数值描述
        /// </summary>
        [Column("PARAM_VALUE_DESC")]
        [StringLength(200, ErrorMessage = "参数值描述长度不能超出200字符")]
        public string paramValueDesc { get; set; }

        /// <summary>
        /// 参数值名称
        /// </summary>
        [Column("PARAM_VALUE_NAME")]
        [Required]
        [StringLength(100, ErrorMessage = "参数值名称长度不能超出100字符")]
        public string paramValueName { get; set; }

        /// <summary>
        /// 参数值名称-其他
        /// </summary>
        [Column("PARAM_VALUE_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "参数值名称-其他长度不能超出100字符")]
        public string paramValueNameAlias { get; set; }

        /// <summary>
        /// 参数值名称-英文
        /// </summary>
        [Column("PARAM_VALUE_NAME_EN")]
        [StringLength(100, ErrorMessage = "参数值名称-英文长度不能超出100字符")]
        public string paramValueNameEn { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
