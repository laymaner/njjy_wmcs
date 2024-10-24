using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_PUTDOWN")]
    [Index(nameof(putdownNo), IsUnique = true)]
    public class WmsPutdown : BasePoco
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
		/// 库位号
		/// </summary>
		[Column("BIN_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库位号长度不能超出100字符")]
		public string binNo { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		[StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
		public string docTypeCode { get; set; }

		/// <summary>
		/// 装载类型 : 1:实盘 ；2:工装；0：空盘；
		/// </summary>
		[Column("LOADED_TYPE")]
		[Required]
		//[StringLength(50, ErrorMessage = "装载类型 : 1:实盘 ；2:工装；0：空盘；长度不能超出50字符")]
		public int? loadedType { get; set; }

		/// <summary>
		/// 关联单据号：触发下架单据号，但下架完成后并不能只看此单据号
		/// </summary>
		[Column("ORDER_NO")]
		[StringLength(100, ErrorMessage = "关联单据号：触发下架单据号，但下架完成后并不能只看此单据号长度不能超出100字符")]
		public string orderNo { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 取货方式：详见字典表
		/// </summary>
		[Column("PICKUP_METHOD")]
		[Required]
		[StringLength(50, ErrorMessage = "取货方式：详见字典表长度不能超出50字符")]
		public string pickupMethod { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 下架单号
		/// </summary>
		[Column("PUTDOWN_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "下架单号长度不能超出100字符")]
		public string putdownNo { get; set; }

		/// <summary>
		/// 下架状态。0：初始创建；31：下架中；90：下架完成；92删除；93强制完成
		/// </summary>
		[Column("PUTDOWN_STATUS")]
		[Required]
		//[StringLength(50, ErrorMessage = "下架状态长度不能超出50字符")]
		public int? putdownStatus { get; set; } = 0;

		/// <summary>
		/// 库区编号
		/// </summary>
		[Column("REGION_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
		public string regionNo { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		public string stockCode { get; set; }

		/// <summary>
		/// 下架目标站台：拣选站台/终点站台
		/// </summary>
		[Column("TARGET_LOC_NO")]
		[StringLength(100, ErrorMessage = "下架目标站台：拣选站台/终点站台长度不能超出100字符")]
		public string targetLocNo { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
