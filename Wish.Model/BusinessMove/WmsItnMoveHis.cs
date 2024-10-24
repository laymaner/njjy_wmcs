using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_MOVE_HIS")]
    [Index(nameof(moveNo), IsUnique = true)]
    public class WmsItnMoveHis : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码(楼号)
        /// </summary>
        [Column("AREA_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 移库单号
        /// </summary>
        [Column("MOVE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "移库单号长度不能超出100字符")]
        public string moveNo { get; set; }

        /// <summary>
        /// 移动状态：0初始创建；41	出库中；90下架完成；92	删除；93	强制关闭
        /// </summary>
        [Column("MOVE_STATUS")]
        //[StringLength(50, ErrorMessage = "移动状态：0初始创建；10下架中；20下架完成；30上架中；90移库完成长度不能超出50字符")]
        public int moveStatus { get; set; } = 0;

        /// <summary>
        /// 备注说明
        /// </summary>
        [Column("ORDER_DESC")]
        [StringLength(500, ErrorMessage = "备注说明长度不能超出500字符")]
        public string orderDesc { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
        public string proprietorCode { get; set; }

        /// <summary>
        /// 移库下架站台
        /// </summary>
        [Column("PUTDOWN_LOC_NO")]
        [StringLength(100, ErrorMessage = "移库下架站台长度不能超出100字符")]
        public string putdownLocNo { get; set; }

        /// <summary>
        /// 仓库号
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
        public string whouseNo { get; set; }

        /// <summary>
        /// 关联单号
        /// </summary>
        //[Column("ORDER_NO")]
        //[StringLength(100, ErrorMessage = "关联单号长度不能超出100字符")]
        //public string orderNo { get; set; }

        /// <summary>
        /// 移动模式
        /// </summary>
        //[Column("MOVE_METHOD")]
        //[StringLength(100, ErrorMessage = "移动模式长度不能超出100字符")]
        //public string moveMethod { get; set; }

        /// <summary>
        /// 移库来源库区
        /// </summary>
        //[Column("FR_REGION_NO")]
        //[StringLength(100, ErrorMessage = "移库来源库区长度不能超出100字符")]
        //public string frRegionNo { get; set; }

        /// <summary>
        /// 移库目标库区
        /// </summary>
        //[Column("ORDER_NO")]
        //[StringLength(100, ErrorMessage = "移库目标库区长度不能超出100字符")]
        //public string toRegionNo { get; set; }


    }
}
