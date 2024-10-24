using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using WISH.Helper.Common;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.Base.BasBSupplierVMs;
using Wish.ViewModel.System.SysSequenceVMs;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs
{
    public partial class WmsInReceiptUniicodeVM
    {
        /// <summary>
        /// 生成入库唯一码信息
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        public async Task<BusinessResult> GenerateUniicode(WmsInReceiptUniicodeGenerateDto inputParams, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string _vpoint = "";
            try
            {
                BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
                BasBSupplierVM basBSupplierVM = Wtm.CreateVM<BasBSupplierVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();

                // 订单明细对应的物料还未生成唯一码的数量
                WmsInOrderDtl wmsInOrderDetail = null;
                #region 1  校验
                #region 11 检验入库单信息
                _vpoint = "检验入库单信息";
                if (inputParams.inDtlID != null)
                {
                    wmsInOrderDetail = await DC.Set<WmsInOrderDtl>().Where(x => x.ID == inputParams.inDtlID).FirstOrDefaultAsync();
                    if (wmsInOrderDetail == null)
                    {
                        //result.code = ResultCode.Error;
                        //result.msg = "未找到相关的入库单信息";
                        //return result;
                        throw new Exception($"未找到相关的入库单信息");
                    }
                    else
                    {
                        // 11-1 检验入库单物料信息与传入信息是否一致
                        _vpoint = "检验入库单物料信息与传入信息是否一致";
                        if (wmsInOrderDetail.materialCode != inputParams.materialCode)
                        {
                            throw new Exception($"入库单物料信息与传入物料信息不一致!,入库单物料信息: {wmsInOrderDetail.materialCode}, 传入物料信息: {inputParams.materialCode}");
                        }

                        // 11-2 检验入库单的单据数量 减去 已生成唯一码数量 大于 本次请求生成唯一码的数量
                        _vpoint = "统计入库单明细对应的入库唯一码数量";
                        // 统计入库单明细对应的入库唯一码数量
                        var wmsInReceiptUniicodes = await DC.Set<WmsInReceiptUniicode>().Where(x => x.inDtlId == inputParams.inDtlID).ToListAsync();
                        var query = from w in wmsInReceiptUniicodes
                                    group w by w.inDtlId into g
                                    select new
                                    {
                                        totalUniicodeQty = g.Sum(w => w.qty)
                                    };
                        var totalUniicodeQty = query.FirstOrDefault() == null ? 0 : query.FirstOrDefault().totalUniicodeQty;
                        if ((wmsInOrderDetail.inQty - totalUniicodeQty) < inputParams.qty)
                        {
                            throw new Exception($"入库单的单据数量 减去 已生成唯一码数量 小于 本次请求生成唯一码的数量");
                        }
                    }
                }

                #endregion

                #region 12 检验物料信息
                _vpoint = "检验物料信息是否存在";
                var material = await basBMaterialVM.GetBasBMaterialAsync(inputParams.materialCode);
                if (material.basBMaterial == null)
                {
                    throw new Exception($"物料信息 {inputParams.materialCode} 不存在!");
                }
                #endregion

                #region 13 检验供应商信息
                _vpoint = "检验供应商信息是否存在";
                var suplier = await basBSupplierVM.GetSupplierAsync(inputParams.supplierCode);
                if (suplier == null)
                {
                    //throw new Exception($"检验供应商信息 {inputParams.supplierCode} 不存在!");
                    //TODO：根据单据类型判断
                }
                #endregion

                #endregion

                #region 2  业务逻辑
                #region 21 生成包装条码信息
                _vpoint = "生成包装条码信息";
                decimal remainQty = inputParams.qty;
                inputParams.packageQty = inputParams.packageQty == 0 ? remainQty : inputParams.packageQty;
                if (inputParams.packageQty < 0)
                {
                    throw new Exception($"参数包装数量小于0!");
                }
                List<WmsInReceiptUniicode> uniicodes = new List<WmsInReceiptUniicode>();
                string batchNo = string.Empty;
                if (string.IsNullOrEmpty(inputParams.batchNo))
                {

                    //batchNo = sysSequenceVM.GetSequence(DictonaryHelper.SequenceCode.SrmBatchNo.GetCode());
                    batchNo = "";
                }
                else
                {
                    batchNo = inputParams.batchNo;
                }

                while (remainQty > 0)
                {
                    string packageSeqNo = await sysSequenceVM.GetSequenceAsync(DictonaryHelper.SequenceCode.PackageNo.GetCode());
                    decimal generateQty = 0;
                    if (remainQty < inputParams.packageQty)
                    {
                        generateQty = remainQty;
                        remainQty = 0;
                    }
                    else
                    {
                        generateQty = inputParams.packageQty;
                        remainQty = remainQty - inputParams.packageQty;
                    }


                    _vpoint = "将包装信息插入入库唯一码表";
                    WmsInReceiptUniicode uniicode = new WmsInReceiptUniicode();
                    if (wmsInOrderDetail != null)
                    {
                        uniicode = await MapWmsInReceiptUniicode(wmsInOrderDetail, invoker);
                        //uniicode.areaNo = wmsInOrderDetail?.areaNo;
                        uniicode.erpWhouseNo = wmsInOrderDetail?.erpWhouseNo;
                    }
                    else
                    {
                        uniicode.materialName = material.basBMaterial.MaterialName;
                        uniicode.materialCode = material.basBMaterial.MaterialCode;
                        uniicode.materialSpec = material.basBMaterial.MaterialSpec;
                        uniicode.unitCode = material.basBMaterial.UnitCode;
                        //uniicode.delFlag = DelFlag.NDelete.GetCode();
                    }
                    // add by Allen 生成唯一码的时候，把入库单明细表中的areaNo和erpWhouseNo一起带过来保存到入库唯一码表
                    uniicode.qty = generateQty;
                    if (suplier != null)
                    {
                        uniicode.supplierName = suplier.supplierName;
                        uniicode.supplierCode = suplier.supplierCode;
                        uniicode.supplierNameEn = suplier.supplierNameEn;
                        uniicode.supplierNameAlias = suplier.supplierNameAlias;
                    }

                    uniicode.uniicode = packageSeqNo;
                    uniicode.batchNo = batchNo;
                    uniicode.recordQty = 0;
                    uniicodes.Add(uniicode);
                }



                //DC.Set<WmsInReceiptUniicode>().AddRange(uniicodes);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(uniicodes);
                await ((DbContext)DC).SaveChangesAsync();
                #endregion
                #endregion

                #region 3  返回
                List<UniicodeDto> uniicodeVies = new List<UniicodeDto>();
                foreach (var item in uniicodes)
                {
                    UniicodeDto uv = new UniicodeDto { uniicode = item.uniicode, qty = item.qty ?? 0, batchNo = batchNo };
                    uniicodeVies.Add(uv);
                }
                result.outParams = uniicodeVies;

                #endregion
                return result;
            }
            catch (Exception e)
            {
                result.code = DictonaryHelper.ResCode.Error;
                result.msg = $"WmsInReceiptUniicodeVM->GenerateUniicode(WmsInOrderView orderView) 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {e.Message} ]";
                return result;
            }
            //return null;
        }

    }
}
