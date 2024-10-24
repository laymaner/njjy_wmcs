using Com.Wish.Model.Base;
using Com.Wish.Model.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Mvc.Binders;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Wish.Helper;
using Wish.Model.System;
using Wish.ViewModel.Common;
using WMS.Model.Base;
using ZXing.QrCode.Internal;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Wish.Controllers.Common
{
    /// <summary>
    /// 下拉框
    /// </summary>
    [Area("Common")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("下拉框管理")]
    [ApiController]
    [Route("api/Option")]
    public class OptionApiController: BaseApiController
    {
        /// <summary>
        /// 0表示字符串
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<ListResultDto<SelectOptionModel>> GetDictionaryCommonOption(string code, int type = 0)
        {
            List<SelectOptionModel> result=new List<SelectOptionModel>();
             var data=await DC.Set<SysDictionary>().Where(t=>t.dictionaryCode==code && t.usedFlag==1).ToListAsync();
            if (data.Any())
            {
                result = data.Select(t => new SelectOptionModel
                {
                    Value = type == 1 ? Convert.ToInt32(t.dictionaryItemCode) : t.dictionaryItemCode,
                    Text = t.dictionaryItemName,
                }).Distinct().ToList();
            }
            return new ListResultDto<SelectOptionModel>(result);
        }
        /// <summary>
        /// 字典通用下拉框
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ActionDescription("字典通用下拉框")]
        [Route("[action]")]
        [HttpGet]
        [Public]
        public  async Task<IActionResult> getDicOption(string code)
        {
            try
            {
                ListResultDto<SelectOptionModel> dic =await GetDictionaryCommonOption(code);
                //return Ok(vm.Entity);
                return new JsonResult(ApiControllerHelper.SearchOk(dic));

            }
            catch(Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
          
        }
        /// <summary>
        /// 字典通用下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("启用标志下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async  Task<IActionResult> getUsedFlagOption()
        {
            return await getDicOption("USED_FLAG");

        }

        /// <summary>
        /// 任务状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("任务状态下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getTaskStatusOption()
        {
            return await getDicOption("TASK_STATUS");

        }
        /// <summary>
        /// 任务反馈状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("任务反馈状态下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getFeedbackStatusOption()
        {
            return await getDicOption("FEEDBACK_STATUS");

        }

        /// <summary>
        /// wms任务类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("wms任务类型下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getWmsTaskTypeOption()
        {
            return await getDicOption("WMS_TASK_TYPE");

        }

        /// <summary>
        /// 任务类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("任务类型下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getTaskTypeOption()
        {
            return await getDicOption("TASK_TYPE_NO");

        }

        /// <summary>
        /// 查询类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("查询类型下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getQueryTypeOption()
        {
            return await getDicOption("QUERY_TYPE");

        }

        /// <summary>
        /// 客户下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("客户下拉框")]
        [Route("[action]")]
        [HttpGet]
        public  async Task<IActionResult> getCustomerOption()
        {
            try
            {
                var entities = await DC.Set<BasBCustomer>().Where(t => t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.CustomerCode,
                    Text = t.CustomerName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 物料大类下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料大类下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialCategoryOption()
        {
            try
            {
                var entities = await DC.Set<BasBMaterialCategory>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.materialCategoryCode,
                    Text = t.materialCategoryCode,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }
        /// <summary>
        /// 物料大类首字母下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料大类首字母下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialCategoryFirstOption()
        {
            try
            {
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                var entities = await DC.Set<BasBMaterialCategory>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                if(entities.Count > 0)
                {
                    var cateNoList = entities.Select(t => t.materialCategoryCode).ToList();
                    var cateFirstNoList = cateNoList.Where(t=>!string.IsNullOrWhiteSpace(t)).Select(t=>t.FirstOrDefault().ToString()).Distinct().ToList();
                    result = cateFirstNoList.Select(t => new SelectOptionModel
                    {
                        Value = t,
                        Text = t+"大类",
                    }).Distinct().OrderBy(x=>x.Text).ToList();
                }
                
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }
        /// <summary>
        /// 物料类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialTypeOption()
        {
            try
            {
                var entities = await DC.Set<BasBMaterialType>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.materialTypeCode,
                    Text = t.materialTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }

        /// <summary>
        /// 物料类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialTypeByCategoryOption(/*string categoryNo*/)
        {
            try
            {
                var entities = await DC.Set<BasBMaterialType>().Where(t =>/*t.materialCategoryCode==categoryNo &&*/ t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.materialTypeCode,
                    Text = t.materialTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }
        /// <summary>
        /// 物料下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialOption()
        {
            try
            {
                var entities = await DC.Set<BasBMaterial>().Where(t =>  t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.MaterialCode,
                    Text = t.MaterialName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }
        /// <summary>
        /// 物料下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialByCategoryOption(string categoryNo)
        {
            try
            {
                var entities = await DC.Set<BasBMaterial>().Where(t => t.MaterialCategoryCode == categoryNo && t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.MaterialCode,
                    Text = t.MaterialName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }
        /// <summary>
        /// 物料下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialByTypeOption(string materialTypeCode)
        {
            try
            {
                var entities = await DC.Set<BasBMaterial>().Where(t => t.MaterialTypeCode == materialTypeCode && t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.MaterialCode,
                    Text = t.MaterialName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }

        /// <summary>
        /// 电子料标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("电子料标记下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getElecMaterialFlagOption()
        {
            return await getDicOption("ELECTRONIC_MATERIAL_FLAG");

        }

        /// <summary>
        /// 物料标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料标记下拉框")]
        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> getMaterialFlagOption()
        {
            return await getDicOption("MATERIAL_FLAG");

        }

        /// <summary>
        /// MSL等级下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("MSL等级下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMslGradeOption()
        {
            try
            {
                var entities = await DC.Set<BasBMslMaintain>().Where(t => t.usedFlag == 1).AsNoTracking().OrderBy(x=>x.mslGradeCode).ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.mslGradeCode,
                    Text = t.mslGradeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }

        }


        /// <summary>
        /// 仓库下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("仓库下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getWhouseOption()
        {
            try
            {
                var entities = await DC.Set<BasWWhouse>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.whouseNo,
                    Text = t.whouseName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 库区类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库区类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRegionTypeOption()
        {
            try
            {
                var entites = await DC.Set<BasWRegionType>().AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entites.Select(t => new SelectOptionModel
                {
                    Value = t.regionTypeCode, 
                    Text = t.regionTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch(Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }


        /// <summary>
        /// 库区设备类型标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库区设备类型标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRegionDeviceFlagOption()
        {
            return await getDicOption("REGION_DEVICE_FLAG");
        }

        /// <summary>
        /// 虚拟标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("虚拟标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getVirtualFlagOption()
        {
            return await getDicOption("VIRTUAL_FLAG");
        }

        /// <summary>
        /// 伸工位类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("伸工位类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSdTypeOption()
        {
            return await getDicOption("SD_TYPE");
        }

        /// <summary>
        /// 人工上架下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("人工上架下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getManualFlagOption()
        {
            return await getDicOption("MANUAL_FLAG");
        }

        /// <summary>
        /// 取货方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("取货方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPickupMethodOption()
        {
            return await getDicOption("PICKUP_METHOD");
        }

        /// <summary>
        /// 带盘存储下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("带盘存储下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPalletMgtOption()
        {
            return await getDicOption("PALLET_MGT");

        }

        /// <summary>
        /// 区域下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("区域下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAreaOption()
        {
            try
            {
                var entities = await DC.Set<BasWArea>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.areaNo,
                    Text = t.areaName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 库区下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库区下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRegionOption(string areaNo)
        {
            try
            {
                var entities = await DC.Set<BasWRegion>()
                    .Where(t => t.usedFlag == 1 && t.areaNo == areaNo).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.regionNo,
                    Text = t.regionName,
                }).Distinct().ToList();
                result = result.DistinctBy(t => t.Value).ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 立库库区下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("立库库区下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLkRegionOption()
        {
            try
            {
                var entities = await DC.Set<BasWRegion>()
                    .Where(t => t.usedFlag == 1 && t.sdType!="PK" && t.regionTypeCode=="SP").AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.regionNo,
                    Text = t.regionName,
                }).Distinct().ToList();
                result = result.DistinctBy(t => t.Value).ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 存储库区下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("存储库区下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSpRegionOption()
        {
            try
            {
                var entities = await DC.Set<BasWRegion>()
                    .Where(t => t.usedFlag == 1  && t.regionTypeCode == "SP").AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.regionNo,
                    Text = t.regionName,
                }).Distinct().ToList();
                result = result.DistinctBy(t=>t.Value).ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 库区下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库区下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRegionByAreaOption(string areaNo)
        {
            try
            {
                var entities = await DC.Set<BasWRegion>()
                    .Where(t => t.usedFlag == 1 && t.virtualFlag == 0 && t.areaNo == areaNo).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.regionNo,
                    Text = t.regionName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 库区下拉框（移库）
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库区下拉框（移库）")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveRegionOption(string areaNo)
        {
            try
            {
                var entities = await DC.Set<BasWRegion>().Where(t => t.usedFlag == 1 && t.areaNo == areaNo && t.regionTypeCode == "SP" && t.pickupMethod == "G2P").AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.regionNo,
                    Text = t.regionName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 异常标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("异常标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getErrFlagOption()
        {
            return await getDicOption("ERR_FLAG");
        }

        /// <summary>
        /// 巷道下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("巷道下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRoadwayOption(string areaNo, string regionNo)
        {
            try
            {
                var entities = await DC.Set<BasWRoadway>().Where(t => t.usedFlag == 1 && t.areaNo == areaNo && t.regionNo == regionNo).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.roadwayNo,
                    Text = t.roadwayName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 允许入库下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("允许入库下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getIsInEnableOption()
        {
            return await getDicOption("IS_IN_ENABLE");
        }

        /// <summary>
        /// 允许出库下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("允许出库下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getIsOutEnableOption()
        {
            return await getDicOption("IS_OUT_ENABLE");
        }

        /// <summary>
        /// 库位分类下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库位分类下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBinTypeOption()
        {
            return await getDicOption("BIN_TYPE");
        }

        /// <summary>
        /// 库位异常下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库位异常标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBinErrFlagOption()
        {
            return await getDicOption("BIN_ERR_FLAG");
        }

        /// <summary>
        /// 货架下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("货架下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getRackOption(string roadwayNo)
        {
            try
            {
                var entities = await DC.Set<BasWRack>()
                    .Where(t => t.usedFlag == 1 && t.roadwayNo == roadwayNo).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.rackNo,
                    Text = t.rackName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 火警标志下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("火警标志下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getFireFlagOption()
        {
            return await getDicOption("FIRE_FLAG");
        }

        /// <summary>
        /// 托盘方向下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘方向下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPalletDirectOption()
        {
            return await getDicOption("PALLET_DIRECT");
        }

        /// <summary>
        /// 托盘和料箱类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAllPalletTypeOption()
        {
            try
            {
                var entities = await DC.Set<BasWPalletType>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.palletTypeCode,
                    Text = t.palletTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 托盘类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPalletTypeOption()
        {
            try
            {
                var entities = await DC.Set<BasWPalletType>().Where(t => t.usedFlag == 1 /*&& t.palletTypeCode !="BX"*/).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.palletTypeCode,
                    Text = t.palletTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 托盘类型标识下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘类型标识下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPalletTypeFlagOption()
        {
            return await getDicOption("PALLET_TYPE_FLAG");
        }

        /// <summary>
        /// 开发标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("开发标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDevelopFlagOption()
        {
            return await getDicOption("DEVELOP_FLAG");
        }

        /// <summary>
        /// 托盘垛管理下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘垛管理下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBarcodeFlagOption()
        {
            return await getDicOption("BARCODE_FLAG");
        }

        /// <summary>
        /// 站台组类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("站台组类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocGroupTypeOption()
        {
            return await getDicOption("LOC_GROUP_TYPE");
        }

        /// <summary>
        /// 单位类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("单位类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getUnitTypeOption()
        {
            return await getDicOption("UNIT_TYPE");
        }

        /// <summary>
        /// 参数类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("参数类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getParValueTypeOption()
        {
            return await getDicOption("PAR_Value_TYPE");
        }

        /// <summary>
        /// 业务下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("业务下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBusinessOption()
        {
            try
            {
                var entities = await DC.Set<CfgBusiness>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.businessCode,
                    Text = t.businessName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 业务模块下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("业务模块下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBusinessModuleOption(string businessCode)
        {
            try
            {
                List<SelectOptionModel> result = new List<SelectOptionModel> { };
                var entities = await DC.Set<CfgBusinessModule>().Where(t => t.usedFlag == 1)
                    .WhereIf(!string.IsNullOrWhiteSpace(businessCode),t=>t.businessCode==businessCode)
                    .AsNoTracking().ToListAsync();
                result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.businessModuleCode,
                    Text = t.businessModuleName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 复选标识下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("复选标识下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCheckFlagOption()
        {
            return await getDicOption("CHECK_FLAG");
        }

        /// <summary>
        /// 是否默认值下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否默认值下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDefaultFlagOption()
        {
            return await getDicOption("DEFAULT_FLAG");
        }

        /// <summary>
        /// 客供类型下拉框,code:单据类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ActionDescription("客供类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCvTypeOption(string code)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(code))
                {
                    return await getDicOption("CV_TYPE");
                }
                var entities = await DC.Set<CfgDocType>().Where(t => t.docTypeCode == code && t.usedFlag == 1).AsNoTracking().FirstOrDefaultAsync();
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                if (entities != null)
                {
                    var data = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "CV_TYPE" && t.dictionaryItemCode == entities.cvType && t.usedFlag == 1).AsNoTracking().ToListAsync();
                    if (data.Any())
                    {
                        result = data.Select(t => new SelectOptionModel
                        {
                            Value = t.dictionaryItemCode,
                            Text = t.dictionaryItemName,
                        }).Distinct().ToList();
                    }
                }
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {

                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 客供类型下拉框,code:单据类型(code为空时，返回空)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ActionDescription("客供类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCvTypeByDocTypeOption(string code)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(code))
                {
                    
                    List<SelectOptionModel> items = new List<SelectOptionModel>();
                    return new JsonResult(ApiControllerHelper.SearchOk(new { items }));
                }
                var entities = await DC.Set<CfgDocType>().Where(t => t.docTypeCode == code && t.usedFlag == 1).AsNoTracking().FirstOrDefaultAsync();
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                if (entities != null)
                {
                    var data = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "CV_TYPE" && t.dictionaryItemCode == entities.cvType && t.usedFlag == 1).AsNoTracking().ToListAsync();
                    if (data.Any())
                    {
                        result = data.Select(t => new SelectOptionModel
                        {
                            Value = t.dictionaryItemCode,
                            Text = t.dictionaryItemName,
                        }).Distinct().ToList();
                    }
                }
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {

                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 策略类型分类下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("策略类型分类下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStrategyTypeCategoryOption()
        {
            return await getDicOption("STRATEGY_TYPE_CATEGORY");
        }

        /// <summary>
        /// 策略类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("策略类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStrategyTypeOption()
        {
            try
            {
                var entities = await DC.Set<CfgStrategyType>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.strategyTypeCode,
                    Text = t.strategyTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 对应关系下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("对应关系下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCfgRelationshipTypeOption()
        {
            try
            {
                var entities = await DC.Set<CfgRelationshipType>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.relationshipTypeCode,
                    Text = t.relationshipTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 有效期管理下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("有效期管理下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getIsValidityPeriodOption()
        {
            return await getDicOption("IS_VALIDITY_PERIOD");
        }

        /// <summary>
        /// 托盘是否校验下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("托盘是否校验下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCheckPalletFlagOption()
        {
            return await getDicOption("CHECK_PALLET_FLAG");
        }

        /// <summary>
        /// 是否自动延期下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否自动延期下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getIsAutoDelayOption()
        {
            return await getDicOption("IS_AUTO_DELAY");
        }

        /// <summary>
        /// 是否共用料下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否共用料下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSharedFlagOption()
        {
            return await getDicOption("SHARED_FALG");
        }

        /// <summary>
        /// 站台类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("站台类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocTypeOption()
        {
            return await getDicOption("LOC_TYPE");
        }

        /// <summary>
        /// 站台组编码下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("站台组编码下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocGroupNoOption()
        {
            try
            {
                var entities = await DC.Set<BasWLocGroup>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.locGroupNo,
                    Text = t.locGroupName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 纸张类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("纸张类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPaperTypeOption()
        {
            return await getDicOption("PRINT_PAGE_KIND");
        }

        /// <summary>
        /// 是否需要优先级下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否需要优先级下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPriorityFlagOption()
        {
            return await getDicOption("PRIORITY_FLAG");
        }

        /// <summary>
        /// 是否免检下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否免检下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getQcFlagOption()
        {
            return await getDicOption("QC_FLAG");
        }

        /// <summary>
        /// 物料单位下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料单位下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialUnitOption()
        {
            try
            {
                var entities = await DC.Set<BasBUnit>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.unitCode,
                    Text = t.unitName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// ERP仓库下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("ERP仓库下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getErpWhouseOption()
        {
            try
            {
                var entities = await DC.Set<BasWErpWhouse>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.erpWhouseNo,
                    Text = t.erpWhouseName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// ERP仓库下拉框,升序
        /// </summary>
        /// <returns></returns>
        [ActionDescription("ERP仓库下拉框（排序）")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getErpWhouseOrderByAscOption()
        {
            try
            {
                var entities = await DC.Set<BasWErpWhouse>().Where(t => t.usedFlag == 1).AsNoTracking().OrderBy(x=>x.erpWhouseNo).ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.erpWhouseNo,
                    Text = t.erpWhouseNo,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        #region 入库管理
        /// <summary>
        /// 单据类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("单据类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDocTypeOption(string businessCode)
        {
            try
            {
                var entities = await DC.Set<CfgDocType>().Where(t => t.usedFlag == 1).WhereIf(!string.IsNullOrWhiteSpace(businessCode),x=>x.businessCode == businessCode).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.docTypeCode,
                    Text = t.docTypeName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 单据状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("单据状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDocStatusOption()
        {
            return await getDicOption("IN_DTL_STATUS");
        }

        /// <summary>
        /// 入库单明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("入库单明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInDtlStatusOption()
        {
            return await getDicOption("IN_DTL_STATUS");
        }

        /// <summary>
        /// 发库单明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("发库单明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInvoiceDtlStatusOption()
        {
            return await getDicOption("INVOICE_DTL_STATUS");
        }

        /// <summary>
        /// 单据状态下拉框（移库）
        /// </summary>
        /// <returns></returns>
        [ActionDescription("单据状态下拉框（移库）")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveDocStatusOption()
        {
            return await getDicOption("MOVE_STATUS");
        }
        //
        /// <summary>
        /// 库存变化类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库存变化类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStockTypeOption()
        {
            return await getDicOption("STOCK_TYPE");
        }


        /// <summary>
        /// 发货单状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("发货单状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getinvoiceStatusOption()
        {
            return await getDicOption("INVOICE_STATUS");
        }

        /// <summary>
        /// 波次状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("波次状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getwaveStatusOption()
        {
            return await getDicOption("WAVE_STATUS");
        }

        /// <summary>
        /// 出库记录状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("出库记录状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getoutRecordStatusOption()
        {
            return await getDicOption("OUT_RECORD_STATUS");
        }

        /// <summary>
        /// 单据来源下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("单据来源下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDocSourceByOption()
        {
            return await getDicOption("SOURCE_BY");
        }

        /// <summary>
        /// 供方类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("供方类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSupplierTypeOption()
        {
            return await getDicOption("SUPPLIER_TYPE");
        }

        /// <summary>
        /// 供方名称下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("供方名称下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSupplierNameOption()
        {
            try
            {
                var entities = await DC.Set<BasBSupplier>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.supplierCode,
                    Text = t.supplierName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 供方名称下拉框,code:SUPPLIER_TYPE
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ActionDescription("供方名称下拉框(code)")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getSupplierNameListOption(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>()));
                }
                switch (code)
                {
                    case "C":
                       return await getCustomerOption();
                        break;
                    //case "D":
                    //    break;
                    //case "P":
                    //    break;
                    case "S":
                        return await getSupplierNameOption();
                        break;
                    case "W":
                        return await getWhouseOption();
                        break;
                    default:
                        return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>()));
                        break;
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 客供名称下拉框，Code参数值：C（客户）、D（部门）、P（产线）、S（供应商）、W（仓库）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ActionDescription("客供名称下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getCvNameOption(string code)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(code))
                {
                    return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>()));
                }
                switch (code)
                {
                    case "C":
                        return await getCustomerOption();
                        break;
                    //case "D":
                    //    break;
                    //case "P":
                    //    break;
                    case "S":
                        return await getSupplierNameOption();
                        break;
                    case "W":
                        return await getWhouseOption();
                        break;
                    default:
                        return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>()));
                        break;
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        private List<SelectOptionModel> getCustomerOptionList()
        {
            try
            {
                var entities = DC.Set<BasBCustomer>().Where(t => t.UsedFlag == 1).ToList();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.CustomerCode,
                    Text = t.CustomerName,
                }).Distinct().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 货主下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("货主下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getProprietorOption()
        {
            try
            {
                var entities = DC.Set<BasBProprietor>().Where(t => t.usedFlag == 1).ToList();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.proprietorCode,
                    Text = t.proprietorName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 装载状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("装载状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLoadedStatusOption()
        {
            return await getDicOption("LOADED_TYPE");
        }

        /// <summary>
        /// 上架单状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("上架单状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPutawayStatusOption()
        {
            return await getDicOption("PUTAWAY_STATUS");
        }

        /// <summary>
        /// 码级管理方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("码级管理方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBarCodeManageFlagOption()
        {
            return await getDicOption("BARCODE_FLAG");
        }

        /// <summary>
        /// 不良选项下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("不良选项下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBadOptionsOption()
        {
            return await getDicOption("BAD_OPTIONS");
        }

        /// <summary>
        /// 不良处理方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("不良处理方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBadHandleWayOption()
        {
            return await getDicOption("BAD_HANDLE_WAY");
        }

        #endregion

        #region 电子料管理

        /// <summary>
        /// 急料标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("急料标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getUrgentFlagOption()
        {
            return await getDicOption("URGENT_FLAG");
        }

        /// <summary>
        /// 补料标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("补料标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getReplenishFlagOption()
        {
            return await getDicOption("REPLENISH_FLAG");
        }

        /// <summary>
        /// 拆封状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("拆封状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getUnpackStatusOption()
        {
            return await getDicOption("UNPACK_STATUS");
        }


        /// <summary>
        /// 是否烘干报废下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否烘干报废下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDriedScrapFlagOption()
        {
            return await getDicOption("DRIED_SCRAP_FLAG");
        }



        #endregion

        #region 库存管理

        /// <summary>
        /// 库存状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库存状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStockStatusOption()
        {
            return await getDicOption("STOCK_STATUS");
        }
        
        /// <summary>
        /// PDA库存状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("PDA库存状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStockStatusPdaOption()
        {
            return await getDicOption("STOCK_PDA_STATUS");
        }

        /// <summary>
        /// PDA库区下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("PDA库区下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getReginoPdaOption()
        {
            return await getDicOption("REGION_PDA");
        }

        /// <summary>
        /// 库存状态下拉框（移库）
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库存状态下拉框（移库）")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveStockStatusOption()
        {
            try
            {
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                var data = await DC.Set<SysDictionary>().Where(t => t.dictionaryCode == "STOCK_STATUS" && (t.dictionaryItemCode == "0" || t.dictionaryItemCode == "50") && t.usedFlag == 1).AsNoTracking().ToListAsync();
                if (data.Any())
                {
                    result = data.Select(t => new SelectOptionModel
                    {
                        Value = t.dictionaryItemCode,
                        Text = t.dictionaryItemName,
                    }).Distinct().ToList();
                }
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 装载类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("装载类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLoadedTypeOption()
        {
            return await getDicOption("LOADED_TYPE");
        }

        /// <summary>
        /// 楼号(区域编码)下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription(" 楼号(区域编码)下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAreaNoOption()
        {
            try
            {
                var entities = await DC.Set<BasWArea>().Where(t => t.usedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.areaNo,
                    Text = t.areaName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 锁定标记下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("锁定标记下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLockFlagOption()
        {
            return await getDicOption("LOCK_FLAG");
        }

        /// <summary>
        /// 超期冻结下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("超期冻结下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDelayFrozenFlagOption()
        {
            return await getDicOption("DELAY_FROZEN_FLAG");
        }

        /// <summary>
        /// 超暴露时长冻结下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("超暴露时长冻结下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getExposeFrozenFlagOption()
        {
            return await getDicOption("EXPOSE_FROZEN_FLAG");
        }

        /// <summary>
        /// 库存明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("库存明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getStockDtlStatusOption()
        {
            return await getDicOption("STOCK_DTL_STATUS");
        }

        /// <summary>
        /// 质量状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("质量状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInspectionResultOption()
        {
            return await getDicOption("INSPECTION_RESULT");
        }

        /// <summary>
        /// 超期类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("超期类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDelayTypeOption()
        {
            return await getDicOption("DELAY_TYPE");
        }

        /// <summary>
        /// 是否呆滞下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否呆滞下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDeadStockOption()
        {
            return await getDicOption("DEAD_STOCK");
        }
        #endregion

        #region 波次

        /// <summary>
        /// 波次类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("波次类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getWaveTypeOption()
        {
            return await getDicOption("WAVE_TYPE");
        }

        /// <summary>
        /// 分配方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("分配方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAllotTypeOption()
        {
            return await getDicOption("ALLOT_TYPE");
        }

        /// <summary>
        /// 空托分配方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("空托分配方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getEmptyAllotTypeOption()
        {
            return await getDicOption("EMPTY_ALLOT_TYPE");
        }

        /// <summary>
        /// 接收方下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("接收方下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAcceptOption(string cvTypeCode)
        {
            try
            {
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                var entities = await DC.Set<WmsOutInvoice>().AsNoTracking().ToListAsync();
                result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.cvCode,
                    Text = t.cvName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
                /*if (cvTypeCode == "C")   //客户
                {
                    var customerList = DC.Set<BasBCustomer>().ToList();
                    result = customerList.Select(t => new SelectOptionModel
                    {
                        Value = t.customerCode,
                        Text = t.customerName,
                    }).Distinct().ToList();
                }
                if (cvTypeCode == "S")   //供应商
                {
                    var customerList = DC.Set<BasBSupplier>().ToList();
                    result = customerList.Select(t => new SelectOptionModel
                    {
                        Value = t.supplierCode,
                        Text = t.supplierName,
                    }).Distinct().ToList();
                }*/
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        #endregion

        /// <summary>
        /// 出库口下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("出库口下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getOutLocOption()
        {
            try
            {
                var entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag == 1 && t.locGroupNo.Contains("TPK") && t.locTypeCode.Contains("OUT")).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.locNo,
                    Text = t.locName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 出库口下拉框(根据托盘类型)
        /// </summary>
        /// <returns></returns>
        [ActionDescription("出库口下拉框(根据托盘类型)")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getOutLocOptionByPalletType(string palletType)
        {
            try
            {
                var entities = new List<BasWLoc>();
                if (string.IsNullOrWhiteSpace(palletType))
                {
                    entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag == 1 && t.locTypeCode.Contains("OUT")).AsNoTracking().ToListAsync();
                }
                else if(palletType.Equals("BX"))
                {
                    entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag == 1 && t.locGroupNo.Contains("LXK") && t.locTypeCode.Contains("OUT")).AsNoTracking().ToListAsync();
                }
                else
                {
                    entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag == 1 && t.locGroupNo.Contains("TPK") && t.locTypeCode.Contains("OUT")).AsNoTracking().ToListAsync();
                }
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.locNo,
                    Text = t.locName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 预警类型下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("预警类型下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getAlertTypeOption()
        {
            try
            {
                var entities = await DC.Set<SysEmail>().Where(t => t.usedFlag == 1).Select(x=>x.alertType).AsNoTracking().Distinct().ToListAsync();
                List<SelectOptionModel> result = new List<SelectOptionModel>();
                foreach (var entity in entities)
                {
                    var one = new SelectOptionModel
                    {
                        Value = entity,
                        Text = entity
                    };
                    result.Add(one);
                }
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 下架站台下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("下架站台下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getPutdownLocOption()
        {
            try
            {
                var entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag ==1 && t.locTypeCode == "MOVE").AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                { 
                    Value = t.locNo,
                    Text = t.locName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch(Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 下架站台下拉框(根据站台类型)：移库：MOVE;抽检：QC;盘点：INVENTORY
        /// </summary>
        /// <returns></returns>
        [ActionDescription("下架站台下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocForTypeOption(string locType)
        {
            try
            {
                var entities = await DC.Set<BasWLoc>().Where(t => t.usedFlag == 1 && t.locTypeCode == locType).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.locNo,
                    Text = t.locName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 大屏站台选择框
        /// </summary>
        /// <param name="locType"></param>
        /// <returns></returns>
        [ActionDescription("大屏站台选择框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocForScreenOption()
        {
            try
            {
                var entities = await DC.Set<SysParameterValue>().Where(t => t.parCode == "LocAndScreen").AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.parValueNo,
                    Text = t.parValueDesc,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 根据库区找下架站台下拉框(根据站台类型)：移库：MOVE;抽检：QC;盘点：INVENTORY
        /// </summary>
        /// <returns></returns>
        [ActionDescription("下架站台下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getLocForRegionAndTypeOption(string regionNo,string locType)
        {
            try
            {
                var entities = await DC.Set<BasWLoc>().Where(t =>t.locGroupNo.Contains(regionNo) &&  t.usedFlag == 1 && t.locTypeCode == locType).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.locNo,
                    Text = t.locName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 物料编码下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("物料编码下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMaterialNoOption()
        {
            try
            {
                var entities = await DC.Set<BasBMaterial>().Where(t => t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.MaterialCode,
                    Text = t.MaterialName,
                }).Distinct().ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }


        /// <summary>
        /// 是否有附件下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("是否有附件下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getHasAttachmentOption()
        {
            return await getDicOption("HAS_ATTACHMENT");
        }

        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <returns></returns>
        //[ActionDescription("用户下拉框")]
        //[Route("[action]")]
        //[HttpGet]
        //public async Task<IActionResult> getUserOption()
        //{
        //    try
        //    {
        //        var entities = await DC.Set<FrameworkUser>().Where(t => t.us == 1).AsNoTracking().ToListAsync();
        //        List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
        //        {
        //            Value = t.userName,
        //            Text = t.realName,
        //        }).Distinct().ToList();
        //        return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
        //    }
        //}

        /// <summary>
        /// 事业部下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("事业部下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDepartmentOption()
        {
            try
            {
                var entities = await DC.Set<BasBDepartment>().Where(t => t.UsedFlag == 1).AsNoTracking().ToListAsync();
                List<SelectOptionModel> result = entities.Select(t => new SelectOptionModel
                {
                    Value = t.DepartmentCode,
                    Text = t.DepartmentName,
                }).Distinct().OrderBy(t=>t.Text).ToList();
                return new JsonResult(ApiControllerHelper.SearchOk(new ListResultDto<SelectOptionModel>(result)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        #region 抽检
        /// <summary>
        /// 抽检记录状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("抽检记录状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getItnQcRecordStatusOption()
        {
            return await getDicOption("ITN_QC_RECORD_STATUS");
        }
        /// <summary>
        /// 抽检明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("抽检明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getItnQcDtlStatusOption()
        {
            return await getDicOption("ITN_QC_DTL_STATUS");
        }
        /// <summary>
        /// 抽检主表状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("抽检主表状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getItnQcStatusOption()
        {
            return await getDicOption("ITN_QC_STATUS");
        }
        #endregion

        #region 盘点
        /// <summary>
        /// 盘点记录状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点记录状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInventoryRecordStatusOption()
        {
            return await getDicOption("INVENTORY_RECORD_STATUS");
        }
        /// <summary>
        /// 盘点明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInventoryDtlStatusOption()
        {
            return await getDicOption("INVENTORY_DTL_STATUS");
        }
        /// <summary>
        /// 盘点主表状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点主表状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getInventoryStatusOption()
        {
            return await getDicOption("INVENTORY_STATUS");
        }

        /// <summary>
        /// 盘点方式下拉框（是否盲盘）
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点方式下拉框（是否盲盘）")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getBlindFlagOption()
        {
            return await getDicOption("BLIND_FLAG");
        }
        /// <summary>
        /// 盘点差异下拉框（盘盈盘亏）
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点差异下拉框（盘盈盘亏））")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDifferenceFlagOption()
        {
            return await getDicOption("DIFFERENCE_FLAG");
        }
        /// <summary>
        /// 盘点有无差异
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点有无差异")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getDifFlagOption()
        {
            return await getDicOption("DIF_FLAG");
        }
        #endregion

        #region 移库
        /// <summary>
        /// 移库记录状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("移库记录状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveRecordStatusOption()
        {
            return await getDicOption("MOVE_RECORD_STATUS");
        }
        /// <summary>
        /// 移库明细状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("移库明细状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveDtlStatusOption()
        {
            return await getDicOption("MOVE_DTL_STATUS");
        }
        /// <summary>
        /// 移库主表状态下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("移库主表状态下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveStatusOption()
        {
            return await getDicOption("MOVE_STATUS");
        }

        /// <summary>
        /// 移库方式下拉框
        /// </summary>
        /// <returns></returns>
        [ActionDescription("移库方式下拉框")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> getMoveMethodOption()
        {
            return await getDicOption("MOVE_METHOD");
        }

        #endregion


    }
}
