using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_OUT_INVOICE_DTL_HIS")]
    public class WmsOutInvoiceDtlHis : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 分配结果
        /// </summary>
        [Column("ALLOCAT_RESULT")]
        [StringLength(2000, ErrorMessage = "分配结果长度不能超出2000字符")]
        public string allocatResult { get; set; }

        /// <summary>
        /// 已分配数量
        /// </summary>
        [Column("ALLOT_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? allotQty { get; set; }

        /// <summary>
        /// 区域编码(楼号)
        /// </summary>
        [Column("AREA_NO")]
        [StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 装配顺序
        /// </summary>
        [Column("ASSEMBLY_IDX")]
        [StringLength(100, ErrorMessage = "装配顺序长度不能超出100字符")]
        public string assemblyIdx { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Column("BATCH_NO")]
        [StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
        public string batchNo { get; set; }

        /// <summary>
        /// 归属事业部
        /// </summary>
        [Column("BELONG_DEPARTMENT")]
        [StringLength(100, ErrorMessage = "归属事业部长度不能超出100字符")]
        public string belongDepartment { get; set; }

        /// <summary>
        /// 已完成数量
        /// </summary>
        [Column("COMPLETE_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? completeQty { get; set; }

        /// <summary>
        /// ERP未发数量
        /// </summary>
        [Column("ERP_UNDELIVER_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? erpUndeliverQty { get; set; }

        /// <summary>
        /// ERP仓库
        /// </summary>
        [Column("ERP_WHOUSE_NO")]
        [Required]
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
        /// 外部出库单行号
        /// </summary>
        [Column("EXTERNAL_OUT_DTL_ID")]
        [StringLength(100, ErrorMessage = "外部出库单行号长度不能超出100字符")]
        public string externalOutDtlId { get; set; }

        /// <summary>
        /// 外部出库单号
        /// </summary>
        [Column("EXTERNAL_OUT_NO")]
        [StringLength(100, ErrorMessage = "外部出库单号长度不能超出100字符")]
        public string externalOutNo { get; set; }

        /// <summary>
        /// 发货单明细状态;0：初始创建；21：分配中；29：分配完成；41：拣选中；49：拣选完成；51：发货中；90：出库完成；92删除；93强制完成
        /// </summary>
        [Column("INVOICE_DTL_STATUS")]
        [Required]
        //[StringLength(50, ErrorMessage = "发货单明细状态;0：初始创建；21：分配中；29：分配完成；41：拣选中；49：拣选完成；51：发货中；90：出库完成；92删除；93强制完成长度不能超出50字符")]
        public int? invoiceDtlStatus { get; set; } = 0;

        /// <summary>
        /// 发货单号
        /// </summary>
        [Column("INVOICE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "发货单号长度不能超出100字符")]
        public string invoiceNo { get; set; }

        /// <summary>
        /// 单据数量
        /// </summary>
        [Column("INVOICE_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? invoiceQty { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("MATERIAL_NAME")]
        [StringLength(500, ErrorMessage = "物料名称长度不能超出500字符")]
        public string materialName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("MATERIAL_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
        public string materialCode { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("MATERIAL_SPEC")]
        [StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
        public string materialSpec { get; set; }

        /// <summary>
        /// 关联单行号
        /// </summary>
        [Column("ORDER_DTL_ID")]
        //[StringLength(100, ErrorMessage = "关联单行号长度不能超出100字符")]
        //public string orderId { get; set; }
        public Int64? orderId { get; set; }

        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("ORDER_NO")]
        [StringLength(100, ErrorMessage = "关联单号长度不能超出100字符")]
        public string orderNo { get; set; }
        
        /// <summary>
        /// 生产地点（厂内/厂外）;接口传什么就是什么
        /// </summary>
        [Column("PRODUCT_LOCATION")]
        [StringLength(50, ErrorMessage = "生产地点（厂内/厂外）;接口传什么就是什么长度不能超出50字符")]
        public string productLocation { get; set; }

        /// <summary>
        /// 生产部门编码;接口传过来的直接显示
        /// </summary>
        [Column("PRODUCT_DEPT_CODE")]
        [StringLength(100, ErrorMessage = "生产部门编码;接口传过来的直接显示长度不能超出100字符")]
        public string productDeptCode { get; set; }

        /// <summary>
        /// 生产部门名称
        /// </summary>
        [Column("PRODUCT_DEPT_NAME")]
        [StringLength(500, ErrorMessage = "生产部门名称长度不能超出500字符")]
        public string productDeptName { get; set; }

        /// <summary>
        /// 项目号
        /// </summary>
        [Column("PROJECT_NO")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
        public string projectNo { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
        public string proprietorCode { get; set; }

        /// <summary>
        /// 已下架数量
        /// </summary>
        [Column("PUTDOWN_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? putdownQty { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [Column("PRODUCT_SN")]
        [StringLength(100, ErrorMessage = "序列号长度不能超出100字符")]
        public string productSn { get; set; }

        /// <summary>
        /// 原序列号
        /// </summary>
        [Column("ORIGINAL_SN")]
        [StringLength(4000, ErrorMessage = "序列号长度不能超出4000字符")]
        public string originalSn { get; set; }

        /// <summary>
        /// 供应类型
        /// </summary>
        [Column("SUPPLY_TYPE")]
        [StringLength(50, ErrorMessage = "供应类型长度不能超出50字符")]
        public string supplyType { get; set; }

        /// <summary>
        /// 工单计划开始时间
        /// </summary>
        [Column("TICKET_PLAN_BEGIN_TIME")]
        public DateTime? ticketPlanBeginTime { get; set; }

        /// <summary>
        /// 波次号
        /// </summary>
        [Column("WAVE_NO")]
        [StringLength(100, ErrorMessage = "波次号长度不能超出100字符")]
        public string waveNo { get; set; }

        /// <summary>
        /// 仓库号
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
        public string whouseNo { get; set; }

        //**********0916新增字段**********
        /// <summary>
        /// 公司代码，固定值：A01，WMS不做任何处理，只做记录
        /// </summary>
        [Column("COMPANY_CODE")]
        [StringLength(100, ErrorMessage = "公司代码长度不能超出100字符")]
        public string companyCode { get; set; }

        /// <summary>
        /// 接口id，WMS不做任何处理，只做记录
        /// </summary>
        [Column("INTF_ID")]
        [StringLength(100, ErrorMessage = "接口id，WMS不做任何处理，只做记录长度不能超出100字符")]
        public string intfId { get; set; }

        /// <summary>
        /// 批次id，WMS不做任何处理，只做记录
        /// </summary>
        [Column("INTF_BATCH_ID")]
        [StringLength(100, ErrorMessage = "批次id，WMS不做任何处理，只做记录长度不能超出100字符")]
        public string intfBatchId { get; set; }

        //**********0917新增字段**********
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("SUPPLIER_CODE")]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
        public string supplierCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("SUPPLIER_NAME")]
        [StringLength(500, ErrorMessage = "供应商名称长度不能超出500字符")]
        public string supplierName { get; set; }

        /// <summary>
        /// 供应商名称-英文
        /// </summary>
        [Column("SUPPLIER_NAME_EN")]
        [StringLength(500, ErrorMessage = "供应商名称-英文长度不能超出500字符")]
        public string supplierNameEn { get; set; }

        /// <summary>
        /// 供应商名称-其他
        /// </summary>
        [Column("SUPPLIER_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "供应商名称-其他长度不能超出500字符")]
        public string supplierNameAlias { get; set; }

        /// <summary>
		/// 工单号
		/// </summary>
		[Column("TICKET_NO")]
        [StringLength(100, ErrorMessage = "工单号长度不能超出100字符")]
        public string ticketNo { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [Column("UNIT_CODE")]
        [Display(Name = "单位编码")]
        [StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
        public string unitCode { get; set; }
    }
}