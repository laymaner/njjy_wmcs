using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using log4net;
using Wish.ViewModel.Interface.InterfaceConfigVMs;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigVM : BaseCRUDVM<DeviceConfig>
    {
        public async Task<BusinessResult> SaveDeviceUseFlag(SaveDeviceUseFlagDto input)
        {
            BusinessResult result = new BusinessResult();
            var hasParentTransaction = false;
            string desc = "改变设备有效状态:";
            string msg = string.Empty;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                var deviceConfigList=await DC.Set<DeviceConfig>().Where(x=>input.ids.Contains(x.ID)).ToListAsync();
                if (deviceConfigList.Any())
                {
                    foreach (var item in deviceConfigList)
                    {
                        if (input.isUseFlag==1)
                        {
                            item.IsEnabled = true;
                        }
                        else if (input.isUseFlag==0)
                        {
                            item.IsEnabled = false;
                        }
                        else if (input.isUseFlag == 2)
                        {
                            item.Plc2WcsStep = 0;
                            item.Wcs2PlcStep = 0;
                        }else if (input.isUseFlag == 3)
                        {
                            item.Mode = 10;
                        }
                        item.UpdateBy=input.invoker;
                        item.UpdateTime=DateTime.Now;
                    }
                    await ((DbContext)DC).Set<DeviceConfig>().BulkUpdateAsync(deviceConfigList);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }
                msg = $"{desc}{ex.Message}";
                logger.Warn($"----->----->{desc}:{msg} ");
                return result.Error(msg);
            }
            msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(input)}】";
            logger.Warn($"----->----->{desc}:{msg} ");
            return result.Success(msg);
        }
    }
}
