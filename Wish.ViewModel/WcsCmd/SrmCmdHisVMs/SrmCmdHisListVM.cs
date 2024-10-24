using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.TaskConfig.Model;
using Wish.Model.System;


namespace Wish.ViewModel.WcsCmd.SrmCmdHisVMs
{
    public partial class SrmCmdHisListVM : BasePagedListVM<SrmCmdHis_View, SrmCmdHisSearcher>
    {

        protected override IEnumerable<IGridColumn<SrmCmdHis_View>> InitGridHeader()
        {
            return new List<GridColumn<SrmCmdHis_View>>{
                this.MakeGridHeader(x => x.SubTask_No),
                this.MakeGridHeader(x => x.Task_No),
                this.MakeGridHeader(x => x.Serial_No),
                this.MakeGridHeader(x => x.Device_No),
                this.MakeGridHeader(x => x.Fork_No),
                this.MakeGridHeader(x => x.Station_Type),
                this.MakeGridHeader(x => x.Check_Point),
                this.MakeGridHeader(x => x.Task_Cmd),
                this.MakeGridHeader(x => x.Task_Type),
                this.MakeGridHeader(x => x.Pallet_Barcode),
                this.MakeGridHeader(x => x.Exec_Status),
                this.MakeGridHeader(x => x.From_Station),
                this.MakeGridHeader(x => x.From_ForkDirection),
                this.MakeGridHeader(x => x.From_Column),
                this.MakeGridHeader(x => x.From_Layer),
                this.MakeGridHeader(x => x.From_Deep),
                this.MakeGridHeader(x => x.To_Station),
                this.MakeGridHeader(x => x.To_ForkDirection),
                this.MakeGridHeader(x => x.To_Column),
                this.MakeGridHeader(x => x.To_Layer),
                this.MakeGridHeader(x => x.To_Deep),
                this.MakeGridHeader(x => x.Recive_Date),
                this.MakeGridHeader(x => x.Begin_Date),
                this.MakeGridHeader(x => x.Pick_Date),
                this.MakeGridHeader(x => x.Put_Date),
                this.MakeGridHeader(x => x.Finish_Date), 
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.Exec_Status_Desc),
                this.MakeGridHeader(x => x.Task_Cmd_Desc),
                this.MakeGridHeader(x => x.WaferID),
                this.MakeGridHeader(x => x.Remark_Desc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<SrmCmdHis_View> GetSearchQuery()
        {
            var query = DC.Set<SrmCmdHis>()
                .CheckContain(Searcher.Task_No, x=>x.Task_No)
                .CheckEqual(Searcher.Serial_No, x=>x.Serial_No)
                .CheckContain(Searcher.Device_No, x=>x.Device_No)
                .CheckContain(Searcher.Fork_No, x=>x.Fork_No)
                .CheckEqual(Searcher.Check_Point, x=>x.Check_Point)
                .CheckEqual(Searcher.Task_Cmd, x=>x.Task_Cmd)
                .CheckContain(Searcher.Task_Type, x=>x.Task_Type)
                .CheckEqual(Searcher.Pallet_Barcode, x => x.Pallet_Barcode)
                .CheckContain(Searcher.WaferID, x=>x.WaferID)
                .CheckEqual(Searcher.Exec_Status, x=>x.Exec_Status)
                .CheckEqual(Searcher.From_Station, x=>x.From_Station)
                .CheckEqual(Searcher.From_ForkDirection, x=>x.From_ForkDirection)
                .CheckEqual(Searcher.From_Column, x=>x.From_Column)
                .CheckEqual(Searcher.From_Layer, x=>x.From_Layer)
                .CheckEqual(Searcher.From_Deep, x=>x.From_Deep)
                .CheckEqual(Searcher.To_Station, x=>x.To_Station)
                .CheckEqual(Searcher.To_ForkDirection, x=>x.To_ForkDirection)
                .CheckEqual(Searcher.To_Column, x=>x.To_Column)
                .CheckEqual(Searcher.To_Layer, x=>x.To_Layer)
                .CheckEqual(Searcher.To_Deep, x=>x.To_Deep)
                .CheckBetween(Searcher.Recive_Date?.GetStartTime(), Searcher.Recive_Date?.GetEndTime(), x => x.Recive_Date, includeMax: false)
                .CheckBetween(Searcher.Begin_Date?.GetStartTime(), Searcher.Begin_Date?.GetEndTime(), x => x.Begin_Date, includeMax: false)
                .CheckBetween(Searcher.Pick_Date?.GetStartTime(), Searcher.Pick_Date?.GetEndTime(), x => x.Pick_Date, includeMax: false)
                .CheckBetween(Searcher.Put_Date?.GetStartTime(), Searcher.Put_Date?.GetEndTime(), x => x.Put_Date, includeMax: false)
                .CheckBetween(Searcher.Finish_Date?.GetStartTime(), Searcher.Finish_Date?.GetEndTime(), x => x.Finish_Date, includeMax: false)
                .Select(x => new SrmCmdHis_View
                {
				    ID = x.ID,
                    SubTask_No = x.SubTask_No,
                    Task_No = x.Task_No,
                    Serial_No = x.Serial_No,
                    Device_No = x.Device_No,
                    Fork_No = x.Fork_No,
                    Station_Type = x.Station_Type,
                    Check_Point = x.Check_Point,
                    Task_Cmd = x.Task_Cmd,
                    Task_Type = x.Task_Type,
                    Pallet_Barcode = x.Pallet_Barcode,
                    Exec_Status = x.Exec_Status,
                    From_Station = x.From_Station,
                    From_ForkDirection = x.From_ForkDirection,
                    From_Column = x.From_Column,
                    From_Layer = x.From_Layer,
                    From_Deep = x.From_Deep,
                    To_Station = x.To_Station,
                    To_ForkDirection = x.To_ForkDirection,
                    To_Column = x.To_Column,
                    To_Layer = x.To_Layer,
                    To_Deep = x.To_Deep,
                    Recive_Date = x.Recive_Date,
                    Begin_Date = x.Begin_Date,
                    Pick_Date = x.Pick_Date,
                    Put_Date = x.Put_Date,
                    Finish_Date = x.Finish_Date,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                    WaferID = x.WaferID,
                    Remark_Desc = x.Remark_Desc,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "CMD_TYPE" && t.dictionaryItemCode == item.Task_Cmd.ToString());
                if (entityCmd != null)
                {
                    item.Task_Cmd_Desc = entityCmd.dictionaryItemName;
                }
                var entityStatus = dicEntities.FirstOrDefault(t => t.dictionaryCode == "EXEC_STATUS" && t.dictionaryItemCode == item.Exec_Status.ToString());
                if (entityStatus != null)
                {
                    item.Exec_Status_Desc = entityStatus.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.CreateTime);
            return queryList;
        }

    }

    public class SrmCmdHis_View : SrmCmdHis{
        public string Task_Cmd_Desc { get; set; }
        public string Exec_Status_Desc { get; set; }
    }
}
