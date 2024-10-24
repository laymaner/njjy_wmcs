using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.Areas.BasWhouse.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.ViewModel.Common.Dtos;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;


namespace Wish.Controllers
{
    [Area("BasWhouse")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("货位信息")]
    [ApiController]
    [Route("api/BasWBin")]
	public partial class BasWBinController : BaseApiController
    {
        #region 框架自带

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(BasWBinSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<BasWBinListVM>(passInit: true);
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
        public BasWBinVM Get(string id)
        {
            var vm = Wtm.CreateVM<BasWBinVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(BasWBinVM vm)
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
        public IActionResult Edit(BasWBinVM vm)
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
            var vm = Wtm.CreateVM<BasWBinBatchVM>();
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
        public IActionResult ExportExcel(BasWBinSearcher searcher)
        {
            var vm = Wtm.CreateVM<BasWBinListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<BasWBinListVM>();
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
            var vm = Wtm.CreateVM<BasWBinImportVM>();
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
        public ActionResult Import(BasWBinImportVM vm)
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
        /// 库位异常处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("库位异常处理")]
        [HttpPost("BinException")]
        [Public]
        public async Task<ActionResult> BinException(BinExceptionDto input)
        {
            try
            {
                var vm = Wtm.CreateVM<BasWBinVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                var result = await vm.BinException(input, invoker);
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
        /// 启用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("启用")]
        [HttpPost("OpenBin")]
        //[Public]
        public async Task<ActionResult> OpenBin(OpenOrCloseBinDto binNo)
        {
            try
            {
                var vm = Wtm.CreateVM<BasWBinVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                OpenOrCloseBinDto input = new OpenOrCloseBinDto()
                {
                    binNo = binNo.binNo,
                    operationType=10,
                    invoker=invoker
                };
                var result = await vm.OpenOrCloseBin(input);
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
        /// 禁用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("禁用")]
        [HttpPost("CloseBin")]
        //[Public]
        public async Task<ActionResult> CloseBin(OpenOrCloseBinDto binNo)
        {
            try
            {
                var vm = Wtm.CreateVM<BasWBinVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
                OpenOrCloseBinDto input = new OpenOrCloseBinDto()
                {
                    binNo = binNo.binNo,
                    operationType=11,
                    invoker=invoker
                };
                var result = await vm.OpenOrCloseBin(input);
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
        /// 库位统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("库位统计")]
        [HttpPost("GetBinStatisticsAsync")]
        [Public]
        public async Task<ActionResult> GetBinStatisticsAsync()
        {
            try
            {
                var vm = Wtm.CreateVM<BasWBinVM>();
                var result = await vm.GetBinStatisticsAsync();
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
        /// 巷道统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("巷道统计")]
        [HttpPost("GetRoadwayStatisticsAsync")]
        [Public]
        public async Task<ActionResult> GetRoadwayStatisticsAsync()
        {
            try
            {
                var vm = Wtm.CreateVM<BasWBinVM>();
                var result = await vm.GetRoadwayStatisticsAsync();
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
