using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.HWConfig.Models;
using Wish.Model.System;


namespace Wish.ViewModel.DevConfig.DeviceAlarmLogVMs
{
    public partial class DeviceAlarmLogListVM : BasePagedListVM<DeviceAlarmLog_View, DeviceAlarmLogSearcher>
    {

        protected override IEnumerable<IGridColumn<DeviceAlarmLog_View>> InitGridHeader()
        {
            return new List<GridColumn<DeviceAlarmLog_View>>{
                this.MakeGridHeader(x => x.Device_Code),
                this.MakeGridHeader(x => x.Message),
                this.MakeGridHeader(x => x.MessageDesc),
                this.MakeGridHeader(x => x.HandleFlag),
                this.MakeGridHeader(x => x.HandleFlagDesc),
                this.MakeGridHeader(x => x.OriginTime),
                this.MakeGridHeader(x => x.EndTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<DeviceAlarmLog_View> GetSearchQuery()
        {
            var query = DC.Set<DeviceAlarmLog>()
                .CheckContain(Searcher.Device_Code, x=>x.Device_Code)
                .CheckContain(Searcher.Message, x=>x.Message)
                .CheckBetween(Searcher.OriginTime?.GetStartTime(), Searcher.OriginTime?.GetEndTime(), x => x.OriginTime, includeMax: false)
                .Select(x => new DeviceAlarmLog_View
                {
				    ID = x.ID,
                    Device_Code = x.Device_Code,
                    HandleFlag = x.HandleFlag,
                    Message = x.Message,
                    OriginTime = x.OriginTime,
                    EndTime = x.EndTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            query.ForEach(item =>
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "HANDLE_FLAG" && t.dictionaryItemCode == item.HandleFlag.ToString());
                if (entityCmd != null)
                {
                    item.HandleFlagDesc = entityCmd.dictionaryItemName;
                }
                List<string> alarmCodes= item.Message.Split(',').ToList();
                var entityAlarm = dicEntities.Where(t => t.dictionaryCode == "ALARM_MSG" && alarmCodes.Contains(t.dictionaryItemCode)).Select(x=>x.dictionaryItemName).ToList();
                if (entityAlarm.Count>0)
                {
                    item.MessageDesc = string.Join(",", entityAlarm);
                }
            });
            //foreach (var item in query)
            //{
            //    var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "HANDLE_FLAG" && t.dictionaryItemCode == item.HandleFlag.ToString());
            //    if (entityCmd != null)
            //    {
            //        item.HandleFlagDesc = entityCmd.dictionaryItemName;
            //    }
            //}
            var queryList = query.AsQueryable().OrderByDescending(x => x.OriginTime);
            return queryList;
        }

    }

    public class DeviceAlarmLog_View : DeviceAlarmLog{
        public string HandleFlagDesc {  get; set; }
        public string MessageDesc {  get; set; }
    }
}
