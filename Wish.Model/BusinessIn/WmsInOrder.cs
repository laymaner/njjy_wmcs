using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_IN_ORDER")]
    [Index(nameof(inNo), IsUnique = true)]
    public class WmsInOrder : BasePoco
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
        [Display(Name = "区域编码")]
        public string areaNo { get; set; }

		/// <summary>
		/// 客供编码
		/// </summary>
		[Column("CV_CODE")]
		[StringLength(100, ErrorMessage = "客供编码长度不能超出100字符")]
		public string cvCode { get; set; }

		/// <summary>
		/// 客供名称
		/// </summary>
		[Column("CV_NAME")]
		[StringLength(500, ErrorMessage = "客供名称长度不能超出500字符")]
		public string cvName { get; set; }

		/// <summary>
		/// 供方名称-其他
		/// </summary>
		[Column("CV_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "供方名称-其他长度不能超出500字符")]
		public string cvNameAlias { get; set; }

		/// <summary>
		/// 客供名称-英文
		/// </summary>
		[Column("CV_NAME_EN")]
		[StringLength(500, ErrorMessage = "客供名称-英文长度不能超出500字符")]
		public string cvNameEn { get; set; }

		/// <summary>
		/// 客供类型;供应商、产线、客户
		/// </summary>
		[Column("CV_TYPE")]
        [Display(Name = "客供类型")]
        [StringLength(100, ErrorMessage = "客供类型;供应商、产线、客户长度不能超出100字符")]
		public string cvType { get; set; }

		/// <summary>
		/// 送货方式:
		/// </summary>
		[Column("DELIVER_MODE")]
		[StringLength(50, ErrorMessage = "送货方式:长度不能超出50字符")]
		public string deliverMode { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		[Required]
        [Display(Name = "单据类型")]
        [StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
		public string docTypeCode { get; set; }

        /*
		/// <summary>
		/// ERP仓库编码
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "ERP仓库编码长度不能超出100字符")]
		public string erpWhouseNo { get; set; }
		*/
        /// <summary>
        /// ERP仓库
        /// </summary>
        [NotMapped]
        [Display(Name = "ERP仓库")]
        public string erpWhouseNo { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [Column("EXTEND1")]
		[StringLength(200, ErrorMessage = "扩展字段1长度不能超出200字符")]
		public string extend1 { get; set; }

		/// <summary>
		/// 扩展字段10
		/// </summary>
		[Column("EXTEND10")]
		[StringLength(200, ErrorMessage = "扩展字段10长度不能超出200字符")]
		public string extend10 { get; set; }

		/// <summary>
		/// 扩展字段11
		/// </summary>
		[Column("EXTEND11")]
		[StringLength(200, ErrorMessage = "扩展字段11长度不能超出200字符")]
		public string extend11 { get; set; }

		/// <summary>
		/// 扩展字段12
		/// </summary>
		[Column("EXTEND12")]
		[StringLength(200, ErrorMessage = "扩展字段12长度不能超出200字符")]
		public string extend12 { get; set; }

		/// <summary>
		/// 扩展字段13
		/// </summary>
		[Column("EXTEND13")]
		[StringLength(200, ErrorMessage = "扩展字段13长度不能超出200字符")]
		public string extend13 { get; set; }

		/// <summary>
		/// 扩展字段14
		/// </summary>
		[Column("EXTEND14")]
		[StringLength(200, ErrorMessage = "扩展字段14长度不能超出200字符")]
		public string extend14 { get; set; }

		/// <summary>
		/// 扩展字段15
		/// </summary>
		[Column("EXTEND15")]
		[StringLength(200, ErrorMessage = "扩展字段15长度不能超出200字符")]
		public string extend15 { get; set; }

		/// <summary>
		/// 扩展字段2
		/// </summary>
		[Column("EXTEND2")]
		[StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
		public string extend2 { get; set; }

		/// <summary>
		/// 扩展字段3
		/// </summary>
		[Column("EXTEND3")]
		[StringLength(200, ErrorMessage = "扩展字段3长度不能超出200字符")]
		public string extend3 { get; set; }

		/// <summary>
		/// 扩展字段4
		/// </summary>
		[Column("EXTEND4")]
		[StringLength(200, ErrorMessage = "扩展字段4长度不能超出200字符")]
		public string extend4 { get; set; }

		/// <summary>
		/// 扩展字段5
		/// </summary>
		[Column("EXTEND5")]
		[StringLength(200, ErrorMessage = "扩展字段5长度不能超出200字符")]
		public string extend5 { get; set; }

		/// <summary>
		/// 扩展字段6
		/// </summary>
		[Column("EXTEND6")]
		[StringLength(200, ErrorMessage = "扩展字段6长度不能超出200字符")]
		public string extend6 { get; set; }

		/// <summary>
		/// 扩展字段7
		/// </summary>
		[Column("EXTEND7")]
		[StringLength(200, ErrorMessage = "扩展字段7长度不能超出200字符")]
		public string extend7 { get; set; }

		/// <summary>
		/// 扩展字段8
		/// </summary>
		[Column("EXTEND8")]
		[StringLength(200, ErrorMessage = "扩展字段8长度不能超出200字符")]
		public string extend8 { get; set; }

		/// <summary>
		/// 扩展字段9
		/// </summary>
		[Column("EXTEND9")]
		[StringLength(200, ErrorMessage = "扩展字段9长度不能超出200字符")]
		public string extend9 { get; set; }

		/// <summary>
		/// 外部入库单ID
		/// </summary>
		[Column("EXTERNAL_IN_ID")]
		[StringLength(100, ErrorMessage = "外部入库单ID长度不能超出100字符")]
		public string externalInId { get; set; }

		/// <summary>
		/// 外部入库单号
		/// </summary>
		[Column("EXTERNAL_IN_NO")]
        [Display(Name = "外部入库单号")]
        [StringLength(100, ErrorMessage = "外部入库单号长度不能超出100字符")]
		public string externalInNo { get; set; }

		/// <summary>
		/// 入库单号
		/// </summary>
		[Column("IN_NO")]
		[Required]
        [Display(Name = "入库单号")]
        [StringLength(100, ErrorMessage = "入库单号长度不能超出100字符")]
		public string inNo { get; set; }

		/// <summary>
		/// 出入库名称
		/// </summary>
		[Column("IN_OUT_NAME")]
		[StringLength(500, ErrorMessage = "出入库名称长度不能超出500字符")]
		public string inOutName { get; set; }

		/// <summary>
		/// 出入库类别代码
		/// </summary>
		[Column("IN_OUT_TYPE_NO")]
		[StringLength(100, ErrorMessage = "出入库类别代码长度不能超出100字符")]
		public string inOutTypeNo { get; set; }

		/// <summary>
		/// 入库单状态;0初始创建，11收货中，19收货完成，21验收中，29验收完成，31组盘中，39组盘完成，41入库中，90入库完成；92删除；93强制完成
		/// </summary>
		[Column("IN_STATUS")]
		[Required]
        [Display(Name = "入库单状态")]
		public int inStatus { get; set; } = 0;

		/// <summary>
		/// 操作原因
		/// </summary>
		[Column("OPERATION_REASON")]
        [Display(Name = "操作原因")]
        [StringLength(255, ErrorMessage = "操作原因长度不能超出255字符")]
		public string operationReason { get; set; }

		/// <summary>
		/// 备注说明
		/// </summary>
		[Column("ORDER_DESC")]
        [Display(Name = "备注说明")]
        [StringLength(255, ErrorMessage = "备注说明长度不能超出255字符")]
		public string orderDesc { get; set; }

		/// <summary>
		/// 预计到货日期
		/// </summary>
		[Column("PLAN_ARRIVAL_DATE")]
		public DateTime? planArrivalDate { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		///// <summary>
		///// 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传;回传SRM用
		///// </summary>
		//[Column("RETURN_FLAG")]
		//[StringLength(50, ErrorMessage = "回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传;回传SRM用长度不能超出50字符")]
		//public string returnFlag { get; set; }

		/// <summary>
		/// 数据来源
		/// </summary>
		[Column("SOURCE_BY")]
		[Required]
        [Display(Name = "数据来源")]
		public int? sourceBy { get; set; }

		/// <summary>
		/// 工单号
		/// </summary>
		[Column("TICKET_NO")]
		[StringLength(100, ErrorMessage = "工单号长度不能超出100字符")]
		public string ticketNo { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
