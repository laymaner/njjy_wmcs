using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.DevConfig.DeviceConfigVMs;
using Wish.HWConfig.Models;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using Wish.ViewModel.Common.Dtos;


namespace Wish.Controllers
{
    [Area("DeviceConfig")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("设备信息")]
    [ApiController]
    [Route("api/DeviceConfig")]
	public partial class DeviceConfigController : BaseApiController
    {
        #region 框架生成

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(DeviceConfigSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<DeviceConfigListVM>(passInit: true);
                vm.Searcher = searcher;
                return Content(vm.GetJson());
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public DeviceConfigVM Get(string id)
        {
            var vm = Wtm.CreateVM<DeviceConfigVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(DeviceConfigVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(DeviceConfigVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<DeviceConfigBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(DeviceConfigSearcher searcher)
        {
            var vm = Wtm.CreateVM<DeviceConfigListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<DeviceConfigListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<DeviceConfigImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(DeviceConfigImportVM vm)
        {
            if (vm!=null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


        [HttpGet("GetStandardDevices")]
        public ActionResult GetStandardDevices()
        {
            return Ok(DC.Set<StandardDevice>().GetSelectListItems(Wtm, x => x.Device_Name));
        }

        [HttpGet("GetPlcConfigs")]
        public ActionResult GetPlcConfigs()
        {
            return Ok(DC.Set<PlcConfig>().GetSelectListItems(Wtm, x => x.Plc_Name));
        }

        #endregion
        /// <summary>
        /// 开启设备
        /// </summary>
        /// <returns></returns>
        [ActionDescription("开启设备")]
        [HttpPost("DeviceOpen")]
        //[Public]
        public async Task<ActionResult> DeviceOpen(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<DeviceConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SaveDeviceUseFlagDto saveDeviceUseFlagDto = new SaveDeviceUseFlagDto()
                {
                    ids = ids,
                    isUseFlag=1,
                    invoker= invoker
                };
                var result = await vm.SaveDeviceUseFlag(saveDeviceUseFlagDto);
                if (result.code == ResCode.OK)
                {
                    return new JsonResult(ApiControllerHelper.SubmitOk(result.msg, result.outParams));
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error(result.msg));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        [ActionDescription("关闭设备")]
        [HttpPost("DeviceClose")]
        //[Public]
        public async Task<ActionResult> DeviceClose(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<DeviceConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SaveDeviceUseFlagDto saveDeviceUseFlagDto = new SaveDeviceUseFlagDto()
                {
                    ids = ids,
                    isUseFlag = 0,
                    invoker = invoker
                };
                var result = await vm.SaveDeviceUseFlag(saveDeviceUseFlagDto);
                if (result.code == ResCode.OK)
                {
                    return new JsonResult(ApiControllerHelper.SubmitOk(result.msg, result.outParams));
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error(result.msg));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }
        /// <summary>
        /// 初始化步序
        /// </summary>
        /// <returns></returns>
        [ActionDescription("初始化步序")]
        [HttpPost("DeviceStepInit")]
        //[Public]
        public async Task<ActionResult> DeviceStepInit(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<DeviceConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SaveDeviceUseFlagDto saveDeviceUseFlagDto = new SaveDeviceUseFlagDto()
                {
                    ids = ids,
                    isUseFlag = 2,
                    invoker = invoker
                };
                var result = await vm.SaveDeviceUseFlag(saveDeviceUseFlagDto);
                if (result.code == ResCode.OK)
                {
                    return new JsonResult(ApiControllerHelper.SubmitOk(result.msg, result.outParams));
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error(result.msg));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        
        /// <summary>
        /// 手动待命
        /// </summary>
        /// <returns></returns>
        [ActionDescription("手动待命")]
        [HttpPost("DeviceWait")]
        //[Public]
        public async Task<ActionResult> DeviceWait(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<DeviceConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SaveDeviceUseFlagDto saveDeviceUseFlagDto = new SaveDeviceUseFlagDto()
                {
                    ids = ids,
                    isUseFlag = 3,
                    invoker = invoker
                };
                var result = await vm.SaveDeviceUseFlag(saveDeviceUseFlagDto);
                if (result.code == ResCode.OK)
                {
                    return new JsonResult(ApiControllerHelper.SubmitOk(result.msg, result.outParams));
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error(result.msg));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }


    }
}
