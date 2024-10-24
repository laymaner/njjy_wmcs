using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using Wish.TaskConfig.Model;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Threading.Tasks;
using Wish.Helper;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.Models.ImportDto;
using WISH.Helper.Common;
using Wish.Model;
using Wish.ViewModel.Common;
using NPOI.SS.Formula.Functions;
using Wish.ViewModel.Interface.InterfaceSendBackVMs;


namespace Wish.Controllers
{
    [Area("WcsCmd")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("堆垛机指令")]
    [ApiController]
    [Route("api/SrmCmd")]
	public partial class SrmCmdController : BaseApiController
    {
        #region 框架生成

        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(SrmCmdSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<SrmCmdListVM>(passInit: true);
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
        public SrmCmdVM Get(string id)
        {
            var vm = Wtm.CreateVM<SrmCmdVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(SrmCmdVM vm)
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
        public IActionResult Edit(SrmCmdVM vm)
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
            var vm = Wtm.CreateVM<SrmCmdBatchVM>();
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
        public IActionResult ExportExcel(SrmCmdSearcher searcher)
        {
            var vm = Wtm.CreateVM<SrmCmdListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<SrmCmdListVM>();
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
            var vm = Wtm.CreateVM<SrmCmdImportVM>();
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
        public ActionResult Import(SrmCmdImportVM vm)
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

        [ActionDescription("指令测试")]
        [HttpPost("CmdTest")]
        [Public]
        public async Task<ActionResult> CmdTest(CreateSrmCmdDto input)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                var result = await vm.SRM_TestCmd(input);
                if (result.Equals("ok"))
                {
                    return new JsonResult(ApiControllerHelper.SubmitOk("ok", null));
                }
                else
                {
                    return new JsonResult(ApiControllerHelper.Error(""));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ApiControllerHelper.SearchError(ex.Message));
            }
        }


        /// <summary>
        /// 查询当前指令
        /// </summary>
        /// <returns></returns>
        [ActionDescription("查询当前指令")]
        [HttpPost("GetCurrentTask")]
        [Public]
        public async Task<ActionResult> GetCurrentTask()
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                var result = await vm.GetCurrentTask();
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
        /// 查询历史指令
        /// </summary>
        /// <returns></returns>
        [ActionDescription("查询历史指令")]
        [HttpPost("GetHistoryTask")]
        [Public]
        public async Task<ActionResult> GetHistoryTask(string timePeriod)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                var result = await vm.GetHistoryTask(timePeriod);
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
        /// 查询出库任务
        /// </summary>
        /// <returns></returns>
        [ActionDescription("查询出库任务")]
        [HttpPost("GetOutTask")]
        [Public]
        public async Task<ActionResult> GetOutTask()
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                var result = await vm.GetOutTask();
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
        /// 处理指令发送
        /// </summary>
        /// <returns></returns>
        [ActionDescription("处理指令发送")]
        [HttpPost("DealCmdTaskSend")]
        //[Public]
        public async Task<ActionResult> DealCmdTaskSend(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealCmdSendDto input = new DealCmdSendDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealCmdSendAsync(input, invoker);
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
        /// 取货完成
        /// </summary>
        /// <returns></returns>
        [ActionDescription("取货完成")]
        [HttpPost("DealSrmTaskPick")]
        //[Public]
        public async Task<ActionResult> DealSrmTaskPick(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 19,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealSrmTaskAsync(input, invoker);
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
        /// 放货完成
        /// </summary>
        /// <returns></returns>
        [ActionDescription("放货完成")]
        [HttpPost("DealSrmTaskPut")]
        //[Public]
        public async Task<ActionResult> DealSrmTaskPut(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 20,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealSrmTaskAsync(input, invoker);
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
        /// 任务拒绝
        /// </summary>
        /// <returns></returns>
        [ActionDescription("任务拒绝")]
        [HttpPost("DealSrmTaskRefuse")]
        //[Public]
        public async Task<ActionResult> DealSrmTaskRefuse(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 0,//待定
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealSrmTaskAsync(input, invoker);
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
        /// 入库申请
        /// </summary>
        /// <returns></returns>
        [ActionDescription("入库申请")]
        [HttpPost("DealSrmTaskIn")]
        //[Public]
        public async Task<ActionResult> DealSrmTaskIn(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 5,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealSrmTaskAsync(input, invoker);
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
        /// 出库申请
        /// </summary>
        /// <returns></returns>
        [ActionDescription("出库申请")]
        [HttpPost("DealSrmTaskOut")]
        //[Public]
        public async Task<ActionResult> DealSrmTaskOut(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 7,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.DealSrmTaskAsync(input, invoker);
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
        /// 创建出库任务
        /// </summary>
        /// <returns></returns>
        [ActionDescription("创建出库任务")]
        [HttpPost("CreateSrmTaskOut")]
        //[Public]
        public async Task<ActionResult> CreateSrmTaskOut(CreateSrmOutDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<InterfaceSendBackVM>();
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.CreateOutRequest(inputDto.invoiceNo, invoker);
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
        /// 指令重发
        /// </summary>
        /// <returns></returns>
        [ActionDescription("指令重发")]
        [HttpPost("CmdResend")]
        //[Public]
        public async Task<ActionResult> CmdResend(InterSrmCmdDto inputDto)
        {
            try
            {
                var vm = Wtm.CreateVM<SrmCmdVM>();
                DealSrmTaskDto input = new DealSrmTaskDto()
                {
                    deviceNo = inputDto.deviceNo,
                    taskNo = null,
                    palletBarcode = inputDto.palletBarcode,
                    actionType = 0,
                    checkPoint = null,
                };
                string invoker = Wtm.LoginUserInfo?.ITCode ?? "";
                var result = await vm.CmdResendAsync(input, invoker);
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
