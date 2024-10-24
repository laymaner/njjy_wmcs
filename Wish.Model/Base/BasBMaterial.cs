using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_MATERIAL")]
    [Index(nameof(MaterialCode), IsUnique = true)]
    public class BasBMaterial : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }


        /// <summary>
        /// 码级管理：0无码 - 默认，1唯一码
        /// </summary>
        [Column("BARCODE_FLAG")]
        [Required]
        public int? BarcodeFlag { get; set; } = 1;

        /// <summary>
        /// 品牌;EBS物料事件
        /// </summary>
        [Column("BRAND")]
        [StringLength(100, ErrorMessage = "品牌;EBS物料事件长度不能超出100字符")]
        public string Brand { get; set; }

        /// <summary>
        /// 采购员编码;EBS物料事件
        /// </summary>
        [Column("BUYER_CODE")]
        [StringLength(100, ErrorMessage = "采购员编码;EBS物料事件长度不能超出100字符")]
        public string BuyerCode { get; set; }

        /// <summary>
        /// 采购员名称;EBS物料事件
        /// </summary>
        [Column("BUYER_NAME")]
        [StringLength(500, ErrorMessage = "采购员名称;EBS物料事件长度不能超出500字符")]
        public string BuyerName { get; set; }

        /// <summary>
        /// 电子料有效期;EBS物料事件
        /// </summary>
        //[Column("EMATERIAL_VTIME")]
        [Column("VALIDITY_DATE_LEN")]
        [StringLength(100, ErrorMessage = "电子料有效期;EBS物料事件长度不能超出100字符")]
        public string EmaterialVtime { get; set; }

        /// <summary>
        /// ERP库位;EBS物料事件  ERP仓库
        /// </summary>
        //[Column("ERP_BIN_NO")]
        [Column("ERP_WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "ERP库位;EBS物料事件长度不能超出100字符")]
        public string ErpBinNo { get; set; }

        /// <summary>
        /// 材料;EBS物料事件
        /// </summary>
        [Column("MATERIAL")]
        [StringLength(100, ErrorMessage = "材料;EBS物料事件长度不能超出100字符")]
        public string Material { get; set; }

        /// <summary>
        /// 物料大类编码;EBS物料事件
        /// </summary>
        [Column("MATERIAL_CATEGORY_CODE")]
        //[Column("MATERIAL_CATEGORY_NO")]
        [StringLength(100, ErrorMessage = "物料大类编码;EBS物料事件长度不能超出100字符")]
        public string MaterialCategoryCode { get; set; }

        /// <summary>
        /// 物料名称;EBS物料事件
        /// </summary>
        [Column("MATERIAL_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "物料名称;EBS物料事件长度不能超出500字符")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料名称-其他
        /// </summary>
        [Column("MATERIAL_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "物料名称-其他长度不能超出500字符")]
        public string MaterialNameAlias { get; set; }

        /// <summary>
        /// 物料名称-英文
        /// </summary>
        [Column("MATERIAL_NAME_EN")]
        [StringLength(500, ErrorMessage = "物料名称-英文长度不能超出500字符")]
        public string MaterialNameEn { get; set; }

        /// <summary>
        /// 物料编码;EBS物料事件
        /// </summary>
        //[Column("MATERIAL_NO")]
        [Column("MATERIAL_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "物料编码;EBS物料事件长度不能超出100字符")]
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料规格型号;EBS物料事件
        /// </summary>
        [Column("MATERIAL_SPEC")]
        [StringLength(4000, ErrorMessage = "物料规格型号;EBS物料事件长度不能超出4000字符")]
        public string MaterialSpec { get; set; }

        /// <summary>
        /// 物料类型描述
        /// </summary>
        [Column("MATERIAL_TYPE_DESC")]
        [StringLength(500, ErrorMessage = "物料类型描述长度不能超出500字符")]
        public string MaterialTypeDesc { get; set; }

        /// <summary>
        /// 物料类型编码;EBS物料事件
        /// </summary>
        //[Column("MATERIAL_TYPE_NO")]
        [Column("MATERIAL_TYPE_CODE")]
        [StringLength(100, ErrorMessage = "物料类型编码;EBS物料事件长度不能超出100字符")]
        public string MaterialTypeCode { get; set; }

        /// <summary>
        /// 延期次数上限(默认3次)
        /// </summary>
        [Column("MAX_DELAY_TIMES")]
        public int? MaxDelayTimes { get; set; } = 3;

        /// <summary>
        /// 烘干次数上限
        /// </summary>
        [Column("MAX_DRIED_TIMES")]
        public int? MaxDriedTimes { get; set; }

        /// <summary>
        /// 最小包装量
        /// </summary>
        [Column("MIN_PKG_QTY", TypeName = "decimal(18,3)")]
        public decimal? MinPkgQty { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        //[Column("MODIFY_BY")]
        //[StringLength(100, ErrorMessage = "修改人长度不能超出100字符")]
        //public string modifyBy { get; set; }

        ///// <summary>
        ///// 修改时间
        ///// </summary>
        //[Column("MODIFY_TIME")]
        //public DateTime? modifyTime { get; set; }

        /// <summary>
        /// 工程图号;EBS物料事件
        /// </summary>
        [Column("PROJECT_DRAWING_NO")]
        [StringLength(4000, ErrorMessage = "工程图号;EBS物料事件长度不能超出4000字符")]
        public string ProjectDrawingNo { get; set; }

        /// <summary>
        /// 货主编码
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
        public string ProprietorCode { get; set; }

        /// <summary>
        /// 是否检验;EBS物料事件
        /// </summary>
        [Column("QC_FLAG")]
        public int? QcFlag { get; set; }

        /// <summary>
        /// 是否共用料
        /// </summary>
        [Column("SHARED_FALG")]
        public int? SharedFalg { get; set; }

        /// <summary>
        /// SKU 创建规则：物料；物料+质量等，详见字典表
        /// </summary>
        [Column("SKU_RULE_NO")]
        [StringLength(100, ErrorMessage = "SKU 创建规则：物料；物料+质量等，详见字典表长度不能超出100字符")]
        public string SkuRuleNo { get; set; }

        /// <summary>
        /// 呆滞天数
        /// </summary>
        [Column("SLUGGISH_TIME")]
        public int? SluggishTime { get; set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        [Column("TECH_PARM")]
        [StringLength(500, ErrorMessage = "技术参数长度不能超出500字符")]
        public string TechParm { get; set; }

        /// <summary>
        /// 单位编码（最小单位）
        /// </summary>
        [Column("UNIT_CODE")]
        [StringLength(100, ErrorMessage = "单位编码（最小单位）长度不能超出100字符")]
        public string UnitCode { get; set; }
        /// <summary>
        /// 最小包装量重量(kg)
        /// </summary>
        [Column("UNIT_WEIGHT")]
        public decimal UnitWeight { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用;EBS物料事件
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int UsedFlag { get; set; } = 1;

        /// <summary>
        /// 虚拟标记：0不是，1是
        /// </summary>
        [Column("VIRTUAL_FLAG")]
        [Required]
        public int? VirtualFlag { get; set; } = 0;

        /// <summary>
        /// 即将过期预警值（默认单位：天）
        /// </summary>
        [Column("WARN_OVERDUE_LEN")]
        public int? WarnOverdueLen { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string WhouseNo { get; set; }

        /// <summary>
        /// 公司代码，固定值：A01，WMS不做任何处理，只做记录
        /// </summary>
        [Column("COMPANY_CODE")]
        [StringLength(100, ErrorMessage = "公司代码长度不能超出100字符")]
        public string CompanyCode { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [Column("EXTEND1")]
        [StringLength(200, ErrorMessage = "扩展字段1长度不能超出200字符")]
        public string Extend1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [Column("EXTEND2")]
        [StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
        public string Extend2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [Column("EXTEND3")]
        [StringLength(200, ErrorMessage = "扩展字段3长度不能超出200字符")]
        public string Extend3 { get; set; }

        /// <summary>
        /// 扩展字段4
        /// </summary>
        [Column("EXTEND4")]
        [StringLength(200, ErrorMessage = "扩展字段4长度不能超出200字符")]
        public string Extend4 { get; set; }

        /// <summary>
        /// 扩展字段5
        /// </summary>
        [Column("EXTEND5")]
        [StringLength(200, ErrorMessage = "扩展字段5长度不能超出200字符")]
        public string Extend5 { get; set; }

        /// <summary>
        /// 扩展字段6
        /// </summary>
        [Column("EXTEND6")]
        [StringLength(200, ErrorMessage = "扩展字段6长度不能超出200字符")]
        public string Extend6 { get; set; }

        /// <summary>
        /// 扩展字段7
        /// </summary>
        [Column("EXTEND7")]
        [StringLength(200, ErrorMessage = "扩展字段7长度不能超出200字符")]
        public string Extend7 { get; set; }

        /// <summary>
        /// 扩展字段8
        /// </summary>
        [Column("EXTEND8")]
        [StringLength(200, ErrorMessage = "扩展字段8长度不能超出200字符")]
        public string Extend8 { get; set; }

        /// <summary>
        /// 扩展字段9
        /// </summary>
        [Column("EXTEND9")]
        [StringLength(200, ErrorMessage = "扩展字段9长度不能超出200字符")]
        public string Extend9 { get; set; }

        /// <summary>
        /// 扩展字段10
        /// </summary>
        [Column("EXTEND10")]
        [StringLength(200, ErrorMessage = "扩展字段10长度不能超出200字符")]
        public string Extend10 { get; set; }

        /// <summary>
        /// 扩展字段11
        /// </summary>
        [Column("EXTEND11")]
        [StringLength(200, ErrorMessage = "扩展字段11长度不能超出200字符")]
        public string Extend11 { get; set; }

        /// <summary>
        /// 扩展字段12
        /// </summary>
        [Column("EXTEND12")]
        [StringLength(200, ErrorMessage = "扩展字段12长度不能超出200字符")]
        public string Extend12 { get; set; }

        /// <summary>
        /// 扩展字段13
        /// </summary>
        [Column("EXTEND13")]
        [StringLength(200, ErrorMessage = "扩展字段13长度不能超出200字符")]
        public string Extend13 { get; set; }

        /// <summary>
        /// 扩展字段14
        /// </summary>
        [Column("EXTEND14")]
        [StringLength(200, ErrorMessage = "扩展字段14长度不能超出200字符")]
        public string Extend14 { get; set; }

        /// <summary>
        /// 扩展字段15
        /// </summary>
        [Column("EXTEND15")]
        [StringLength(200, ErrorMessage = "扩展字段15长度不能超出200字符")]
        public string Extend15 { get; set; }
    }
}