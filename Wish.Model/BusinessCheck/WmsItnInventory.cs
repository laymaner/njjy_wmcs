using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_INVENTORY")]
    [Index(nameof(inventoryNo), IsUnique = true)]
    public class WmsItnInventory : BasePoco
	{
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码(楼号)
        /// </summary>
        [Column("AREA_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
		public string areaNo { get; set; }

		/// <summary>
		/// 盘点方法:是否盲盘;0：否；1：是
		/// </summary>
		[Column("BLIND_FLAG")]
		//[Required]
		//[StringLength(50, ErrorMessage = "盘点方法:是否盲盘;0：否；1：是长度不能超出50字符")]
		//public string blindFlag { get; set; }
		public int blindFlag { get; set; } = 0;

		/// <summary>
		/// 盘点单类型;1：全盘；2：动盘；3：抽盘；4：随机盘点，按比例
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		[Required]
		[StringLength(50, ErrorMessage = "盘点单类型;1：全盘；2：动盘；3：抽盘；4：随机盘点，按比例长度不能超出50字符")]
		public string docTypeCode { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

		/// <summary>
		/// 盘点站台
		/// </summary>
		[Column("INVENTORY_LOC_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "盘点站台长度不能超出100字符")]
		public string inventoryLocNo { get; set; }

		/// <summary>
		/// 盘点单号
		/// </summary>
		[Column("INVENTORY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "盘点单号长度不能超出100字符")]
		public string inventoryNo { get; set; }

		/// <summary>
		/// 盘点单状态;0：初始创建；51：盘点中；90：盘点完成；93：强制结束盘点；92：已删除；
		/// </summary>
		[Column("INVENTORY_STATUS")]
		//[Required]
		//[StringLength(50, ErrorMessage = "盘点单状态;0：初始创建；22：开始盘点(任务下发)；55：盘点中；99：盘点完成；100：强制结束盘点；111：已删除；长度不能超出50字符")]
		//public string inventoryStatus { get; set; } = "0";
		public int inventoryStatus { get; set; } = 0;

		/// <summary>
		/// 任务下发标识;0：未下发，1：已下发，2：挂起；
		/// </summary>
		[Column("ISSUED_FLAG")]
		//[Required]
		//[StringLength(50, ErrorMessage = "任务下发标识;0：未下发，1：已下发，2：挂起；长度不能超出50字符")]
		//public string issuedFlag { get; set; }
		public int issuedFlag { get; set; } = 0;

		/// <summary>
		/// 下发操作人
		/// </summary>
		[Column("ISSUED_OPERATOR")]
		[StringLength(100, ErrorMessage = "下发操作人长度不能超出100字符")]
		public string issuedOperator { get; set; }

		/// <summary>
		/// 任务下发时间
		/// </summary>
		[Column("ISSUED_TIME")]
		public DateTime? issuedTime { get; set; }

		/// <summary>
		/// 操作原因
		/// </summary>
		[Column("OPERATION_REASON")]
		[StringLength(1000, ErrorMessage = "操作原因长度不能超出1000字符")]
		public string operationReason { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		[Column("ORDER_DESC")]
		[StringLength(1000, ErrorMessage = "描述长度不能超出1000字符")]
		public string orderDesc { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		///// <summary>
		///// 回传状态;0默认，1可回传，2回传失败，3回传成功，4无需回传
		///// </summary>
		//[Column("RETURN_FLAG")]
		//[StringLength(50, ErrorMessage = "回传状态;0默认，1可回传，2回传失败，3回传成功，4无需回传长度不能超出50字符")]
		//public string returnFlag { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
