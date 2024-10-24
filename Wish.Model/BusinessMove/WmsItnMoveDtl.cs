using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_MOVE_DTL")]
	public class WmsItnMoveDtl : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 仓库号
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
        public string whouseNo { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
        public string proprietorCode { get; set; }

        ///// <summary>
        ///// 区域编码(楼号)
        ///// </summary>
        [Column("AREA_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 移动单号
        /// </summary>
        [Column("MOVE_NO")]
        [StringLength(100, ErrorMessage = "移动单号长度不能超出100字符")]
        public string moveNo { get; set; }

        /// <summary>
        /// 库区编号
        /// </summary>
        [Column("REGION_NO")]
        [StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
        public string regionNo { get; set; }

        /// <summary>
        /// 巷道编码
        /// </summary>
        [Column("ROADWAY_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "巷道编码长度不能超出100字符")]
        public string roadwayNo { get; set; }

        /// <summary>
        /// 库位号
        /// </summary>
        [Column("BIN_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "库位号长度不能超出100字符")]
        public string binNo { get; set; }

        /// <summary>
        /// 库存编码
        /// </summary>
        [Column("STOCK_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
        public string stockCode { get; set; }

        /// <summary>
        /// 载体条码
        /// </summary>
        [Column("PALLET_BARCODE")]
        [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
        public string palletBarcode { get; set; }

        /// <summary>
        /// 状态（0：初始创建；41：移库中；90：移库完成(生命周期结束)；92删除（撤销）；93强制完成）
        /// </summary>
        [Column("MOVE_DTL_STATUS")]
        [Required]
        //public int stockStatus { get; set; } = 0;
        public int moveDtlStatus { get; set; } = 0;

        /// <summary>
        /// 装载类型(1:实盘 ；2:工装；0：空盘)
        /// </summary>
        [Column("LOADED_TYPE")]
        //[StringLength(50, ErrorMessage = "装载类型(1:实盘 ；2:工装；0：空盘)长度不能超出50字符")]
        public int? loadedType { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        //      [Column("BATCH_NO")]
        //[StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
        //public string batchNo { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        //[Column("DOC_TYPE_CODE")]
        //[StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
        //public string docTypeCode { get; set; }



        /// <summary>
        /// 失效日期
        /// </summary>
        //[Column("EXP_DATE")]
        //public DateTime expDate { get; set; }

        /// <summary>
        /// 质量标记
        /// </summary>
        //[Column("INSPECTION_RESULT")]
        //[StringLength(100, ErrorMessage = "质量标记长度不能超出100字符")]
        //public int inspectionResult { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        //[Column("MATERIAL_CODE")]
        //[StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
        //public string materialCode { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        //[Column("MATERIAL_SPEC")]
        //[StringLength(100, ErrorMessage = "物料规格长度不能超出100字符")]
        //public string materialSpec { get; set; }

        /// <summary>
        /// 最小包装量
        /// </summary>
        //[Column("MIN_PKG_QTY")]
        //[StringLength(100, ErrorMessage = "最小包装量长度不能超出100字符")]
        //public string minPkgQty { get; set; }

        /// <summary>
        /// 移动状态
        /// </summary>
        //[Column("MOVE_DTL_STATUS")]
        //public int moveDtlStatus { get; set; }

        /// <summary>
        /// 移库计划数量
        /// </summary>
        //[Column("MOVE_QTY")]
        //[StringLength(100, ErrorMessage = "移库计划数量长度不能超出100字符")]
        //public string moveQty { get; set; }

        /// <summary>
        /// 移库计划数量
        /// </summary>
        [Column("MOVE_QTY")]
        //[StringLength(100, ErrorMessage = "移库计划数量长度不能超出100字符")]
        public decimal? moveQty { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        //[Column("PRODUCT_DATE")]
        //[StringLength(100, ErrorMessage = "生产日期长度不能超出100字符")]
        //public string productDate { get; set; }

        /// <summary>
        /// 上架数量
        /// </summary>
        //[Column("PUTAWAY_QTY")]
        //[StringLength(100, ErrorMessage = "上架数量长度不能超出100字符")]
        //public int putawayQty { get; set; }

        /// <summary>
        /// 下架数量
        /// </summary>
        //[Column("PUTDOWN_QTY")]
        //[StringLength(100, ErrorMessage = "下架数量长度不能超出100字符")]
        //public int putdownQty { get; set; }

        /// <summary>
        /// 供应商批次
        /// </summary>
        //[Column("SUPPLIER_BATCH_NO")]
        //[StringLength(100, ErrorMessage = "供应商批次长度不能超出100字符")]
        //public string supplierBatchNo { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        //[Column("SUPPLIER_CODE")]
        //[StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
        //public string supplierCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        //[Column("SUPPLIER_NAME")]
        //[StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
        //public string supplierName { get; set; }

        /// <summary>
        /// 供应商名称-英文
        /// </summary>
        //[Column("SUPPLIER_NAME_EN")]
        //[StringLength(100, ErrorMessage = "供应商名称-英文长度不能超出100字符")]
        //public string supplierNameEn { get; set; }

        /// <summary>
        /// 供应商名称-其他语言
        /// </summary>
        //[Column("SUPPLIER_NAME_ALIAS")]
        //[StringLength(100, ErrorMessage = "供应商名称-其他语言长度不能超出100字符")]
        //public string supplierNameAlias { get; set; }

        /// <summary>
        /// 供应商类型
        /// </summary>
        //[Column("SUPPLIER_TYPE")]
        //[StringLength(100, ErrorMessage = "供应商类型长度不能超出100字符")]
        //public string supplierType { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        //[Column("UNIT_CODE")]
        //[StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
        //public string unitCode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("CREATE_BY")]
        public string createBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("UPDATE_BY")]
        public string updateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UPDATE_TIME")]
        public DateTime updateTime { get; set; }

    }
}
