using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_PRINT_TASK")]
    public class CfgPrintTask : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [Column("BARCODE")]
        [StringLength(100, ErrorMessage = "条码长度不能超出100字符")]
        public string barCode { get; set; }

        /// <summary>
        /// 存货代码
        /// </summary>
        [Column("MATERIAL_NO")]
        [StringLength(100, ErrorMessage = "存货代码长度不能超出100字符")]
        public string materialCode { get; set; }

        /// <summary>
        /// 存货名称
        /// </summary>
        [Column("MATERIAL_NAME")]
        [StringLength(500, ErrorMessage = "存货名称长度不能超出500字符")]
        public string materialName { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [Column("SN")]
        [StringLength(100, ErrorMessage = "序列号长度不能超出100字符")]
        public string sn { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Column("QTY", TypeName = "decimal(18,3)")]
        public decimal qty { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        [Column("UNIT")]
        [StringLength(100, ErrorMessage = "数量单位长度不能超出100字符")]
        public string unit { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("SUPPLIER_CODE")]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
        public string supplierCode { get; set; }

        /// <summary>
        /// 物料大类编码
        /// </summary>
        [Column("MATERIAL_CATEGORY_NO")]
        [StringLength(100, ErrorMessage = "物料大类编码长度不能超出100字符")]
        public string materialCategoryCode { get; set; }

        /// <summary>
        /// 楼号(楼号+打印机模板编码 是为了要确定打印机唯一性)
        /// </summary>
        [Column("AREA_NO")]
        [StringLength(100, ErrorMessage = "楼号长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 楼号名称
        /// </summary>
        [Column("AREA_NAME")]
        [StringLength(500, ErrorMessage = "楼号名称长度不能超出500字符")]
        public string areaName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("MATERIAL_SPEC")]
        [StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
        public string materialSpec { get; set; }

        /// <summary>
        /// 工程图号
        /// </summary>
        [Column("PROJECT_DRAWING_NO")]
        [StringLength(4000, ErrorMessage = "工程图号长度不能超出4000字符")]
        public string projectDrawingNo { get; set; }

        /// <summary>
        /// 项目号
        /// </summary>
        [Column("PROJECT_NO")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
        public string projectNo { get; set; }

        /// <summary>
        /// 急料标记
        /// </summary>
        [Column("URGENT_FLAG")]
        [StringLength(50, ErrorMessage = "急料标记长度不能超出50字符")]
        public string urgentFlag { get; set; }

        /// <summary>
        /// 归属事业部
        /// </summary>
        [Column("BELONG_DEPARTMENT")]
        [StringLength(100, ErrorMessage = "归属事业部长度不能超出100字符")]
        public string belongDepartment { get; set; }

        /// <summary>
        /// MSL等级编码
        /// </summary>
        [Column("MSL_GRADE_CODE")]
        [StringLength(100, ErrorMessage = "MSL等级编码长度不能超出100字符")]
        public string mslGradeCode { get; set; }

        /// <summary>
        /// DATACODE
        /// </summary>
        [Column("DATA_CODE")]
        [StringLength(100, ErrorMessage = "DATACODE长度不能超出100字符")]
        public string dataCode { get; set; }

        /// <summary>
        /// 预计到货日期
        /// </summary>
        [Column("PLAN_ARRIVAL_DATE")]
        public string planArrivalDate { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        [Column("ORDER_NO")]
        [StringLength(100, ErrorMessage = "工单号长度不能超出100字符")]
        public string orderNo { get; set; }

        /// <summary>
        /// 工单行号
        /// </summary>
        [Column("ORDER_LINE_NO")]
        [StringLength(100, ErrorMessage = "工单行号长度不能超出100字符")]
        public string orderLineNo { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Column("LOT_NO")]
        [StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
        public string lotNo { get; set; }

        /// <summary>
        /// 采购订单号
        /// </summary>
        [Column("PURCHASE_ORDER_NO")]
        [StringLength(100, ErrorMessage = "采购订单号长度不能超出100字符")]
        public string purchaseOrderNo { get; set; }

        /// <summary>
        /// 批次总数量
        /// </summary>
        [Column("LOT_TOTAL_NUM")]
        public decimal lotTotalNum { get; set; }

        /// <summary>
        /// 装配
        /// </summary>
        [Column("ZP")]
        public string zp { get; set; }

        /// <summary>
        /// 业务编码
        /// </summary>
        [Column("BUSINESS_CODE")]
        [StringLength(100, ErrorMessage = "业务编码长度不能超出100字符")]
        public string businessCode { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [Column("BUSINESS_NAME")]
        [StringLength(500, ErrorMessage = "业务名称长度不能超出500字符")]
        public string businessName { get; set; }

        /// <summary>
        /// 业务模块编码
        /// </summary>
        [Column("BUSINESS_MODULE_CODE")]
        [StringLength(100, ErrorMessage = "业务模块编码长度不能超出100字符")]
        public string businessModuleCode { get; set; }

        /// <summary>
        /// 业务模块名称
        /// </summary>
        [Column("BUSINESS_MODULE_NAME")]
        [StringLength(500, ErrorMessage = "业务模块名称长度不能超出500字符")]
        public string businessModuleName { get; set; }

        /// <summary>
        /// 打印模板编码
        /// </summary>
        [Column("PRINT_TEMPLATE_CODE")]
        [StringLength(100, ErrorMessage = "打印模板编码长度不能超出100字符")]
        public string printTemplateCode { get; set; }

        /// <summary>
        /// 打印模板名称
        /// </summary>
        [Column("PRINT_TEMPLATE_NAME")]
        [StringLength(500, ErrorMessage = "打印模板名称长度不能超出500字符")]
        public string printTemplateName { get; set; }

        /// <summary>
        /// 业务参数
        /// </summary>
        [Column("IN_PARAMS")]
        [StringLength(4000, ErrorMessage = "业务参数长度不能超出4000字符")]
        public string inParams { get; set; }

        /// <summary>
        /// 打印状态
        /// </summary>
        [Column("PRINT_STATUS")]
        [StringLength(50, ErrorMessage = "打印状态长度不能超出50字符")]
        public string printStatus { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        [Column("PRINT_COUNT")]
        public int printCount { get; set; }

        /// <summary>
        /// 打印机编号
        /// </summary>
        [Column("PRINTER_NO")]
        [StringLength(100, ErrorMessage = "打印机编号长度不能超出100字符")]
        public string printerNo { get; set; }

        /// <summary>
        /// 使用标志。0：停用；1：启用；
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 接口请求原始传入参数
        /// </summary>
        [Column("REQUEST_PARAMS")]
        [StringLength(4000, ErrorMessage = "接口原始传入参数长度不能超出4000字符")]
        public string requestParams { get; set; }

        /* ********** 20231027新增字段以下5字段 ********** */
        /// <summary>
        /// 生产日期
        /// </summary>
        [Column("PRODUCT_DATE")]
        public DateTime? productDate { get; set; }

        /// <summary>
        /// 单据类型编码
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [StringLength(100, ErrorMessage = "单据类型编码长度不能超出100字符")]
        public string docTypeCode { get; set; }

        /// <summary>
        /// 单据类型名称
        /// </summary>
        [Column("DOC_TYPE_NAME")]
        [StringLength(500, ErrorMessage = "单据类型名称长度不能超出500字符")]
        public string docTypeName { get; set; }

        /// <summary>
        /// 需要一次打印的份数
        /// </summary>
        [Column("COPY_COUNT")]
        public int copyCount { get; set; }

        /// <summary>
        /// 最小包装量
        /// </summary>
        [Column("MIN_PKG_QTY", TypeName = "decimal(18,3)")]
        public decimal? minPkgQty { get; set; }

        /* ********** 20231129新增字段以下2字段 ********** */
        /// <summary>
        /// 生成PDF路径
        /// </summary>
        [Column("PDF_FILE_PATH")]
        [StringLength(500, ErrorMessage = "生成PDF路径长度不能超出500字符")]
        public string pdfFilePath { get; set; }

        /// <summary>
        /// 生成PDF状态: 成功/失败
        /// </summary>
        [Column("PDF_STATUS")]
        [StringLength(100, ErrorMessage = "生成PDF状态: 成功/失败")]
        public string pdfStatus { get; set; }
    }
}