using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Quartz.Util;
using Wish.ViewModel.Base.BasBMaterialVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs
{
    public partial class WmsInReceiptUniicodeVM
    {

        /// <summary>
        /// 成品生成入库唯一码表
        /// </summary>
        /// <param name="inOrderDetial"></param>
        /// <returns></returns>
        public async Task<WmsInReceiptUniicode> MapWmsInReceiptUniicode(WmsInOrderDtl inOrderDetial, string invoker)
        {
            BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
            var _basBMaterialView = await basBMaterialVM.GetBasBMaterialAsync(inOrderDetial.materialCode);
            if (_basBMaterialView == null || _basBMaterialView.basBMaterialCategory == null ||
                _basBMaterialView.basBMaterial == null)
            {
                throw new Exception($"物料信息 {inOrderDetial.materialCode} 维护不完整!");
            }

            DateTime productDate = DateTime.Now;
            DateTime? exTime = null;
            if (!_basBMaterialView.basBMaterial.EmaterialVtime.IsNullOrWhiteSpace())
            {
                exTime = productDate.AddDays(Double.Parse(_basBMaterialView.basBMaterial.EmaterialVtime));
            }


            // 插入入库唯一码表，直接生成入库唯一码信息
            WmsInReceiptUniicode wmsInReceiptUniicode = new WmsInReceiptUniicode();
            wmsInReceiptUniicode.batchNo = inOrderDetial.batchNo;
            // DataCode
            //wmsInReceiptUniicode.dataCode;
            //wmsInReceiptUniicode.delFlag = "0";
            wmsInReceiptUniicode.erpWhouseNo = inOrderDetial.erpWhouseNo;
            // 失效日期
            wmsInReceiptUniicode.expDate = exTime;
            wmsInReceiptUniicode.extend1 = inOrderDetial.extend1;
            wmsInReceiptUniicode.extend2 = inOrderDetial.extend2;
            wmsInReceiptUniicode.extend3 = inOrderDetial.extend3;
            wmsInReceiptUniicode.extend4 = inOrderDetial.extend4;
            wmsInReceiptUniicode.extend5 = inOrderDetial.extend5;
            wmsInReceiptUniicode.extend6 = inOrderDetial.extend6;
            wmsInReceiptUniicode.extend7 = inOrderDetial.extend7;
            wmsInReceiptUniicode.extend8 = inOrderDetial.extend8;
            wmsInReceiptUniicode.extend9 = inOrderDetial.extend9;
            wmsInReceiptUniicode.extend10 = inOrderDetial.extend10;
            wmsInReceiptUniicode.externalInDtlId = inOrderDetial.externalInDtlId;
            wmsInReceiptUniicode.externalInNo = inOrderDetial.externalInNo;
            wmsInReceiptUniicode.inNo = inOrderDetial.inNo;
            wmsInReceiptUniicode.inDtlId = inOrderDetial.ID;
            //检验单号
            //wmsInReceiptUniicodeiqcResultNo
            wmsInReceiptUniicode.materialCode = inOrderDetial.materialCode;
            wmsInReceiptUniicode.materialName = inOrderDetial.materialName;
            wmsInReceiptUniicode.materialSpec = inOrderDetial.materialSpec;
            wmsInReceiptUniicode.unitCode = _basBMaterialView.basBMaterial.UnitCode;
            // MSL等级编码
            wmsInReceiptUniicode.mslGradeCode = "";

            // 载体条码
            wmsInReceiptUniicode.curPalletBarcode = "";

            // 所在载体位置
            wmsInReceiptUniicode.curPositionNo = "";

            // 生产日期//除电子料外，其他物料的生产日期选用当日上架日期
            wmsInReceiptUniicode.productDate = productDate;
            wmsInReceiptUniicode.projectNo = inOrderDetial.projectNo;
            wmsInReceiptUniicode.proprietorCode = inOrderDetial.proprietorCode;

            // 收货单明细ID
            //wmsInReceiptUniicode.receiptDtlId;
            // 收货单编号
            //wmsInReceiptUniicode.receiptNo
            wmsInReceiptUniicode.qty = inOrderDetial.inQty;

            // 入库记录ID 
            //wmsInReceiptUniicode.receiptRecordId

            // 库存编码
            //wmsInReceiptUniicode.stockCode
            wmsInReceiptUniicode.skuCode = inOrderDetial.materialCode;

            // 库存明细ID
            //wmsInReceiptUniicode.stockDtlId 

            // 供应商暴露时长
            //wmsInReceiptUniicode.supplierExposeTimeDuration 
            wmsInReceiptUniicode.supplierName = inOrderDetial.supplierName;
            wmsInReceiptUniicode.supplierNameEn = inOrderDetial.supplierNameEn;
            wmsInReceiptUniicode.supplierNameAlias = inOrderDetial.supplierNameAlias;
            wmsInReceiptUniicode.supplierCode = inOrderDetial.supplierCode;
            wmsInReceiptUniicode.runiiStatus = Convert.ToInt32(DictonaryHelper.InUniicodeStatus.InitCreate.GetCode());
            wmsInReceiptUniicode.whouseNo = inOrderDetial.whouseNo;
            //wmsInReceiptUniicode.areaNo = inOrderDetial.areaNo;
            wmsInReceiptUniicode.CreateBy = invoker;
            wmsInReceiptUniicode.CreateTime = DateTime.Now;
            return wmsInReceiptUniicode;
        }


        /// <summary>
        /// 检查入库唯一码是否存在
        /// </summary>
        /// <param name="uniicode"></param>
        /// <returns></returns>
        public bool CheckUniicodeExist(string uniicode)
        {
            bool flag = DC.Set<WmsInReceiptUniicode>().Where(x => x.uniicode == uniicode).Any();
            return flag;
        }

        public async Task<bool> CheckUniicodeExistAsync(string uniicode)
        {
            bool flag = await DC.Set<WmsInReceiptUniicode>().Where(x => x.uniicode == uniicode).AnyAsync();
            return flag;
        }

        /// <summary>
        /// 检查电子料的基础信息是否维护完整
        /// </summary>
        /// <param name="wmsInReceiptUniicode"></param>
        /// <returns></returns>
        public bool CheckElectronicMaterialBaseInfo(List<WmsInReceiptUniicode> wmsInReceiptUniicode)
        {
            var flag = false;
            foreach (var item in wmsInReceiptUniicode)
            {
                if (item.dataCode.IsNullOrWhiteSpace() ||
                    item.supplierExposeTimeDuration == null)//||item.mslGradeCode.IsNullOrWhiteSpace()
                {
                    flag = false;
                    return flag;
                }
                else
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// 根据载体编号获取入库唯一码信息
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetWmsInReceiptUniicodeByPalletCode(string barCode)
        {
            List<WmsInReceiptUniicode> wmsInReceiptUniicodes = DC.Set<WmsInReceiptUniicode>().Where(x => x.curPalletBarcode == barCode).ToList();
            return wmsInReceiptUniicodes;
        }

        /// <summary>
        /// 根据入库编码获取入库唯一码
        /// </summary>
        /// <param name="inNo"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetWmsInReceiptUniicodeByInNo(string inNo)
        {
            List<WmsInReceiptUniicode> wmsInReceiptUniicodes = DC.Set<WmsInReceiptUniicode>().Where(x => x.inNo == inNo).ToList();
            return wmsInReceiptUniicodes;
        }
        public async Task<List<WmsInReceiptUniicode>> GetWmsInReceiptUniicodeByInNoAsync(string inNo)
        {
            List<WmsInReceiptUniicode> wmsInReceiptUniicodes = await DC.Set<WmsInReceiptUniicode>().Where(x => x.inNo == inNo).ToListAsync();
            return wmsInReceiptUniicodes;
        }
        /// <summary>
        /// 根据收货单明细ID获取入库唯一码信息
        /// </summary>
        /// <param name="receiptDtlId"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetWmsInReceiptUniicodeReceiptDtlId(Int64 receiptDtlId)
        {
            return DC.Set<WmsInReceiptUniicode>().Where(x => x.receiptDtlId == receiptDtlId).ToList();
        }
        public async Task<List<WmsInReceiptUniicode>> GetWmsInReceiptUniicodeReceiptDtlIdAsync(Int64 receiptDtlId)
        {
            return await DC.Set<WmsInReceiptUniicode>().Where(x => x.receiptDtlId == receiptDtlId).ToListAsync();
        }
        /// <summary>
        /// 根据唯一码获取入库唯一码信息
        /// </summary>
        /// <param name="uniicode"></param>
        /// <returns></returns>
        public WmsInReceiptUniicode GetWmsInReceiptUniicode(string uniicode)
        {
            return DC.Set<WmsInReceiptUniicode>().Where(x => x.uniicode == uniicode).FirstOrDefault();
        }
        public async Task<WmsInReceiptUniicode> GetWmsInReceiptUniicodeAsync(string uniicode)
        {
            return await DC.Set<WmsInReceiptUniicode>().Where(x => x.uniicode == uniicode).FirstOrDefaultAsync();
        }
        public async Task<WmsInReceiptUniicodeHis> GetWmsInReceiptUniicodeHis(string uniicode)
        {
            return await DC.Set<WmsInReceiptUniicodeHis>().Where(x => x.uniicode == uniicode).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据主键ID查找出入库记录表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WmsInReceiptUniicode GetWmsInReceiptUniicodeById(Int64 id)
        {
            return DC.Set<WmsInReceiptUniicode>().Where(x => x.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据入库唯一码主键数组获取入库唯一码记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetWmsInReceiptUniicodeListByIds(List<Int64> ids)
        {
            return DC.Set<WmsInReceiptUniicode>().Where(x => ids.Contains(x.ID)).ToList();
        }

        /// <summary>
        /// 根据批次查询已经维护电子料信息的唯一码
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetUniicodeByBatchNo(string batchNo)
        {
            var InUniicodes = DC.Set<WmsInReceiptUniicode>().Where(x => x.batchNo == batchNo).ToList();
            var inUniicodelist = InUniicodes.Where(x => !x.dataCode.IsNullOrWhiteSpace() && !x.mslGradeCode.IsNullOrWhiteSpace() && x.supplierExposeTimeDuration != null).ToList();
            return inUniicodelist;
        }
        public async Task<List<WmsInReceiptUniicode>> GetUniicodeByBatchNoAsync(string batchNo)
        {
            var InUniicodes = await DC.Set<WmsInReceiptUniicode>().Where(x => x.batchNo == batchNo).ToListAsync();
            var inUniicodelist = InUniicodes.Where(x => !x.dataCode.IsNullOrWhiteSpace() && !x.mslGradeCode.IsNullOrWhiteSpace() && x.supplierExposeTimeDuration != null).ToList();
            return inUniicodelist;
        }
        /// <summary>
        /// 根据批次List查询已经维护电子料信息的唯一码
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public List<WmsStockUniicode> GetUniicodeByBatchNos(List<string> batchNo)
        {
            var InUniicodes = DC.Set<WmsStockUniicode>().Where(x => batchNo.Contains(x.batchNo)).ToList();
            var bacthNos = InUniicodes.Select(x => x.batchNo).Distinct().ToList();
            var batchNoHisList = batchNo.Where(x => !bacthNos.Contains(x)).ToList();
            var InUniicodeHiss = DC.Set<WmsStockUniicodeHis>().Where(x => batchNoHisList.Contains(x.batchNo)).ToList();

            var inUniicodelist = new List<WmsStockUniicode>();

            var stockUniicodes = InUniicodes.Where(x => !x.dataCode.IsNullOrWhiteSpace() && !x.mslGradeCode.IsNullOrWhiteSpace()).ToList();
            inUniicodelist.AddRange(stockUniicodes);

            if (InUniicodeHiss.Count == 0 || !InUniicodeHiss.Any())
            {
                var stockHisUniicodes = InUniicodeHiss.Where(x => !x.dataCode.IsNullOrWhiteSpace() && !x.mslGradeCode.IsNullOrWhiteSpace())
                    .Select(x => new WmsStockUniicode
                    {
                        ID = x.ID,
                        areaNo = x.areaNo,
                        batchNo = x.batchNo,
                        dataCode = x.dataCode,
                        delayFrozenFlag = x.delayFrozenFlag,
                        delayFrozenReason = x.delayFrozenReason,
                        delayReason = x.delayReason,
                        delayTimes = x.delayTimes,
                        delayToEndDate = x.delayToEndDate,
                        driedScrapFlag = x.driedScrapFlag,
                        driedTimes = x.driedTimes,
                        erpWhouseNo = x.erpWhouseNo,
                        expDate = x.expDate,
                        exposeFrozenFlag = x.exposeFrozenFlag,
                        exposeFrozenReason = x.exposeFrozenReason,
                        extend1 = x.extend1,
                        extend2 = x.extend2,
                        extend3 = x.extend3,
                        extend4 = x.extend4,
                        extend5 = x.extend5,
                        extend6 = x.extend6,
                        extend7 = x.extend7,
                        extend8 = x.extend8,
                        extend9 = x.extend9,
                        extend10 = x.extend10,
                        extend11 = x.extend11,
                        chipSize = x.chipSize,
                        chipThickness = x.chipThickness,
                        chipModel = x.chipModel,
                        dafType = x.dafType,
                        inspectionResult = x.inspectionResult,
                        inwhTime = x.inwhTime,
                        leftMslTimes = x.leftMslTimes,
                        materialCode = x.materialCode,
                        materialName = x.materialName,
                        materialSpec = x.materialSpec,
                        mslGradeCode = x.mslGradeCode,
                        occupyQty = x.occupyQty,
                        packageTime = x.packageTime,
                        palletBarcode = x.palletBarcode,
                        positionNo = x.positionNo,
                        productDate = x.productDate,
                        projectNo = x.projectNo,
                        proprietorCode = x.proprietorCode,
                        realExposeTimes = x.realExposeTimes,
                        skuCode = x.skuCode,
                        stockCode = x.stockCode,
                        stockDtlId = x.stockDtlId,
                        qty = x.qty,
                        supplierCode = x.supplierCode,
                        supplierExposeTimes = x.supplierExposeTimes,
                        supplierName = x.supplierName,
                        supplierNameAlias = x.supplierNameAlias,
                        supplierNameEn = x.supplierNameEn,
                        uniicode = x.uniicode,
                        unpackStatus = x.unpackStatus,
                        unpackTime = x.unpackTime,
                        unitCode = x.unitCode,
                        whouseNo = x.whouseNo,
                        projectNoBak = x.projectNoBak
                    }).ToList();
                inUniicodelist.AddRange(stockHisUniicodes);
            }
            return inUniicodelist;
        }
        /// <summary>
        /// 根据收货单明细ID获取不合格入库唯一码信息
        /// </summary>
        /// <param name="receiptDtlId"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetReceiptUniicodeByReceiptDtlId(Int64 receiptDtlId)
        {
            return DC.Set<WmsInReceiptUniicode>().Where(x => x.receiptDtlId == receiptDtlId).ToList();
        }

        /// <summary>
        /// todo 尽量不要用这个方法
        /// 根据入库单明细id和批次号获取入库唯一码列表（希望结果是只有一条入库唯一码记录，该方法是在入库单打印标签中使用，可能存在一些问题）
        /// </summary>
        /// <param name="inDtlId"></param>
        /// <param name="batNo"></param>
        /// <returns></returns>
        public List<WmsInReceiptUniicode> GetReceiptUniicodeByInDtlIdAndBatNo(Int64 inDtlId, string batNo)
        {
            // todo add by Allen 先不根据批次号（入库唯一码表里面的批次号是空，但是入库明细行里面有批次，帮有待考究）
            return DC.Set<WmsInReceiptUniicode>().Where(x => x.inDtlId == inDtlId).ToList();
        }
        /// <summary>
        /// 删除入库唯一码表入库完成的记录，并转成历史
        /// </summary>
        public void RemoveUniicodetoHis()
        {
            var hasParentTransaction = false;
            try
            {
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (hasParentTransaction == false)
                {
                    DC.Database.BeginTransaction();
                }
                var inUniicodeDels = DC.Set<WmsInReceiptUniicode>().Where(x => x.runiiStatus >= 90).ToList();
                if (inUniicodeDels.Any() || inUniicodeDels.Count() > 0)
                {
                    List<WmsInReceiptUniicodeHis> wmsInReceiptUniicodeHis = new List<WmsInReceiptUniicodeHis>();
                    inUniicodeDels.ForEach(t =>
                    {
                        var his = CommonHelper.Map<WmsInReceiptUniicode, WmsInReceiptUniicodeHis>(t, "ID");
                        wmsInReceiptUniicodeHis.Add(his);
                    });
                    DC.Set<WmsInReceiptUniicode>().RemoveRange(inUniicodeDels);
                    DC.Set<WmsInReceiptUniicodeHis>().AddRange(wmsInReceiptUniicodeHis);
                    DC.SaveChanges();
                }
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        DC.Database.CommitTransaction();
                    }
                }
            }
            catch (Exception)
            {
                if (hasParentTransaction == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        DC.Database.RollbackTransaction();
                    }
                }
                throw;
            }

        }
    }
}
