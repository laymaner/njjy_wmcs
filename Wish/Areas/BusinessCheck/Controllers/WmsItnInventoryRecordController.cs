using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs;
using Com.Wish.Model.Business;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.Models.ImportDto;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using WISH.Helper.Common;
using Quartz.Util;


namespace Wish.Controllers
{
    [Area("BusinessCheck")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("盘点记录")]
    [ApiController]
    [Route("api/WmsItnInventoryRecord")]
    public partial class WmsItnInventoryRecordController : BaseApiController
    {
        #region 框架生成

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
        public IActionResult Search(WmsItnInventoryRecordSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<WmsItnInventoryRecordListVM>(passInit: true);
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
        public WmsItnInventoryRecordVM Get(string id)
        {
            var vm = Wtm.CreateVM<WmsItnInventoryRecordVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(WmsItnInventoryRecordVM vm)
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
        public IActionResult Edit(WmsItnInventoryRecordVM vm)
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
            var vm = Wtm.CreateVM<WmsItnInventoryRecordBatchVM>();
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
        public IActionResult ExportExcel(WmsItnInventoryRecordSearcher searcher)
        {
            var vm = Wtm.CreateVM<WmsItnInventoryRecordListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<WmsItnInventoryRecordListVM>();
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
            var vm = Wtm.CreateVM<WmsItnInventoryRecordImportVM>();
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
        public ActionResult Import(WmsItnInventoryRecordImportVM vm)
        {
            if (vm != null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
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
        /// 创建盘点单
        /// </summary>
        /// <returns></returns>
        [ActionDescription("创建盘点单")]
        [HttpPost("CreateInventoryTask")]
        //[Public]
        public async Task<ActionResult> CreateInventoryTask(CreateInventoryTaskDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<WmsItnInventoryRecordVM>();
                inputDto.invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.CreateInventoryTask(inputDto);
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
        /// 查询库存
        /// </summary>
        /// <returns></returns>
        [ActionDescription("查询库存")]
        [HttpPost("GetStockDtlByMaterial")]
        [Public]
        public async Task<ActionResult> GetStockDtlByMaterial(WmsItnInventoryRecordSearcher inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<WmsItnInventoryRecordVM>();
                var result =await vm.GetStockDtlByMaterial(inputDto);

                return new JsonResult(ApiControllerHelper.SubmitOk("查询成功", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }

        /// <summary>
        /// 盘点确认
        /// </summary>
        /// <returns></returns>
        [ActionDescription("盘点确认")]
        [HttpPost("ConfirmCheck")]
        //[Public]
        public async Task<ActionResult> ConfirmCheck(ConfirmInventoryTaskDto inputDto)
        {
            try
            {
                BusinessResult result = new BusinessResult();
                var vm = Wtm.CreateVM<WmsItnInventoryRecordVM>();
                if (inputDto == null)
                {
                    return new JsonResult(ApiControllerHelper.Error("入参为空"));
                }
                if (inputDto.ID == null)
                {
                    return new JsonResult(ApiControllerHelper.Error("入参ID为空"));
                }
                if (inputDto.confirmQty == null)
                {
                    return new JsonResult(ApiControllerHelper.Error("入参确认数量confirmQty为空"));
                }
                if (inputDto.isBack == null)
                {
                    return new JsonResult(ApiControllerHelper.Error("入参回库标记isBack为空"));
                }
                if (inputDto.differenceFlag == null)
                {
                    return new JsonResult(ApiControllerHelper.Error("入参差异标识differenceFlag为空"));
                }
                if (string.IsNullOrWhiteSpace(inputDto.confirmReason))
                {
                    return new JsonResult(ApiControllerHelper.Error("入参确认理由confirmReason为空"));
                }
                inputDto.confirmBy = Wtm.LoginUserInfo?.ITCode ?? "";
                if (inputDto.isBack == 1)
                {
                    result = await vm.ConfirmInventoryBack(inputDto);
                }
                else if (inputDto.isBack == 0)
                {
                    result = await vm.ConfirmInventoryOut(inputDto);
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error($"入参回库标记isBack[{inputDto.isBack}]错误"));
                }
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
