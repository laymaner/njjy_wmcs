using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_OUT_INVOICE_RECORD_HIS")]
    public class WmsOutInvoiceRecordHis : BasePoco
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
        /// 分配数量
        /// </summary>
        [Column("ALLOT_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? allotQty { get; set; }

        /// <summary>
        /// 分配方式;分配方式：0：自动分配；1：手动分配；2：波次分配；3：差异补料
        /// </summary>
        [Column("ALLOT_TYPE")]
        //[StringLength(50, ErrorMessage = "分配方式;分配方式：0：自动分配；1：手动分配；2：波次分配；3：差异补料长度不能超出50字符")]
        public int? allotType { get; set; }

        /// <summary>
        /// 单据类型编码
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [StringLength(100, ErrorMessage = "单据类型编码长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 区域编码(楼号)
        /// </summary>
        [Column("AREA_NO")]
        [Required]
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
        /// 库位号
        /// </summary>
        [Column("BIN_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库位号长度不能超出100字符")]
        public string binNo { get; set; }

        /// <summary>
        /// 发货站台
        /// </summary>
        [Column("DELIVERY_LOC_NO")]
        [StringLength(100, ErrorMessage = "发货站台长度不能超出100字符")]
        public string deliveryLocNo { get; set; }

        /// <summary>
        /// ERP货位
        /// </summary>
        //[Column("ERP_BIN_NO")]
        //[StringLength(100, ErrorMessage = "ERP货位长度不能超出100字符")]
        //public string erpBinNo { get; set; }

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
        /// 成品名称
        /// </summary>
        [Column("FP_NAME")]
        [StringLength(500, ErrorMessage = "成品名称长度不能超出500字符")]
        public string fpName { get; set; }

        /// <summary>
        /// 成品编码
        /// </summary>
        [Column("FP_NO")]
        [StringLength(100, ErrorMessage = "成品编码长度不能超出100字符")]
        public string fpNo { get; set; }

        /// <summary>
        /// 成品数量
        /// </summary>
        [Column("FP_QTY", TypeName = "decimal(18,3)")]
        public decimal? fpQty { get; set; }

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
        /// 发货单明细ID
        /// </summary>
        [Column("INVOICE_DTL_ID")]
        //[Required]
        //[StringLength(36, ErrorMessage = "发货单明细ID长度不能超出36字符")]
        //public string invoiceDtlId { get; set; }
        public Int64? invoiceDtlId { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        [Column("INVOICE_NO")]
        //[Required]
        //[StringLength(100, ErrorMessage = "发货单号长度不能超出100字符")]
        public string invoiceNo { get; set; }

        /// <summary>
        /// 是否指定批次出库;0：不指定；1：指定
        /// </summary>
        [Column("IS_BATCH")]
        [StringLength(50, ErrorMessage = "是否指定批次出库;0：不指定；1：指定长度不能超出50字符")]
        public int? isBatch { get; set; }

        /// <summary>
        /// 任务下发结果（存储过程执行结果）
        /// </summary>
        [Column("ISSUED_RESULT")]
        [StringLength(2000, ErrorMessage = "任务下发结果（存储过程执行结果）长度不能超出2000字符")]
        public string issuedResult { get; set; }

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
        //public string orderDtlId { get; set; }
        public Int64? orderDtlId { get; set; }

        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("ORDER_NO")]
        [StringLength(100, ErrorMessage = "关联单号长度不能超出100字符")]
        public string orderNo { get; set; }

        /// <summary>
        /// 出库记录状态;0：初始创建；31：下架中；39：下架完成；40：待拣选；41：拣选中；90：拣选完成；92删除；93强制完成
        /// </summary>
        [Column("OUT_RECORD_STATUS")]
        [Required]
        //[StringLength(50, ErrorMessage = "出库记录状态;0：初始创建；31：下架中；39：下架完成；40：待拣选；41：拣选中；90：拣选完成；92删除；93强制完成长度不能超出50字符")]
        public int? outRecordStatus { get; set; } = 0;

        /// <summary>
        /// 载体条码
        /// </summary>
        [Column("PALLET_BARCODE")]
        [Required]
        [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
        public string palletBarcode { get; set; }

        /// <summary>
        /// 托盘拣选类型;托盘拣选类型：0：整出，1：拣选，针对载体
        /// </summary>
        [Column("PALLET_PICK_TYPE")]
        [Required]
        //[StringLength(50, ErrorMessage = "托盘拣选类型;托盘拣选类型：0：整出，1：拣选，针对载体长度不能超出50字符")]
        public int? palletPickType { get; set; }

        /// <summary>
        /// 拣选站台
        /// </summary>
        [Column("PICK_LOC_NO")]
        [StringLength(100, ErrorMessage = "拣选站台长度不能超出100字符")]
        public string pickLocNo { get; set; }

        /// <summary>
        /// 拣选数量
        /// </summary>
        [Column("PICK_QTY", TypeName = "decimal(18,3)")]
        [Required]
        public decimal? pickQty { get; set; }

        /// <summary>
        /// 拣选任务编号;就是自动生成序列号
        /// </summary>
        [Column("PICK_TASK_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "拣选任务编号;就是自动生成序列号长度不能超出100字符")]
        public string pickTaskNo { get; set; }

        /// <summary>
        /// 物料拣选类型;物料拣选类型：0：整出，1：拣选，针对物料
        /// </summary>
        [Column("PICK_TYPE")]
        [Required]
        //[StringLength(50, ErrorMessage = "物料拣选类型;物料拣选类型：0：整出，1：拣选，针对物料长度不能超出50字符")]
        public int? pickType { get; set; }

        /// <summary>
        /// 预分配库存明细ID（占用数量根据此字段进行增减）
        /// </summary>
        [Column("PRE_STOCK_DTL_ID")]
        //[Required]
        //[StringLength(36, ErrorMessage = "预分配库存明细ID（占用数量根据此字段进行增减）长度不能超出36字符")]
        //public string preStockDtlId { get; set; }
        public Int64 preStockDtlId { get; set; }

        /// <summary>
        /// 生产部门编码
        /// </summary>
        [Column("PRODUCT_DEPT_CODE")]
        [StringLength(100, ErrorMessage = "生产部门编码长度不能超出100字符")]
        public string productDeptCode { get; set; }

        /// <summary>
        /// 生产部门名称
        /// </summary>
        [Column("PRODUCT_DEPT_NAME")]
        [StringLength(500, ErrorMessage = "生产部门名称长度不能超出500字符")]
        public string productDeptName { get; set; }

        /// <summary>
        /// 生产地点（厂内/厂外）
        /// </summary>
        [Column("PRODUCT_LOCATION")]
        [StringLength(50, ErrorMessage = "生产地点（厂内/厂外）长度不能超出50字符")]
        public string productLocation { get; set; }

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
        /// 库区编号
        /// </summary>
        [Column("REGION_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 反拣标记：0不反拣，1反拣
        /// </summary>
        [Column("REVERSE_PICK_FLAG")]
        //[StringLength(50, ErrorMessage = "反拣标记：0不反拣，1反拣长度不能超出50字符")]
        public int? reversePickFlag { get; set; }

        /// <summary>
        /// SKU编码
        /// </summary>
        [Column("SKU_CODE")]
        [StringLength(500, ErrorMessage = "SKU编码长度不能超出500字符")]
        public string skuCode { get; set; }

        /// <summary>
        /// 数据来源;0：内部创建WMS，1：外部系统
        /// </summary>
        [Column("SOURCE_BY")]
        //[StringLength(50, ErrorMessage = "数据来源;0：内部创建WMS，1：外部系统长度不能超出50字符")]
        public int? sourceBy { get; set; }

        /// <summary>
        /// 库存编码
        /// </summary>
        [Column("STOCK_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
        public string stockCode { get; set; }

        /// <summary>
        /// 实际拣选库存明细ID
        /// </summary>
        [Column("STOCK_DTL_ID")]
        //[StringLength(2000, ErrorMessage = "实际拣选库存明细ID长度不能超出2000字符")]
        //public string stockDtlId { get; set; }
        public Int64 stockDtlId { get; set; }

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
        /// 供应类型
        /// </summary>
        [Column("SUPPLY_TYPE")]
        [StringLength(50, ErrorMessage = "供应类型长度不能超出50字符")]
        public string supplyType { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        [Column("TICKET_NO")]
        [StringLength(100, ErrorMessage = "工单号长度不能超出100字符")]
        public string ticketNo { get; set; }

        /// <summary>
        /// 工单计划开始时间
        /// </summary>
        [Column("TICKET_PLAN_BEGIN_TIME")]
        public DateTime? ticketPlanBeginTime { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        [Column("TICKET_TYPE")]
        [StringLength(50, ErrorMessage = "工单类型长度不能超出50字符")]
        public string ticketType { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [Column("UNIT_CODE")]
        [Display(Name = "单位编码")]
        [StringLength(50, ErrorMessage = "单位编码长度不能超出50字符")]
        public string unitCode { get; set; }

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

        /// <summary>
        /// 操作方式
        /// </summary>
        [Column("OPERATION_MODE")]
        [StringLength(50, ErrorMessage = "操作方式长度不能超出50字符")]
        public string operationMode { get; set; }

        /// <summary>
        /// 出库条码
        /// </summary>
        [Column("OUT_BARCODE")]
        [StringLength(50, ErrorMessage = "出库条码长度不能超出50字符")]
        public string outBarCode { get; set; }

        /// <summary>
        /// 急料标记
        /// </summary>
        [Column("URGENT_FLAG")]
        [StringLength(50, ErrorMessage = "急料标记长度不能超出50字符")]
        public int? urgentFlag { get; set; }

        /// <summary>
        /// 装载状态 : 1:实盘 ；2:工装；0：空盘；
        /// </summary>
        [Column("LOADED_TYPE")]
        [Display(Name = "装载状态")]
        //[StringLength(50, ErrorMessage = "装载状态长度不能超出50字符")]
        public int? loadedTtype { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [Column("PRODUCT_SN")]
        [Display(Name = "序列号")]
        [StringLength(100, ErrorMessage = "序列号长度不能超出100字符")]
        public string productSn { get; set; }

        /// <summary>
        /// 灯光颜色
        /// </summary>
        [Column("LIGHT_COLOR")]
        [Display(Name = "灯光颜色")]
        [StringLength(100, ErrorMessage = "灯光颜色长度不能超出100字符")]
        public string lightColor { get; set; }
    }
}