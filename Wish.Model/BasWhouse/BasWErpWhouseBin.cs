using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_ERP_WHOUSE_BIN")]
    public class BasWErpWhouseBin : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Column("AREA_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "区域编码长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 库位号
        /// </summary>
        [Column("BIN_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库位号长度不能超出100字符")]
        public string binNo { get; set; }

        /// <summary>
        /// 删除标志(0-有效,1-已删除)
        /// </summary>
        [Column("DEL_FLAG")]
        [Required]
        [StringLength(50, ErrorMessage = "删除标志(0-有效,1-已删除)长度不能超出50字符")]
        public string delFlag { get; set; } = "0";

        /// <summary>
        /// ERP仓库编码
        /// </summary>
        [Column("ERP_WHOUSE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "ERP仓库编码长度不能超出100字符")]
        public string erpWhouseNo { get; set; }

        /// <summary>
        /// 库区编码
        /// </summary>
        [Column("REGION_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库区编码长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
