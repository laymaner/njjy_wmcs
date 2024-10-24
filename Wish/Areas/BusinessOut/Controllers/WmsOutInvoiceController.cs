using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs;
using Com.Wish.Model.Business;
using System.Threading.Tasks;
using WISH.Helper.Common;
using Wish.Helper;
using Wish.ViewModel.Common.Dtos;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.IdentityModel.Tokens.Jwt;


namespace Wish.Controllers
{
    [Area("BusinessOut")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("出库信息")]
    [ApiController]
    [Route("api/WmsOutInvoice")]
	public partial class WmsOutInvoiceController : BaseApiController
    {
        #region 框架生成

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(WmsOutInvoiceSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<WmsOutInvoiceListVM>(passInit: true);
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
        public WmsOutInvoiceVM Get(string id)
        {
            var vm = Wtm.CreateVM<WmsOutInvoiceVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(WmsOutInvoiceVM vm)
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
        public IActionResult Edit(WmsOutInvoiceVM vm)
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
            var vm = Wtm.CreateVM<WmsOutInvoiceBatchVM>();
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
        public IActionResult ExportExcel(WmsOutInvoiceSearcher searcher)
        {
            var vm = Wtm.CreateVM<WmsOutInvoiceListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<WmsOutInvoiceListVM>();
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
            var vm = Wtm.CreateVM<WmsOutInvoiceImportVM>();
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
        public ActionResult Import(WmsOutInvoiceImportVM vm)
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
        /// 创建发货单据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ActionDescription("Sys.CreateOutInvoiceOrder")]
        [HttpPost("CreateOutInvoiceOrder")]
        [Public]
        public async Task<IActionResult> CreateOutInvoiceOrder(CreateWmsOutInvoiceDto input)
        {
            BusinessResult result = new BusinessResult();
            var vm = Wtm.CreateVM<WmsOutInvoiceVM>();
            if (input == null)
            {
                result.code = ResCode.Error;
                result.msg = $"创建发货单【发货单】入参：input{input}为空";
            }

            string invoker = Wtm.LoginUserInfo?.ITCode ?? "WMS";
            result = await vm.CreateOutInvoiceOrderByDocType(input, invoker);
            if (result.code == ResCode.OK)
            {
                return new JsonResult(ApiControllerHelper.AddOk());
            }
            else
            {
                return new JsonResult(ApiControllerHelper.AddError(result.msg));
            }
        }
        /// <summary>
        /// 新增类型为库存选取时查询库存明细
        /// </summary>
        /// <param name="searcher"></param>
        /// <returns></returns>
        [ActionDescription("发货单管理-新增类型为库存选取时查询库存明细")]
        [HttpPost("GetStockDtlByMaterial")]
        public IActionResult GetStockDtlByMaterial(WmsOutInvoiceSearcher searcher)
        {
            var vm = Wtm.CreateVM<WmsOutInvoiceVM>();
            var res = vm.GetStockDtlByMaterial(searcher);
            return new JsonResult(ApiControllerHelper.SearchOk(res));
        }


    }
}
