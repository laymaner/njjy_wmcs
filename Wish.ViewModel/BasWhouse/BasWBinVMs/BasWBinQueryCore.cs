using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using Com.Wish.Model.Base;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.Protocols;
using WISH.Helper.Common;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using AutoMapper;
using Wish.ViewModel.BasWhouse.BasWRackVMs;
using Wish.ViewModel.Common.Dtos;

namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinVM
    {
        /// <summary>
        /// 根据区域编号获取对应的货位信息
        /// </summary>
        /// <param name="regionCode"></param>
        /// <returns></returns>
        public List<BasWBin> GetBinByRegionNo(string areaNo, string regionCode)
        {
            return DC.Set<BasWBin>().Where(x => x.areaNo == areaNo && x.regionNo == regionCode && x.usedFlag == 1).ToList();
        }

        public async Task<BasWBin> GetBinByRegionNoAsync(string areaNo, string regionCode)
        {
            return await DC.Set<BasWBin>().Where(x => x.areaNo == areaNo && x.regionNo == regionCode && x.usedFlag == 1).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据库位号获取库位信息
        /// </summary>
        /// <param name="binNo"></param>
        /// <returns></returns>
        public BasWBin GetBinByBinNo(string binNo)
        {
            return DC.Set<BasWBin>().Where(x => x.binNo == binNo).FirstOrDefault();
        }
        public async Task<BasWBin> GetBinByBinNoAsync(string binNo)
        {
            return await DC.Set<BasWBin>().Where(x => x.binNo == binNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取所有库位(外部接口导入暂时用)
        /// </summary>
        /// <returns></returns>
        public List<BasWBin> GetAllBasWBin()
        {
            return DC.Set<BasWBin>().Where(x => x.usedFlag == 1 && x.binNo != "RC01_00_010101").ToList();
        }

        public async Task<List<BasWBin>> GetBinByAreaRegion(string areaNo, string regionNo)
        {
            List<BasWBin> bins = await DC.Set<BasWBin>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo).ToListAsync();
            return bins;
        }
        public List<BasWBin> GetBinByAreaRegionErpBin(string areaNo, string regionNo, List<string> erpBinList)
        {
            List<BasWBin> bins = DC.Set<BasWBin>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo && erpBinList.Contains(x.binNo)).ToList();
            return bins;
        }
        public List<BasWErpWhouseBin> GetErpBinbyAreaRegionErp(string areaNo, string regionNo, string erpWhouseNo)
        {
            List<BasWErpWhouseBin> bins = DC.Set<BasWErpWhouseBin>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo && x.erpWhouseNo == erpWhouseNo).ToList();
            return bins;
        }
        /// <summary>
        /// 根据区域、分区信息获取货位信息
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="regionNo"></param>
        /// <returns></returns>
        public async Task<List<WRackBinDto>> GetRackBinInfoByAreaRegion(string areaNo, string regionNo)
        {
            BasWRackVM basWRackApiVM = Wtm.CreateVM<BasWRackVM>();

            // 1 - 获取货架信息
            List<BasWRack> racks = await DC.Set<BasWRack>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo).ToListAsync();

            // 2 - 获取货位信息
            List<BasWBin> bins = await GetBinByAreaRegion(areaNo, regionNo);

            // 3 - 组织返回数据
            List<WRackBinDto> binViews = new List<WRackBinDto>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BasWRack, WRackBinDto>());
            var mapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BasWBin, WBinDto>());
            var mapper2 = config2.CreateMapper();
            foreach (var item in racks)
            {
                WRackBinDto rbv = mapper.Map<WRackBinDto>(item);
                List<BasWBin> fbins = bins.Where(x => x.rackNo == item.rackNo).ToList();
                foreach (var bin in fbins)
                {
                    rbv.binList.Add(mapper2.Map<WBinDto>(bin));
                }
                binViews.Add(rbv);
            }
            return binViews;
        }
        /// <summary>
        /// 根据区域、分区、ERO仓库信息获取货位信息
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="regionNo"></param>
        /// <param name="erpWhouseNo"></param>
        /// <returns></returns>
        public async Task<List<WRackBinDto>> GetRackBinInfoByAreaRegionErp(string areaNo, string regionNo, string erpWhouseNo)
        {
            BasWRackVM basWRackApiVM = Wtm.CreateVM<BasWRackVM>();

            // 1 - 获取货架信息
            List<BasWRack> racks = await DC.Set<BasWRack>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo).ToListAsync();
            // 2-1 - 获取ERP仓库货位
            List<BasWErpWhouseBin> erpBins = await DC.Set<BasWErpWhouseBin>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo && x.erpWhouseNo == erpWhouseNo).ToListAsync();
            var erpBinList = erpBins.Select(x => x.binNo).ToList();
            // 2-2 - 获取货位信息
            List<BasWBin> bins = await DC.Set<BasWBin>().Where(x => x.regionNo == regionNo && x.areaNo == areaNo && erpBinList.Contains(x.binNo)).ToListAsync();

            // 3 - 组织返回数据
            List<WRackBinDto> binViews = new List<WRackBinDto>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BasWRack, WRackBinDto>());
            var mapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BasWBin, WRackBinDto>());
            var mapper2 = config2.CreateMapper();
            foreach (var item in racks)
            {
                WRackBinDto rbv = mapper.Map<WRackBinDto>(item);
                List<BasWBin> fbins = bins.Where(x => x.rackNo == item.rackNo).ToList();
                foreach (var bin in fbins)
                {
                    rbv.binList.Add(mapper2.Map<WBinDto>(bin));
                }
                binViews.Add(rbv);
            }
            return binViews;
        }

        /// <summary>
        /// 库位统计
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> GetBinStatisticsAsync()
        {
            BusinessResult result = new BusinessResult();
            List<BinStatisticsDto> binStatisticsDtos = new List<BinStatisticsDto>();
            try
            {
                var queryBin = await DC.Set<BasWBin>().Where(x => x.usedFlag == 1).ToListAsync();
                var queryOccupy = await DC.Set<WmsStock>().Where(x => !string.IsNullOrWhiteSpace(x.binNo) && x.stockStatus < 90).GroupJoin(DC.Set<BasWBin>().Where(x => x.binErrFlag == "0"), a => a.binNo, b => b.binNo, (a, b) => new
                {
                    binNo = a.binNo,
                    bin = b
                }).SelectMany(a => a.bin.DefaultIfEmpty(), (a, b) => new
                {
                    binNo = a.binNo,
                    binName = b.binName,
                }).ToListAsync();
                var occupyCount = queryOccupy.Count();
                BinStatisticsDto occupyQty = new BinStatisticsDto()
                {
                    name = "占用",
                    value = occupyCount,
                };
                binStatisticsDtos.Add(occupyQty);
                var abnormalCount = queryBin.Where(x => x.binErrFlag != "0" ).Count();
                BinStatisticsDto abnormalQty = new BinStatisticsDto()
                {
                    name = "异常",
                    value = abnormalCount,
                };
                binStatisticsDtos.Add(abnormalQty);
                var emptyCount = queryBin.Count() - occupyCount - abnormalCount;
                BinStatisticsDto emptyQty = new BinStatisticsDto()
                {
                    name = "空闲",
                    value = emptyCount,
                };
                binStatisticsDtos.Add(emptyQty);
                return result.Ok("查询成功", binStatisticsDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
         
        /// <summary>
        /// 巷道统计
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> GetRoadwayStatisticsAsync()
        {
            BusinessResult result = new BusinessResult();
            List<RoadwayStatisticsDto> roadwayStatisticsDtos = new List<RoadwayStatisticsDto>();
            try
            {
                var roadwayLists = await DC.Set<BasWRoadway>().Where(x => x.usedFlag == 1).ToListAsync();
                var binLists = await DC.Set<BasWBin>().Where(x => x.usedFlag == 1).ToListAsync();
                var stockList = await DC.Set<WmsStock>().Where(x=>!string.IsNullOrWhiteSpace(x.binNo) && x.stockStatus<90).ToListAsync();
                foreach (var item in roadwayLists)
                {
                    var roadwayBins = binLists.Where(x => x.roadwayNo == item.roadwayNo).ToList();
                    var stockListBins = stockList.Where(x => x.roadwayNo == item.roadwayNo).ToList();
                    var queryOccupy = stockListBins.GroupJoin(roadwayBins, a => a.binNo, b => b.binNo, (a, b) => new
                    {
                        binNo = a.binNo,
                        bin = b
                    }).SelectMany(a => a.bin.DefaultIfEmpty(), (a, b) => new
                    {
                        binNo = a.binNo,
                        binName = b.binName,
                    }).ToList();
                    var occupyCount = queryOccupy.Count() + roadwayBins.Where(x => x.binErrFlag != "0").Count();
                    var emptyCount = roadwayBins.Count() - occupyCount;

                    RoadwayStatisticsDto occupyQty = new RoadwayStatisticsDto()
                    {
                        name=item.roadwayName,
                        value1 = emptyCount,
                        value2 = occupyCount,
                    };
                    roadwayStatisticsDtos.Add(occupyQty);
                }
                return result.Ok("查询成功", roadwayStatisticsDtos);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
