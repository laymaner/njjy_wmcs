using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WISH.Helper.Common;
using Wish.ViewModel.Common.Dtos;


namespace Wish.ViewModel.DevConfig.PlcConfigVMs
{
    public partial class PlcConfigVM
    {
        public async Task<BusinessResult> SavePlcUseFlag(SavePlcUseFlagDto input)
        {
            BusinessResult result = new BusinessResult();
            var hasParentTransaction = false;
            string msg = string.Empty;
            string desc = "改变PLC有效状态:";
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
                var deviceConfigList = await DC.Set<PlcConfig>().Where(x => input.ids.Contains(x.ID)).ToListAsync();
                if (deviceConfigList.Any())
                {
                    foreach (var item in deviceConfigList)
                    {
                        if (input.isUseFlag == 1)
                        {
                            item.IsEnabled = true;
                        }
                        else if (input.isUseFlag == 0)
                        {
                            item.IsEnabled = false;
                        }
                        item.UpdateBy = input.invoker;
                        item.UpdateTime = DateTime.Now;
                    }
                    await ((DbContext)DC).Set<PlcConfig>().BulkUpdateAsync(deviceConfigList);
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
