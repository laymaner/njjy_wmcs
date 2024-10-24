using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;
using log4net;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using Com.Wish.Model.Business;
using Wish.HWConfig.Models;
using ASRS.WCS.Common.Enum;
using Wish.Model.System;
using AutoMapper.Internal.Mappers;
using System.Collections;
using Quartz.Util;
using Wish.Model.DevConfig;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.WcsCmd.SrmCmdVMs
{
    public partial class SrmCmdVM
    {
        /// <summary>
        /// 查询当前任务
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> GetCurrentTask()
        {
            BusinessResult result = new BusinessResult();
            List<CurrentTaskDto> currentTaskDtos = new List<CurrentTaskDto>();
            try
            {
                var srmCmdLists = await DC.Set<SrmCmd>().Where(x => x.Exec_Status < 90).ToListAsync();
                var inCmdLists = srmCmdLists.Where(x => x.Task_Type.Equals("IN")).ToList();
                CurrentTaskDto inCmdQty = new CurrentTaskDto()
                {
                    name = "入库",
                    value = inCmdLists.Count,
                };
                currentTaskDtos.Add(inCmdQty);
                CurrentTaskDto outCmdQty = new CurrentTaskDto()
                {
                    name = "出库",
                    value = srmCmdLists.Count - inCmdLists.Count,
                };
                currentTaskDtos.Add(outCmdQty);
                return result.Ok("查询成功", currentTaskDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        /// <summary>
        /// 查询历史指令
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns></returns>
        public async Task<BusinessResult> GetHistoryTask(string timePeriod)
        {
            BusinessResult result = new BusinessResult();
            List<HistoryTaskDto> historyTaskDtos = new List<HistoryTaskDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(timePeriod))
                {
                    List<string> timePeriodList = new List<string>() { "week", "month", "quarter", "year" };
                    if (!timePeriodList.Contains(timePeriod))
                    {
                        throw new Exception($"入参timePeriod:[{timePeriod}]不存在");
                    }
                    int currentYear = DateTime.Today.Year;
                    var cmdHisList = await DC.Set<SrmCmdHis>().Where(x => x.Recive_Date.Year == currentYear).ToListAsync();
                    if (timePeriod.Equals("week"))
                    {
                        DateTime startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                        DateTime endDate = startDate.AddDays(6);
                        var dailyTaskCounts = Enumerable.Range(0, 7)
                           .Select(i => new HistoryTaskDto
                           {
                               name = startDate.AddDays(i).DayOfWeek switch
                               {
                                   DayOfWeek.Sunday => "周日",
                                   DayOfWeek.Monday => "周一",
                                   DayOfWeek.Tuesday => "周二",
                                   DayOfWeek.Wednesday => "周三",
                                   DayOfWeek.Thursday => "周四",
                                   DayOfWeek.Friday => "周五",
                                   DayOfWeek.Saturday => "周六",
                                   _ => ""
                               },
                               value1 = cmdHisList.Count(task => task.Recive_Date >= startDate.AddDays(i) && task.Recive_Date < startDate.AddDays(i + 1) && task.Task_Type == "IN"),
                               value2 = cmdHisList.Count(task => task.Recive_Date >= startDate.AddDays(i) && task.Recive_Date < startDate.AddDays(i + 1) && task.Task_Type != "IN")
                           }).ToList();
                        historyTaskDtos.AddRange(dailyTaskCounts);
                    }
                    else if (timePeriod.Equals("month"))
                    {
                        DateTime startOfMonth = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
                        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                        DateTime startOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                        int daysSinceStartOfYear = (int)(DateTime.Today - startOfYear).TotalDays;
                        int currentWeekOfYear = (int)Math.Ceiling(daysSinceStartOfYear / 7.0);

                        var weeklyTaskCounts = Enumerable.Range(0, (int)Math.Ceiling((endOfMonth - startOfMonth).TotalDays / 7.0))
                          .Select(i => new HistoryTaskDto
                          {
                              name = $"第{currentWeekOfYear - (int)Math.Floor((DateTime.Today - startOfMonth.AddDays(i * 7)).TotalDays / 7.0)}周",
                              value1 = cmdHisList.Count(task => task.Recive_Date >= startOfMonth.AddDays(i * 7) && task.Recive_Date < startOfMonth.AddDays((i + 1) * 7) && task.Task_Type == "IN"),
                              value2 = cmdHisList.Count(task => task.Recive_Date >= startOfMonth.AddDays(i * 7) && task.Recive_Date < startOfMonth.AddDays((i + 1) * 7) && task.Task_Type != "IN")
                          }).ToList();
                        historyTaskDtos.AddRange(weeklyTaskCounts);
                    }
                    else if (timePeriod.Equals("quarter"))
                    {
                        DateTime currentDate = DateTime.Today;
                        int currentQuarter = (currentDate.Month - 1) / 3 + 1;
                        DateTime startOfQuarter = new DateTime(currentDate.Year, (currentQuarter - 1) * 3 + 1, 1);
                        DateTime endOfQuarter = startOfQuarter.AddMonths(3).AddDays(-1);

                        var monthlyTaskCounts = Enumerable.Range(0, 3)
                           .Select(i => new HistoryTaskDto
                           {
                               name = ($"第{startOfQuarter.AddMonths(i).Month}月"),
                               value1 = cmdHisList.Count(task => task.Recive_Date >= startOfQuarter.AddMonths(i) && task.Recive_Date < startOfQuarter.AddMonths(i + 1) && task.Task_Type == "IN"),
                               value2 = cmdHisList.Count(task => task.Recive_Date >= startOfQuarter.AddMonths(i) && task.Recive_Date < startOfQuarter.AddMonths(i + 1) && task.Task_Type != "IN")
                           }).ToList();
                        historyTaskDtos.AddRange(monthlyTaskCounts);
                    }
                    else if (timePeriod.Equals("year"))
                    {
                        DateTime startOfYear = new DateTime(currentYear, 1, 1);

                        var quarterlyTaskCounts = Enumerable.Range(0, 4)
                          .Select(i => new HistoryTaskDto
                          {
                              name = $"第{i + 1}季度",
                              //StartDate = startOfYear.AddMonths(i * 3),
                              //EndDate = startOfYear.AddMonths((i + 1) * 3).AddDays(-1),
                              value1 = cmdHisList.Count(task => task.Recive_Date >= startOfYear.AddMonths(i * 3) && task.Recive_Date <= startOfYear.AddMonths((i + 1) * 3).AddDays(-1) && task.Task_Type == "IN"),
                              value2 = cmdHisList.Count(task => task.Recive_Date >= startOfYear.AddMonths(i * 3) && task.Recive_Date <= startOfYear.AddMonths((i + 1) * 3).AddDays(-1) && task.Task_Type != "IN")
                          }).ToList();
                        historyTaskDtos.AddRange(quarterlyTaskCounts);
                    }
                }
                else
                {
                    throw new Exception($"入参timePeriod为空");
                }
                return result.Ok("查询成功", historyTaskDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        /// <summary>
        /// 查询出库任务
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> GetOutTask()
        {
            BusinessResult result = new BusinessResult();
            List<OutTaskDto> outTaskDtos = new List<OutTaskDto>();
            try
            {
                DateTime oneWeekAgo = DateTime.Today.AddDays(-7);
                var outTaskLists = await DC.Set<WmsTask>().Where(x => x.taskTypeNo == "OUT" && x.CreateTime > oneWeekAgo).Select(x => new OutTaskDto
                {
                    name = $"SRM{x.roadwayNo.Substring(x.roadwayNo.Length - 2, 2)}",
                    value1 = x.palletBarcode,
                    value2 = x.taskStatus < 10 ? "初始" : (x.taskStatus >= 10 && x.taskStatus < 90 ? "执行中" : "完成"),
                    createTime = x.CreateTime,
                }).OrderByDescending(x => x.createTime).Take(6).ToListAsync();
                outTaskDtos.AddRange(outTaskLists);
                return result.Ok("查询成功", outTaskDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<devInfoDto>> GetDeviceInfo()
        {
            List<devInfoDto> devInfoDtos = new List<devInfoDto>();
            var devConfigs = await DC.Set<DeviceConfig>().Include(x => x.PlcConfig).Include(x => x.StandardDevice).Where(x => x.StandardDevice.DeviceType == EDeviceType.S1Srm).Select(x => new devInfoDto
            {
                devNo = x.Device_Code,
                devName = x.Device_Name,
                devTypeNo = x.StandardDevice.DeviceType.ToString(),
                isConnect = x.IsOnline == true ? 1 : 0,
                usedFlag = x.IsEnabled == true ? 1 : 0,
            }).ToListAsync();
            if (devConfigs.Any())
            {
                List<string> list = new List<string>() { "DEV_TYPE_NO", "IS_CONNECT", "USED_FLAG" };
                var dicEntities = await DC.Set<SysDictionary>().Where(t => list.Contains(t.dictionaryCode)).AsNoTracking().ToListAsync();
                devConfigs.ForEach(t =>
                {
                    devInfoDto dto = DevMapper(t, dicEntities);
                    devInfoDtos.Add(dto);
                });
            }
            devInfoDtos = devInfoDtos.OrderBy(t => t.devTypeNo).ToList();
            return devInfoDtos;
        }
        private devInfoDto DevMapper(devInfoDto dto, List<SysDictionary> dicEntities)
        {
            if (dto != null)
            {
                dto.devTypeName = GetDisplayVal(dto.devTypeNo, dicEntities);
                dto.connectStatus = GetDisplayVal(dto.isConnect.ToString(), dicEntities);
                dto.usedStatus = GetDisplayVal(dto.usedFlag.ToString(), dicEntities);
            }
            return dto;
        }
        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="dicEntities"></param>
        /// <returns></returns>
        private string GetDisplayVal(string itemCode, List<SysDictionary> dicEntities)
        {
            string result = "";
            var dic = dicEntities.Where(t => t.dictionaryItemCode == itemCode).FirstOrDefault();
            if (dic != null)
            {
                result = dic.dictionaryName;
            }
            return result;
        }
        /// <summary>
        /// 获取当前报警信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<alarmInfoDto>> GetAlarmInfos()
        {
            List<alarmInfoDto> alarmInfoDtos = new List<alarmInfoDto>();
            var alarmEntities = await DC.Set<DeviceAlarmLog>().Where(t => t.HandleFlag == 0 || t.EndTime == null).AsNoTracking().OrderByDescending(t => t.OriginTime).Select(x => new alarmInfoDto
            {
                devNo = x.Device_Code,
                partNo = x.Device_Code,
                partLocNo = x.Device_Code,
                alarmCode = x.Message,
            }).ToListAsync();
            if (alarmEntities.Any())
            {
                var devPartEntities = await DC.Set<DeviceConfig>().AsNoTracking().ToListAsync();
                var dicEntities = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "ALARM_CODE").AsNoTracking().ToListAsync();
                alarmEntities.ForEach(t =>
                {
                    alarmInfoDto dto = alarmMapper(t, devPartEntities, dicEntities);
                    alarmInfoDtos.Add(dto);
                });
            }
            return alarmInfoDtos;
        }
        private alarmInfoDto alarmMapper(alarmInfoDto dto, List<DeviceConfig> devPartEntities, List<SysDictionary> sysDictionaries)
        {
            if (dto != null)
            {
                List<string> list = dto.alarmCode.Split(',').ToList();
                var dicLists = sysDictionaries.Where(t => list.Contains(t.dictionaryCode)).Select(x => x.dictionaryItemName).ToList();
                if (dicLists.Any())
                {
                    dto.alarmDesc = string.Join(",", dicLists);
                }
                var partEntity = devPartEntities.Where(t => t.Device_Code == dto.devNo).FirstOrDefault();
                if (partEntity != null)
                {
                    dto.partName = partEntity.Device_Name;
                    dto.partLocNo = partEntity.Device_Code;
                    dto.devNo = dto.partNo;
                    dto.devName = dto.partName;
                }
            }
            return dto;
        }

        /// <summary>
        /// 整线监控状态变化数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<DevDetailInfoDto>> GetDevDetailInfos()
        {
            List<DevDetailInfoDto> dtos = new List<DevDetailInfoDto>();
            var devDtos = await GetDevInfos();
            //var scanDtos = await GetScanInfos();
            dtos.AddRange(devDtos);
            //dtos.AddRange(scanDtos);
            return dtos;
        }

        private async Task<List<DevDetailInfoDto>> GetDevInfos()
        {
            var query = from devPart in DC.Set<DeviceConfig>().Include(x => x.PlcConfig).Include(x => x.StandardDevice)
                        join motDevPart in DC.Set<MotDevParts>() on devPart.Device_Code equals motDevPart.DevNo into tempMotDevPart
                        from motDevPart in tempMotDevPart.DefaultIfEmpty()
                        where motDevPart.PartNo != "0"
                        select new
                        {
                            devPart.Device_Code,
                            devPart.Device_Name,
                            devPart.IsEnabled,
                            motDevPart.CmdNo,
                            motDevPart.PalletNo,
                            motDevPart.DevRunMode,
                            motDevPart.IsFree,
                            motDevPart.IsHasGoods,
                            motDevPart.IsAlarming,
                            motDevPart.AlarmCode,
                            motDevPart.StationNo,
                            motDevPart.CurrentX,
                            motDevPart.CurrentY,
                            motDevPart.CurrentZ,
                            motDevPart.SrmRoadway,
                            devPart.StandardDevice.DeviceType,
                            devPart.PlcConfig.IsConnect
                        };
            var result = await query.ToListAsync();
            var dicEntities = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "DEV_TYPE_NO").ToListAsync();
            List<DevDetailInfoDto> dtos = new List<DevDetailInfoDto>();
            foreach (var t in result)
            {
                DevDetailInfoDto dto = new DevDetailInfoDto()
                {
                    devType = t.DeviceType.ToString(),
                    devTypeName = GetDictionaryItemName(t.DeviceType.ToString(), dicEntities),
                    code = t.Device_Code,
                    name = t.Device_Name,
                    usedFlag = t.IsEnabled == true ? 1 : 0,
                    palletNo = t.PalletNo,
                    cmdNo = t.CmdNo,
                    stationNo = t.StationNo,
                    x = t.CurrentX,
                    y = t.CurrentY,
                    z = t.CurrentZ,
                    isConnect = t.IsConnect == true ? 1 : 0,
                    isAlarm = t.IsAlarming,
                    isAuto = t.DevRunMode == 1 ? 1 : 0,
                    isFree = t.IsFree,
                    isHasGoods = t.IsHasGoods,
                    category = "NORMAL"
                };
                if ((t.DeviceType == EDeviceType.S1Srm || t.DeviceType == EDeviceType.S2Srm))
                {
                    dto.totalCol = await GetColCount(t.SrmRoadway);
                    var direction = GetDeviceDirect(t.Device_Code);
                    if (!string.IsNullOrWhiteSpace(direction))
                    {
                        dto.direction = direction;
                    }
                    var forkDto = result.Where(x => x.DeviceType == t.DeviceType && x.Device_Code == t.Device_Code && !x.PalletNo.IsNullOrWhiteSpace()).FirstOrDefault();
                    if (forkDto != null)
                    {
                        dto.isAuto = forkDto.DevRunMode == 1 ? 1 : 0;
                        dto.isFree = forkDto.IsFree;
                        dto.palletNo = forkDto.PalletNo;
                        dto.cmdNo = forkDto.CmdNo;
                        dto.isHasGoods = forkDto.IsHasGoods;
                        dto.isAlarm = forkDto.IsAlarming;
                        if (dto.isAlarm == 1 && dto.alarmMessage.IsNullOrWhiteSpace())
                            dto.alarmMessage = await GetAlarmMsg(forkDto.AlarmCode);
                        dto.x = forkDto.CurrentX;
                        dto.y = forkDto.CurrentY;
                        dto.z = forkDto.CurrentZ;
                    }
                }
                else if (t.DeviceType == EDeviceType.S1Srm || t.DeviceType == EDeviceType.S2Srm)
                {
                    dto.category = "FORK";
                }
                else if (t.DeviceType == EDeviceType.RGV)
                {
                    dto.loctionX = t.CurrentX;
                    dto.x = 0;
                    var direction = GetDeviceDirect(t.Device_Code);
                    if (!string.IsNullOrWhiteSpace(direction))
                    {
                        dto.direction = direction;
                        //dto.totalCol = rgvDto.Col;
                        //dto.length = rgvDto.Length;
                        //dto.y = CalCol(rgvDto.Length, dto.loctionX ?? 0, rgvDto.Col);
                    }
                }
                if (dto.isAlarm == 1 && dto.alarmMessage.IsNullOrWhiteSpace())
                    dto.alarmMessage = await GetAlarmMsg(t.AlarmCode);
                dtos.Add(dto);
            }
            return dtos;
        }

        private int CalCol(int length, int curX, int col)
        {
            if (length == 0)
                length = 1;
            int result = Convert.ToInt32(Math.Floor((double)(curX * col) / length));
            return result;
        }

        private async Task<int?> GetColCount(string roadwayNo)
        {
            int? count = 0;
            var rockInfo = await DC.Set<BasWRoadway>().Where(t => t.roadwayNo == roadwayNo).ToListAsync();
            if (rockInfo != null && rockInfo.Any())
            {
                //count = rockInfo.Select(t => t.ColSize).Max();
                if (count == null || count == 0)
                {
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.roadwayNo == roadwayNo).ToListAsync();
                    if (binInfo != null && binInfo.Any())
                    {
                        count = binInfo.Select(t => t.binCol).Max();
                    }
                }
            }
            else
            {
                var binInfo = await DC.Set<BasWBin>().Where(t => t.roadwayNo == roadwayNo).ToListAsync();
                if (binInfo != null && binInfo.Any())
                {
                    count = binInfo.Select(t => t.binCol).Max();
                }
            }
            return count;
        }

        private string GetDictionaryItemName(string itemCode, List<SysDictionary> dicEntities)
        {
            var result = "";
            var dic = dicEntities.FirstOrDefault(t => t.dictionaryItemCode == itemCode);
            if (dic != null)
            {
                result = dic.dictionaryItemName;
            }
            return result;
        }

        //private async Task<List<DevDetailInfoDto>> GetScanInfos()
        //{
        //    var query = from readBarcode in _MotReadBarcodeRepository
        //                select new
        //                {
        //                    readBarcode.LocNo,
        //                    readBarcode.LocName,
        //                    readBarcode.LocNameEn,
        //                    readBarcode.LocNameAlias,
        //                    readBarcode.Barcode
        //                };
        //    var result = await query.ToListAsync();
        //    var dicEntities = await _SysDictionaryRepository.GetListAsync(t => t.DictionaryCode == "DEV_TYPE_NO");
        //    List<DevDetailInfoDto> dtos = new List<DevDetailInfoDto>();
        //    result.ForEach(t =>
        //    {
        //        DevDetailInfoDto dto = new DevDetailInfoDto()
        //        {
        //            devType = "SCAN",
        //            devTypeName = GetDictionaryItemName("SCAN", dicEntities),
        //            code = t.LocNo,
        //            name = DataLocalize(t.LocName, t.LocNameEn, t.LocNameAlias),
        //            usedFlag = 1,
        //            palletNo = t.Barcode,
        //            category = "NORMAL"
        //        };
        //        dtos.Add(dto);
        //    });
        //    return dtos;
        //}
        private string GetDeviceDirect(string devNo)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            string direction = string.Empty;
            var devMotSection = configuration.GetSection("DevMot");
            if (devMotSection.Exists())
            {
                var devMotJObject = JObject.Parse(devMotSection.Value);
                foreach (var property in devMotJObject.Properties())
                {
                    var deviceName = property.Name;
                    if (devNo == deviceName)
                    {
                        direction = property.Value["Direction"]?.ToString();
                        if (direction != null)
                        {
                            return direction;
                        }
                    }

                }
            }
            return direction;
        }

        private async Task<string> GetAlarmMsg(string alarmCode)
        {
            var result = "";
            if (!alarmCode.IsNullOrWhiteSpace())
            {
                List<string> codeList = alarmCode.Split(',').ToList();
                var alarmInfos = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "ALARM_CODE").ToListAsync();
                if (alarmInfos != null)
                {
                    List<string> msgList = alarmInfos.Select(t => t.dictionaryItemName).Distinct().ToList();
                    result = string.Join(';', msgList);
                }
            }
            return result;
        }
    }
}
