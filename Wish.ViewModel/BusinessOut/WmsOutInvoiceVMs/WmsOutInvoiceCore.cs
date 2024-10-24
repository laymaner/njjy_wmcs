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
using Wish.Areas.BasWhouse.Model;
using WISH.Helper.Common;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.Common;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BasWhouse.BasWWhouseVMs;

namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceVM 
    {
        #region 发货单创建

        /// <summary>
        /// 内部创建发货单据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateOutInvoiceOrderByDocType(CreateWmsOutInvoiceDto input, string invoker)
        {
            CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();

            AddWmsOutInvoiceDto wmsOutInvoice = CommonHelper.Map<CreateWmsOutInvoiceDto, AddWmsOutInvoiceDto>(input);

            List<AddWmsOutInvoiceDtlDto> outInvoiceDtls = new List<AddWmsOutInvoiceDtlDto>();
            bool? flag = await cfgDocTypeVM.GetIsCheckStockByDocTypeCodeAsync(input.docTypeCode);
            if (flag == true)
            {
                // 库存明细 WmsStockDtl
                var stockDtlList = JsonConvert.DeserializeObject<List<AddStockDtlDto>>(input.stockDtl.ToString());
                foreach (var stock in stockDtlList)
                {
                    AddWmsOutInvoiceDtlDto wmsOutInvoiceDtl = new AddWmsOutInvoiceDtlDto();
                    wmsOutInvoiceDtl.erpWhouseNo = stock.erpWhouseNo;
                    wmsOutInvoiceDtl.materialCode = stock.materialCode;
                    wmsOutInvoiceDtl.materialName = stock.materialName;
                    wmsOutInvoiceDtl.materialSpec = stock.materialSpec;
                    //wmsOutInvoiceDtl.batchNo = stock.batchNo;
                    wmsOutInvoiceDtl.invoiceQty = stock.invoiceQty;
                    wmsOutInvoiceDtl.erpUndeliverQty = stock.invoiceQty;
                    wmsOutInvoiceDtl.allotQty = 0;
                    wmsOutInvoiceDtl.putdownQty = 0;
                    wmsOutInvoiceDtl.completeQty = 0;
                    wmsOutInvoiceDtl.invoiceDtlStatus = 0;
                    wmsOutInvoiceDtl.CreateBy = invoker;
                    wmsOutInvoiceDtl.CreateTime = DateTime.Now;
                    outInvoiceDtls.Add(wmsOutInvoiceDtl);
                }
            }
            else
            {
                // 物料明细 BasBMaterial
                var materialList = JsonConvert.DeserializeObject<List<AddMaterialDto>>(input.stockDtl.ToString());
                foreach (var material in materialList)
                {
                    AddWmsOutInvoiceDtlDto wmsOutInvoiceDtl = new AddWmsOutInvoiceDtlDto();
                    wmsOutInvoiceDtl.erpWhouseNo = material.erpWhouseNo;
                    wmsOutInvoiceDtl.materialCode = material.MaterialCode;
                    wmsOutInvoiceDtl.materialName = material.MaterialName;
                    wmsOutInvoiceDtl.materialSpec = material.MaterialSpec;
                    wmsOutInvoiceDtl.batchNo = material.batchNo;
                    wmsOutInvoiceDtl.invoiceQty = material.invoiceQty;
                    wmsOutInvoiceDtl.erpUndeliverQty = material.invoiceQty;
                    wmsOutInvoiceDtl.allotQty = 0;
                    wmsOutInvoiceDtl.putdownQty = 0;
                    wmsOutInvoiceDtl.completeQty = 0;
                    wmsOutInvoiceDtl.invoiceDtlStatus = 0;
                    wmsOutInvoiceDtl.CreateBy = invoker;
                    wmsOutInvoiceDtl.CreateTime = DateTime.Now;
                    outInvoiceDtls.Add(wmsOutInvoiceDtl);
                }
            }

            wmsOutInvoice.dtlList = outInvoiceDtls;
            BusinessResult result = await CreateOutInvoiceOrder(wmsOutInvoice, invoker);
            return result;
        }


        /// <summary>
        /// 创建发货单据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateOutInvoiceOrder(AddWmsOutInvoiceDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();

            var inParam = JsonConvert.SerializeObject(input);
            var beginTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

            CommonValidModel valid = await ValidOutInvoice(input);
            if (valid.status == 0)
            {
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                BasWWhouseVM basWWhouseVM = Wtm.CreateVM<BasWWhouseVM>();

                var hasParentTransaction = false;
                try
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        hasParentTransaction = true;
                    }

                    if (!hasParentTransaction)
                    {
                        await DC.Database.BeginTransactionAsync();
                    }

                    // 发货单
                    string invoiceNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InvoiceOrderNo.GetCode());
                    var invoiceEntity = CommonHelper.Map<AddWmsOutInvoiceDto, WmsOutInvoice>(input);
                    BasWWhouse whouse = await basWWhouseVM.GetWhouseInfo();
                    if (whouse != null)
                    {
                        invoiceEntity.whouseNo = whouse.whouseNo;
                    }
                    else
                    {
                        invoiceEntity.whouseNo = "TZ";
                    }

                    invoiceEntity.proprietorCode = "TZ";
                    invoiceEntity.sourceBy = input.sourceBy == null ? 0 : input.sourceBy;
                    invoiceEntity.invoiceStatus = 0;
                    invoiceEntity.invoiceNo = invoiceNo;
                    invoiceEntity.CreateBy = invoker;

                    invoiceEntity.CreateTime = DateTime.Now;

                    // 检验单据类型是否存在，没有则创建该单据类型
                    CfgDocTypeDto cfgDocType_View = await cfgDocTypeVM.GetCfgDocTypeAsync(invoiceEntity.docTypeCode);
                    if (cfgDocType_View == null)
                    {
                        await cfgDocTypeVM.CreateInOrderCfgDocTypeAsync(invoiceEntity.docTypeCode, invoker);
                    }

                    //DC.AddEntity(invoiceEntity);
                    await ((DbContext)DC).BulkInsertAsync(new WmsOutInvoice[] { invoiceEntity });
                    // 发货单明细
                    foreach (var item in input.dtlList)
                    {
                        WmsOutInvoiceDtl dtl = CommonHelper.Map<AddWmsOutInvoiceDtlDto, WmsOutInvoiceDtl>(item);
                        if (invoiceEntity.docTypeCode != BusinessCode.OutProduceOrder.GetCode())
                        {
                            item.erpUndeliverQty = item.invoiceQty;
                        }

                        item.whouseNo = invoiceEntity.whouseNo;
                        item.proprietorCode = invoiceEntity.proprietorCode;
                        //item.externalOutNo = invoiceEntity.externalOutNo;
                        item.invoiceNo = invoiceNo;
                        // 如果 ERP未发数量 <= 0, 那么单据状态就是出库完成状态 add by Allen 2023-11-08
                        //item.invoiceDtlStatus = item.erpUndeliverQty <= 0 ? 90 : 0;
                        item.invoiceDtlStatus = 0;
                        item.CreateBy = invoker;

                        item.CreateTime = DateTime.Now;
                        // DC.AddEntity(item);
                    }

                    await ((DbContext)DC).BulkInsertAsync(input.dtlList);
                    await ((DbContext)DC).SaveChangesAsync();
                    //DC.Set<WmsOutInvoiceDtl>().AddRange(input.dtlList);
                    //DC.SaveChanges();
                    if (!hasParentTransaction)
                    {
                        await DC.Database.CommitTransactionAsync();
                    }
                }
                catch (Exception e)
                {
                    if (!hasParentTransaction)
                    {
                        await DC.Database.RollbackTransactionAsync();
                    }

                    throw new Exception(e.Message);
                }
            }
            else
            {
                result.code = ResCode.Error;
                result.msg = valid.msg;
            }

            var endTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string outResult = "";
            if (result.outParams != null)
            {
                outResult = JsonConvert.SerializeObject(result.outParams);
            }
            logger.Warn($"----->info----->创建发货单据操作:, 开始时间:{beginTimeStr}, 结束时间:{endTimeStr}, 入参:{inParam}, 出参:{outResult}");

            return result;
        }

        /// <summary>
        /// 发货单据参数校验
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<CommonValidModel> ValidOutInvoice(AddWmsOutInvoiceDto input)
        {
            CommonValidModel result = new CommonValidModel();

            BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();


            var addInvoiceEntity = CommonHelper.Map<AddWmsOutInvoiceDto, WmsOutInvoice>(input);
            foreach (var item in input.dtlList)
            {
                WmsOutInvoiceDtl dtl = CommonHelper.Map<AddWmsOutInvoiceDtlDto, WmsOutInvoiceDtl>(item);
                var flag = await basBMaterialVM.IsMaterialExistAsync(dtl.materialCode);
                if (flag == false)
                {
                    return result.vaildError($"物料信息不存在");
                }

                BasBMaterialDto basBMaterialView = await basBMaterialVM.GetBasBMaterialAsync(dtl.materialCode);
                if (basBMaterialView.basBMaterialCategory == null)
                {
                    return result.vaildError($"物料【{dtl.materialName}】的物料大类不存在");
                }

                // 工单发料不校验SN号
                // modified by Allen 借出借用单 / 发货单 也不校验成品的SN号, 发货单OutInvoice
                if (!BusinessCode.OutProduceOrder.GetCode().Equals(input.docTypeCode) &&
                    !input.docTypeCode.Equals(BusinessCode.OutLoanSlip.GetCode()) &&
                    !input.docTypeCode.Equals(BusinessCode.OutInvoice.GetCode()))
                {
                    if (basBMaterialView.basBMaterialCategory.materialFlag == MaterialFlag.Product.GetCode())
                    {
                        if (string.IsNullOrWhiteSpace(dtl.productSn))
                        {
                            return result.vaildError($"物料【{dtl.materialName}】为成品，SN号不能为空");
                        }
                    }
                }
            }

            return result.vaildOk();
        }

        #endregion
    }
}
