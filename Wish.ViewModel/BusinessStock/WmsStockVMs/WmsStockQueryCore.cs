using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        /// <summary>
        /// 检验库存明细是否都已经完成分配
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public bool CheckWmsStockAllocateFinished(string stockCode)
        {
            bool res = DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode && x.qty > x.occupyQty).Any();
            return res;
        }

        /// <summary>
        /// 根据库存编号获取库存明细信息
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public List<WmsStockDtl> GetWmsStockDtl(string stockCode)
        {
            return DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode).ToList();
        }
        public async Task<List<WmsStockDtl>> GetWmsStockDtlAsync(string stockCode)
        {
            return await DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode).ToListAsync();
        }

        /// <summary>
        /// 根据库位，载体在库存中查询库存记录
        /// </summary>
        /// <param name="bin"></param>
        /// <param name="palletCode"></param>
        /// <returns></returns>
        public async Task<WmsStock> GetWmsStockByBinPalletAsync(string binNo, string palletCode)
        {
            return await DC.Set<WmsStock>().Where(x => x.binNo == binNo && x.palletBarcode == palletCode).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 检查入库单明细的状态是否都是传入的状态，如果都是status状态，将入库单主表也更新status;否则将主表状态改为status2
        /// </summary>
        /// <param name="receiptNo"></param>
        /// <param name="status"></param>
        /// <param name="status2"></param>
        public bool UpdateStockStatus(string stockCode, int? status, int? status2, string invoker)
        {
            var dtls = DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode).ToList();
            var statusDtls = dtls.Where(x => x.stockDtlStatus == status).ToList();
            if (dtls.Count == statusDtls.Count)
            {
                WmsStock wmsStock = DC.Set<WmsStock>().Where(x => x.stockCode == stockCode).FirstOrDefault();

                wmsStock.stockStatus = Convert.ToInt32(status);
                wmsStock.UpdateBy = invoker;
                wmsStock.UpdateTime = DateTime.Now;
                DC.UpdateEntity(wmsStock);
                DC.SaveChanges();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsStock wmsStock = DC.Set<WmsStock>().Where(x => x.stockCode == stockCode).FirstOrDefault();
                    wmsStock.stockStatus = Convert.ToInt32(status);
                    wmsStock.UpdateBy = invoker;
                    wmsStock.UpdateTime = DateTime.Now;
                    DC.UpdateEntity(wmsStock);
                    DC.SaveChanges();
                    return true;
                }
            }

            return false;
        }
        public async Task<bool> UpdateStockStatusAsync(string stockCode, int? status, int? status2, string invoker)
        {
            var dtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode).ToListAsync();
            var statusDtls = dtls.Where(x => x.stockDtlStatus == status).ToList();
            if (dtls.Count == statusDtls.Count)
            {
                WmsStock wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == stockCode).FirstOrDefaultAsync();

                wmsStock.stockStatus = Convert.ToInt32(status);
                wmsStock.UpdateBy = invoker;
                wmsStock.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsStock);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                await ((DbContext)DC).BulkSaveChangesAsync();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsStock wmsStock = DC.Set<WmsStock>().Where(x => x.stockCode == stockCode).FirstOrDefault();
                    wmsStock.stockStatus = Convert.ToInt32(status);
                    wmsStock.UpdateBy = invoker;
                    wmsStock.UpdateTime = DateTime.Now;
                    //DC.UpdateEntity(wmsStock);
                    //DC.SaveChanges();
                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 根据库存唯一码获取库存明细信息
        /// </summary>
        /// <param name="uniicodes"></param>
        /// <returns></returns>
        public List<WmsStockDtl> GetWmsStockDtlsByUniicodes(List<WmsStockUniicode> uniicodes)
        {
            List<WmsStockDtl> wmsStockDtls = new List<WmsStockDtl>();
            foreach (var uniicode in uniicodes)
            {
                WmsStockDtl wmsStockDtl = DC.Set<WmsStockDtl>().Where(x => x.ID == uniicode.stockDtlId).FirstOrDefault();
                wmsStockDtls.Add(wmsStockDtl);
            }

            return wmsStockDtls;
        }

        /// <summary>
        /// 根据托盘编号获取库存
        /// </summary>
        /// <param name="palletCode"></param>
        /// <returns></returns>
        public WmsStock GetWmsStockByPalletCode(string palletCode)
        {
            return DC.Set<WmsStock>().Where(x => x.palletBarcode == palletCode).FirstOrDefault();
        }
        public async Task<WmsStock> GetWmsStockByPalletCodeAsync(string palletCode)
        {
            return await DC.Set<WmsStock>().Where(x => x.palletBarcode == palletCode).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据库存编号获取库存明细信息
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public List<WmsStockDtl> GetWmsStockDtlByStockCode(string stockCode)
        {
            return DC.Set<WmsStockDtl>().Where(x => x.stockCode == stockCode).ToList();
        }

        /// <summary>
        /// 更新库存明细的占用数量
        /// </summary>
        /// <param name="stockDtlId"></param>
        /// <param name="occupyQty"></param>
        public void DoStockDtlOccupy(long stockDtlId, decimal occupyQty, string invoker)
        {
            WmsStockDtl stockDtl = DC.Set<WmsStockDtl>().Where(x => x.ID == stockDtlId).FirstOrDefault();
            /*if (stockDtl.occupyQty + occupyQty > stockDtl.qty)
            {
                throw new Exception($"库存明细 {stockDtlId} 的可分配数量小于 {occupyQty}!");
            }
            else
            {
                stockDtl.occupyQty = stockDtl.occupyQty + occupyQty;
                stockDtl.updateBy = invoker;
                stockDtl.updateTime = DateTime.Now;
                DC.UpdateEntity(stockDtl);
                DC.SaveChanges();
            }*/
            stockDtl.occupyQty = stockDtl.occupyQty + occupyQty;
            stockDtl.UpdateBy = invoker;
            stockDtl.UpdateTime = DateTime.Now;
            //DC.UpdateEntity(stockDtl);
            //DC.SaveChanges();

            ((DbContext)DC).BulkUpdate(new WmsStockDtl[] { stockDtl });
            ((DbContext)DC).BulkSaveChanges();
        }

        /// <summary>
        /// 根据库存明细ID获取库存明细信息
        /// </summary>
        /// <param name="stockDtlId"></param>
        /// <returns></returns>
        public WmsStockDtl GetWmsStockDtlByDtlId(long stockDtlId)
        {
            return DC.Set<WmsStockDtl>().Where(x => x.ID == stockDtlId).FirstOrDefault();
        }

        /// <summary>
        /// 根据库存编号获取库存信息
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public WmsStock GetWmsStock(string stockCode)
        {
            return DC.Set<WmsStock>().Where(x => x.stockCode == stockCode).FirstOrDefault();
        }

        /// <summary>
        /// 根据库存编码和托盘编号获取库存信息
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="palletCode"></param>
        /// <returns></returns>
        public WmsStock GetWmsStockPalletCode(string stockCode, string palletCode)
        {
            return DC.Set<WmsStock>().Where(x => x.stockCode == stockCode && x.palletBarcode == palletCode).FirstOrDefault();
        }
        public async Task<WmsStock> GetWmsStockPalletCodeAsync(string stockCode, string palletCode)
        {
            return await DC.Set<WmsStock>().Where(x => x.stockCode == stockCode && x.palletBarcode == palletCode).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据库位号获取库存信息
        /// </summary>
        /// <param name="binNo"></param>
        /// <returns></returns>
        public WmsStock GetStockByBinNo(string binNo)
        {
            return DC.Set<WmsStock>().Where(x => x.binNo == binNo).FirstOrDefault();
        }

        /// <summary>
        /// 为Excel导入而增加的查询库存明细
        /// </summary>
        /// <param name="stockDtlId"></param>
        /// <returns></returns>
        public WmsStockDtl GetWmsStockDtlForImport(string materialCode, string erpWhouseNo, string barCode, string batchNo)
        {
            return DC.Set<WmsStockDtl>().Where(x => x.materialCode == materialCode && x.erpWhouseNo == erpWhouseNo && x.palletBarcode == barCode /*&& x.batchNo == barCode*/)
                .FirstOrDefault();
        }
    }
}
