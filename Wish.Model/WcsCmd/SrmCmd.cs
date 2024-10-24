//=============================================================================
//
// 堆垛机任务信息。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;


namespace Wish.TaskConfig.Model
{
    [Table("WCS_SRM_CMD")]
    public class SrmCmd : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 指令号
        /// </summary>
        [Column("Task_Id")]
        [Display(Name = "UnfinishedTask.WCSTaskNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string SubTask_No { get; set; }

        /// <summary>
        /// WMS任务号
        /// </summary>
        [Column("Source_Id")]
        [Display(Name = "UnfinishedTask.WMSTaskNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Task_No { get; set; }

        /// <summary>
        /// 堆垛机流水号
        /// </summary>
        [Column("Serial_No")]
        [Display(Name = "UnfinishedTask.SerialNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Int16 Serial_No { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = "DeviceInfo.DeviceNo")]
        [Column("Srm_No")]
        [StringLength(16, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_No { get; set; }

        /// <summary>
        /// 货叉编号
        /// </summary>
        [StringLength(2, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("Lhd_Id")]
        [Display(Name = "DevStatus.ForkNo")]
        public string Fork_No { get; set; }

        /// <summary>
        /// 站台类型
        /// </summary>
        [Column("Station_Type")]
        [Display(Name = "UnfinishedTask.StationType")]
        [StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Station_Type { get; set; }

        /// <summary>
        /// 校验位（流水号与排列层之和）
        /// </summary>
        [Display(Name = "UnfinishedTask.CheckPoint")]
        [Column("Check_Point")]
        public Int16 Check_Point { get; set; }

        /// <summary>
        /// 任务指令: 4 取货，6 放货，8 双重改址，9 倒库，10 召回
        /// </summary>
        [Display(Name = "UnfinishedTask.ActionType")]
        [Column("Task_Cmd")]
        public Int16 Task_Cmd { get; set; }

        /// <summary>
        /// 指令类型 IN入库，OUT出库，MOVE移库
        /// </summary>
        [Display(Name = "UnfinishedTask.TaskType")]
        [Column("Task_Type")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Task_Type { get; set; }

        /// <summary>
        /// 托盘号
        /// </summary>
        [Display(Name = "UnfinishedTask.PalletBarcode")]
        [Column("Pallet_Barcode")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public Int32 Pallet_Barcode { get; set; }
        //public string Pallet_Barcode { get; set; }
        
        /// <summary>
        /// 晶圆ID
        /// </summary>
        [Column("WaferID")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string WaferID { get; set; }


        /// <summary>
        /// 指令状态: 0 初始状态，3 待下发，5 正在下发，10 正常已下发，20取货完成，25 放货正在下发，30 放货下发完成，40放货完成，90完成，91 删除，92空出，93满入，95无法放货，96无法取货
        /// </summary>
        [Display(Name = "UnfinishedTask.TaskStatus")]
        [Column("Task_Status")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Int16 Exec_Status { get; set; }

        /// <summary>
        /// 起始站台
        /// </summary>
        [Display(Name = "TaskStatus.SourceStation")]
        [Column("From_Station")]
        public Int16 From_Station { get; set; }

        /// <summary>
        /// 起始排
        /// </summary>
        [Display(Name = "TaskStatus.SourceRay")]
        [Column("From_ForkDirection")]
        public Int16 From_ForkDirection { get; set; }

        /// <summary>
        /// 起始列
        /// </summary>
        [Display(Name = "TaskStatus.SourceColumn")]
        [Column("From_Column")]
        public Int16 From_Column { get; set; }

        /// <summary>
        /// 起始层
        /// </summary>
        [Display(Name = "TaskStatus.SourceLayer")]
        [Column("From_Layer")]
        public Int16 From_Layer { get; set; }

        /// <summary>
        /// 起始深度
        /// </summary>
        [Display(Name = "TaskStatus.SourceDeep")]
        [Column("From_Deep")]
        public Int16 From_Deep { get; set; }


        /// <summary>
        /// 目的站台
        /// </summary>
        [Display(Name = "TaskStatus.TargetStation")]
        [Column("To_Station")]
        public Int16 To_Station { get; set; }

        /// <summary>
        /// 目的排
        /// </summary>
        [Display(Name = "TaskStatus.TargetRay")]
        [Column("To_ForkDirection")]
        public Int16 To_ForkDirection { get; set; }

        /// <summary>
        /// 目的列
        /// </summary>
        [Display(Name = "TaskStatus.TargetColumn")]
        [Column("To_Column")]
        public Int16 To_Column { get; set; }

        /// <summary>
        /// 目的层
        /// </summary>
        [Display(Name = "TaskStatus.TargetLayer")]
        [Column("To_Layer")]
        public Int16 To_Layer { get; set; }


        /// <summary>
        /// 目的层
        /// </summary>
        [Display(Name = "TaskStatus.TargetDeep")]
        [Column("To_Deep")]
        public Int16 To_Deep { get; set; }


        /// <summary>
        /// 接受时间
        /// </summary>
        [Display(Name = "UnfinishedTask.CreateTime")]
        [Column("Recive_Date")]
        //[StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public DateTime Recive_Date { get; set; }


        /// <summary>
        /// 开始执行时间
        /// </summary>
        [Display(Name = "UnfinishedTask.SendTime")]
        [Column("Begin_Date")]
        //[StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public DateTime? Begin_Date { get; set; }

        /// <summary>
        /// 取货时间
        /// </summary>
        [Display(Name = "UnfinishedTask.PickTime")]
        [Column("Pick_Date")]
        //[StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public DateTime? Pick_Date { get; set; }

        /// <summary>
        /// 放货时间
        /// </summary>
        [Display(Name = "UnfinishedTask.PutTime")]
        [Column("Put_Date")]
        //[StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public DateTime? Put_Date { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [Display(Name = "UnfinishedTask.FinishTime")]
        [Column("Finish_Date")]
        //[StringLength(4, ErrorMessage = "{0}最多输入{1}个字符")]
        public DateTime? Finish_Date  { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [Display(Name = "UnfinishedTask.SendTime")]
        [Column("Remark_Desc")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Remark_Desc { get; set; }
    }
}
