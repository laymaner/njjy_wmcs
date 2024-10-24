using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_STOCK")]
    [Index(nameof(stockCode), IsUnique = true)]
    public class WmsStock : BasePoco
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
		/// 异常标记(0正常，10异常，20火警)
		/// </summary>
		[Column("ERR_FLAG")]
		//[StringLength(50, ErrorMessage = "异常标记(0正常，10异常，20火警)长度不能超出50字符")]
		public int? errFlag { get; set; }

		/// <summary>
		/// 异常说明
		/// </summary>
		[Column("ERR_MSG")]
		[StringLength(255, ErrorMessage = "异常说明长度不能超出255字符")]
		public string errMsg { get; set; }

		/// <summary>
		/// 组盘后托盘高度
		/// </summary>
		[Column("HEIGHT")]
		public int?  height { get; set; }

		/// <summary>
		/// 预拣选完成后的发货单号
		/// </summary>
		[Column("INVOICE_NO")]
		[StringLength(100, ErrorMessage = "预拣选完成后的发货单号长度不能超出100字符")]
		public string invoiceNo { get; set; }

		/// <summary>
		/// 装载类型(1:实盘 ；2:工装；0：空盘)
		/// </summary>
		[Column("LOADED_TYPE")]
		//[StringLength(50, ErrorMessage = "装载类型(1:实盘 ；2:工装；0：空盘)长度不能超出50字符")]
		public int? loadedType { get; set; }

		/// <summary>
		/// 分配站台组，双工位使用
		/// </summary>
		[Column("LOC_ALLOT_GROUP")]
		[StringLength(100, ErrorMessage = "分配站台组，双工位使用长度不能超出100字符")]
		public string locAllotGroup { get; set; }

		/// <summary>
		/// 当前站台号
		/// </summary>
		[Column("LOC_NO")]
		[StringLength(100, ErrorMessage = "当前站台号长度不能超出100字符")]
		public string locNo { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
		[Column("REGION_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
		public string regionNo { get; set; }

		/// <summary>
		/// 巷道编号
		/// </summary>
		[Column("ROADWAY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "巷道编号长度不能超出100字符")]
		public string roadwayNo { get; set; }

		/// <summary>
		/// 特殊标记
		/// </summary>
		[Column("SPECIAL_FLAG")]
		public int?  specialFlag { get; set; }

		/// <summary>
		/// 特殊说明
		/// </summary>
		[Column("SPECIAL_MSG")]
		[StringLength(255, ErrorMessage = "特殊说明长度不能超出255字符")]
		public string specialMsg { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		public string stockCode { get; set; }

		/// <summary>
		/// 库存状态（0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成）
		/// </summary>
		[Column("STOCK_STATUS")]
		[Required]
		public int stockStatus { get; set; } = 0;

		/// <summary>
		/// 组盘重量
		/// </summary>
		[Column("WEIGHT", TypeName = "decimal(18,3)")]
		public decimal?  weight { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
