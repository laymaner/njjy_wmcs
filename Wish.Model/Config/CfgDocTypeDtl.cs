using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_DOC_TYPE_DTL")]
    public class CfgDocTypeDtl : BasePoco
    {
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
        /// 单据类型编码
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "单据类型编码长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [Column("PARAM_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数代码长度不能超出100字符")]
        public string paramCode { get; set; }

        /// <summary>
        /// 参数项值
        /// </summary>
        [Column("PARAM_VALUE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数项值长度不能超出100字符")]
        public string paramValueCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
