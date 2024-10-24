using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_QC_DTL")]
	public class WmsItnQcDtl : BasePoco
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
		/// 批次号
		/// </summary>
		[Column("BATCH_NO")]
		[StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
		public string batchNo { get; set; }

		/// <summary>
		/// 确认数量：取样/还样数量
		/// </summary>
		[Column("CONFIRM_QTY", TypeName = "decimal(18,3)")]
		public decimal  confirmQty { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

		/// <summary>
		/// 质检结果;质检结果。0：待检；1：合格；2：不合格；3：免检；4：冻结；
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质检结果;质检结果。0：待检；1：合格；2：不合格；3：免检；4：冻结；长度不能超出50字符")]
		//public string inspectionResult { get; set; }
		public int inspectionResult { get; set; }

        /// <summary>
        /// 抽检记录状态;0：初始创建；51：下架中；90：抽检完成；91：强制完成；92：已删除
        /// </summary>
        [Column("ITN_QC_DTL_STATUS")]
		//[Required]
		//[StringLength(50, ErrorMessage = "抽检记录状态;抽检记录状态：0：初始创建；11：已下发；66：待确认中；77：已确认；99：抽检完成；111：已删除长度不能超出50字符")]
		//public string itnQcDtlStatus { get; set; } = "0";
		public int itnQcDtlStatus { get; set; } = 0;

		/// <summary>
		/// 抽检单号
		/// </summary>
		[Column("ITN_QC_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "抽检单号长度不能超出100字符")]
		public string itnQcNo { get; set; }

		/// <summary>
		/// 抽检数量
		/// </summary>
		[Column("ITN_QC_QTY", TypeName = "decimal(18,3)")]
		public decimal  itnQcQty { get; set; }

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
		/// 物料规格
		/// </summary>
		[Column("MATERIAL_SPEC")]
		[StringLength(4000, ErrorMessage = "物料规格长度不能超出4000字符")]
		public string materialSpec { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 单位编码
		/// </summary>
		[Column("UNIT_CODE")]
		[StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
		public string unitCode { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
