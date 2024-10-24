using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_PUTAWAY")]
    [Index(nameof(putawayNo), IsUnique = true)]
    public class WmsPutaway : BasePoco
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
		/// 删除标志;0-有效,1-已删除
		/// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")]
		//public string delFlag { get; set; } = "0";

		/// <summary>
		/// ERP仓库
		/// </summary>
		//[Column("ERP_WHOUSE_NO")]
		//[Required]
		//[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		//public string erpWhouseNo { get; set; }

		///// <summary>
		///// 扩展字段1
		///// </summary>
		//[Column("EXTEND1")]
		//[StringLength(200, ErrorMessage = "扩展字段1长度不能超出200字符")]
		//public string extend1 { get; set; }

		///// <summary>
		///// 扩展字段10
		///// </summary>
		//[Column("EXTEND10")]
		//[StringLength(200, ErrorMessage = "扩展字段10长度不能超出200字符")]
		//public string extend10 { get; set; }

		///// <summary>
		///// 扩展字段11
		///// </summary>
		//[Column("EXTEND11")]
		//[StringLength(200, ErrorMessage = "扩展字段11长度不能超出200字符")]
		//public string extend11 { get; set; }

		///// <summary>
		///// 扩展字段12
		///// </summary>
		//[Column("EXTEND12")]
		//[StringLength(200, ErrorMessage = "扩展字段12长度不能超出200字符")]
		//public string extend12 { get; set; }

		///// <summary>
		///// 扩展字段13
		///// </summary>
		//[Column("EXTEND13")]
		//[StringLength(200, ErrorMessage = "扩展字段13长度不能超出200字符")]
		//public string extend13 { get; set; }

		///// <summary>
		///// 扩展字段14
		///// </summary>
		//[Column("EXTEND14")]
		//[StringLength(200, ErrorMessage = "扩展字段14长度不能超出200字符")]
		//public string extend14 { get; set; }

		///// <summary>
		///// 扩展字段15
		///// </summary>
		//[Column("EXTEND15")]
		//[StringLength(200, ErrorMessage = "扩展字段15长度不能超出200字符")]
		//public string extend15 { get; set; }

		///// <summary>
		///// 扩展字段2
		///// </summary>
		//[Column("EXTEND2")]
		//[StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
		//public string extend2 { get; set; }

		///// <summary>
		///// 扩展字段3
		///// </summary>
		//[Column("EXTEND3")]
		//[StringLength(200, ErrorMessage = "扩展字段3长度不能超出200字符")]
		//public string extend3 { get; set; }

		///// <summary>
		///// 扩展字段4
		///// </summary>
		//[Column("EXTEND4")]
		//[StringLength(200, ErrorMessage = "扩展字段4长度不能超出200字符")]
		//public string extend4 { get; set; }

		///// <summary>
		///// 扩展字段5
		///// </summary>
		//[Column("EXTEND5")]
		//[StringLength(200, ErrorMessage = "扩展字段5长度不能超出200字符")]
		//public string extend5 { get; set; }

		///// <summary>
		///// 扩展字段6
		///// </summary>
		//[Column("EXTEND6")]
		//[StringLength(200, ErrorMessage = "扩展字段6长度不能超出200字符")]
		//public string extend6 { get; set; }

		///// <summary>
		///// 扩展字段7
		///// </summary>
		//[Column("EXTEND7")]
		//[StringLength(200, ErrorMessage = "扩展字段7长度不能超出200字符")]
		//public string extend7 { get; set; }

		///// <summary>
		///// 扩展字段8
		///// </summary>
		//[Column("EXTEND8")]
		//[StringLength(200, ErrorMessage = "扩展字段8长度不能超出200字符")]
		//public string extend8 { get; set; }

		///// <summary>
		///// 扩展字段9
		///// </summary>
		//[Column("EXTEND9")]
		//[StringLength(200, ErrorMessage = "扩展字段9长度不能超出200字符")]
		//public string extend9 { get; set; }

		/// <summary>
		/// 装载类型;1:实盘 ；2:工装；0：空盘；
		/// </summary>
		[Column("LOADED_TYPE")]
		//[StringLength(50, ErrorMessage = "装载类型;1:实盘 ；2:工装；0：空盘；长度不能超出50字符")]
		public int? loadedType { get; set; }

		/// <summary>
		/// 是否允许人工上架;0默认不允许，1允许
		/// </summary>
		[Column("MANUAL_FLAG")]
		[Required]
		public int?  manualFlag { get; set; }

		/// <summary>
		/// 上线站台：WCS请求时的站台
		/// </summary>
		[Column("ONLINE_LOC_NO")]
		[StringLength(100, ErrorMessage = "上线站台：WCS请求时的站台长度不能超出100字符")]
		public string onlineLocNo { get; set; }

		/// <summary>
		/// 上线方式;0自动上线；1人工上线；2组盘上线；3直接上架
		/// </summary>
		[Column("ONLINE_METHOD")]
		[StringLength(50, ErrorMessage = "上线方式;0自动上线；1人工上线；2组盘上线；3直接上架长度不能超出50字符")]
		public string onlineMethod { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 上架库区编号
		/// </summary>
		[Column("PTA_REGION_NO")]
		[StringLength(100, ErrorMessage = "上架库区编号长度不能超出100字符")]
		public string ptaRegionNo { get; set; }

		/// <summary>
		/// 上架单编号
		/// </summary>
		[Column("PUTAWAY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "上架单编号长度不能超出100字符")]
		public string putawayNo { get; set; }

		/// <summary>
		/// 状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成
		/// </summary>
		[Column("PUTAWAY_STATUS")]
		[Required]
		//[StringLength(50, ErrorMessage = "状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成长度不能超出50字符")]
		public int putawayStatus { get; set; } = 0;

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
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
