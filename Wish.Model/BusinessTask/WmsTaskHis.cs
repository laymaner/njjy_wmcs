using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_TASK_HIS")]
    [Index(nameof(wmsTaskNo), IsUnique = true)]
    public class WmsTaskHis : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 反馈描述
        /// </summary>
        [Column("FEEDBACK_DESC")]
		[StringLength(255, ErrorMessage = "反馈描述长度不能超出255字符")]
		public string feedbackDesc { get; set; }

		/// <summary>
		/// 任务反馈状态;0初始创建，90反馈成功
		/// </summary>
		[Column("FEEDBACK_STATUS")]
		//[Required]
		public int  feedbackStatus { get; set; }

		/// <summary>
		/// 起点编号
		/// </summary>
		[Column("FR_LOCATION_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "起点编号长度不能超出100字符")]
		public string frLocationNo { get; set; }

		/// <summary>
		/// 起点类型：站台、库位
		/// </summary>
		[Column("FR_LOCATION_TYPE")]
		//[Required]
		//[StringLength(50, ErrorMessage = "起点类型：站台、库位长度不能超出50字符")]
		public int? frLocationType { get; set; }

		/// <summary>
		/// 装载类型 。详见字典表;(1:实盘 ；2:工装；0：空盘)
		/// </summary>
		[Column("LOADED_TYPE")]
		//[Required]
		//[StringLength(50, ErrorMessage = "装载类型 。详见字典表;(1:实盘 ；2:工装；0：空盘)长度不能超出50字符")]
		public int? loadedType { get; set; }

		/// <summary>
		/// 高
		/// </summary>
		[Column("MAT_HEIGHT")]
		public int?  matHeight { get; set; }

		/// <summary>
		/// 长
		/// </summary>
		[Column("MAT_LENGTH")]
		public int?  matLength { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		[Column("MAT_QTY", TypeName = "decimal(18,3)")]
		//[Required]
		public decimal?  matQty { get; set; }

		/// <summary>
		/// 重量
		/// </summary>
		[Column("MAT_WEIGHT")]
		public int?  matWeight { get; set; }

		/// <summary>
		/// 宽
		/// </summary>
		[Column("MAT_WIDTH")]
		public int?  matWidth { get; set; }

		/// <summary>
		/// 单据编号：上架单、下架单
		/// </summary>
		[Column("ORDER_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "单据编号：上架单、下架单长度不能超出100字符")]
		public string orderNo { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

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
		/// 巷道编码
		/// </summary>
		[Column("ROADWAY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "巷道编码长度不能超出100字符")]
		public string roadwayNo { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
		//[Required]
		[StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		public string stockCode { get; set; }

		/// <summary>
		/// 任务描述：操作说明（删除等）/处理结果（反馈处理结果）
		/// </summary>
		[Column("TASK_DESC")]
		[StringLength(255, ErrorMessage = "任务描述：操作说明（删除等）/处理结果（反馈处理结果）长度不能超出255字符")]
		public string taskDesc { get; set; }

		/// <summary>
		/// 优先级（用于出库时任务的执行优先）
		/// </summary>
		[Column("TASK_PRIORITY")]
		//[Required]
		public int  taskPriority { get; set; }

		/// <summary>
		/// 任务状态：详见字典表
		/// </summary>
		[Column("TASK_STATUS")]
		//[Required]
		//[StringLength(50, ErrorMessage = "任务状态：详见字典表长度不能超出50字符")]
		public int taskStatus { get; set; } = 0;

		/// <summary>
		/// 任务类型。详见字典表
		/// </summary>
		[Column("TASK_TYPE_NO")]
		[Required]
		[StringLength(50, ErrorMessage = "任务类型。详见字典表长度不能超出50字符")]
		public string taskTypeNo { get; set; }

		/// <summary>
		/// 终点编号
		/// </summary>
		[Column("TO_LOCATION_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "终点编号长度不能超出100字符")]
		public string toLocationNo { get; set; }

		/// <summary>
		/// 终点类型：站台0、库位1
		/// </summary>
		[Column("TO_LOCATION_TYPE")]
		//[Required]
		//[StringLength(50, ErrorMessage = "终点类型：站台、库位长度不能超出50字符")]
		public int? toLocationType { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }

		/// <summary>
		/// 任务编号
		/// </summary>
		[Column("WMS_TASK_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "任务编号长度不能超出100字符")]
		public string wmsTaskNo { get; set; }

		/// <summary>
		/// WMS任务类型。详见字典表。
		/// </summary>
		[Column("WMS_TASK_TYPE")]
		[Required]
		[StringLength(50, ErrorMessage = "WMS任务类型。详见字典表。长度不能超出50字符")]
		public string wmsTaskType { get; set; }

    }
}
