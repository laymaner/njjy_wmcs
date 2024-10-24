using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;


namespace Wish.ViewModel.WcsCmd.SrmCmdVMs
{
    public partial class SrmCmdSearcher : BaseSearcher
    {
        [Display(Name = "UnfinishedTask.WCSTaskNo")]
        public String SubTask_No { get; set; }
        [Display(Name = "UnfinishedTask.WMSTaskNo")]
        public String Task_No { get; set; }
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_No { get; set; }
        [Display(Name = "UnfinishedTask.CheckPoint")]
        public Int16? Check_Point { get; set; }
        [Display(Name = "UnfinishedTask.ActionType")]
        public Int16? Task_Cmd { get; set; }
        [Display(Name = "UnfinishedTask.TaskType")]
        public String Task_Type { get; set; }
        [Display(Name = "UnfinishedTask.PalletBarcode")]
        public Int32? Pallet_Barcode { get; set; }
        public string WaferID { get; set; }
        [Display(Name = "UnfinishedTask.TaskStatus")]
        public Int16? Exec_Status { get; set; }
        [Display(Name = "TaskStatus.SourceStation")]
        public Int16? From_Station { get; set; }
        [Display(Name = "TaskStatus.SourceRay")]
        public Int16? From_ForkDirection { get; set; }
        [Display(Name = "TaskStatus.SourceColumn")]
        public Int16? From_Column { get; set; }
        [Display(Name = "TaskStatus.SourceLayer")]
        public Int16? From_Layer { get; set; }
        [Display(Name = "TaskStatus.SourceDeep")]
        public Int16? From_Deep { get; set; }
        [Display(Name = "TaskStatus.TargetStation")]
        public Int16? To_Station { get; set; }
        [Display(Name = "TaskStatus.TargetRay")]
        public Int16? To_ForkDirection { get; set; }
        [Display(Name = "TaskStatus.TargetColumn")]
        public Int16? To_Column { get; set; }
        [Display(Name = "TaskStatus.TargetLayer")]
        public Int16? To_Layer { get; set; }
        [Display(Name = "TaskStatus.TargetDeep")]
        public Int16? To_Deep { get; set; }
        [Display(Name = "UnfinishedTask.CreateTime")]
        public DateRange Recive_Date { get; set; }
        [Display(Name = "UnfinishedTask.SendTime")]
        public DateRange Begin_Date { get; set; }
        [Display(Name = "UnfinishedTask.PickTime")]
        public DateRange Pick_Date { get; set; }
        [Display(Name = "UnfinishedTask.PutTime")]
        public DateRange Put_Date { get; set; }
        [Display(Name = "UnfinishedTask.FinishTime")]
        public DateRange Finish_Date { get; set; }

        protected override void InitVM()
        {
        }

    }
}
