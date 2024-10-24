using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using WISH.Helper.Common;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordVMs
{
    public partial class WmsInReceiptIqcRecordVM
    {
        /// <summary>
        /// 质检录入
        /// </summary>
        /// <param name="wmsItnQcInParamView"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DoInReceiptIqc(DoReceiptIqcRecordDto wmsItnQcInParamView, string invoker)
        {
            BusinessResult result = new BusinessResult();
            WmsInReceiptVM wmsInReceiptVM = Wtm.CreateVM<WmsInReceiptVM>();
            BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
            WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
            WmsInReceiptIqcResultVM wmsInReceiptIqcResultVM = Wtm.CreateVM<WmsInReceiptIqcResultVM>();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BusinessResult itfResult = null;
            string inParam = "";
            string outParam = "";
            string success = "";
            string _vpoint = "";
            var hasParentTranslation = false;
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTranslation = true;
                }

                #region 1  校验

                _vpoint = "校验收货单据状态是否正确：初始创建";


                if (hasParentTranslation == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }

                // 1-1 校验收货单据状态是否正确：初始创建
                WmsInReceiptDtl wmsInReceiptDt = await wmsInReceiptVM.GetWmsInReceiptDtAsync(wmsItnQcInParamView.inReceiptDtlId);

                if (wmsInReceiptDt == null)
                {
                    throw new Exception($"收货单明细数据 {wmsItnQcInParamView.inReceiptDtlId}不存在!");
                }
                WmsInReceipt wmsInReceipt = await wmsInReceiptVM.GetWmsInReceiptAsync(wmsInReceiptDt.receiptNo);
                // 单据类型是 委外入库 或 采购入库
                var poVsOemDocType =
                    (wmsInReceipt.docTypeCode == InOrderDocType.PO.GetCode())
                    || (wmsInReceipt.docTypeCode == InOrderDocType.OEM.GetCode());
                if (wmsInReceipt == null)
                {
                    throw new Exception($"收货单明细数据 {wmsItnQcInParamView.inReceiptDtlId} 对应的收货单不存在!");
                }

                if (wmsInReceipt.receiptStatus != Convert.ToInt32(ReceiptOrDtlStatus.Init.GetCode())
                    && wmsInReceipt.receiptStatus != Convert.ToInt32(ReceiptOrDtlStatus.QCRecord.GetCode())
                    && wmsInReceipt.receiptStatus != Convert.ToInt32(ReceiptOrDtlStatus.IQCing.GetCode())
                     && wmsInReceipt.receiptStatus != Convert.ToInt32(ReceiptOrDtlStatus.PutAwaying.GetCode()))
                {
                    throw new Exception($"收货单 {wmsInReceipt.receiptNo} 不是初始状态或质检录入状态!");
                }

                if (wmsInReceiptDt.receiptDtlStatus != Int16.Parse(ReceiptOrDtlStatus.Init.GetCode()))
                {
                    throw new Exception($"收货单明细 {wmsInReceiptDt.ID} 不是初始状态状态!");
                }

                _vpoint = "校验不合格数量 > 0，不良选项、详细说明、不良处理方式 参数不能为空";
                // 1-2 不合格数量>0，不良选项、详细说明、不良处理方式 参数不能为空
                if (wmsItnQcInParamView.noPassQty > 0)
                {
                    if (string.IsNullOrWhiteSpace(wmsItnQcInParamView.noPassItem))
                    {
                        throw new Exception($"不合格数量>0，不良选项不能为空!");
                    }

                    if (string.IsNullOrWhiteSpace(wmsItnQcInParamView.detailDescription))
                    {
                        throw new Exception($"不合格数量>0，详细说明不能为空!");
                    }

                    if (string.IsNullOrWhiteSpace(wmsItnQcInParamView.badOptions))
                    {
                        throw new Exception($"不合格数量>0，不良处理方式不能为空!");
                    }
                }

                _vpoint = "校验电子子料是否存在唯一码未录入DC、MSL、供应商暴露时长等信息";
                // 1-3 电子料是否存在唯一码未录入DC、MSL、供应商暴露时长等信息
                BasBMaterialDto bBasBMaterialView = await basBMaterialVM.GetBasBMaterialAsync(wmsInReceiptDt.materialCode);
                var isElectronicMaterial = bBasBMaterialView.basBMaterialCategory.materialFlag == MaterialFlag.Electronic.GetCode();
                // 电子料和采购入库 委外入库 单据质检才会做此校验，其他类型入库 自动收货，自动质检 不做此校验，信息录入提示
                if (isElectronicMaterial && poVsOemDocType)
                {
                    // 查找唯一码信息
                    var WmsInReceiptUniicode = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeReceiptDtlIdAsync(wmsInReceiptDt.ID);
                    if (WmsInReceiptUniicode == null)
                    {
                        throw new Exception($"收货明细对应的唯一码不存在!");
                    }

                    if (wmsInReceiptUniicodeVM.CheckElectronicMaterialBaseInfo(WmsInReceiptUniicode) == false)
                    {
                        throw new Exception($"该收货明细唯一码未维护电子料信息，请联系仓库人员维护!");
                    }
                }

                #endregion

                #region 2  逻辑

                _vpoint = "生成质检记录信息";
                var inReceiptIqcRecord = await DC.Set<WmsInReceiptIqcRecord>().Where(x => x.receiptDtlId == wmsInReceiptDt.ID).FirstOrDefaultAsync();
                if (inReceiptIqcRecord != null)
                {
                    throw new Exception($"该收货明细已生成质检记录!");
                }
                // 2-1 生成质检记录信息
                var InReceiptQCNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InReceiptQCNo.GetCode());
                WmsInReceiptIqcRecord wmsInReceiptIqcRecord =
                    BuildInReceiptIqcRecord(wmsInReceipt, wmsInReceiptDt, InReceiptQCNo, wmsItnQcInParamView, invoker);
                //DC.AddEntity(wmsInReceiptIqcRecord);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(new WmsInReceiptIqcRecord[] { wmsInReceiptIqcRecord });
                await ((DbContext)DC).SaveChangesAsync();
                _vpoint = "更新收货单、收货单明细状态为质检录入状态";
                // 2-1-1 更新收货单、收货单明细状态为质检录入状态
                wmsInReceipt.receiptStatus = Convert.ToInt32(ReceiptOrDtlStatus.QCRecord.GetCode());
                wmsInReceipt.UpdateBy = invoker;
                wmsInReceipt.UpdateTime = DateTime.Now;

                wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.QCRecord.GetCode());
                wmsInReceiptDt.UpdateBy = invoker;
                wmsInReceiptDt.UpdateTime = DateTime.Now;

                DC.UpdateEntity(wmsInReceipt);
                DC.UpdateEntity(wmsInReceiptDt);
                DC.SaveChanges();
                await ((DbContext)DC).BulkUpdateAsync(new WmsInReceipt[] { wmsInReceipt });
                await ((DbContext)DC).BulkUpdateAsync(new WmsInReceiptDtl[] { wmsInReceiptDt });
                await ((DbContext)DC).SaveChangesAsync();
                _vpoint = "如果有合格数量，触发 质检确认";
                // 2-2 如果有合格数量，触发 质检确认
                /*
                 * 1、页面上质检，除委外入库 或 采购入库 单据类型外，需调用质检确认，页面传参IQCYOrN，默认：Y，满足 poVsOemDocType
                 * 2、委外入库 或 采购入库 请求EBS质检，如免检即IQCYOrN为N，需调用质检确认  ，满足IQCYOrN为N （√）
                 * 3、
                 * 3.1 委外入库 或 采购入库 请求EBS质检，不免检，EBS外部质检（默认：Y）不需要调用质检确认（X） 
                 * 3.2 或页面质检（默认：Y），不需要调用质检确认，（X）
                 */
                if (wmsItnQcInParamView.passQty > 0 && (poVsOemDocType == false || wmsItnQcInParamView.IQCYOrN.Equals("N")))
                {
                    DoInReceiptIqcResultDto doItnQcConfirmInParamView = new DoInReceiptIqcResultDto();
                    doItnQcConfirmInParamView.inReceiptDtID = wmsInReceiptIqcRecord.receiptDtlId;
                    doItnQcConfirmInParamView.qty = wmsItnQcInParamView.passQty;
                    doItnQcConfirmInParamView.qcFlag = QcFlag.Pass.GetCode();
                    doItnQcConfirmInParamView.qcResult = InspectionResult.Qualitified.GetCode();
                    doItnQcConfirmInParamView.ngReason = "";
                    doItnQcConfirmInParamView.IQCYOrN = wmsItnQcInParamView.IQCYOrN;
                    doItnQcConfirmInParamView.IQCParam = wmsItnQcInParamView;
                    result = await wmsInReceiptIqcResultVM.DoIDoItnQcConfirm(doItnQcConfirmInParamView, invoker);
                    if (result.code == ResCode.Error)
                    {
                        throw new Exception(result.msg);
                    }
                }


                #endregion



                #region 操作日志

                string inparams = JsonConvert.SerializeObject(wmsItnQcInParamView);
                string outparams = JsonConvert.SerializeObject(result);
                //var logRes = logOperationVm.addLogOperation("入库", $"质检录入单号{InReceiptQCNo}", inparams, invoker, "质检录入", "", outparams,
                //(!wmsItnQcInParamView.noPassItem.IsNullOrWhiteSpace() && wmsItnQcInParamView.noPassItem.Contains("EBS")) ? "ERP" : "WMS");
                //if (logRes.code == ResultCode.Error)
                //{
                //    throw new Exception(_vpoint + logRes.msg);
                //}

                #endregion

                if (hasParentTranslation == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        await DC.Database.CommitTransactionAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                if (hasParentTranslation == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        await DC.Database.RollbackTransactionAsync();
                    }
                }

                result.code = ResCode.Error;
                result.msg = $"WmsItnQcVM->DoInReceiptIqc 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }
            finally
            {


                #region 操作日志

                string inparams = JsonConvert.SerializeObject(wmsItnQcInParamView);
                string outparams = JsonConvert.SerializeObject(result);
                //var logRes = logOperationVm.addLogOperation("入库", $"质检录入", inparams, invoker, "质检录入", "", outparams,
                //    (!wmsItnQcInParamView.noPassItem.IsNullOrWhiteSpace() && wmsItnQcInParamView.noPassItem.Contains("EBS")) ? "ERP" : "WMS");
                //if (logRes.code == ResultCode.Error)
                //{
                //    throw new Exception(_vpoint + logRes.msg);
                //}
                logger.Warn($"--->质检录入，操作人：{invoker}-->{_vpoint}：入参{inparams},出参{outparams}");
                #endregion
            }

            return result;
        }

    }
}
