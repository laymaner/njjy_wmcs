using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.BasWhouse.BasWWhouseVMs
{
    public partial class BasWWhouseVM
    {
        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <returns></returns>
        public async Task<BasWWhouse> GetWhouseInfo()
        {
            return await DC.Set<BasWWhouse>().FirstOrDefaultAsync();
        }
    }
}
