using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.DevConfig.PlcConfigVMs;
using Wish.HWConfig.Models;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.DevConfig.DeviceConfigVMs;


namespace Wish.Controllers
{
    [Area("DeviceConfig")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("PLC")]
    [ApiController]
    [Route("api/PlcConfig")]
	public partial class PlcConfigController : BaseApiController
    {
        #region MyRegion

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(PlcConfigSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<PlcConfigListVM>(passInit: true);
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
        public PlcConfigVM Get(string id)
        {
            var vm = Wtm.CreateVM<PlcConfigVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(PlcConfigVM vm)
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
        public IActionResult Edit(PlcConfigVM vm)
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
            var vm = Wtm.CreateVM<PlcConfigBatchVM>();
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
        public IActionResult ExportExcel(PlcConfigSearcher searcher)
        {
            var vm = Wtm.CreateVM<PlcConfigListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<PlcConfigListVM>();
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
            var vm = Wtm.CreateVM<PlcConfigImportVM>();
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
        public ActionResult Import(PlcConfigImportVM vm)
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

        #endregion

        /// <summary>
        /// 开启PLC
        /// </summary>
        /// <returns></returns>
        [ActionDescription("开启PLC")]
        [HttpPost("PlcOpen")]
        [Public]
        public async Task<ActionResult> PlcOpen(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<PlcConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SavePlcUseFlagDto savePlcUseFlagDto = new SavePlcUseFlagDto()
                {
                    ids = ids,
                    isUseFlag = 1,
                    invoker = invoker
                };
                var result = await vm.SavePlcUseFlag(savePlcUseFlagDto);
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
        /// 关闭PLC
        /// </summary>
        /// <returns></returns>
        [ActionDescription("关闭PLC")]
        [HttpPost("PlcClose")]
        [Public]
        public async Task<ActionResult> PlcClose(List<long> ids)
        {
            try
            {
                var vm = Wtm.CreateVM<PlcConfigVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                SavePlcUseFlagDto savePlcUseFlagDto = new SavePlcUseFlagDto()
                {
                    ids = ids,
                    isUseFlag = 0,
                    invoker = invoker
                };
                var result = await vm.SavePlcUseFlag(savePlcUseFlagDto);
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
