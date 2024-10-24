using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_PALLET_TYPE")]
    [Index(nameof(palletTypeCode), IsUnique = true)]
    public class BasWPalletType : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 托盘垛管理方式  0：无码管理；1：唯一码管理
        /// </summary>
        [Column("BARCODE_FLAG")]
        [Required]
        public int barcodeFlag { get; set; } = 0;

        /// <summary>
        /// 校验公式
        /// </summary>
        [Column("CHECK_FORMULA")]
        [StringLength(200, ErrorMessage = "校验公式长度不能超出200字符")]
        public string checkFormula { get; set; }

        /// <summary>
        /// 容器条码是否使用公式校验。0：不校验；1：使用公式校验
        /// </summary>
        [Column("CHECK_PALLET_FLAG")]
        [Required]
        public int checkPalletFlag { get; set; } = 0;

        /// <summary>
        /// 检验描述
        /// </summary>
        [Column("CHEK_DESC")]
        [StringLength(200, ErrorMessage = "检验描述长度不能超出200字符")]
        public string chekDesc { get; set; }

        /// <summary>
        /// 仅限开发人员标志 0：所有人都可见； 1：超级管理员账号可见；
        /// </summary>
        [Column("DEVELOP_FLAG")]
        [Required]
        public int? developFlag { get; set; } = 0;

        /// <summary>
        /// 空托叠盘上限
        /// </summary>
        [Column("EMPTY_MAX_QTY")]
        public int? emptyMaxQty { get; set; }

        /// <summary>
        /// 最大承重
        /// </summary>
        [Column("MAX_WEIGHT", TypeName = "decimal(18,3)")]
        public decimal? maxWeight { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [Column("PALLET_HEIGHT")]
        public int? palletHeight { get; set; }

        /// <summary>
        /// 长（mm）
        /// </summary>
        [Column("PALLET_LENGTH")]
        public int? palletLength { get; set; }

        /// <summary>
        /// 载体类型编码
        /// </summary>
        [Column("PALLET_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "载体类型编码长度不能超出100字符")]
        public string palletTypeCode { get; set; }

        /// <summary>
        /// 载体类型标识 1：装载托盘；2：废料工装类型，3: 子托工装类型,4：虚拟托盘类型,5:唯一虚拟托盘
        /// </summary>
        [Column("PALLET_TYPE_FLAG")]
        [Required]
        public int? palletTypeFlag { get; set; } = 1;

        ///// <summary>
        ///// 托盘类型扩展，1=托盘;2=料箱
        ///// </summary>
        //[Column("PALLET_TYPE_EXTEND")]
        //[Required]
        //public string palletTypeExtend { get; set; } = "1";

        /// <summary>
        /// 载体类型名称
        /// </summary>
        [Column("PALLET_TYPE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "载体类型名称长度不能超出500字符")]
        public string palletTypeName { get; set; }

        /// <summary>
        /// 载体类型名称-其他
        /// </summary>
        [Column("PALLET_TYPE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "载体类型名称-其他长度不能超出500字符")]
        public string palletTypeNameAlias { get; set; }

        /// <summary>
        /// 载体类型名称-英文
        /// </summary>
        [Column("PALLET_TYPE_NAME_EN")]
        [StringLength(500, ErrorMessage = "载体类型名称-英文长度不能超出500字符")]
        public string palletTypeNameEn { get; set; }

        /// <summary>
        /// 容器自重
        /// </summary>
        [Column("PALLET_WEIGHT", TypeName = "decimal(18,3)")]
        public decimal? palletWeight { get; set; }

        /// <summary>
        /// 宽（mm）
        /// </summary>
        [Column("PALLET_WIDTH")]
        public int? palletWidth { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        [Column("POSITION_COL")]
        public int? positionCol { get; set; }

        /// <summary>
        /// 添加方向
        /// </summary>
        [Column("POSITION_DIRECT")]
        public int? positionDirect { get; set; }

        /// <summary>
        /// 通道管理标记：0无通道，默认都是1；1有通道：按通道排列及方向处理
        /// </summary>
        [Column("POSITION_FLAG")]
        public int? positionFlag { get; set; } = 1;

        /// <summary>
        /// 排
        /// </summary>
        [Column("POSITION_ROW")]
        public int? positionRow { get; set; }

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

        /// <summary>
        /// 默认拣选规则编码
        /// </summary>
        //      [Column("PICK_RULE_NO")]
        //[StringLength(100, ErrorMessage = "默认拣选规则编码长度不能超出100字符")]
        //public string pickRuleNo { get; set; }

        /// <summary>
        /// 默认存储规则编码
        /// </summary>
        //      [Column("STORAGE_RULE_NO")]
        //[StringLength(100, ErrorMessage = "默认存储规则编码编码长度不能超出100字符")]
        //public string storageRuleNo { get; set; }


    }
}
