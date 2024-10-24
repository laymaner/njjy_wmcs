using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_PALLET")]
    [Index(nameof(palletBarcode), IsUnique = true)]
    public class BasWPallet : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 载体条码
        /// </summary>
        [Column("PALLET_BARCODE")]
        [Required]
        [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
        public string palletBarcode { get; set; }

        /// <summary>
        /// 载体描述
        /// </summary>
        [Column("PALLET_DESC")]
        [StringLength(500, ErrorMessage = "载体描述长度不能超出500字符")]
        public string palletDesc { get; set; }

        /// <summary>
        /// 载体描述-其他
        /// </summary>
        [Column("PALLET_DESC_ALIAS")]
        [StringLength(500, ErrorMessage = "载体描述-其他长度不能超出500字符")]
        public string palletDescAlias { get; set; }

        /// <summary>
        /// 载体描述-英文
        /// </summary>
        [Column("PALLET_DESC_EN")]
        [StringLength(500, ErrorMessage = "载体描述-英文长度不能超出500字符")]
        public string palletDescEn { get; set; }

        /// <summary>
        /// 载体类型编码
        /// </summary>
        [Column("PALLET_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "载体类型编码长度不能超出100字符")]
        public string palletTypeCode { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        [Column("PRINTS_QTY")]
        public int? printsQty { get; set; }

        /// <summary>
        /// 使用标识 0：禁用；1：启用
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
