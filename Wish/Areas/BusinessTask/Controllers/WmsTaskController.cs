using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using Com.Wish.Model.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;

namespace Wish.Controllers
{
    [Area("BusinessTask")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("任务信息")]
    [ApiController]
    [Route("api/WmsTask")]
	public partial class WmsTaskController : BaseApiController
    {
        #region 框架生成

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(WmsTaskSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<WmsTaskListVM>(passInit: true);
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
        public WmsTaskVM Get(string id)
        {
            var vm = Wtm.CreateVM<WmsTaskVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(WmsTaskVM vm)
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
        public IActionResult Edit(WmsTaskVM vm)
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
            var vm = Wtm.CreateVM<WmsTaskBatchVM>();
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
        public IActionResult ExportExcel(WmsTaskSearcher searcher)
        {
            var vm = Wtm.CreateVM<WmsTaskListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<WmsTaskListVM>();
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
            var vm = Wtm.CreateVM<WmsTaskImportVM>();
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
        public ActionResult Import(WmsTaskImportVM vm)
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
        /// 任务重发
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("任务重发")]
        [HttpPost("TaskReload")]
        //[Public]
        public async Task<ActionResult> TaskReload(taskOperationDto input)
        {
            try
            {
                var vm = Wtm.CreateVM<WmsTaskVM>();
                input.invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                var result = await vm.TaskReload(input);
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
        /// 任务手动完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("任务手动完成")]
        [HttpPost("TaskFinish")]
        //[Public]
        public async Task<ActionResult> TaskFinish(taskOperationDto input)
        {
            try
            {
                var vm = Wtm.CreateVM<WmsTaskVM>();
                input.invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                var result = await vm.TaskFinish(input);
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
        /// 任务手动关闭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("任务手动关闭")]
        [HttpPost("TaskClose")]
        //[Public]
        public async Task<ActionResult> TaskClose(taskOperationDto input)
        {
            try
            {
                var vm = Wtm.CreateVM<WmsTaskVM>();
                input.invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                var result = await vm.TaskClose(input);
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
