using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using Wish.Model.System;


namespace Wish.ViewModel.DevConfig.DeviceAlarmLogVMs
{
    public partial class DeviceAlarmLogVM
    {
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public async Task<BusinessResult> GetAlarmLog()
        {
            BusinessResult result = new BusinessResult();
            List<AlarmLogDto> alarmLogDtos = new List<AlarmLogDto>();
            try
            {
                var alarmLogLists = await DC.Set<DeviceAlarmLog>().Where(x => x.OriginTime.Date == DateTime.Today.Date)
                    .OrderByDescending(x => x.OriginTime)
                    .Select(x => new AlarmLogDto
                    {
                        name = x.Device_Code,
                        value1 = x.Message,
                        value2 = x.OriginTime,
                    }).Take(6).ToListAsync();
                dicEntities = await DC.Set<SysDictionary>().AsNoTracking().ToListAsync();
                foreach (var item in alarmLogLists)
                {
                    var alarmCodes=item.value1.Split(',');
                    List<string> alarmMsgs = new List<string>();
                    foreach (var code in alarmCodes)
                    {
                        string alarmMsg = GetDicItemDisVal(dicEntities, "ALARM_MSG", code.ToString());
                        if (!string.IsNullOrWhiteSpace(alarmMsg))
                        {
                            alarmMsgs.Add(alarmMsg);
                        }
                    }
                    item.value1=string.Join(",", alarmMsgs);
                }
                alarmLogDtos.AddRange(alarmLogLists);
                return result.Ok("查询成功", alarmLogDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        /// <summary>
        /// 转字典
        /// </summary>
        /// <param name="sysDictionaries"></param>
        /// <param name="code"></param>
        /// <param name="itemCode"></param>
        /// <param name="lanType"></param>
        /// <returns></returns>
        private string GetDicItemDisVal(List<SysDictionary> sysDictionaries, string code, string itemCode, string lanType = "Zh")
        {
            string result = "";
            if (itemCode != null)
            {
                itemCode = itemCode.ToString();
                var entity = sysDictionaries.FirstOrDefault(t => t.dictionaryCode == code && t.dictionaryItemCode == itemCode);
                if (entity != null)
                {
                    result = entity.dictionaryItemName;
                }
            }

            return result;
        }
    }
}
