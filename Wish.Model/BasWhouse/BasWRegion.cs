using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.BasWhouse.Model
{
    [Table("BAS_W_REGION")]
    [Index(nameof(regionNo), IsUnique = true)]
    public class BasWRegion : BasePoco
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
        /// 是否允许人工上架
        /// </summary>
        [Column("MANUAL_FLAG")]
        [Required]
        public int manualFlag { get; set; }

        /// <summary>
        /// 是否带盘存储
        /// </summary>
        [Column("PALLET_MGT")]
        [Required]
        public int? palletMgt { get; set; }

        /// <summary>
        /// 取货方式：详见字典表
        /// </summary>
        [Column("PICKUP_METHOD")]
        [StringLength(100, ErrorMessage = "取货方式：详见字典表长度不能超出100字符")]
        public string pickupMethod { get; set; }

        /// <summary>
        /// 库区名称
        /// </summary>
        [Column("REGION_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "库区名称长度不能超出500字符")]
        public string regionName { get; set; }

        /// <summary>
        /// 库区名称-其他
        /// </summary>
        [Column("REGION_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "库区名称-其他长度不能超出500字符")]
        public string regionNameAlias { get; set; }

        /// <summary>
        /// 库区名称-英文
        /// </summary>
        [Column("REGION_NAME_EN")]
        [StringLength(500, ErrorMessage = "库区名称-英文长度不能超出500字符")]
        public string regionNameEn { get; set; }

        /// <summary>
        /// 库区编码
        /// </summary>
        [Column("REGION_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库区编码长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 库区类型（平库，立库）
        /// </summary>
        [Column("REGION_TYPE_CODE")]
        [Required]
        [StringLength(50, ErrorMessage = "库区类型（平库，立库）长度不能超出50字符")]
        public string regionTypeCode { get; set; }


        //      /// <summary>
        //      /// 库区设备类型标记, 1=托盘库, 2=料箱库, 3=平库
        //      /// </summary>
        //      [Column("REGION_DEVICE_FLAG")]
        //[Required]
        //      [StringLength(50, ErrorMessage = "库区分类（托盘库,料箱库,平库）长度不能超出50字符")]
        //      public string regionDeviceFlag { get; set; }

        /// <summary>
        /// 伸工位类型(无;SS单伸单工位，DS双伸单工位，SD单伸双工位)
        /// </summary>
        [Column("SD_TYPE")]
        [StringLength(50, ErrorMessage = "伸工位类型(无;SS单伸单工位，DS双伸单工位，SD单伸双工位)长度不能超出50字符")]
        public string sdType { get; set; }

        /// <summary>
        /// 是否启用  0：停用；1：启用；
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 虚拟标记：0不是，1是
        /// </summary>
        [Column("VIRTUAL_FLAG")]
        [Required]
        public int? virtualFlag { get; set; } = 0;

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }


    }
}
