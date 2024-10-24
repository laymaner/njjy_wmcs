using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_OUT_WAVE")]
    [Index(nameof(waveNo), IsUnique = true)]
    public class WmsOutWave : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 自动分配标识;0：默认，1：开启自动分配；
        /// </summary>
        [Column("ALLOT_FLAG")]
		[StringLength(50, ErrorMessage = "自动分配标识;0：默认，1：开启自动分配；长度不能超出50字符")]
		public string allotFlag { get; set; } = "0";

		/// <summary>
		/// 分配操作人
		/// </summary>
		[Column("ALLOT_OPERATOR")]
        [Display(Name = "分配操作人")]
        [StringLength(100, ErrorMessage = "分配操作人长度不能超出100字符")]
		public string allotOperator { get; set; }

		/// <summary>
		/// 分配开始时间
		/// </summary>
		[Column("ALLOT_TIME")]
        [Display(Name = "分配开始时间")]
        public DateTime? allotTime { get; set; }

		/// <summary>
		/// 发货站台
		/// </summary>
		[Column("DELIVERY_LOC_NO")]
		[StringLength(100, ErrorMessage = "发货站台长度不能超出100字符")]
		public string deliveryLocNo { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		[Required]
        [Display(Name = "单据类型")]
        [StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
		public string docTypeCode { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		//[Column("ERP_WHOUSE_NO")]
		//[Required]
		//[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		//public string erpWhouseNo { get; set; }

		/// <summary>
		/// 任务下发标识;0：未下发，1：已下发，2：挂起；
		/// </summary>
		[Column("ISSUED_FLAG")]
		[StringLength(50, ErrorMessage = "任务下发标识;0：未下发，1：已下发，2：挂起；长度不能超出50字符")]
		public string issuedFlag { get; set; } = "0";

		/// <summary>
		/// 下发操作人
		/// </summary>
		[Column("ISSUED_OPERATOR")]
		[StringLength(100, ErrorMessage = "下发操作人长度不能超出100字符")]
		public string issuedOperator { get; set; }

		/// <summary>
		/// 任务下发结果
		/// </summary>
		[Column("ISSUED_RESULT")]
		[StringLength(2000, ErrorMessage = "任务下发结果长度不能超出2000字符")]
		public string issuedResult { get; set; }

		/// <summary>
		/// 任务下发时间
		/// </summary>
		[Column("ISSUED_TIME")]
		public DateTime? issuedTime { get; set; }

		/// <summary>
		/// 操作原因
		/// </summary>
		[Column("OPERATION_REASON")]
		[StringLength(500, ErrorMessage = "操作原因长度不能超出500字符")]
		public string operationReason { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 波次号
		/// </summary>
		[Column("WAVE_NO")]
		[Required]
        [Display(Name = "波次号")]
        [StringLength(100, ErrorMessage = "波次号长度不能超出100字符")]
		public string waveNo { get; set; }

		/// <summary>
		/// 波次单状态;0：初始创建；11：分配中；22：分配完成；99：确认完成；100: 强制完成；111：已删除。
		/// </summary>
		[Column("WAVE_STATUS")]
        [Display(Name = "波次单状态")]
        [Required]
		//[StringLength(50, ErrorMessage = "波次单状态;0：初始创建；11：分配中；22：分配完成；99：确认完成；100: 强制完成；111：已删除。长度不能超出50字符")]
		public int? waveStatus { get; set; } = 0;

		/// <summary>
		/// 波次类型;0：默认波次（不做波次管理时按单生成），1：手动创建，2：自动生成
		/// </summary>
		[Column("WAVE_TYPE")]
		[Required]
        [Display(Name = "波次类型")]
        //[StringLength(50, ErrorMessage = "波次类型;0：默认波次（不做波次管理时按单生成），1：手动创建，2：自动生成长度不能超出50字符")]
		public int? waveType { get; set; } = 0;

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
