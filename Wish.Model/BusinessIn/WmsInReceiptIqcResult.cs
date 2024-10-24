using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_IN_RECEIPT_IQC_RESULT")]
    [Index(nameof(iqcResultNo), IsUnique = true)]
    public class WmsInReceiptIqcResult : BasePoco
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
        [Display(Name = "楼号")]
        public string areaNo { get; set; }

        /// <summary>
        /// 不良说明
        /// </summary>
        [Column("BAD_DESCRIPTION")]
        [Display(Name = "不良说明")]
        [StringLength(255, ErrorMessage = "不良说明长度不能超出255字符")]
        public string badDescription { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Column("BATCH_NO")]
		[StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
        [Display(Name = "批次")]
        public string batchNo { get; set; }

		/// <summary>
		/// 库位号
		/// </summary>
		[Column("BIN_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库位号长度不能超出100字符")]
        [Display(Name = "库位号")]
        public string binNo { get; set; }

		///// <summary>
		///// 删除标志;0-有效,1-已删除
		///// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")] 
		//public string delFlag { get; set; } = "0";

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("DEPARTMENT_NAME")]
        [Display(Name = "部门名称")]
        [StringLength(500, ErrorMessage = "部门名称长度不能超出500字符")]
		public string departmentName { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
        //[Display(Name = "单据类型")]
        public string docTypeCode { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[Required]
        [Display(Name = "ERP仓库")]
        [StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
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
        /// 扩展字段15------作为备注说明
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
		/// 外部入库单行号
		/// </summary>
		//[Column("EXTERNAL_IN_ID")]
		[Column("EXTERNAL_IN_DTL_ID")]
        [Display(Name = "外部入库单行号")]
        [StringLength(100, ErrorMessage = "外部入库单行号长度不能超出100字符")]
		public string externalInDtlId { get; set; }

		/// <summary>
		/// 外部入库单号
		/// </summary>
		[Column("EXTERNAL_IN_NO")]
        [Display(Name = "外部入库单号")]
        [StringLength(100, ErrorMessage = "外部入库单号长度不能超出100字符")]
		public string externalInNo { get; set; }

		///// <summary>
		///// 入库完成数量
		///// </summary>
		//[Column("IN_COMPLETE_QTY", TypeName = "decimal(18,3)")]
  //      [Display(Name = "入库完成数量")]
  //      public decimal?  inCompleteQty { get; set; }

		/// <summary>
		/// 入库单明细ID
		/// </summary>
		[Column("IN_DTL_ID")]
		[Required]
		//[StringLength(36, ErrorMessage = "入库单明细ID长度不能超出36字符")]
		//public string inDtlId { get; set; }
		public Int64 inDtlId { get; set; }

		/// <summary>
		/// 入库单号
		/// </summary>
		[Column("IN_NO")]
		[Required]
        [Display(Name = "WMS入库单号")]
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
		/// 质检结果;待检、合格、不合格
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质检结果;待检、合格、不合格长度不能超出50字符")]
		public int? inspectionResult { get; set; }

		/// <summary>
		/// 质检员
		/// </summary>
		[Column("INSPECTOR")]
        [Display(Name = "质检员")]
        [StringLength(100, ErrorMessage = "质检员长度不能超出100字符")]
		public string inspector { get; set; }

		/// <summary>
		/// 检验记录单号;也可以通过收货单明细ID关联质检记录单
		/// </summary>
		[Column("IQC_RECORD_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "检验记录单号;也可以通过收货单明细ID关联质检记录单长度不能超出100字符")]
        [Display(Name = "WMS检验记录单号")]
        public string iqcRecordNo { get; set; }

		/// <summary>
		/// 检验结果单号
		/// </summary>
		[Column("IQC_RESULT_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "检验结果单号长度不能超出100字符")]
        [Display(Name = "WMS检验结果单号")]
        public string iqcResultNo { get; set; }

		/// <summary>
		/// 检验结果状态;0初始创建，31组盘中，39组盘完成，41入库中，90入库完成；92删除；93强制完成
		/// </summary>
		[Column("IQC_RESULT_STATUS")]
		//[Required]
		//[StringLength(50, ErrorMessage = "检验结果状态;0初始创建，31组盘中，39组盘完成，41入库中，90入库完成；92删除；93强制完成长度不能超出50字符")]
        [Display(Name = "检验结果状态")]
        public int iqcResultStatus { get; set; } = 0;

		/// <summary>
		/// 质检方式;WMS质检、ERP质检
		/// </summary>
		[Column("IQC_TYPE")]
		[StringLength(50, ErrorMessage = "质检方式;WMS质检、ERP质检长度不能超出50字符")]
        [Display(Name = "质检方式")]
        public string iqcType { get; set; }

		/// <summary>
		/// 直接退货标记;0：否  1：是
		/// </summary>
		[Column("IS_RETURN_FLAG")]
		//[StringLength(50, ErrorMessage = "直接退货标记;0：否  1：是长度不能超出50字符")]
		public int? isReturnFlag { get; set; }

		/// <summary>
		/// 物料名称
		/// </summary>
		[Column("MATERIAL_NAME")]
        [Display(Name = "物料名称")]
        [StringLength(500, ErrorMessage = "物料名称长度不能超出500字符")]
		public string materialName { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_CODE")]
		[Required]
        [Display(Name = "物料编码")]
        [StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }

		/// <summary>
		/// 规格型号
		/// </summary>
		[Column("MATERIAL_SPEC")]
        [Display(Name = "规格")]
        [StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
		public string materialSpec { get; set; }

		/// <summary>
		/// 备注说明
		/// </summary>
		[Column("ORDER_DESC")]
		[StringLength(255, ErrorMessage = "备注说明长度不能超出255字符")]
		public string orderDesc { get; set; }

		/// <summary>
		/// 关联单行号
		/// </summary>
		[Column("ORDER_DTL_ID")]
		[StringLength(100, ErrorMessage = "关联单行号长度不能超出100字符")]
		public Int64? orderDtlId { get; set; }

		/// <summary>
		/// 关联单号
		/// </summary>
		[Column("ORDER_NO")]
        [Display(Name = "关联单号")]
        [StringLength(100, ErrorMessage = "关联单号长度不能超出100字符")]
		public string orderNo { get; set; }

		/// <summary>
		/// 包装数量
		/// </summary>
		[Column("MIN_PKG_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "包装数量")]
        public decimal?  minPkgQty { get; set; }

		/// <summary>
		/// 已回传数量
		/// </summary>
		[Column("POST_BACK_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "已回传数量")]
        public decimal?  postBackQty { get; set; }

		/// <summary>
		/// 成品序列号
		/// </summary>
		[Column("PRODUCT_SN")]
        [Display(Name = "成品序列号")]
        [StringLength(1000, ErrorMessage = "成品序列号长度不能超出1000字符")]
		public string productSn { get; set; }

		/// <summary>
		/// 项目号
		/// </summary>
		[Column("PROJECT_NO")]
		[StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
        [Display(Name = "项目号")]
        public string projectNo { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 上架数量
		/// </summary>
		[Column("PUTAWAY_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "上架数量")]
        public decimal?  putawayQty { get; set; } 
		
		/// <summary>
		/// 数量;随检验结果对应合格数量/不合格数量，一条质检记录可以有多条质检结果
		/// </summary>
		[Column("QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "数量")]
        public decimal?  qty { get; set; } 

		/// <summary>
		/// 收货单明细ID;明细与质检记录一一对应 ,通过这个关联
		/// </summary>
		[Column("RECEIPT_DTL_ID")]
		[Required]
		//[StringLength(36, ErrorMessage = "收货单明细ID;明细与质检记录一一对应 ,通过这个关联长度不能超出36字符")]
		//public string receiptDtlId { get; set; }
		public Int64 receiptDtlId { get; set; }

		/// <summary>
		/// 收货单编号
		/// </summary>
		[Column("RECEIPT_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "收货单编号长度不能超出100字符")]
        [Display(Name = "WMS收货单号")]
        public string receiptNo { get; set; }

		/// <summary>
		/// 已组盘数量
		/// </summary>
		[Column("RECORD_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "已组盘数量")]
        public decimal?  recordQty { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
		[Column("REGION_NO")]
		[Required]
        [Display(Name = "库区编号")]
        [StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
		public string regionNo { get; set; }

		/// <summary>
		/// 直接退货数量
		/// </summary>
		[Column("RETURN_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "直接退货数量")]
        public decimal?  returnQty { get; set; }

		/// <summary>
		/// SKU 编码
		/// </summary>
		[Column("SKU_CODE")]
		[StringLength(500, ErrorMessage = "SKU 编码长度不能超出500字符")]
        //[Display(Name = "编码")]
        public string skuCode { get; set; }

		/// <summary>
		/// 数据来源;0：WMS，1：外部系统,2：到货通知单生成
		/// </summary>
		[Column("SOURCE_BY")]
		//[Required]
		//[StringLength(50, ErrorMessage = "数据来源;0：WMS，1：外部系统,2：到货通知单生成长度不能超出50字符")]
        [Display(Name = "数据来源")]
        public int? sourceBy { get; set; }

		/// <summary>
		/// 供应商名称
		/// </summary>
		[Column("SUPPLIER_NAME")]
        [Display(Name = "供应商名称")]
        [StringLength(500, ErrorMessage = "供应商名称长度不能超出500字符")]
		public string supplierName { get; set; }

		/// <summary>
		/// 供应商名称-其他
		/// </summary>
		[Column("SUPPLIER_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "供应商名称-其他长度不能超出500字符")]
		public string supplierNameAlias { get; set; }

		/// <summary>
		/// 供应商名称-英文
		/// </summary>
		[Column("SUPPLIER_NAME_EN")]
		[StringLength(500, ErrorMessage = "供应商名称-英文长度不能超出500字符")]
		public string supplierNameEn { get; set; }

		/// <summary>
		/// 供应商编码
		/// </summary>
		//[Column("SUPPLIER_NO")]
		[Column("SUPPLIER_CODE")]
        [Display(Name = "供应商编码")]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 工单号
		/// </summary>
		[Column("TICKET_NO")]
		[StringLength(100, ErrorMessage = "工单号长度不能超出100字符")]
		public string ticketNo { get; set; }
		
		/// <summary>
		/// 单位
		/// </summary>
		[Column("UNIT_CODE")]
		[StringLength(100, ErrorMessage = "工单号长度不能超出50字符")]
		public string unitCode { get; set; }

		/// <summary>
		/// 急料标记
		/// </summary>
		[Column("URGENT_FLAG")]
		//[StringLength(50, ErrorMessage = "急料标记长度不能超出50字符")]
        [Display(Name = "急料标记")]
        public int? urgentFlag { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
