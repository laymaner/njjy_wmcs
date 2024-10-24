using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_ERP_WHOUSE")]

    public class CfgErpWhouse : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// ERP仓库编号
        /// </summary>
        [Column("ERP_WHOUSE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "ERP仓库编号长度不能超出100字符")]
        public string erpWhouseNo { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Column("PRIORITY")]
        [Required]
        public int priority { get; set; }

        /// <summary>
        /// ERP仓库名称
        /// </summary>
        [Column("ERP_WHOUSE_NAME")]
        [StringLength(500, ErrorMessage = "ERP仓库名称长度不能超出500字符")]
        public string erpWhouseName { get; set; }

        /// <summary>
        /// ERP仓库名称-英文
        /// </summary>
        [Column("ERP_WHOUSE_NAME_EN")]
        [StringLength(500, ErrorMessage = "ERP仓库名称-英文长度不能超出100字符")]
        public string erpWhouseNameEn { get; set; }

        /// <summary>
        /// ERP仓库名称-其他
        /// </summary>
        [Column("ERP_WHOUSE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "ERP仓库名称-其他长度不能超出100字符")]
        public string erpWhouseNameAlias { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
