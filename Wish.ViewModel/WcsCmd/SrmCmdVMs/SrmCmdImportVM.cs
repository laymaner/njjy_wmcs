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
    public partial class SrmCmdTemplateVM : BaseTemplateVM
    {
        [Display(Name = "UnfinishedTask.WCSTaskNo")]
        public ExcelPropety SubTask_No_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.SubTask_No);
        [Display(Name = "UnfinishedTask.WMSTaskNo")]
        public ExcelPropety Task_No_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Task_No);
        [Display(Name = "UnfinishedTask.SerialNo")]
        public ExcelPropety Serial_No_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Serial_No);
        [Display(Name = "DeviceInfo.DeviceNo")]
        public ExcelPropety Device_No_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Device_No);
        [Display(Name = "DevStatus.ForkNo")]
        public ExcelPropety Fork_No_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Fork_No);
        [Display(Name = "UnfinishedTask.StationType")]
        public ExcelPropety Station_Type_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Station_Type);
        [Display(Name = "UnfinishedTask.CheckPoint")]
        public ExcelPropety Check_Point_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Check_Point);
        [Display(Name = "UnfinishedTask.ActionType")]
        public ExcelPropety Task_Cmd_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Task_Cmd);
        [Display(Name = "UnfinishedTask.TaskType")]
        public ExcelPropety Task_Type_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Task_Type);
        [Display(Name = "UnfinishedTask.PalletBarcode")]
        public ExcelPropety Pallet_Barcode_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Pallet_Barcode);
        [Display(Name = "UnfinishedTask.TaskStatus")]
        public ExcelPropety Exec_Status_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Exec_Status);
        [Display(Name = "TaskStatus.SourceStation")]
        public ExcelPropety From_Station_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.From_Station);
        [Display(Name = "TaskStatus.SourceRay")]
        public ExcelPropety From_ForkDirection_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.From_ForkDirection);
        [Display(Name = "TaskStatus.SourceColumn")]
        public ExcelPropety From_Column_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.From_Column);
        [Display(Name = "TaskStatus.SourceLayer")]
        public ExcelPropety From_Layer_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.From_Layer);
        [Display(Name = "TaskStatus.SourceDeep")]
        public ExcelPropety From_Deep_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.From_Deep);
        [Display(Name = "TaskStatus.TargetStation")]
        public ExcelPropety To_Station_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.To_Station);
        [Display(Name = "TaskStatus.TargetRay")]
        public ExcelPropety To_ForkDirection_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.To_ForkDirection);
        [Display(Name = "TaskStatus.TargetColumn")]
        public ExcelPropety To_Column_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.To_Column);
        [Display(Name = "TaskStatus.TargetLayer")]
        public ExcelPropety To_Layer_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.To_Layer);
        [Display(Name = "TaskStatus.TargetDeep")]
        public ExcelPropety To_Deep_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.To_Deep);
        [Display(Name = "UnfinishedTask.CreateTime")]
        public ExcelPropety Recive_Date_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Recive_Date);
        [Display(Name = "UnfinishedTask.SendTime")]
        public ExcelPropety Begin_Date_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Begin_Date);
        [Display(Name = "UnfinishedTask.PickTime")]
        public ExcelPropety Pick_Date_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Pick_Date);
        [Display(Name = "UnfinishedTask.PutTime")]
        public ExcelPropety Put_Date_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Put_Date);
        [Display(Name = "UnfinishedTask.FinishTime")]
        public ExcelPropety Finish_Date_Excel = ExcelPropety.CreateProperty<SrmCmd>(x => x.Finish_Date);

	    protected override void InitVM()
        {
        }

    }

    public class SrmCmdImportVM : BaseImportVM<SrmCmdTemplateVM, SrmCmd>
    {

    }

}
