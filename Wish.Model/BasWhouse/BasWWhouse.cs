using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_WHOUSE")]
    [Index(nameof(whouseNo), IsUnique = true)]
    public class BasWWhouse : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Column("CONTACTS")]
        [StringLength(100, ErrorMessage = "联系人长度不能超出100字符")]
        public string contacts { get; set; }

        /// <summary>
        /// 仓库最大出库任务数量
        /// </summary>
        [Column("MAX_TASK_QTY")]
        public int? maxTaskQty { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Column("TELEPHONE")]
        [StringLength(100, ErrorMessage = "电话长度不能超出100字符")]
        public string telephone { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 地址
        /// </summary>
        [Column("WHOUSE_ADDRESS")]
        [StringLength(500, ErrorMessage = "地址长度不能超出500字符")]
        public string whouseAddress { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("WHOUSE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "仓库名称长度不能超出500字符")]
        public string whouseName { get; set; }

        /// <summary>
        /// 仓库名称-其他
        /// </summary>
        [Column("WHOUSE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "仓库名称-其他长度不能超出500字符")]
        public string whouseNameAlias { get; set; }

        /// <summary>
        /// 仓库名称-英文
        /// </summary>
        [Column("WHOUSE_NAME_EN")]
        [StringLength(500, ErrorMessage = "仓库名称-英文长度不能超出500字符")]
        public string whouseNameEn { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }

        /// <summary>
        /// 仓库类型：详见字典表
        /// </summary>
        [Column("WHOUSE_TYPE")]
        [Required]
        [StringLength(50, ErrorMessage = "仓库类型：详见字典表长度不能超出50字符")]
        public string whouseType { get; set; }


    }
}
