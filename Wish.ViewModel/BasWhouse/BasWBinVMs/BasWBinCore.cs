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
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Data;
using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Z.BulkOperations;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.System.SysSequenceVMs;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinVM
    {
        #region 分配库位

        /// <summary>
        /// 分配类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<BusinessResult> GetBinAllotType(AllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult() { code = ResCode.Error, outParams = input };
            string allotType = string.Empty;
            //DictonaryHelper.QueryType.Cur.GetCode()
            if (input == null)
            {
                return result.Error($"入参为空");
            }
            if (input.iqcResultId == null)
            {
                if (!string.IsNullOrWhiteSpace(input.roadwayNos) && !string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    var palletTypeInfo = await GetPalletType(input.palletBarcode);
                    if (palletTypeInfo == null)
                    {
                        return result.Error($"入参载体条码【{input.palletBarcode}】未找到对应载体类型");
                    }
                    //获取巷道集合
                    var roadwayList = input.roadwayNos.Split(',').ToList();
                    roadwayList = roadwayList.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    //查找巷道
                    var roadwayInfos = await DC.Set<BasWRoadway>().Where(t => roadwayList.Contains(t.roadwayNo) && t.errFlag == 0 && t.usedFlag == 1 && !string.IsNullOrWhiteSpace(t.regionNo)).ToListAsync();
                    if (roadwayInfos.Any())
                    {
                        int dtlCount = roadwayInfos.Select(t => t.regionNo).Distinct().Count();
                        if (dtlCount > 1)
                        {
                            return result.Error($"根据载体条码【{input.palletBarcode}】，巷道字符串集合【{input.roadwayNos}】找到巷道信息所属多个库区，请检查数据");
                        }
                        //获取库区
                        var regionNo = roadwayInfos.FirstOrDefault().regionNo;
                        var regionInfo = await DC.Set<BasWRegion>().Where(t => t.regionNo == regionNo && t.usedFlag == 1).FirstOrDefaultAsync();
                        if (regionInfo != null)
                        {
                            if (regionInfo.pickupMethod == "SRM" && palletTypeInfo.palletTypeCode != "BX")
                            {
                                allotType = DictonaryHelper.AllotBinType.TPK.GetCode();
                                result.code = ResCode.OK;
                                result.outParams = allotType;
                            }
                            else if (regionInfo.pickupMethod == "MTV" && palletTypeInfo.palletTypeCode == "BX")
                            {
                                allotType = DictonaryHelper.AllotBinType.LXK.GetCode();
                                result.code = ResCode.OK;
                                result.outParams = allotType;
                            }
                            else
                            {
                                return result.Error($"载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}({palletTypeInfo.palletTypeCode})】与巷道集合【{input.roadwayNos}】对应库区【{regionInfo.regionName}({regionInfo.regionNo})】不匹配");
                            }

                        }
                        else
                        {
                            return result.Error($"根据载体条码【{input.palletBarcode}】，巷道字符串集合【{input.roadwayNos}】找到对应库区编号【{regionNo}】，但未找到对应库区记录，请检查WMS库区表是否配置或启用");
                        }


                    }
                    else
                    {
                        return result.Error($"根据载体条码【{input.palletBarcode}】，巷道字符串集合【{input.roadwayNos}】未找到巷道信息,请检查WMS巷道表是否存在记录、是否异常、是否配置库区或启用");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(input.roadwayNos))
                    {
                        return result.Error($"巷道集合为空");
                    }
                    if (string.IsNullOrWhiteSpace(input.palletBarcode))
                    {
                        return result.Error($"载体条码为空");
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    allotType = DictonaryHelper.AllotBinType.PK.GetCode();
                    result.code = ResCode.OK;
                    result.outParams = allotType;
                }
                else
                {

                }
            }
            return result;
        }


        /// <summary>
        /// 分配库位入口(推荐库位)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> AllotBin(AllotBinInputDto input)
        {
            var result = new BusinessResult();
            string allotType = string.Empty;
            result = await GetBinAllotType(input);
            if (result.code == ResCode.Error)
            {
                return result;
            }
            else
            {
                if (result.outParams != null)
                {
                    allotType = result.outParams.ToString();
                }
            }

            if (allotType == AllotBinType.PK.GetCode())
            {
                result = await AllotBinForPk(input);
            }
            else if (allotType == AllotBinType.TPK.GetCode())
            {
                result = await AllotBinForTPK(input);
            }
            else if (allotType == AllotBinType.LXK.GetCode())
            {
                result = await AllotBinForLXK(input);
            }
            else
            {
                result.code = ResCode.Error;
                result.msg = result.msg + "分配库位类型为空，根据入参无法判别";
                result.outParams = input;
            }
            return result;
        }



        #region 平库
        public async Task<bool> iSElecMaterial(string materialCode)
        {
            bool result = false;
            var matInfo = await DC.Set<BasBMaterial>().Where(t => t.MaterialCode == materialCode).FirstOrDefaultAsync();
            if (matInfo != null)
            {
                var matCateInfo = await DC.Set<BasBMaterialCategory>().Where(t => t.materialCategoryCode == matInfo.MaterialCategoryCode).FirstOrDefaultAsync();
                if (matCateInfo != null)
                {
                    if (matCateInfo.materialFlag == MaterialFlag.Electronic.GetCode())
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private async Task<BusinessResult> QueryBinForPk(WmsInReceiptIqcResult iqcResultInfo, List<BasBSupplierBin> supperList)
        {
            BusinessResult result = new BusinessResult();
            //获取物料大类,是否是电子料
            var isElecFlag = await iSElecMaterial(iqcResultInfo.materialCode);
            var regionInfos = await DC.Set<BasWRegion>().Where(t => t.pickupMethod == "PDA" && t.sdType == "PK").AsNoTracking().ToListAsync();
            List<string> regionList = regionInfos.Select(t => t.regionNo).Distinct().ToList();
            //if (isElecFlag)
            if (isElecFlag && !iqcResultInfo.erpWhouseNo.Equals("01B"))
            {
                regionInfos = await DC.Set<BasWRegion>().Where(t => t.pickupMethod == "PTL").AsNoTracking().ToListAsync();
                if (regionInfos.Count > 0)
                {
                    regionList = regionInfos.Select(t => t.regionNo).Distinct().ToList();
                }
            }
            List<AllotBinResultDto> queryResult = new List<AllotBinResultDto>();
            if (supperList.Any())
            {
                var supperBinList = supperList.Select(t => t.binNo).ToList();
                var idleQuery = (from bwewb in DC.Set<BasWErpWhouseBin>()
                                 join bbin in DC.Set<BasWBin>() on new { bwewb.regionNo, bwewb.binNo } equals new { bbin.regionNo, bbin.binNo }
                                 where bwewb.areaNo == iqcResultInfo.areaNo && bwewb.erpWhouseNo == iqcResultInfo.erpWhouseNo && bwewb.delFlag == "0" && bbin.binErrFlag == "0" && bbin.usedFlag == 1
                                       && supperBinList.Contains(bwewb.binNo) && regionList.Contains(bwewb.regionNo)
                                 select new AllotBinResultDto
                                 {
                                     binNo = bwewb.binNo,
                                     areaNo = bwewb.areaNo,
                                     erpWhouseNo = bwewb.erpWhouseNo,
                                     binPriority = bbin.binPriority
                                 }).OrderBy(x => x.binPriority);
                queryResult = await idleQuery.ToListAsync();
                if (queryResult != null && queryResult.Any())
                {
                    var binInfo = queryResult.FirstOrDefault();
                    result.msg = "库位分配成功";
                    result.code = ResCode.OK;
                    result.outParams = binInfo;
                    return result;
                }
            }
            //2.2 根据物料编码、ERP仓库、区域、在库状态、批次号获取存在相同物料的库存明细数组，并关联库存表
            //注意：此处库位没有库存码放量
            var query = (from uniicode in DC.Set<WmsStockUniicode>()
                         join main in DC.Set<WmsStock>() on uniicode.stockCode equals main.stockCode into mainTemp
                         from main in mainTemp.DefaultIfEmpty()
                         join bbin in DC.Set<BasWBin>() on main.binNo equals bbin.binNo into tempBin
                         from bbin in tempBin.DefaultIfEmpty()
                         where uniicode.materialCode == iqcResultInfo.materialCode && uniicode.areaNo == iqcResultInfo.areaNo && uniicode.erpWhouseNo == iqcResultInfo.erpWhouseNo && main.stockStatus == 50 && uniicode.batchNo == iqcResultInfo.batchNo
                         && regionList.Contains(bbin.regionNo)
                         select new AllotBinResultDto
                         {
                             binNo = main.binNo,
                             areaNo = uniicode.areaNo,
                             erpWhouseNo = uniicode.erpWhouseNo,
                             binPriority = bbin.binPriority
                         }).OrderBy(x => x.binPriority);
            queryResult = await query.ToListAsync();
            //2.3 如果存在[同物料、同批次]库存，取出库存内优先级最高的库位
            if (queryResult != null && queryResult.Any())
            {
                var binInfo = queryResult.FirstOrDefault();
                result.msg = "库位分配成功";
                result.code = ResCode.OK;
                result.outParams = binInfo;
                return result;
            }
            else
            {
                var info = (from dtl in DC.Set<WmsStockDtl>()
                            join main in DC.Set<WmsStock>() on dtl.stockCode equals main.stockCode into mainTemp
                            from main in mainTemp.DefaultIfEmpty()
                            join bbin in DC.Set<BasWBin>() on main.binNo equals bbin.binNo into tempBin
                            from bbin in tempBin.DefaultIfEmpty()
                            where dtl.materialCode == iqcResultInfo.materialCode && dtl.areaNo == iqcResultInfo.areaNo && dtl.erpWhouseNo == iqcResultInfo.erpWhouseNo && dtl.stockDtlStatus == 50
                                    && regionList.Contains(bbin.regionNo)
                            select new AllotBinResultDto
                            {
                                binNo = main.binNo,
                                areaNo = dtl.areaNo,
                                erpWhouseNo = dtl.erpWhouseNo,
                                binPriority = bbin.binPriority
                            }).OrderBy(x => x.binPriority);
                //2.4.1 取出同物料，库位优先级高的库存
                queryResult = await info.ToListAsync();
                if (queryResult != null && queryResult.Any())
                {
                    var binInfo = queryResult.FirstOrDefault();
                    result.msg = "库位分配成功";
                    result.code = ResCode.OK;
                    result.outParams = binInfo;
                    return result;
                }
                else
                {
                    //  2.5 获取同区域、同ERP仓库内优先级最高的空闲库位
                    var idleInfo = (from bwewb in DC.Set<BasWErpWhouseBin>()
                                    join bbin in DC.Set<BasWBin>() on new { bwewb.regionNo, bwewb.binNo } equals new { bbin.regionNo, bbin.binNo }
                                    where bwewb.areaNo == iqcResultInfo.areaNo && bwewb.erpWhouseNo == iqcResultInfo.erpWhouseNo && bwewb.delFlag == "0" && bbin.binErrFlag == "0" && bbin.usedFlag == 1
                                          && regionList.Contains(bwewb.regionNo)
                                    select new AllotBinResultDto
                                    {
                                        binNo = bwewb.binNo,
                                        areaNo = bwewb.areaNo,
                                        erpWhouseNo = bwewb.erpWhouseNo,
                                        binPriority = bbin.binPriority,
                                        regionNo = bbin.regionNo,
                                    }).OrderBy(x => x.binPriority);
                    // 2.51 取出优先级最高的空闲库位
                    queryResult = await idleInfo.ToListAsync();
                    if (queryResult != null && queryResult.Any())
                    {
                        var binInfo = queryResult.FirstOrDefault();
                        result.msg = "库位分配成功";
                        result.code = ResCode.OK;
                        result.outParams = binInfo;
                        return result;
                    }
                    else
                    {
                        result.msg = "不存在可用库位";
                        result.code = ResCode.Error;
                        return result;
                    }
                }

            }


        }
        /// <summary>
        /// 平库分配
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> AllotBinForPk(AllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult();
            var iqcResultInfo = await DC.Set<WmsInReceiptIqcResult>().Where(t => t.ID == input.iqcResultId).FirstOrDefaultAsync();
            if (iqcResultInfo == null)
            {
                return result.Error($"平库分配库位失败，根据质检结果id【{input.iqcResultId}】未找到质检结果记录");
            }
            if (iqcResultInfo.iqcResultStatus >= 90)
            {
                return result.Error($"平库分配库位失败，质检结果id【{input.iqcResultId}】对应质检结果记录状态不可入库，状态为【{iqcResultInfo.iqcResultStatus}】，小于90可入库");
            }
            List<BasBSupplierBin> supperList = new List<BasBSupplierBin>();
            if (!string.IsNullOrWhiteSpace(iqcResultInfo.supplierCode) && iqcResultInfo.erpWhouseNo.Equals("01B"))
            {
                supperList = await DC.Set<BasBSupplierBin>().Where(x => x.supplierCode == iqcResultInfo.supplierCode).ToListAsync();
            }
            result = await QueryBinForPk(iqcResultInfo, supperList);
            return result;
        }
        #endregion

        #region 立库通用
        /// <summary>
        /// 通过托盘号获取托盘类型
        /// </summary>
        /// <param name="PalletBarcode">托盘号</param>
        /// <returns></returns>
        public async Task<BasWPalletType> GetPalletType(string PalletBarcode)
        {
            var PalletType = (await DC.Set<BasWPalletType>().AsNoTracking().ToListAsync()).Where(t => Regex.IsMatch(PalletBarcode, t.checkFormula) == true).FirstOrDefault();
            return PalletType;
        }
        /// <summary>
        /// 获取分配库位策略
        /// </summary>
        /// <param name="regionNo"></param>
        /// <returns></returns>
        public async Task<BusinessResult> GetStrategyDtlsForBinAllot(string regionNo)
        {
            BusinessResult result = new BusinessResult();
            AlootBinStrategyDto strategyView = new AlootBinStrategyDto();
            List<CfgStrategyDtl> strategyDtls = new List<CfgStrategyDtl>();
            var relationInfos = await DC.Set<CfgRelationship>().Where(t => t.relationshipTypeCode == "Region&BinAllotStrategy" && t.leftCode == regionNo).FirstOrDefaultAsync();
            if (relationInfos != null)
            {
                strategyDtls = await DC.Set<CfgStrategyDtl>().Where(t => t.strategyNo == relationInfos.rightCode /*&& t.usedFlag == 1*/).ToListAsync();
                if (strategyDtls.Count > 0)
                {
                    var distributed = strategyDtls.FirstOrDefault(t => t.strategyItemNo == "Distributed");
                    if (distributed != null)
                    {
                        List<string> vals1 = new List<string>() { "0", "1", "2" };
                        if (vals1.Contains(distributed.strategyItemValue1))
                        {
                            strategyView.distributed = distributed.strategyItemValue1;
                        }
                        else
                        {
                            return result.Error($"当前库区【{regionNo}】配置的库位分配策略【{relationInfos.rightCode}】中，【均布分布(Distributed)】策略明细记录取值不在有效范围内(0,1,2)，请检查数据是否配置或启用");
                        }
                    }
                    else
                    {
                        return result.Error($"当前库区【{regionNo}】配置的库位分配策略【{relationInfos.rightCode}】未找到【均布分布(Distributed)】策略明细记录，请检查数据是否配置或启用");
                    }

                    var storeNearby = strategyDtls.FirstOrDefault(t => t.strategyItemNo == "StoreNearby");
                    if (storeNearby != null)
                    {
                        List<string> vals1 = new List<string>() { "0", "1" };
                        if (vals1.Contains(storeNearby.strategyItemValue1))
                        {
                            strategyView.storeNearby = storeNearby.strategyItemValue1;
                        }
                        else
                        {
                            return result.Error($"当前库区【{regionNo}】配置的库位分配策略【{relationInfos.rightCode}】中，【巷道内分布(StoreNearby)】策略明细记录取值不在有效范围内(0,1)，请检查数据是否配置或启用");
                        }
                    }
                    else
                    {
                        return result.Error($"当前库区【{regionNo}】配置的库位分配策略【{relationInfos.rightCode}】未找到【巷道内分布(StoreNearby)】策略明细记录，请检查数据是否配置或启用");
                    }
                    result.outParams = strategyView;

                }
                else
                {
                    return result.Error($"当前库区【{regionNo}】配置的库位分配策略【{relationInfos.rightCode}】未找到可用策略明细记录，请检查数据是否配置或启用");
                }
            }
            else
            {
                return result.Error($"当前库区【{regionNo}】未配置【库区和库位分配策略对应关系】");
            }
            return result;
        }
        /// <summary>
        /// 获取可用库位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CanUseBinDto>> GetCanUseBinInfos(AllotBinForMatDto input, List<BasWRoadway> roadwayInfos)
        {
            List<CanUseBinDto> result = new List<CanUseBinDto>();
            //获取库区
            var regionNo = roadwayInfos.Select(x => x.regionNo).Distinct().FirstOrDefault();
            //获取巷道
            var roadwayNoLists = roadwayInfos.Select(x => x.roadwayNo).ToList();
            //库存库位
            var stockInfos = await ((DbContext)DC).Set<WmsStock>().Where(t => !string.IsNullOrWhiteSpace(t.binNo) && t.regionNo.Equals(regionNo) && roadwayNoLists.Contains(t.roadwayNo)).ToListAsync();
            var stockBinList = stockInfos.Select(t => t.binNo).Distinct().ToList();
            IQueryable<CanUseBinDto> query = null;
            //空托和空箱
            if (string.IsNullOrWhiteSpace(input.erpWhouseNo) || input.loadedType == 0)
            {
                query =
                        (from bbin in ((DbContext)DC).Set<BasWBin>()
                         where bbin.binErrFlag == "0" && bbin.usedFlag == 1 && bbin.isInEnable == 1 && bbin.binType == "ST" && bbin.virtualFlag == 0
                        && bbin.regionNo == input.regionNo && input.roadwayList.Contains(bbin.roadwayNo)
                         select new CanUseBinDto
                         {
                             binNo = bbin.binNo,
                             areaNo = bbin.areaNo,
                             erpWhouseNo = "",
                             binPriority = bbin.binPriority,
                             roadwayNo = bbin.roadwayNo,
                             regionNo = bbin.regionNo,
                             binCol = bbin.binCol,
                             binLayer = bbin.binLayer,
                             binRow = bbin.binRow,
                             binGroupIdx = bbin.binGroupIdx,
                             binGroupNo = bbin.binGroupNo,
                             extensionGroupNo = bbin.extensionGroupNo,
                             extensionIdx = bbin.extensionIdx,
                             binHeight = bbin.binHeight
                         });
            }
            else
            {
                query =
                         (from bbin in ((DbContext)DC).Set<BasWBin>()
                              //join bwewb in ((DbContext)DC).Set<BasWErpWhouseBin>() on new { bbin.binNo, bbin.regionNo } equals new { bwewb.binNo, bwewb.regionNo }
                          join bwewb in DC.Set<BasWErpWhouseBin>() on new { bbin.binNo, bbin.regionNo } equals new { bwewb.binNo, bwewb.regionNo } into tempErpBin
                          from bwewb in tempErpBin.DefaultIfEmpty()
                          where bwewb.erpWhouseNo == input.erpWhouseNo && bbin.binErrFlag == "0" && bbin.usedFlag == 1 && bbin.isInEnable == 1 && bbin.binType == "ST" && bbin.virtualFlag == 0
                         && bbin.regionNo == input.regionNo && input.roadwayList.Contains(bbin.roadwayNo)
                          select new CanUseBinDto
                          {
                              binNo = bbin.binNo,
                              areaNo = bbin.areaNo,
                              //erpWhouseNo = input.erpWhouseNo,
                              erpWhouseNo = bwewb.erpWhouseNo,
                              binPriority = bbin.binPriority,
                              roadwayNo = bbin.roadwayNo,
                              regionNo = bbin.regionNo,
                              binCol = bbin.binCol,
                              binLayer = bbin.binLayer,
                              binRow = bbin.binRow,
                              binGroupIdx = bbin.binGroupIdx,
                              binGroupNo = bbin.binGroupNo,
                              extensionGroupNo = bbin.extensionGroupNo,
                              extensionIdx = bbin.extensionIdx,
                              binHeight = bbin.binHeight
                          });
            }

            //关联库存
            if (input.sdType == "DS")
            {
                result = await query.Distinct().ToListAsync();

                #region 排除不可分配的库位伸位组
                //占用和出库库存
                var stockCodeList = await ((DbContext)DC).Set<WmsStockDtl>().Where(t => t.occupyQty > 0 || t.stockDtlStatus >= 20).Select(t => t.stockCode).ToListAsync();
                var filterBinList = stockInfos.Where(t => stockCodeList.Contains(t.stockCode) || t.stockStatus > 50).Select(t => t.binNo).ToList();
                //获取存在分配或者出库待过滤伸位组
                var filterExtensionGroupList = result.Where(t => filterBinList.Contains(t.binNo)).Select(t => t.extensionGroupNo).Distinct().ToList();
                //获取存在近伸有库存的伸位组
                filterBinList = stockInfos.Where(t => stockCodeList.Contains(t.stockCode) || t.stockStatus == 50).Select(t => t.binNo).ToList();
                var filterNearExtensionGroupList = result.Where(t => filterBinList.Contains(t.binNo) && t.extensionIdx == 1).Select(t => t.extensionGroupNo).Distinct().ToList();
                filterExtensionGroupList.AddRange(filterNearExtensionGroupList);
                #endregion
                result = query.WhereIf(input.layer != null, t => t.binLayer == input.layer)
                    .Where(t => !filterExtensionGroupList.Contains(t.extensionGroupNo)).Distinct().ToList();
            }
            else if (input.sdType == "SS")
            {
                result = await query.Where(t => t.binHeight >= input.height).Distinct().ToListAsync();

            }
            //初始方法
            //result = result.Where(t => !stockBinList.Contains(t.binNo)).Distinct().ToList();
            //Parallel方法
            //result = result.AsParallel().Where(t => !stockBinList.Contains(t.binNo)).Distinct().ToList();
            //使用 HashSet 代替 List
            HashSet<string> stockBinSet = new HashSet<string>(stockBinList);
            result = result.Where(t => !stockBinSet.Contains(t.binNo)).Distinct().ToList();
            return result;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="canUseBinList"></param>
        /// <param name="strategyView"></param>
        /// <returns></returns>
        public List<CanUseBinDto> GetuseBinViewSort(List<CanUseBinDto> canUseBinList, AlootBinStrategyDto strategyView)
        {
            //canUseBinList = canUseBinList.OrderBy(t => t.binHeight).ThenBy(t =>
            //canUseBinList = canUseBinList.OrderByDescending(t => t.binCol).ThenBy(t =>
            canUseBinList = canUseBinList.OrderBy(t =>
            {
                //逐列分布：在列的基础上，按层
                if (strategyView.distributed == "0")
                {
                    if (strategyView.storeNearby == "1")
                    {
                        return (((Int64)Math.Abs(t.binCol.Value - 100000)) * 10000) + t.binLayer.Value;
                    }
                    else
                    {
                        return (((Int64)Math.Abs(t.binCol.Value)) * 10000) + t.binLayer.Value;
                    }
                }
                //逐层分布：在层的基础上，按列
                else if (strategyView.distributed == "1")
                {
                    if (strategyView.storeNearby == "1")
                    {
                        return (t.binLayer.Value * 10000) + (Int64)(Math.Abs(t.binCol.Value - 100000));
                    }
                    else
                    {
                        return (t.binLayer.Value * 10000) + t.binCol.Value;
                    }
                }
                //梯形
                else
                {
                    if (strategyView.storeNearby == "1")
                    {
                        return ((Int64)Math.Abs(t.binCol.Value - 100000)) + t.binLayer.Value;
                    }
                    else
                    {
                        //return t.binCol.Value + t.binLayer.Value;
                        return ((Int64)Math.Abs(t.binCol.Value - 29)) + t.binLayer.Value;
                    }
                }
            }).ThenBy(t => t.binPriority).ToList();
            return canUseBinList;
        }
        #endregion

        #region 立库
        #region 托盘库(单伸单工位)
        /// <summary>
        /// 托盘库(单伸单工位)查询可用库位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="roadwayInfos"></param>
        /// <returns></returns>
        public async Task<BusinessResult> QueryBinForSS(AllotBinForMatDto input, List<BasWRoadway> roadwayInfos)
        {
            BusinessResult result = new BusinessResult();
            try
            {
                AllotBinResultDto allotBin = null;
                input.roadwayList = roadwayInfos.Select(t => t.roadwayNo).ToList();
                var canUseBinQuery = await GetCanUseBinInfos(input, roadwayInfos);
                if (canUseBinQuery.Count == 0)
                {
                    return result.Error($"根据ERP仓库【{input.erpWhouseNo}】和巷道集合【{string.Join(',', input.roadwayList)}】未找到可用库位");
                }
                else
                {
                    //按巷道计算
                    var inTaskInfos = await DC.Set<WmsTask>().AsNoTracking().Where(t => t.wmsTaskType.Contains("IN") && t.taskStatus < 90 && input.roadwayList.Contains(t.roadwayNo)).ToListAsync();
                    List<CanUseBinForRoadwayDto> binForRoadwayViews = new List<CanUseBinForRoadwayDto>();
                    var groupBin = canUseBinQuery.GroupBy(t => t.roadwayNo);
                    foreach (var item in groupBin)
                    {
                        var roadwayInfo = roadwayInfos.FirstOrDefault(t => t.roadwayNo == item.Key);
                        if (roadwayInfo != null)
                        {
                            //防止异常值
                            if (roadwayInfo.reservedQty == null)
                            {
                                roadwayInfo.reservedQty = 0;
                            }
                            //空库位大于0的巷道才可用
                            int emptyBinCount = item.Count() - roadwayInfo.reservedQty.Value;
                            if (emptyBinCount > 0)
                            {
                                CanUseBinForRoadwayDto binforRoadwayView = new CanUseBinForRoadwayDto()
                                {
                                    roadwayNo = item.Key,
                                    emptyBinCount = emptyBinCount,
                                    taskCount = inTaskInfos.Count(t => t.roadwayNo == item.Key),
                                    canUseBinList = item.ToList()

                                };
                                binforRoadwayView.rate = Math.Round(((decimal)binforRoadwayView.taskCount / (decimal)binforRoadwayView.emptyBinCount), 4);
                                binForRoadwayViews.Add(binforRoadwayView);
                            }

                        }
                    }
                    if (binForRoadwayViews.Count > 0)
                    {
                        //获取策略
                        result = await GetStrategyDtlsForBinAllot(input.regionNo);
                        if (result.code == ResCode.Error)
                        {
                            return result;
                        }
                        AlootBinStrategyDto strategyView = (AlootBinStrategyDto)result.outParams;
                        if (strategyView == null)
                        {
                            return result.Error("策略转换异常");
                        }
                        //入库任务数量/空库位数比值升序，空库位数量降序，巷道
                        binForRoadwayViews = binForRoadwayViews.OrderBy(t => t.rate).ThenByDescending(t => t.emptyBinCount).ThenBy(t => t.roadwayNo).ToList();
                        foreach (var item in binForRoadwayViews)
                        {
                            var binList = GetuseBinViewSort(item.canUseBinList, strategyView);
                            var binInfo = binList.FirstOrDefault();
                            if (binInfo != null)
                            {
                                allotBin = new AllotBinResultDto()
                                {
                                    binNo = binInfo.binNo,
                                    isAllotBin = true,
                                    roadwayNo = binInfo.roadwayNo,
                                    palletBarcode = input.palletBarcode,
                                    regionNo = binInfo.regionNo,
                                };
                                result.outParams = allotBin;
                                result.code = ResCode.OK;
                                result.msg = "库位分配成功";
                                return result;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (allotBin == null)
                        {
                            return result.Error("分配库位失败");
                        }
                    }
                    else
                    {
                        return result.Error($"根据ERP仓库【{input.erpWhouseNo}】和巷道集合【{string.Join(',', input.roadwayList)}】未找到可用库位(经过巷道设置空库位容量过滤)");
                    }

                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }

            return result;


        }


        #endregion

        #region 料箱库（双伸单工位）
        /// <summary>
        /// 料箱库（双伸单工位）查询可用库位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="roadwayInfos"></param>
        /// <returns></returns>
        public async Task<BusinessResult> QueryBinForDS(AllotBinForMatDto input, List<BasWRoadway> roadwayInfos)
        {
            BusinessResult result = new BusinessResult();
            try
            {
                AllotBinResultDto allotBin = null;
                CanUseBinDto binInfo = null;
                input.roadwayList = roadwayInfos.Select(t => t.roadwayNo).ToList();
                var canUseBinList = await GetCanUseBinInfos(input, roadwayInfos);
                if (canUseBinList.Count == 0)
                {
                    return result.Error($"根据ERP仓库【{input.erpWhouseNo}】和巷道集合【{string.Join(',', input.roadwayList)}】未找到可用库位");
                }
                else
                {
                    if (input.isMove)
                    {
                        //获取策略
                        result = await GetStrategyDtlsForBinAllot(input.regionNo);
                        if (result.code == ResCode.Error)
                        {
                            return result;
                        }
                        AlootBinStrategyDto strategyView = (AlootBinStrategyDto)result.outParams;
                        if (strategyView == null)
                        {
                            return result.Error("策略转换异常");
                        }
                        canUseBinList = GetuseBinViewSort(canUseBinList, strategyView);
                        binInfo = canUseBinList.FirstOrDefault(t => t.extensionIdx == 2);
                        if (binInfo != null)
                        {
                            allotBin = new AllotBinResultDto()
                            {
                                binNo = binInfo.binNo,
                                isAllotBin = true,
                                roadwayNo = binInfo.roadwayNo,
                                palletBarcode = input.palletBarcode,
                                regionNo = binInfo.regionNo,
                                erpWhouseNo = binInfo.erpWhouseNo,
                            };
                            result.outParams = allotBin;
                            result.code = ResCode.OK;
                            result.msg = "库位分配成功";
                            return result;
                        }
                        else
                        {
                            binInfo = canUseBinList.FirstOrDefault();
                            if (binInfo != null)
                            {
                                allotBin = new AllotBinResultDto()
                                {
                                    binNo = binInfo.binNo,
                                    isAllotBin = true,
                                    roadwayNo = binInfo.roadwayNo,
                                    palletBarcode = input.palletBarcode,
                                    regionNo = binInfo.regionNo,
                                    erpWhouseNo = binInfo.erpWhouseNo,
                                };
                                result.outParams = allotBin;
                                result.code = ResCode.OK;
                                result.msg = "库位分配成功";
                                return result;
                            }
                        }

                        if (allotBin == null)
                        {
                            return result.Error("分配库位失败");
                        }
                    }
                    else
                    {
                        var inTaskInfos = DC.Set<WmsTask>().AsNoTracking().Where(t => t.wmsTaskType.Contains("IN") && t.taskStatus < 90 && input.roadwayList.Contains(t.roadwayNo)).ToList();
                        //是否存在双伸远伸已在库库位与待分配托盘物料批次一致的
                        var query = from bin in DC.Set<BasWBin>()
                                    join stock in DC.Set<WmsStock>() on bin.binNo equals stock.binNo into tempStock
                                    from stock in tempStock.DefaultIfEmpty()
                                    join stockDtl in DC.Set<WmsStockDtl>() on stock.stockCode equals stockDtl.stockCode into tempStockDtl
                                    from stockDtl in tempStockDtl.DefaultIfEmpty()
                                    where stockDtl.stockDtlStatus == 50 && stockDtl.occupyQty <= 0 && stockDtl.skuCode == input.skuCode && stockDtl.erpWhouseNo == input.erpWhouseNo
                                          && stock.stockStatus == 50 && bin.extensionIdx == 2 && bin.binType == "ST" && bin.virtualFlag == 0 && bin.regionNo == input.regionNo
                                    select bin;
                        var stockList = await query.ToListAsync();
                        var sameMatQuery = from canUseBin in canUseBinList
                                           join stockBin in stockList on canUseBin.extensionGroupNo equals stockBin.extensionGroupNo
                                           where canUseBin.extensionIdx == 1 && canUseBin.extensionIdx != stockBin.extensionIdx
                                           select canUseBin;
                        var sameMatQueryResult = sameMatQuery.ToList();
                        if (sameMatQueryResult.Any())
                        {
                            List<CanUseBinForRoadwayDto> binForRoadwayforSameMatList = GetCanAllotByRoadway(roadwayInfos, sameMatQueryResult, inTaskInfos);
                            if (binForRoadwayforSameMatList.Count > 0)
                            {
                                foreach (var item in binForRoadwayforSameMatList)
                                {
                                    binInfo = item.canUseBinList.OrderBy(t => t.binPriority).FirstOrDefault();
                                    if (binInfo != null)
                                    {
                                        allotBin = new AllotBinResultDto()
                                        {
                                            binNo = binInfo.binNo,
                                            isAllotBin = true,
                                            roadwayNo = binInfo.roadwayNo,
                                            palletBarcode = input.palletBarcode,
                                            regionNo = binInfo.regionNo,
                                            erpWhouseNo = binInfo.erpWhouseNo,

                                        };
                                        result.outParams = allotBin;
                                        result.code = ResCode.OK;
                                        result.msg = "库位分配成功";
                                        return result;
                                    }
                                }
                            }

                        }
                        //按巷道计算
                        List<CanUseBinForRoadwayDto> binForRoadwayViews = GetCanAllotByRoadway(roadwayInfos, canUseBinList, inTaskInfos);
                        if (binForRoadwayViews.Count > 0)
                        {
                            //获取策略
                            result = await GetStrategyDtlsForBinAllot(input.regionNo);
                            if (result.code == ResCode.Error)
                            {
                                return result;
                            }
                            AlootBinStrategyDto strategyView = (AlootBinStrategyDto)result.outParams;
                            if (strategyView == null)
                            {
                                return result.Error("策略转换异常");
                            }
                            //入库任务数量/空库位数比值升序，空库位数量降序，巷道
                            binForRoadwayViews = binForRoadwayViews.OrderBy(t => t.rate).ThenByDescending(t => t.emptyBinCount).ThenBy(t => t.roadwayNo).ToList();
                            foreach (var item in binForRoadwayViews)
                            {
                                var binList = GetuseBinViewSort(item.canUseBinList, strategyView);
                                binInfo = binList.FirstOrDefault(t => t.extensionIdx == 2);
                                if (binInfo != null)
                                {
                                    allotBin = new AllotBinResultDto()
                                    {
                                        binNo = binInfo.binNo,
                                        isAllotBin = true,
                                        roadwayNo = binInfo.roadwayNo,
                                        palletBarcode = input.palletBarcode,
                                        regionNo = binInfo.regionNo,
                                        erpWhouseNo = binInfo.erpWhouseNo,
                                    };
                                    result.outParams = allotBin;
                                    result.code = ResCode.OK;
                                    result.msg = "库位分配成功";
                                    return result;
                                }
                                else
                                {
                                    binInfo = binList.FirstOrDefault();
                                    if (binInfo != null)
                                    {
                                        allotBin = new AllotBinResultDto()
                                        {
                                            binNo = binInfo.binNo,
                                            isAllotBin = true,
                                            roadwayNo = binInfo.roadwayNo,
                                            palletBarcode = input.palletBarcode,
                                            regionNo = binInfo.regionNo,
                                            erpWhouseNo = binInfo.erpWhouseNo,
                                        };
                                        result.outParams = allotBin;
                                        result.code = ResCode.OK;
                                        result.msg = "库位分配成功";
                                        return result;
                                    }
                                }
                            }
                            if (allotBin == null)
                            {
                                return result.Error("分配库位失败");
                            }
                        }
                        else
                        {
                            return result.Error($"根据ERP仓库【{input.erpWhouseNo}】和巷道集合【{string.Join(',', input.roadwayList)}】未找到可用库位(经过巷道设置空库位容量过滤)");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }

            return result;


        }

        private static List<CanUseBinForRoadwayDto> GetCanAllotByRoadway(List<BasWRoadway> roadwayInfos, List<CanUseBinDto> canUseBinList, List<WmsTask> inTaskInfos)
        {
            List<CanUseBinForRoadwayDto> binForRoadwayViews = new List<CanUseBinForRoadwayDto>();
            var groupBin = canUseBinList.GroupBy(t => t.roadwayNo);
            foreach (var item in groupBin)
            {
                var roadwayInfo = roadwayInfos.FirstOrDefault(t => t.roadwayNo == item.Key);
                if (roadwayInfo != null)
                {
                    //防止异常值
                    if (roadwayInfo.reservedQty == null)
                    {
                        roadwayInfo.reservedQty = 0;
                    }
                    //空库位大于0的巷道才可用
                    int emptyBinCount = item.Count() - roadwayInfo.reservedQty.Value;
                    if (emptyBinCount > 0)
                    {
                        CanUseBinForRoadwayDto binforRoadwayView = new CanUseBinForRoadwayDto()
                        {
                            roadwayNo = item.Key,
                            emptyBinCount = emptyBinCount,
                            taskCount = inTaskInfos.Count(t => t.roadwayNo == item.Key),
                            canUseBinList = item.ToList()

                        };
                        binforRoadwayView.rate = Math.Round(((decimal)binforRoadwayView.taskCount / (decimal)binforRoadwayView.emptyBinCount), 4);
                        binForRoadwayViews.Add(binforRoadwayView);
                    }

                }
            }

            return binForRoadwayViews;
        }

        /// <summary>
        /// 立库分配库位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="roadwayInfos"></param>
        /// <returns></returns>
        public async Task<BusinessResult> QueryBinForLk(AllotBinForMatDto input, List<BasWRoadway> roadwayInfos)
        {
            BusinessResult result = new BusinessResult();
            if (input.sdType == "DS")
            {
                result = await QueryBinForDS(input, roadwayInfos);
                //result = await QueryBinForDSByProcedure(input, roadwayInfos);
            }
            else if (input.sdType == "SS")
            {
                result = await QueryBinForSS(input, roadwayInfos);
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 托盘库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> AllotBinForTPK(AllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult();
            AllotBinResultDto allotBinResult = new AllotBinResultDto();
            AllotBinForMatDto allotBinForMat = new AllotBinForMatDto() { palletBarcode = input.palletBarcode, layer = input.layer };
            List<BasWRoadway> roadwayInfos = new List<BasWRoadway>();
            string regionNo = string.Empty;
            string Lk = "托盘库";
            bool isAllotBin = false;
            //if (input.height == null)
            //{
            //    return result.Error($"{Lk}分配库位失败：入参高度为空");
            //}


            var palletTypeInfo = await GetPalletType(input.palletBarcode);
            if (palletTypeInfo == null)
            {
                //var palletInfo = DC.Set<BasWPallet>().AsNoTracking().Where(t => t.palletBarcode == input.palletBarcode).FirstOrDefault();
                //if (palletInfo == null)
                //{
                //    return result.Error($"入参载体条码【{input.palletBarcode}】未找到对应载体类型,且载体未在WMS托盘表中注册");
                //}
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应载体类型");
            }
            else
            {
                if (palletTypeInfo.palletTypeCode == "BX")
                {
                    return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】对应载体类型是料箱,与分配类型【托盘库位分配】不一致");
                }
            }
            //查找库存
            var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
            if (stockInfo == null)
            {
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应入库中库存");
            }
            if (stockInfo.height == null && input.height == null)
            {
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】对应入库中库存和请求入参中高度都为空，请保持一个有数据");
            }
            //if (stockInfo.stockStatus >= 50)
            //{
            //    return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】对应库存状态不是入库中，实际状态为【{stockInfo.stockStatus}】");
            //}
            var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == stockInfo.stockCode).ToListAsync();
            if (stockDtlInfos.Count == 0)
            {
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应库存明细");
            }
            else
            {
                allotBinForMat.loadedType = stockInfo.loadedType == null ? 1 : stockInfo.loadedType.Value;
                allotBinForMat.height = stockInfo.height == null ? input.height : stockInfo.height;
                if (stockDtlInfos.Count == 1)
                {
                    var stockDtlInfo = stockDtlInfos.FirstOrDefault();
                    allotBinForMat.skuCode = string.IsNullOrWhiteSpace(stockDtlInfo.skuCode) ? stockDtlInfo.materialCode : stockDtlInfo.skuCode;
                    allotBinForMat.batchNo = "";
                    allotBinForMat.erpWhouseNo = stockDtlInfo.erpWhouseNo;
                }
                else
                {
                    var stockDtlInfo = stockDtlInfos.FirstOrDefault();
                    allotBinForMat.skuCode = "99999999";
                    allotBinForMat.batchNo = "";
                    allotBinForMat.erpWhouseNo = stockDtlInfo.erpWhouseNo;
                }
            }
            //获取可用巷道
            var roadwayList = input.roadwayNos.Split(',').ToList();
            roadwayList = roadwayList.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            roadwayInfos = await DC.Set<BasWRoadway>().Where(t => roadwayList.Contains(t.roadwayNo) && t.errFlag == 0 && t.usedFlag == 1 && !string.IsNullOrWhiteSpace(t.regionNo)).ToListAsync();
            regionNo = roadwayInfos.FirstOrDefault().regionNo;
            var regionInfo = await DC.Set<BasWRegion>().Where(t => t.regionNo == regionNo && t.usedFlag == 1).FirstOrDefaultAsync();
            if (regionInfo == null)
            {
                return result.Error($"{Lk}分配库位失败：库区【{regionNo}】未找到记录，检查数据是否配置或启用");
            }
            allotBinForMat.regionNo = regionInfo.regionNo;
            allotBinForMat.sdType = regionInfo.sdType;
            //判断是否已分配库位
            var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == stockInfo.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
            if (binInfo == null)
            {
                isAllotBin = true;
            }
            else
            {
                //库位异常：异常、禁用、不可入
                if (binInfo.binErrFlag != "0" || binInfo.usedFlag == 0 || binInfo.isInEnable == 0)
                {
                    isAllotBin = true;
                }

                if (input.isAllotAgain == false)
                {
                    var roadwayInfo = roadwayInfos.FirstOrDefault(t => t.roadwayNo == binInfo.roadwayNo);
                    if (roadwayInfo != null)
                    {
                        //库位可用
                        #region 库存巷道异常
                        if (binInfo.roadwayNo != stockInfo.roadwayNo)
                        {
                            stockInfo.errMsg = $"库存巷道【{stockInfo.roadwayNo}】与库位对应巷道【{binInfo.roadwayNo}】不一致，更新为库位对应巷道";
                            stockInfo.roadwayNo = binInfo.roadwayNo;
                            stockInfo.regionNo = binInfo.regionNo;
                            stockInfo.UpdateBy = input.invoker;
                            stockInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                            await ((DbContext)DC).BulkSaveChangesAsync(t => t.BatchSize = 2000);

                        }
                        #endregion

                        allotBinResult.palletBarcode = stockInfo.palletBarcode;
                        allotBinResult.binNo = stockInfo.binNo;
                        allotBinResult.roadwayNo = binInfo.roadwayNo;
                        allotBinResult.regionNo = binInfo.regionNo;
                        allotBinResult.isAllotBin = false;
                        result.outParams = allotBinResult;
                        return result;

                    }
                    else
                    {
                        isAllotBin = true;
                    }
                }
            }

            if (isAllotBin)
            {
                result = await QueryBinForLk(allotBinForMat, roadwayInfos);
            }

            return result;


        }
        /// <summary>
        /// 料箱库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> AllotBinForLXK(AllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult();
            AllotBinResultDto allotBinResult = new AllotBinResultDto();
            AllotBinForMatDto allotBinForMat = new AllotBinForMatDto() { palletBarcode = input.palletBarcode, isMove = input.isMove };
            List<BasWRoadway> roadwayInfos = new List<BasWRoadway>();
            string regionNo = string.Empty;
            string Lk = "料箱库";
            bool isAllotBin = false;
            //if (input.wcsAllotType == "0")
            //{

            //}
            //else if(input.wcsAllotType == "1")
            //{

            //}
            //else if(input.wcsAllotType== "2")
            //{

            //}
            var palletTypeInfo = await GetPalletType(input.palletBarcode);
            if (palletTypeInfo == null)
            {
                //var palletInfo = DC.Set<BasWPallet>().AsNoTracking().Where(t => t.palletBarcode == input.palletBarcode).FirstOrDefault();
                //if (palletInfo == null)
                //{
                //    return result.Error($"入参载体条码【{input.palletBarcode}】未找到对应载体类型,且载体未在WMS托盘表中注册");
                //}
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应载体类型");
            }
            else
            {
                if (palletTypeInfo.palletTypeCode != "BX")
                {
                    return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】对应载体类型是托盘,与分配类型【料箱库位分配】不一致");
                }
            }
            //查找库存
            WmsStock stockInfo = null;
            if (input.isMove)
            {
                stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus <= 50).FirstOrDefaultAsync();

            }
            else
            {
                stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode && t.stockStatus < 50).FirstOrDefaultAsync();
            }
            if (stockInfo == null)
            {
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应库存");
            }
            //if (stockInfo.stockStatus >= 50)
            //{
            //    return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】对应库存状态不是入库中，实际状态为【{stockInfo.stockStatus}】");
            //}
            var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == stockInfo.stockCode).ToListAsync();
            if (stockDtlInfos.Count == 0)
            {
                return result.Error($"{Lk}分配库位失败：入参载体条码【{input.palletBarcode}】未找到对应库存明细");
            }
            else
            {
                allotBinForMat.loadedType = stockInfo.loadedType == null ? 1 : stockInfo.loadedType.Value;
                if (stockDtlInfos.Count == 1)
                {
                    var stockDtlInfo = stockDtlInfos.FirstOrDefault();
                    allotBinForMat.skuCode = stockDtlInfo.skuCode;
                    allotBinForMat.batchNo = "";
                    allotBinForMat.erpWhouseNo = stockDtlInfo.erpWhouseNo;
                }
                else
                {
                    var stockDtlInfo = stockDtlInfos.FirstOrDefault();
                    allotBinForMat.skuCode = "99999999";
                    allotBinForMat.batchNo = "";
                    allotBinForMat.erpWhouseNo = stockDtlInfo.erpWhouseNo;
                }
            }
            //获取可用巷道
            var roadwayList = input.roadwayNos.Split(',').ToList();
            roadwayList = roadwayList.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            roadwayInfos = await DC.Set<BasWRoadway>().Where(t => roadwayList.Contains(t.roadwayNo) && t.errFlag == 0 && t.usedFlag == 1 && !string.IsNullOrWhiteSpace(t.regionNo)).ToListAsync();
            regionNo = roadwayInfos.FirstOrDefault().regionNo;
            var regionInfo = await DC.Set<BasWRegion>().Where(t => t.regionNo == regionNo && t.usedFlag == 1).FirstOrDefaultAsync();
            if (regionInfo == null)
            {
                return result.Error($"{Lk}分配库位失败：库区【{regionNo}】未找到记录，检查数据是否配置或启用");
            }
            allotBinForMat.regionNo = regionInfo.regionNo;
            allotBinForMat.sdType = regionInfo.sdType;
            //判断是否已分配库位
            var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == stockInfo.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
            if (binInfo == null)
            {
                isAllotBin = true;
            }
            else
            {
                if (input.isMove)
                {
                    isAllotBin = true;
                }
                else
                {
                    //库位异常：异常、禁用、不可入
                    if (binInfo.binErrFlag != "0" || binInfo.usedFlag == 0 || binInfo.isInEnable == 0)
                    {
                        isAllotBin = true;
                    }
                    if (input.isAllotAgain == false)
                    {
                        var roadwayInfo = roadwayInfos.FirstOrDefault(t => t.roadwayNo == binInfo.roadwayNo);
                        if (roadwayInfo != null)
                        {
                            //库位可用
                            #region 库存巷道异常
                            if (binInfo.roadwayNo != stockInfo.roadwayNo)
                            {
                                stockInfo.errMsg = $"库存巷道【{stockInfo.roadwayNo}】与库位对应巷道【{binInfo.roadwayNo}】不一致，更新为库位对应巷道";
                                stockInfo.roadwayNo = binInfo.roadwayNo;
                                stockInfo.UpdateBy = input.invoker;
                                stockInfo.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                                await ((DbContext)DC).BulkSaveChangesAsync(t => t.BatchSize = 2000);

                            }
                            #endregion

                            allotBinResult.palletBarcode = stockInfo.palletBarcode;
                            allotBinResult.binNo = stockInfo.binNo;
                            allotBinResult.roadwayNo = binInfo.roadwayNo;
                            allotBinResult.regionNo = binInfo.regionNo;
                            allotBinResult.isAllotBin = false;
                            result.outParams = allotBinResult;
                            return result;

                        }
                        else
                        {
                            isAllotBin = true;
                        }
                    }
                }



                allotBinForMat.layer = binInfo.binLayer == 0 ? null : binInfo.binLayer;
            }

            if (isAllotBin)
            {
                result = await QueryBinForLk(allotBinForMat, roadwayInfos);
            }

            return result;


        }
        #endregion

        #endregion

        public WcsAllotBinResultDto GetWcsAllotBinResult(AllotBinResultDto input)
        {
            WcsAllotBinResultDto resultView = new WcsAllotBinResultDto()
            {
                roadwayNo = input.roadwayNo,
                palletBarcode1 = input.palletBarcode,
                binNo1 = input.binNo
            };
            return resultView;
        }


        #region Wcs分配巷道
        /// <summary>
        /// Wcs分配巷道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> WcsAllotRoadway(WcsAllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult();
            WcsAllotBinResultDto wcsAllotBinResult = new WcsAllotBinResultDto();
            string msg = "";
            try
            {
                #region 校验
                if (input == null)
                {
                    msg = $"入参为空";
                    return result.Error($"分配巷道失败：" + msg);
                }
                logger.Warn($"----->Warn----->分配巷道--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.palletBarcode1))
                {
                    msg = $"入参托盘号为空";
                    return result.Error($"分配巷道失败：" + msg);
                }
                if (string.IsNullOrWhiteSpace(input.roadwayNos))
                {
                    msg = $"入参可用巷道集合为空";
                    return result.Error($"分配巷道失败：" + msg);
                }

                var putawayInfo = await DC.Set<WmsPutaway>().Where(t => t.palletBarcode == input.palletBarcode1 && t.putawayStatus < 90).FirstOrDefaultAsync();
                if (putawayInfo == null)
                {
                    msg = $"托盘【{input.palletBarcode1}】未找到未完成的上架单";
                    return result.Error($"分配巷道失败：" + msg);
                }
                var putawayDtlInfos = await DC.Set<WmsPutawayDtl>().Where(t => t.putawayNo == putawayInfo.putawayNo && t.palletBarcode == input.palletBarcode1 && t.putawayDtlStatus < 90).ToListAsync();
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode1 && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo == null)
                {
                    msg = $"托盘【{input.palletBarcode1}】未找到未完成的任务";
                    return result.Error($"分配巷道失败：" + msg);
                }
                if (!taskInfo.wmsTaskType.Contains("IN") && !taskInfo.wmsTaskType.Contains("BACK"))
                {
                    msg = $"托盘【{input.palletBarcode1}】找到未完成的任务类型不是入库，任务号【{taskInfo.wmsTaskNo}】";
                    return result.Error($"分配巷道失败：" + msg);
                }
                //查找库存
                var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode1 && t.stockStatus < 50).FirstOrDefaultAsync();
                if (stockInfo == null)
                {
                    msg = $"托盘【{input.palletBarcode1}】未找到对应入库中库存";
                    return result.Error($"分配巷道失败：" + msg);
                }
                //if (stockInfo.stockStatus >= 50)
                //{
                //    msg = $"入参载体条码【{input.palletBarcode1}】对应库存状态不是入库中，实际状态为【{stockInfo.stockStatus}】";
                //    return result.Error($"分配巷道失败："+ msg);
                //}

                AllotBinInputDto allotBinInput = new AllotBinInputDto()
                {
                    palletBarcode = input.palletBarcode1,
                    roadwayNos = input.roadwayNos,
                    height = input.height,
                    invoker = input.invoker,
                };
                result = await AllotBin(allotBinInput);
                if (result.code == ResCode.Error)
                {
                    result.msg = $"分配巷道失败：" + result.msg;
                    return result;
                }
                else
                {
                    AllotBinResultDto allotBinResult = (AllotBinResultDto)result.outParams;
                    if (allotBinResult != null)
                    {
                        if (allotBinResult.isAllotBin == true)
                        {
                            //业务处理
                            #region 可以重新校验库位是否可用
                            //可以重新校验库位是否可用
                            #endregion
                            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                            {
                                try
                                {
                                    //更新上架单
                                    putawayDtlInfos.ForEach(t =>
                                    {
                                        t.binNo = allotBinResult.binNo;
                                        t.regionNo = allotBinResult.regionNo;
                                        t.roadwayNo = allotBinResult.roadwayNo;
                                        t.UpdateBy = input.invoker;
                                        t.UpdateTime = DateTime.Now;
                                    });
                                    await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos);
                                    //更新库存
                                    stockInfo.locNo = input.locNo1;//新增
                                    stockInfo.binNo = allotBinResult.binNo;
                                    stockInfo.regionNo = allotBinResult.regionNo;
                                    stockInfo.roadwayNo = allotBinResult.roadwayNo;
                                    stockInfo.UpdateBy = input.invoker;
                                    stockInfo.UpdateTime = DateTime.Now;
                                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                                    //更新任务
                                    taskInfo.frLocationNo = input.locNo1;//新增
                                    taskInfo.toLocationNo = allotBinResult.binNo;
                                    taskInfo.regionNo = allotBinResult.regionNo;
                                    taskInfo.roadwayNo = allotBinResult.roadwayNo;
                                    taskInfo.taskStatus = 3;//新增状态
                                    taskInfo.UpdateBy = input.invoker;
                                    taskInfo.UpdateTime = DateTime.Now;
                                    await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);

                                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                                    await tran.CommitAsync();
                                }
                                catch
                                {
                                    await tran.RollbackAsync();
                                    throw;
                                }
                            }
                        }

                        wcsAllotBinResult = GetWcsAllotBinResult(allotBinResult);
                        result.outParams = wcsAllotBinResult;

                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                return result.Error($"分配巷道失败：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region Wcs分配库位
        /// <summary>
        /// Wcs分配库位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> WcsAllotBin(WcsAllotBinInputDto input)
        {
            BusinessResult result = new BusinessResult();
            WcsAllotBinResultDto wcsAllotBinResult = new WcsAllotBinResultDto();
            string msg = "";
            string wcsAllotTypeDesc = "分配库位：";
            try
            {
                #region 校验
                if (input == null)
                {
                    msg = $"{wcsAllotTypeDesc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->分配库位--入参:{JsonConvert.SerializeObject(input)} ");
                if (string.IsNullOrWhiteSpace(input.palletBarcode1))
                {
                    msg = $"{wcsAllotTypeDesc}入参托盘号为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.roadwayNos))
                {
                    msg = $"{wcsAllotTypeDesc}入参可用巷道集合为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.wcsAllotType))
                {
                    msg = $"{wcsAllotTypeDesc}入参分配库位类型为空";
                    return result.Error(msg);
                }
                List<string> allotTypeList = new List<string>() { "0", "1", "2" };
                if (!allotTypeList.Contains(input.wcsAllotType))
                {
                    msg = $"{wcsAllotTypeDesc}入参分配库位类型【{input.wcsAllotType}】不在取值范围内";
                    return result.Error(msg);
                }
                if (input.wcsAllotType == "0")
                {

                }
                if (input.wcsAllotType == "1")
                {
                    wcsAllotTypeDesc = "置换库位失败：";
                    //if (string.IsNullOrWhiteSpace(input.palletBarcode2))
                    //{
                    //    msg = $"{wcsAllotTypeDesc}入参分配库位类型为置换库位时，托盘号2字段不能为空";
                    //    return result.Error(msg);
                    //}

                }
                else if (input.wcsAllotType == "2")
                {
                    wcsAllotTypeDesc = "重新分配库位失败：";
                    if (string.IsNullOrWhiteSpace(input.errFlag))
                    {
                        msg = $"{wcsAllotTypeDesc}入参分配库位类型为异常时，异常标识不是为空";
                        return result.Error(msg);
                    }
                    List<string> errList = new List<string>() { "11", "12" };
                    if (!errList.Contains(input.errFlag))
                    {
                        msg = $"{wcsAllotTypeDesc}入参分配库位类型为异常时，异常标识无法识别";
                        return result.Error(msg);
                    }

                }
                var putawayInfo = await DC.Set<WmsPutaway>().Where(t => t.palletBarcode == input.palletBarcode1 && t.putawayStatus < 90).FirstOrDefaultAsync();
                if (putawayInfo == null)
                {
                    msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode1}】未找到未完成的上架单";
                    return result.Error(msg);
                }
                var putawayDtlInfos = await DC.Set<WmsPutawayDtl>().Where(t => t.putawayNo == putawayInfo.putawayNo && t.palletBarcode == input.palletBarcode1 && t.putawayDtlStatus < 90).ToListAsync();
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode1 && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo == null)
                {
                    msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode1}】未找到未完成的任务";
                    return result.Error(msg);
                }
                if (!taskInfo.wmsTaskType.Contains("IN") && !taskInfo.wmsTaskType.Contains("MOVE") && !taskInfo.wmsTaskType.Contains("BACK"))
                {
                    msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode1}】找到未完成的任务类型不是入库或者移库，，实际任务类型【{taskInfo.wmsTaskType}】,任务号【{taskInfo.wmsTaskNo}】";
                    return result.Error(msg);
                }
                //查找库存
                var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode1 && t.stockStatus < 50).FirstOrDefaultAsync();
                if (stockInfo == null)
                {
                    msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode1}】未找到对应入库中库存";
                    return result.Error(msg);
                }
                //if (stockInfo.stockStatus >= 50)
                //{
                //    msg = $"{wcsAllotTypeDesc}入参载体条码【{input.palletBarcode1}】对应库存状态不是入库中，实际状态为【{stockInfo.stockStatus}】";
                //    return result.Error(msg);
                //}
                #endregion
                AllotBinInputDto allotBinInput = new AllotBinInputDto()
                {
                    palletBarcode = input.palletBarcode1,
                    roadwayNos = input.roadwayNos,
                    height = input.height,
                };
                if (input.wcsAllotType == "0")
                {
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == stockInfo.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                    if (binInfo != null)
                    {
                        if (binInfo.extensionIdx == 1)
                        {
                            if (binInfo.binErrFlag == "0" && binInfo.isInEnable == 1)
                            {
                                //库位可用
                                wcsAllotBinResult.binNo1 = stockInfo.binNo;
                                wcsAllotBinResult.palletBarcode1 = input.palletBarcode1;
                                msg = $"托盘【{input.palletBarcode1}】分配完成";
                                return result.Success(msg, wcsAllotBinResult);
                            }
                        }
                        else
                        {
                            BasWBin nearBinInfo = await DC.Set<BasWBin>().Where(t => t.extensionIdx != binInfo.extensionIdx && t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                            if (nearBinInfo == null)
                            {
                                wcsAllotBinResult.binNo1 = stockInfo.binNo;
                                wcsAllotBinResult.palletBarcode1 = input.palletBarcode1;
                                msg = $"托盘【{input.palletBarcode1}】分配完成";
                                return result.Success(msg, wcsAllotBinResult);
                            }
                            else
                            {
                                var nearStockInfo = await DC.Set<WmsStock>().Where(t => t.binNo == nearBinInfo.binNo && t.stockStatus >= 50).FirstOrDefaultAsync();
                                if (nearStockInfo == null && binInfo.binErrFlag == "0" && binInfo.isInEnable == 1)
                                {
                                    wcsAllotBinResult.binNo1 = stockInfo.binNo;
                                    //wcsAllotBinResult.binNo1 = nearBinInfo.binNo;
                                    wcsAllotBinResult.palletBarcode1 = input.palletBarcode1;
                                    msg = $"托盘【{input.palletBarcode1}】分配完成";
                                    return result.Success(msg, wcsAllotBinResult);
                                }
                            }
                        }

                        allotBinInput.layer = binInfo.binLayer;
                    }

                    result = await AllotBin(allotBinInput);
                    if (result.code == ResCode.Error)
                    {
                        result.msg = $"{wcsAllotTypeDesc}" + result.msg;
                        return result;
                    }
                    else
                    {
                        AllotBinResultDto allotBinResult = (AllotBinResultDto)result.outParams;
                        if (allotBinResult != null)
                        {
                            if (allotBinResult.isAllotBin == true)
                            {
                                //业务处理
                                #region 可以重新校验库位是否可用
                                //可以重新校验库位是否可用
                                #endregion
                                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                                {
                                    try
                                    {
                                        //更新上架单
                                        putawayDtlInfos.ForEach(t =>
                                        {
                                            t.binNo = allotBinResult.binNo;
                                            t.regionNo = allotBinResult.regionNo;
                                            t.roadwayNo = allotBinResult.roadwayNo;
                                            t.UpdateBy = input.invoker;
                                            t.UpdateTime = DateTime.Now;
                                        });
                                        await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos);
                                        //更新库存
                                        stockInfo.binNo = allotBinResult.binNo;
                                        stockInfo.regionNo = allotBinResult.regionNo;
                                        stockInfo.roadwayNo = allotBinResult.roadwayNo;
                                        stockInfo.UpdateBy = input.invoker;
                                        stockInfo.UpdateTime = DateTime.Now;
                                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                                        //更新任务
                                        taskInfo.toLocationNo = allotBinResult.binNo;
                                        taskInfo.regionNo = allotBinResult.regionNo;
                                        taskInfo.roadwayNo = allotBinResult.roadwayNo;
                                        taskInfo.UpdateBy = input.invoker;
                                        taskInfo.UpdateTime = DateTime.Now;
                                        await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);

                                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                                        await tran.CommitAsync();
                                    }
                                    catch
                                    {
                                        await tran.RollbackAsync();
                                        throw;
                                    }
                                }
                            }

                            wcsAllotBinResult = GetWcsAllotBinResult(allotBinResult);
                            result.outParams = wcsAllotBinResult;

                        }
                    }
                }
                else if (input.wcsAllotType == "1")
                {
                    //近伸
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == stockInfo.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                    if (binInfo != null)
                    {
                        BasWBin binInfo2 = await DC.Set<BasWBin>().Where(t => t.extensionIdx != binInfo.extensionIdx && t.roadwayNo == binInfo.roadwayNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                        if (binInfo2 != null)
                        {
                            var taskInfo2 = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode2 && t.taskStatus < 90).FirstOrDefaultAsync();
                            if (taskInfo2 == null)
                            {
                                //msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode2}】未找到未完成的任务";
                                //return result.Error(msg);
                            }
                            else
                            {
                                if (!taskInfo2.wmsTaskType.Contains("IN") && !taskInfo2.wmsTaskType.Contains("MOVE"))
                                {
                                    msg = $"{wcsAllotTypeDesc}载体2【{input.palletBarcode2}】找到未完成的任务类型不是入库或者移库，，实际任务类型【{taskInfo2.wmsTaskType}】,任务号【{taskInfo2.wmsTaskNo}】";
                                    return result.Error(msg);
                                }
                            }

                            //查找库存
                            var stockInfo2 = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode2).FirstOrDefaultAsync();
                            if (stockInfo2 == null)
                            {
                                //msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode2}】未找到库存";
                                //return result.Error(msg);
                            }
                            else
                            {
                                if (stockInfo2.stockStatus >= 50)
                                {
                                    msg = $"{wcsAllotTypeDesc}入参载体2条码【{input.palletBarcode2}】对应库存状态不是入库中，实际状态为【{stockInfo2.stockStatus}】";
                                    return result.Error(msg);
                                }
                            }
                            List<WmsPutawayDtl> putawayDtlInfos2 = new List<WmsPutawayDtl>();
                            var putawayInfo2 = await DC.Set<WmsPutaway>().Where(t => t.palletBarcode == input.palletBarcode2 && t.putawayStatus < 90).FirstOrDefaultAsync();
                            if (putawayInfo2 == null)
                            {
                                //msg = $"{wcsAllotTypeDesc}托盘【{input.palletBarcode2}】未找到未完成的上架单";
                                //return result.Error(msg);
                            }
                            else
                            {
                                putawayDtlInfos2 = await DC.Set<WmsPutawayDtl>().Where(t => t.putawayNo == putawayInfo2.putawayNo && t.palletBarcode == input.palletBarcode2 && t.putawayDtlStatus < 90).ToListAsync();
                            }

                            if (taskInfo2 != null && stockInfo2 == null)
                            {
                                msg = $"{wcsAllotTypeDesc}入参载体2条码【{input.palletBarcode2}】有未完成的任务，任务号【{taskInfo2.wmsTaskNo}】，但是无库存";
                                return result.Error(msg);
                            }

                            //是否已经交换过
                            //已经交换过
                            if (binInfo.extensionIdx == 2 && binInfo2.extensionIdx == 1)
                            {
                                //更新任务1
                                taskInfo.toLocationNo = binInfo.binNo;
                                taskInfo.regionNo = binInfo.regionNo;
                                taskInfo.roadwayNo = binInfo.roadwayNo;
                                taskInfo.UpdateBy = input.invoker;
                                taskInfo.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);

                                //更新任务2
                                if (taskInfo2 != null)
                                {
                                    taskInfo2.regionNo = binInfo2.regionNo;
                                    taskInfo2.roadwayNo = binInfo2.roadwayNo;
                                    taskInfo2.UpdateBy = input.invoker;
                                    taskInfo2.UpdateTime = DateTime.Now;
                                    taskInfo2.toLocationNo = binInfo2.binNo;
                                    await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo2);
                                }

                                BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                                wcsAllotBinResult.palletBarcode1 = input.palletBarcode1;
                                wcsAllotBinResult.binNo1 = binInfo.binNo;
                                wcsAllotBinResult.palletBarcode2 = input.palletBarcode2;
                                wcsAllotBinResult.binNo2 = binInfo2.binNo;
                                result.code = ResCode.OK;
                                result.msg = "置换库位成功";
                                result.outParams = wcsAllotBinResult;

                            }
                            else if (binInfo.extensionIdx == 1 && binInfo2.extensionIdx == 2)
                            {
                                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                                {
                                    try
                                    {
                                        #region 托盘1
                                        //更新上架单
                                        putawayDtlInfos.ForEach(t =>
                                        {
                                            t.binNo = binInfo2.binNo;
                                            t.regionNo = binInfo2.regionNo;
                                            t.roadwayNo = binInfo2.roadwayNo;
                                            t.UpdateBy = input.invoker;
                                            t.UpdateTime = DateTime.Now;
                                        });
                                        await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos);
                                        //更新库存
                                        stockInfo.binNo = binInfo2.binNo;
                                        stockInfo.regionNo = binInfo2.regionNo;
                                        stockInfo.roadwayNo = binInfo2.roadwayNo;
                                        stockInfo.UpdateBy = input.invoker;
                                        stockInfo.UpdateTime = DateTime.Now;
                                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                                        //更新任务
                                        taskInfo.toLocationNo = binInfo2.binNo;
                                        taskInfo.regionNo = binInfo2.regionNo;
                                        taskInfo.roadwayNo = binInfo2.roadwayNo;
                                        taskInfo.UpdateBy = input.invoker;
                                        taskInfo.UpdateTime = DateTime.Now;
                                        await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);
                                        #endregion

                                        #region 托盘2
                                        //更新上架单
                                        if (putawayDtlInfos2.Any())
                                        {
                                            putawayDtlInfos2.ForEach(t =>
                                            {
                                                t.binNo = binInfo.binNo;
                                                t.regionNo = binInfo.regionNo;
                                                t.roadwayNo = binInfo.roadwayNo;
                                                t.UpdateBy = input.invoker;
                                                t.UpdateTime = DateTime.Now;
                                            });
                                            await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos2);
                                        }

                                        //更新库存
                                        if (stockInfo2 != null)
                                        {
                                            stockInfo2.binNo = binInfo.binNo;
                                            stockInfo2.regionNo = binInfo.regionNo;
                                            stockInfo2.roadwayNo = binInfo.roadwayNo;
                                            stockInfo2.UpdateBy = input.invoker;
                                            stockInfo2.UpdateTime = DateTime.Now;
                                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo2);
                                        }

                                        //更新任务
                                        if (taskInfo2 != null)
                                        {
                                            taskInfo2.toLocationNo = binInfo.binNo;
                                            taskInfo2.regionNo = binInfo.regionNo;
                                            taskInfo2.roadwayNo = binInfo.roadwayNo;
                                            taskInfo2.UpdateBy = input.invoker;
                                            taskInfo2.UpdateTime = DateTime.Now;
                                            await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo2);
                                        }

                                        #endregion

                                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                                        await tran.CommitAsync();
                                        wcsAllotBinResult.palletBarcode1 = input.palletBarcode1;
                                        wcsAllotBinResult.binNo1 = binInfo2.binNo;
                                        wcsAllotBinResult.palletBarcode2 = input.palletBarcode2;
                                        wcsAllotBinResult.binNo2 = binInfo.binNo;
                                        result.code = ResCode.OK;
                                        result.msg = "置换库位成功";
                                        result.outParams = wcsAllotBinResult;
                                    }
                                    catch
                                    {
                                        await tran.RollbackAsync();
                                        throw;
                                    }
                                }

                            }
                            else
                            {
                                msg = $"{wcsAllotTypeDesc}伸位组号相等，伸位组索引不是近伸远伸关系，入参载体1条码【{input.palletBarcode1}】对应库位【{binInfo.binNo}】,伸位组【{binInfo.extensionGroupNo}】，伸位组序号【{binInfo.extensionIdx}】和载体2条码【{input.palletBarcode2}】对应库位【{binInfo2.binNo}】,伸位组【{binInfo2.extensionGroupNo}】，伸位组序号【{binInfo2.extensionIdx}】";
                                return result.Error(msg);
                            }
                        }
                        else
                        {
                            msg = $"{wcsAllotTypeDesc}载体【{input.palletBarcode1}】对应库位【{stockInfo.binNo}未找到同一伸位组库位";
                            return result.Error(msg);
                        }


                    }
                    else
                    {
                        msg = $"{wcsAllotTypeDesc}载体【{input.palletBarcode1}】未找到对应库位【{stockInfo.binNo}";
                        return result.Error(msg);
                    }
                }
                else if (input.wcsAllotType == "2")
                {
                    var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == stockInfo.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                    if (binInfo != null)
                    {
                        if (input.errFlag == "11")
                        {
                            //满入,标记库位异常
                            binInfo.binErrFlag = input.errFlag;
                            binInfo.binErrMsg = input.errMsg;
                            binInfo.UpdateBy = input.invoker;
                            binInfo.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(binInfo);
                            BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                            await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        }
                        else if (input.errFlag == "12")
                        {
                            var binInfos = await DC.Set<BasWBin>().Where(t => t.extensionGroupNo == binInfo.extensionGroupNo && t.binType == "ST" && t.virtualFlag == 0).ToListAsync();
                            binInfos.ForEach(t =>
                            {
                                //满入,标记库位异常
                                t.binErrFlag = input.errFlag;
                                t.binErrMsg = input.errMsg;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(binInfos);
                            BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                            await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        }

                        //重新分配库位
                        allotBinInput.layer = binInfo.binLayer;
                        allotBinInput.isAllotAgain = true;
                        result = await AllotBin(allotBinInput);
                        if (result.code == ResCode.Error)
                        {
                            result.msg = $"{wcsAllotTypeDesc}" + result.msg;
                            return result;
                        }
                        else
                        {
                            AllotBinResultDto allotBinResult = (AllotBinResultDto)result.outParams;
                            if (allotBinResult != null)
                            {
                                if (allotBinResult.isAllotBin == true)
                                {
                                    //业务处理
                                    #region 可以重新校验库位是否可用
                                    //可以重新校验库位是否可用
                                    #endregion
                                    using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                                    {
                                        try
                                        {
                                            //更新上架单
                                            putawayDtlInfos.ForEach(t =>
                                            {
                                                t.binNo = allotBinResult.binNo;
                                                t.regionNo = allotBinResult.regionNo;
                                                t.roadwayNo = allotBinResult.roadwayNo;
                                                t.UpdateBy = input.invoker;
                                                t.UpdateTime = DateTime.Now;
                                            });
                                            await ((DbContext)DC).Set<WmsPutawayDtl>().BulkUpdateAsync(putawayDtlInfos);
                                            //更新库存
                                            stockInfo.binNo = allotBinResult.binNo;
                                            stockInfo.regionNo = allotBinResult.regionNo;
                                            stockInfo.roadwayNo = allotBinResult.roadwayNo;
                                            stockInfo.UpdateBy = input.invoker;
                                            stockInfo.UpdateTime = DateTime.Now;
                                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                                            //更新任务
                                            taskInfo.toLocationNo = allotBinResult.binNo;
                                            taskInfo.regionNo = allotBinResult.regionNo;
                                            taskInfo.roadwayNo = allotBinResult.roadwayNo;
                                            taskInfo.UpdateBy = input.invoker;
                                            taskInfo.UpdateTime = DateTime.Now;
                                            await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(taskInfo);

                                            BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                            await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                                            await tran.CommitAsync();
                                        }
                                        catch
                                        {
                                            await tran.RollbackAsync();
                                            throw;
                                        }
                                    }
                                }

                                wcsAllotBinResult = GetWcsAllotBinResult(allotBinResult);
                                result.outParams = wcsAllotBinResult;

                            }
                        }
                    }
                    else
                    {
                        msg = $"{wcsAllotTypeDesc}载体【{input.palletBarcode1}】未找到对应库位【{stockInfo.binNo}";
                        return result.Error(msg);
                    }

                }


            }
            catch (Exception ex)
            {
                return result.Error($"{wcsAllotTypeDesc}" + ex.Message);
            }
            return result;
        }
        #endregion


        #region 空托、空箱组盘
        /// <summary>
        /// 空托组盘
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> EmptyIn(EmptyInDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = "";
            string opeDesc = "空托组盘：";
            try
            {
                if (input == null)
                {
                    msg = $"{opeDesc}入参为空";
                    return result.Error(msg);
                }
                if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    msg = $"{opeDesc}入参载体条码为空";
                    return result.Error(msg);
                }
                if (input.qty <= 0)
                {
                    msg = $"{opeDesc}入参堆叠数量不大于0";
                    return result.Error(msg);
                }
                var docTypeInfo = await DC.Set<CfgDocType>().Where(t => t.businessCode == "EMPTY_IN").FirstOrDefaultAsync();
                if (docTypeInfo == null)
                {
                    msg = $"{opeDesc}未配置空托（空箱）入库单据类型";
                    return result.Error(msg);
                }

                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    msg = $"{opeDesc}载体【{input.palletBarcode}】未找到未完成的任务";
                    return result.Error(msg);
                }

                //查找库存
                var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                if (stockInfo != null)
                {
                    msg = $"{opeDesc}载体【{input.palletBarcode}】存在库存";
                    return result.Error(msg);
                }
                //载体类型
                var palletTypeInfo = await GetPalletType(input.palletBarcode);
                if (palletTypeInfo == null)
                {
                    msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】未找到对应载体类型";
                    return result.Error(msg);
                }
                if (palletTypeInfo.emptyMaxQty == null)
                {
                    msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】未配置叠盘上限数量";
                    return result.Error(msg);
                }
                if (palletTypeInfo.emptyMaxQty < input.qty)
                {
                    msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】叠盘上限数量【{palletTypeInfo.emptyMaxQty}】小于入参数量【{input.qty}】";
                    return result.Error(msg);
                }
                //if (palletTypeInfo.palletTypeCode != "BX")
                //{
                //    if (input.height == null || input.height <= 0)
                //    {
                //        msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】高度不能为空";
                //        return result.Error(msg);
                //    }
                //}
                //获取物料
                var matInfo = await DC.Set<BasBMaterial>().Where(t => t.MaterialCode == palletTypeInfo.palletTypeCode).FirstOrDefaultAsync();
                if (matInfo == null)
                {
                    msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】未找到物料";
                    return result.Error(msg);
                }
                if (matInfo.UsedFlag == 0)
                {
                    msg = $"{opeDesc}入参载体条码【{input.palletBarcode}】对应载体类型【{palletTypeInfo.palletTypeName}（{palletTypeInfo.palletTypeCode}）】找到物料【{matInfo.MaterialCode}】被禁用";
                    return result.Error(msg);
                }
                //var skuInfo=DC.Set<basbs>

                using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
                {
                    try
                    {
                        SysSequenceVM sysSequenceApiVM = Wtm.CreateVM<SysSequenceVM>();
                        //创建库存
                        #region 创建库存
                        WmsStock wmsStock = CreateStock(input, sysSequenceApiVM);
                        #endregion
                        //库存明细
                        #region 库存明细
                        WmsStockDtl wmsStockDtl = CreateStockDtl(input, matInfo, wmsStock);
                        #endregion
                        //入库记录
                        #region 入库记录
                        WmsInReceiptRecord wmsInReceiptRecord = CreateInrecord(docTypeInfo, matInfo, wmsStock, wmsStockDtl);
                        #endregion
                        //上架单和明细
                        #region 上架单
                        WmsPutaway wmsPutaway = CreatePutaway(sysSequenceApiVM, wmsStock);
                        #endregion

                        #region 上架单明细
                        WmsPutawayDtl wmsPutawayDtl = CreatePutawayDtl(wmsStock, wmsStockDtl, wmsInReceiptRecord, wmsPutaway);
                        #endregion

                        #region 数据库处理
                        await ((DbContext)DC).Set<WmsStock>().AddAsync(wmsStock);
                        await ((DbContext)DC).Set<WmsStockDtl>().AddAsync(wmsStockDtl);
                        await ((DbContext)DC).Set<WmsInReceiptRecord>().AddAsync(wmsInReceiptRecord);
                        await ((DbContext)DC).Set<WmsPutaway>().AddAsync(wmsPutaway);
                        await ((DbContext)DC).Set<WmsPutawayDtl>().AddAsync(wmsPutawayDtl);

                        BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                        await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                        await tran.CommitAsync();
                        #endregion

                        result.code = ResCode.OK;
                        result.msg = $"【{input.palletBarcode}】组盘成功";

                    }
                    catch
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }

            }
            catch (Exception ex)
            {
                return result.Error($"{opeDesc}" + ex.Message);
            }
            return result;
        }

        private static WmsPutawayDtl CreatePutawayDtl(WmsStock wmsStock, WmsStockDtl wmsStockDtl, WmsInReceiptRecord wmsInReceiptRecord, WmsPutaway wmsPutaway)
        {
            WmsPutawayDtl wmsPutawayDtl = new WmsPutawayDtl();
            //wmsPutawayDtl.batchNo = ""; // 批次号
            //wmsPutawayDtl.delFlag = DelFlag.NDelete.GetCode();
            ; // 删除标志;0-有效,1-已删除
            wmsPutawayDtl.docTypeCode = wmsInReceiptRecord.docTypeCode; // 单据类型
                                                                        //wmsPutawayDtl.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库
            wmsPutawayDtl.inspectionResult = wmsStockDtl.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
            wmsPutawayDtl.materialCode = wmsStockDtl.materialCode; // 物料编码
            wmsPutawayDtl.materialSpec = wmsStockDtl.materialSpec; // 物料规格
            wmsPutawayDtl.materialName = wmsStockDtl.materialName;
            wmsPutawayDtl.orderDtlId = null; // 关联单据明细ID
            wmsPutawayDtl.orderNo = ""; // 关联单据编号
            wmsPutawayDtl.areaNo = wmsPutaway.areaNo;
            wmsPutawayDtl.palletBarcode = wmsStockDtl.palletBarcode; // 载体条码
                                                                     //wmsPutawayDtl.projectNo = wmsInReceiptIqcResult.projectNo; // 项目号
            wmsPutawayDtl.proprietorCode = wmsStockDtl.proprietorCode; // 货主
            wmsPutawayDtl.ptaBinNo = null; // 上架库位
            wmsPutawayDtl.putawayDtlStatus = 0;
            wmsPutawayDtl.putawayNo = wmsPutaway.putawayNo; // 上架单编号
            wmsPutawayDtl.recordId = wmsInReceiptRecord.ID; // 记录ID
                                                            //wmsPutawayDtl.recordQty = uniicodeQtys; // 数量(组盘数量)
                                                            //wmsPutawayDtl.qty = wmsStockDtl.qty; // 数量(组盘数量)
            wmsPutawayDtl.recordQty = wmsStockDtl.qty; // 数量(组盘数量)
            wmsPutawayDtl.unitCode = wmsStockDtl.unitCode;
            wmsPutawayDtl.regionNo = wmsStock.regionNo; // 库区编号
            wmsPutawayDtl.roadwayNo = wmsStock.roadwayNo; // 巷道
            wmsPutawayDtl.binNo = wmsStock.binNo;
            wmsPutawayDtl.skuCode = wmsStockDtl.skuCode; // SKU编码
            wmsPutawayDtl.stockCode = wmsStock.stockCode; // 库存编码
            wmsPutawayDtl.stockDtlId = (long)wmsStockDtl?.ID; // 库存明细ID
            //wmsPutawayDtl.supplierBatchNo = null; // 供应商批次
            wmsPutawayDtl.supplierCode = wmsStockDtl.supplierCode; // 供应商编码
            wmsPutawayDtl.supplierName = wmsStockDtl.supplierName; // 供方名称
            wmsPutawayDtl.supplierNameAlias = wmsStockDtl.supplierNameAlias; // 供方名称-其他
            wmsPutawayDtl.supplierNameEn = wmsStockDtl.supplierNameEn; // 供方名称-英文
            //wmsPutawayDtl.supplierType = ""; // 供货方类型;供应商、产线
            wmsPutawayDtl.whouseNo = wmsStock.whouseNo; // 仓库号
            wmsPutawayDtl.CreateBy = wmsStock.CreateBy; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsPutawayDtl.CreateTime = DateTime.Now;
            wmsPutawayDtl.UpdateBy = wmsStock.UpdateBy; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsPutawayDtl.UpdateTime = DateTime.Now;
            return wmsPutawayDtl;
        }

        private static WmsPutaway CreatePutaway(SysSequenceVM sysSequenceApiVM, WmsStock wmsStock)
        {
            WmsPutaway wmsPutaway = new WmsPutaway();
            var putawayNoSeq = sysSequenceApiVM.GetSequence(SequenceCode.WmsPutawayNo.GetCode());
            wmsPutaway.whouseNo = wmsStock.whouseNo; // 仓库号
                                                     //wmsPutaway.areaNo = wmsInReceiptIqcResult.areaNo; // 区域编码(楼号)
                                                     //wmsPutaway.areaNo = wmsStock.areaNo; // 区域编码(楼号)
                                                     //wmsPutaway.delFlag = "0"; // 删除标志;0-有效,1-已删除
                                                     //wmsPutaway.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库
            wmsPutaway.loadedType = wmsStock.loadedType; // 装载类型;1:实盘 ；2:工装；0：空盘；
            wmsPutaway.manualFlag = 0; // 是否允许人工上架;0默认不允许，1允许
            wmsPutaway.onlineLocNo = ""; // 上线站台：WCS请求时的站台
            wmsPutaway.onlineMethod = "3"; // 上线方式;0自动上线；1人工上线；2组盘上线；3直接上架
            wmsPutaway.palletBarcode = wmsStock.palletBarcode; // 载体条码
            wmsPutaway.proprietorCode = wmsStock.proprietorCode; // 货主
            wmsPutaway.ptaRegionNo = wmsStock.regionNo; // 上架库区编号
            wmsPutaway.putawayNo = putawayNoSeq; // 上架单编号
            wmsPutaway.putawayStatus = 0; // 状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成
            wmsPutaway.regionNo = wmsStock.areaNo; // 库区编号
            wmsPutaway.stockCode = wmsStock.stockCode; // 库存编码
                                                       //wmsPutaway.extend1 = wmsInReceiptIqcResult.extend1; // 扩展字段1
                                                       //wmsPutaway.extend2 = wmsInReceiptIqcResult.extend2; // 扩展字段2
                                                       //wmsPutaway.extend3 = wmsInReceiptIqcResult.extend3; // 扩展字段3
                                                       //wmsPutaway.extend4 = wmsInReceiptIqcResult.extend4; // 扩展字段4
                                                       //wmsPutaway.extend5 = wmsInReceiptIqcResult.extend5; // 扩展字段5
                                                       //wmsPutaway.extend6 = wmsInReceiptIqcResult.extend6; // 扩展字段6
                                                       //wmsPutaway.extend7 = wmsInReceiptIqcResult.extend7; // 扩展字段7
                                                       //wmsPutaway.extend8 = wmsInReceiptIqcResult.extend8; // 扩展字段8
                                                       //wmsPutaway.extend9 = wmsInReceiptIqcResult.extend9; // 扩展字段9
                                                       //wmsPutaway.extend10 = wmsInReceiptIqcResult.extend10; // 扩展字段10
                                                       //wmsPutaway.extend11 = wmsInReceiptIqcResult.extend11; // 扩展字段11
                                                       //wmsPutaway.extend12 = wmsInReceiptIqcResult.extend12; // 扩展字段12
                                                       //wmsPutaway.extend13 = wmsInReceiptIqcResult.extend13; // 扩展字段13
                                                       //wmsPutaway.extend14 = wmsInReceiptIqcResult.extend14; // 扩展字段14
                                                       //wmsPutaway.extend15 = wmsInReceiptIqcResult.extend15; // 扩展字段15
            wmsPutaway.CreateBy = wmsStock.CreateBy; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsPutaway.CreateTime = DateTime.Now;
            wmsPutaway.UpdateBy = wmsStock.UpdateBy;
            wmsPutaway.UpdateTime = DateTime.Now;
            wmsPutaway.areaNo = wmsStock.areaNo;
            return wmsPutaway;
        }

        private static WmsInReceiptRecord CreateInrecord(CfgDocType docTypeInfo, BasBMaterial matInfo, WmsStock wmsStock, WmsStockDtl wmsStockDtl)
        {
            WmsInReceiptRecord wmsInReceiptRecord = new WmsInReceiptRecord();
            //wmsInReceiptRecord.areaNo = wmsInReceiptIqcResult.areaNo; // 区域
            wmsInReceiptRecord.areaNo = wmsStock.areaNo; // 区域
            wmsInReceiptRecord.erpWhouseNo = ""; // ERP仓库号
            wmsInReceiptRecord.whouseNo = wmsStock.whouseNo; // ERP仓库号
            wmsInReceiptRecord.regionNo = wmsStock.regionNo; // 库区
            wmsInReceiptRecord.binNo = wmsStock.binNo; // 库位
            wmsInReceiptRecord.proprietorCode = wmsStock.proprietorCode; // 货主
            wmsInReceiptRecord.iqcResultNo = ""; // 检验单号
            wmsInReceiptRecord.receiptNo = ""; // 收货单号
            wmsInReceiptRecord.receiptDtlId = null; // 收货明细ID
            wmsInReceiptRecord.inNo = ""; // 入库单号
            wmsInReceiptRecord.inDtlId = null; // 入库单明细ID
            wmsInReceiptRecord.externalInNo = ""; // 外部入库单号
            wmsInReceiptRecord.externalInDtlId = ""; // 外部入库单行号
            wmsInReceiptRecord.orderNo = ""; // 关联单号
            wmsInReceiptRecord.orderDtlId = null; // 关联单行号
            wmsInReceiptRecord.docTypeCode = docTypeInfo.docTypeCode; // 单据类型
            wmsInReceiptRecord.sourceBy = 0; // 数据来源
            wmsInReceiptRecord.inOutTypeNo = ""; // 出入库类别代码
            wmsInReceiptRecord.inOutTypeNo = ""; // 出入库类别代码
            wmsInReceiptRecord.inOutName = ""; // 出入库名称
            wmsInReceiptRecord.stockCode = wmsStock.stockCode; // 库存编码
            wmsInReceiptRecord.palletBarcode = wmsStock.palletBarcode; // 载体条码
            wmsInReceiptRecord.loadedType = wmsStock.loadedType; // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.ptaBinNo = ""; // 实际上架库位号
            wmsInReceiptRecord.ptaPalletBarcode = ""; // 实际上架后的托盘号
            wmsInReceiptRecord.returnFlag = 4; // 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传
            wmsInReceiptRecord.returnTime = DateTime.Now; // 回传时间
                                                          //wmsInReceiptRecord.returnResult = returnResult;                                                // 回传结果
            wmsInReceiptRecord.materialCode = wmsStockDtl.materialCode; // 物料代码
            wmsInReceiptRecord.materialName = wmsStockDtl.materialName; // 物料名称
            wmsInReceiptRecord.supplierCode = wmsStockDtl.supplierCode; // 供应商编码
            wmsInReceiptRecord.supplierName = wmsStockDtl.supplierName; // 供应商名称
            wmsInReceiptRecord.supplierNameEn = wmsStockDtl.supplierNameEn; // 供应商名称-英文
            wmsInReceiptRecord.supplierNameAlias = wmsStockDtl.supplierNameAlias; // 供应商名称-其他
            wmsInReceiptRecord.batchNo = ""; // 批次
            wmsInReceiptRecord.materialSpec = wmsStockDtl.materialSpec; // 规格型号
            wmsInReceiptRecord.recordQty = wmsStockDtl.qty; // 组盘数量
            wmsInReceiptRecord.inspectionResult = wmsStockDtl.inspectionResult; // 质检结果：待检、合格、特采、不合格
            wmsInReceiptRecord.inRecordStatus = 0; // 状态：0：初始创建（组盘完成）；41：入库中；90入库完成；92删除（撤销）；93强制完成
            wmsInReceiptRecord.skuCode = wmsStockDtl.skuCode; // SKU 编码
            wmsInReceiptRecord.departmentName = ""; // 部门名称
            wmsInReceiptRecord.projectNo = wmsStockDtl.projectNo; // 项目号
            wmsInReceiptRecord.ticketNo = ""; // 工单号
            wmsInReceiptRecord.inspector = ""; // 质检员
            wmsInReceiptRecord.minPkgQty = matInfo.MinPkgQty; // 包装数量
            wmsInReceiptRecord.urgentFlag = 0; // 急料标记
            wmsInReceiptRecord.unitCode = matInfo.UnitCode; // 单位
            wmsInReceiptRecord.CreateTime = DateTime.Now;
            wmsInReceiptRecord.CreateBy = wmsStockDtl.CreateBy; // LoginUserInfo?.Name == null ? "EBS" : LoginUserInfo?.Name;
            wmsInReceiptRecord.UpdateTime = DateTime.Now;
            wmsInReceiptRecord.UpdateBy = wmsStockDtl.UpdateBy;
            return wmsInReceiptRecord;
        }

        private static WmsStockDtl CreateStockDtl(EmptyInDto input, BasBMaterial matInfo, WmsStock wmsStock)
        {
            WmsStockDtl wmsStockDtl = new WmsStockDtl();
            wmsStockDtl.whouseNo = wmsStock.whouseNo; // 仓库号
                                                      //wmsStockDtl.areaNo = item.areaNo; // 区域编码(楼号)
            wmsStockDtl.areaNo = wmsStock.areaNo; // 区域编码(楼号)
                                                  //wmsStockDtl.batchNo = item.batchNo; // 批次
                                                  //wmsStockDtl.delFlag = 0; // 删除标志;0-有效,1-已删除
            wmsStockDtl.erpWhouseNo = ""; // ERP仓库
            wmsStockDtl.inspectionResult = 10; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
            wmsStockDtl.lockFlag = 0; // 锁定状态;0：未锁定库存；1：已锁定库存。
            wmsStockDtl.lockReason = ""; // 锁定原因
            wmsStockDtl.materialName = matInfo.MaterialName; // 物料名称
            wmsStockDtl.materialCode = matInfo.MaterialCode; // 物料编码
            wmsStockDtl.materialSpec = matInfo.MaterialSpec; // 规格型号
            wmsStockDtl.occupyQty = 0; // 占用数量
            wmsStockDtl.palletBarcode = wmsStock.palletBarcode; // 载体条码
            wmsStockDtl.projectNo = ""; // 项目号
            wmsStockDtl.projectNoBak = ""; // 项目号
            wmsStockDtl.proprietorCode = wmsStock.proprietorCode; // 货主
            wmsStockDtl.qty = input.qty; // 库存数量
            wmsStockDtl.skuCode = matInfo.MaterialCode; // SKU编码
            wmsStockDtl.stockCode = wmsStock.stockCode; // 库存编码
            wmsStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()); // 库存明细状态;0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成
            wmsStockDtl.supplierCode = ""; // 供应商编码
            wmsStockDtl.supplierName = ""; // 供方名称
            wmsStockDtl.supplierNameAlias = ""; // 供方名称-其他
            wmsStockDtl.supplierNameEn = "";// 供方名称-英文
            wmsStockDtl.extend1 = null; // 扩展字段1
            wmsStockDtl.extend2 = null; // 扩展字段2
            wmsStockDtl.extend3 = null; // 扩展字段3
            wmsStockDtl.extend4 = null; // 扩展字段4
            wmsStockDtl.extend5 = null; // 扩展字段5
            wmsStockDtl.extend6 = null; // 扩展字段6
            wmsStockDtl.extend7 = null; // 扩展字段7
            wmsStockDtl.extend8 = null; // 扩展字段8
            wmsStockDtl.extend9 = null; // 扩展字段9
            wmsStockDtl.extend10 = null; // 扩展字段10
            wmsStockDtl.extend11 = null; // 扩展字段11
            wmsStockDtl.chipSize = null; // 扩展字段12
            wmsStockDtl.chipThickness = null; // 扩展字段13
            wmsStockDtl.chipModel = null; // 扩展字段14
            wmsStockDtl.dafType = null; // 扩展字段15
            wmsStockDtl.unitCode = matInfo.UnitCode;
            wmsStockDtl.CreateBy = wmsStock.CreateBy; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockDtl.UpdateBy = wmsStock.UpdateBy; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockDtl.CreateTime = DateTime.Now;
            wmsStockDtl.UpdateTime = DateTime.Now;
            return wmsStockDtl;
        }

        private static WmsStock CreateStock(EmptyInDto input, SysSequenceVM sysSequenceApiVM)
        {
            WmsStock wmsStock = new WmsStock();
            //wmsStock.areaNo = areaNo; // 区域编码(楼号)
            wmsStock.areaNo = "Area06"; // 区域编码(楼号)
            wmsStock.binNo = "WS01_01_01_01"; // 库位号
            wmsStock.errFlag = 0; // 异常标记(0正常，10异常，20火警)
            wmsStock.errMsg = ""; // 异常说明
            wmsStock.height = input.height; // 组盘后托盘高度
            wmsStock.invoiceNo = null; // 预拣选完成后的发货单号
            wmsStock.loadedType = 0; // 装载类型(1:实盘 ；2:工装；0：空盘)
            wmsStock.locAllotGroup = null; // 分配站台组，双工位使用
            wmsStock.locNo = null; // 当前站台号。应该为虚拟站台号，数据定义好后进行填充。
            wmsStock.palletBarcode = input.palletBarcode; // 载体条码
            wmsStock.proprietorCode = "TZ"; // 货主
            wmsStock.regionNo = "WS01"; // 库区编号
            wmsStock.roadwayNo = "00"; // 巷道编号
            wmsStock.specialFlag = 0; // 特殊标记
            wmsStock.specialMsg = null; // 特殊说明
            wmsStock.stockCode = sysSequenceApiVM.GetSequence(SequenceCode.StockCode.GetCode()); // 库存编码
            wmsStock.stockStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()); // 库存状态（0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成）
            wmsStock.weight = null; // 组盘重量
            wmsStock.whouseNo = "TZ"; // 仓库号
            wmsStock.CreateBy = input.invoker ?? input.invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStock.CreateTime = DateTime.Now;
            wmsStock.UpdateBy = input.invoker ?? input.invoker; ; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStock.UpdateTime = DateTime.Now;
            return wmsStock;
        }
        #endregion
    }
}
