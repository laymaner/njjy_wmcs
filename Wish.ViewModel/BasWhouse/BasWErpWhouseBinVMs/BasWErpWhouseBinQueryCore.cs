using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.ViewModel.Common.Dtos;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs
{
    public partial class BasWErpWhouseBinVM 
    {
        /// <summary>
        /// 根据ERP仓库号 区域编号 库区编号获取空闲库位
        /// </summary>
        /// <param name="erpWhouseNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="regionNo"></param>
        /// <param name="wRegionDeviceFlag">区域设备类型</param>
        public bool CheckExistFreeBin(string erpWhouseNo, string areaNo, string regionNo, WRegionDeviceFlag wRegionDeviceFlag)
        {
            var basWErpWhouseBins = DC.Set<BasWErpWhouseBin>()
                .Where(x => x.erpWhouseNo == erpWhouseNo && x.areaNo == areaNo && x.regionNo == regionNo);
            var basWRegions = DC.Set<BasWRegion>().Where(x => x.usedFlag == 1 && x.areaNo == areaNo && x.regionNo == regionNo/*&& x.regionTypeCode == wRegionDeviceFlag.GetCode()*/);
            var wmsStocks = DC.Set<WmsStock>();
            var query = from m1 in basWErpWhouseBins
                        join n1 in basWRegions
                            on m1.regionNo equals n1.regionNo
                        where !(from o in wmsStocks select o.binNo).Contains(m1.binNo)
                        select m1;

            return query.ToList().Count > 0;

        }

        /// <summary>
        /// 检查是否存在对应的erp货位
        /// </summary>
        /// <param name="erpWhouseNo"></param>
        /// <param name="regtionTypeCode"></param>
        /// <param name="wRegionDeviceFlag"></param>
        /// <returns></returns>
        public bool CheckExistBin(string erpWhouseNo, string regtionTypeCode, WRegionDeviceFlag wRegionDeviceFlag)
        {
            var basWErpWhouseBins = DC.Set<BasWErpWhouseBin>()
                .Where(x => x.erpWhouseNo == erpWhouseNo);
            var basWRegions = DC.Set<BasWRegion>().Where(x => x.usedFlag == 1 //&& x.regionDeviceFlag == wRegionDeviceFlag.GetCode()
            && x.regionTypeCode == regtionTypeCode);
            var query = from m1 in basWErpWhouseBins
                        join n1 in basWRegions
                            on m1.regionNo equals n1.regionNo
                        select m1;

            return query.ToList().Count > 0;
        }

        public bool CheckIsErpWhouseBin(string erpWhouseNo, string binNo)
        {
            bool isErpNo = false;
            var baserpWhouseNo = DC.Set<BasWErpWhouseBin>().Where(x => x.erpWhouseNo == erpWhouseNo && x.binNo == binNo).FirstOrDefault();
            if (baserpWhouseNo != null)
            {
                isErpNo = true;
            }
            //return DC.Set<BasWErpWhouseBin>().Where(x => x.erpWhouseNo == erpWhouseNo && x.binNo == binNo).Any();
            return isErpNo;
        }

        public async Task<bool> CheckIsErpWhouseBinAsync(string erpWhouseNo, string binNo)
        {
            bool isErpNo = false;
            var baserpWhouseNo = await DC.Set<BasWErpWhouseBin>().Where(x => x.erpWhouseNo == erpWhouseNo && x.binNo == binNo).FirstOrDefaultAsync();
            if (baserpWhouseNo != null)
            {
                isErpNo = true;
            }
            //return DC.Set<BasWErpWhouseBin>().Where(x => x.erpWhouseNo == erpWhouseNo && x.binNo == binNo).Any();
            return isErpNo;
        }
        /// <summary>
        /// 根据区域、分区、ERP编号获取货位信息
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="regionNo"></param>
        /// <param name="erpWhouseNo"></param>
        /// <returns></returns>
        public async Task<List<WRackBinDto>> GetErpWhouseBinsByAreaRegion(string areaNo, string regionNo, string erpWhouseNo)
        {
            BasWBinVM basWBinApiVM = Wtm.CreateVM<BasWBinVM>();
            List<WRackBinDto> binViews = await basWBinApiVM.GetRackBinInfoByAreaRegion(areaNo, regionNo);

            // 获取供应商货位信息
            List<BasWErpWhouseBin> supplierBins = await DC.Set<BasWErpWhouseBin>()
                .CheckEqual(areaNo, x => x.areaNo)
                .CheckEqual(regionNo, x => x.regionNo)
                .CheckEqual(erpWhouseNo, x => x.erpWhouseNo).ToListAsync();

            foreach (var item in binViews)
            {
                var query = from a in item.binList
                            join b in supplierBins on a.binNo equals b.binNo
                            select a;
                foreach (var obj in query.ToList())
                {
                    obj.isCheck = true;
                }
            }
            return binViews;
        }

        /// <summary>
        /// 更新Erp货位信息
        /// </summary>
        /// <param name="whouseBins"></param>
        public async Task SaveErpWhouseBins(BasWErpWhouseBinDto whouseBins)
        {
            bool hasParentTransaction = false;
            try
            {
                if (((DbContext)DC).Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }
                if (hasParentTransaction == false)
                {
                    await ((DbContext)DC).Database.BeginTransactionAsync();
                }
                // 处理逻辑 ====================================
                // 1、删除 ERPWhouseNo + Rack 对应的货位已有配置
                // 2、逐一添加新配置
                // =============================================
                List<BasWErpWhouseBin> addBins = new List<BasWErpWhouseBin>();
                List<BasWErpWhouseBin> delBins = new List<BasWErpWhouseBin>();
                foreach (var item in whouseBins.racks)
                {
                    BasWErpWhouseBin basWErp = new BasWErpWhouseBin();
                    var bins = await ((DbContext)DC).Set<BasWErpWhouseBin>().Where(x => x.regionNo == item.regionNo && x.areaNo == item.areaNo && x.erpWhouseNo == whouseBins.erpWhouseNo).ToListAsync();
                    foreach (var del in bins)
                    {
                        delBins.Add(del);
                    }
                    foreach (var bin in item.binList)
                    {
                        //新增货位
                        BasWErpWhouseBin erpBin = new BasWErpWhouseBin();
                        erpBin.areaNo = item.areaNo;
                        erpBin.regionNo = item.regionNo;
                        erpBin.delFlag = "0";
                        erpBin.whouseNo = "TZ";
                        //erpBin.IsValid = true;
                        erpBin.CreateTime = DateTime.Now;
                        erpBin.CreateBy = LoginUserInfo?.ITCode;
                        erpBin.binNo = bin;
                        erpBin.erpWhouseNo = whouseBins.erpWhouseNo;
                        addBins.Add(erpBin);
                    }

                }
                if (delBins.Count > 0)
                {
                    await ((DbContext)DC).Set<BasWErpWhouseBin>().BulkDeleteAsync(delBins);
                }
                if (addBins.Count > 0)
                {
                    await ((DbContext)DC).Set<BasWErpWhouseBin>().AddRangeAsync(addBins);
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                if (hasParentTransaction == false)
                {
                    await ((DbContext)DC).Database.CommitTransactionAsync();
                }
            }
            catch (Exception e)
            {
                if (hasParentTransaction == false)
                {
                    await ((DbContext)DC).Database.RollbackTransactionAsync();
                }
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据ERP仓库号和库位号获取ERP库位信息
        /// </summary>
        /// <param name="erpWhouseNo"></param>
        /// <param name="binNo"></param>
        /// <returns></returns>
        public BasWErpWhouseBin GetErpWhouseBin(string erpWhouseNo, string binNo)
        {
            return DC.Set<BasWErpWhouseBin>().Where(x => x.erpWhouseNo == erpWhouseNo && x.binNo == binNo).FirstOrDefault();
        }
    }
}
