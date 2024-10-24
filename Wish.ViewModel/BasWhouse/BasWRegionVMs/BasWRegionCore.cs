using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.BasWhouse.BasWRegionVMs
{
    public partial class BasWRegionVM 
    {
        /// <summary>
        /// 获取库区信息
        /// </summary>
        /// <param name="regionNo"></param>
        /// <returns></returns>
        public BasWRegion GetRegion(string regionNo)
        {
            return DC.Set<BasWRegion>().Where(x => x.regionNo == regionNo).FirstOrDefault();
        }
        public async Task<BasWRegion> GetRegionAsync(string regionNo)
        {
            return await DC.Set<BasWRegion>().Where(x => x.regionNo == regionNo).FirstOrDefaultAsync();
        }
        //根据
        public BasWRegion GetRegionByAreaAndType(string regionNo, string regionTypeCode, string areaNo)
        {
            return DC.Set<BasWRegion>().Where(x => x.regionNo == regionNo && x.regionTypeCode == regionTypeCode && x.areaNo == areaNo).FirstOrDefault();
        }
        public async Task<BasWRegion> GetRegionByAreaAndTypeAsync(string regionNo, string regionTypeCode, string areaNo)
        {
            return await DC.Set<BasWRegion>().Where(x => x.regionNo == regionNo && x.regionTypeCode == regionTypeCode && x.areaNo == areaNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取区域 + 库区类型对应的库区信息
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="regionType"></param>
        /// <returns></returns>
        public List<BasWRegion> GetRegionByRegionType(string areaNo, string regionTypeCode)
        {
            return DC.Set<BasWRegion>().Where(x =>
                    x.areaNo == areaNo && x.regionTypeCode == regionTypeCode && x.usedFlag == 1)
                .ToList();
        }
        public async Task<BasWRegion> GetRegionByRegionTypeAsync(string areaNo, string regionTypeCode)
        {
            return await DC.Set<BasWRegion>().Where(x =>
                    x.areaNo == areaNo && x.regionTypeCode == regionTypeCode && x.usedFlag == 1)
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据区域编号获取对应库区
        /// </summary>
        /// <param name="areaNo"></param>
        /// <returns></returns>
        public BasWRegion getBasWRegionByAreaNo(String areaNo)
        {
            BasWRegion region = DC.Set<BasWRegion>()
                .Where(x => x.usedFlag == 1 && x.areaNo == areaNo).FirstOrDefault();
            return region;
        }

        /// <summary>
        /// 根据区域编号获取对应库区编号
        /// </summary>
        /// <param name="areaNo"></param>
        /// <returns></returns>
        public String getRegionNoByAreaNo(String areaNo)
        {
            BasWRegion region = DC.Set<BasWRegion>()
                .Where(x => x.usedFlag == 1 && x.areaNo == areaNo).FirstOrDefault();
            if (region == null)
            {
                return "";
            }

            return region.regionNo;
        }
    }
}
