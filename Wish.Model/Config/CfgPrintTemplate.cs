using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_PRINT_TEMPLATE")]
    [Index(nameof(printTemplateCode), IsUnique = true)]
    public class CfgPrintTemplate : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 纸张高度
        /// </summary>
        [Column("PRINT_PAGE_HEIGHT")]
        [Required]
        public int? printPageHeight { get; set; }

        /// <summary>
        /// 纸张类型：0：自定义
        /// </summary>
        [Column("PRINT_PAGE_KIND")]
        public int printPageKind { get; set; }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        [Column("PRINT_PAGE_WIDTH")]
        [Required]
        public int? printPageWidth { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Column("PRINT_TEMPLATE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "模板编码长度不能超出100字符")]
        public string printTemplateCode { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [Column("PRINT_TEMPLATE_CONTENT")]
        //[Required]
        [StringLength(4000, ErrorMessage = "模板内容长度不能超出4000字符")]
        public string printTemplateContent { get; set; }

        /// <summary>
        /// PC预览模板内容
        /// </summary>
        [Column("TEMPLATE_CONTENT_PREVIEW_PC")]
        [StringLength(4000, ErrorMessage = "PC预览模板内容长度不能超出4000字符")]
        public string templateContentPreviewPc { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Column("PRINT_TEMPLATE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "模板名称长度不能超出500字符")]
        public string printTemplateName { get; set; }

        /// <summary>
        /// 模板名称-其他
        /// </summary>
        [Column("PRINT_TEMPLATE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "模板名称-其他长度不能超出500字符")]
        public string printTemplateNameAlias { get; set; }

        /// <summary>
        /// 模板名称-英文
        /// </summary>
        [Column("PRINT_TEMPLATE_NAME_EN")]
        [StringLength(500, ErrorMessage = "模板名称-英文长度不能超出500字符")]
        public string printTemplateNameEn { get; set; }

        /// <summary>
        /// 使用标志。0：停用；1：启用；
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
