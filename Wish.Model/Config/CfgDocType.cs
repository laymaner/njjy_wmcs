using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_DOC_TYPE")]
    [Index(nameof(docTypeCode), IsUnique = true)]
    public class CfgDocType : BasePoco
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
        /// 客供类型：客户、供应商、产线等等
        /// </summary>
        [Column("CV_TYPE")]
        [StringLength(10, ErrorMessage = "客供类型：客户、供应商、产线等等长度不能超出10字符")]
        public string cvType { get; set; }

        /// <summary>
        /// 仅限开发人员标志，默认1，只有开发人员才能看见。0：所有人都能看见；1：只有开发人员能看见；
        /// </summary>
        [Column("DEVELOP_FLAG")]
        [Required]
        public int developFlag { get; set; } = 1;

        /// <summary>
        /// 单据类型下发优先级：数字越小，优先级越高
        /// </summary>
        [Column("DOC_PRIORITY")]
        [Required]
        public int? docPriority { get; set; }

        /// <summary>
        /// 单据类型编码
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "单据类型编码长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("DOC_TYPE_DESC")]
        [StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
        public string docTypeDesc { get; set; }

        /// <summary>
        /// 单据类型名称
        /// </summary>
        [Column("DOC_TYPE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "单据类型名称长度不能超出500字符")]
        public string docTypeName { get; set; }

        /// <summary>
        /// 单据类型名称-其他
        /// </summary>
        [Column("DOC_TYPE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "单据类型名称-其他长度不能超出500字符")]
        public string docTypeNameAlias { get; set; }

        /// <summary>
        /// 单据类型名称-英文
        /// </summary>
        [Column("DOC_TYPE_NAME_EN")]
        [StringLength(500, ErrorMessage = "单据类型名称-英文长度不能超出500字符")]
        public string docTypeNameEn { get; set; }

        /// <summary>
        /// 外部单据类型编码
        /// </summary>
        [Column("EXTERNAL_DOC_TYPE_CODE")]
        [StringLength(100, ErrorMessage = "外部单据类型编码长度不能超出100字符")]
        public string externalDocTypeCode { get; set; }

        /// <summary>
        /// 下发任务上限
        /// </summary>
        [Column("TASK_MAX_QTY")]
        public int? taskMaxQty { get; set; }

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
