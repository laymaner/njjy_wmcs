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


namespace Wish.ViewModel.BusinessIn.WmsInOrderVMs
{
    public partial class WmsInOrderVM
    {

        /// <summary>
        /// 根据送货单号和送货单行号可以匹配到唯一的入库明细数据
        /// </summary>
        /// <param name="asnCode"></param> 送货单号
        /// <param name="asnLine"></param> 送货单行号
        /// <returns></returns>
        public async Task<WmsInOrderDtl> getByOrderNoAndId(String asnCode, string asnLine)
        {
            return await DC.Set<WmsInOrderDtl>().Where(x => x.orderNo == asnCode && x.orderDtlId == asnLine).FirstOrDefaultAsync();
        }


        /// <summary>
        /// 根据入库订单号获取订单信息
        /// </summary>
        /// <param name="inOrderNo"></param>
        /// <returns></returns>
        public WmsInOrder GetWmsInOrder(string inOrderNo)
        {
            return DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefault();
        }
        public async Task<WmsInOrder> GetWmsInOrderAsync(string inOrderNo)
        {
            return await DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据外部入库单ID获取入库单（注意：一个外部入库单可能对应我们系统中多个入库单号）
        /// </summary>
        /// <param name="inOrderNo"></param>
        /// <returns></returns>
        public List<WmsInOrder> GetWmsInOrderByExternalInId(string externalInId)
        {
            return DC.Set<WmsInOrder>().Where(x => x.externalInId == externalInId).ToList();
        }
        public async Task<List<WmsInOrder>> GetWmsInOrderByExternalInIdAsync(string externalInId)
        {
            return await DC.Set<WmsInOrder>().Where(x => x.externalInId == externalInId).ToListAsync();
        }
        /// <summary>
        /// 根据外部入库单号（即单据号）查找出主表数据记录
        /// </summary>
        /// <param name="externalInNo"></param>
        /// <returns></returns>
        public WmsInOrder GetWmsInOrderByExternalNo(string externalInNo)
        {
            return DC.Set<WmsInOrder>().Where(x => x.externalInNo == externalInNo).FirstOrDefault();
        }
        public async Task<WmsInOrder> GetWmsInOrderByExternalNoAsync(string externalInNo)
        {
            return await DC.Set<WmsInOrder>().Where(x => x.externalInNo == externalInNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据入库单号、外部入库单号和外部入库单行号匹配找到入库单明细记录
        /// </summary>
        /// <param name="inNo"></param> 入库单号
        /// <param name="externalInNo"></param> 外部入库单号
        /// <param name="externalInId"></param> 外部入库单明细行号
        /// <returns></returns>
        public WmsInOrderDtl GetWmsInOrderDtlByExternalInNoAndLine(string inNo, string externalInNo, string externalInId)
        {
            return DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inNo && x.externalInNo == externalInNo).WhereIf(!externalInId.IsNullOrWhiteSpace(), x => x.externalInDtlId == externalInId).FirstOrDefault();
        }

        public async Task<WmsInOrderDtl> GetWmsInOrderDtlByExternalInNoAndLineAsync(string inNo, string externalInNo, string externalInId)
        {
            return await DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inNo && x.externalInNo == externalInNo).WhereIf(!externalInId.IsNullOrWhiteSpace(), x => x.externalInDtlId == externalInId).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据入库订单号获取订单信息
        /// </summary>
        /// <param name="inOrderNo"></param>
        /// <returns></returns>
        public WmsInOrder GetWmsInOrderByNo(string inOrderNo)
        {
            return DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).AsNoTracking().FirstOrDefault();
        }
        public async Task<WmsInOrder> GetWmsInOrderByNoAsync(string inOrderNo)
        {
            return await DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).AsNoTracking().FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据入库订单号获取入库订单明细
        /// </summary>
        /// <param name="inOrderNo"></param>
        /// <returns></returns>
        public List<WmsInOrderDtl> GetWmsInOrderDtls(string inOrderNo)
        {
            return DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inOrderNo).ToList();
        }
        public async Task<List<WmsInOrderDtl>> GetWmsInOrderDtlsAsync(string inOrderNo)
        {
            return await DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inOrderNo).ToListAsync();
        }
        /// <summary>
        /// 根据入库单明细获取入库单信息
        /// </summary>
        /// <param name="inOrderDtlId"></param>
        /// <returns></returns>
        public WmsInOrderDtl GetWmsInOrderDtl(Int64 inOrderDtlId)
        {
            return DC.Set<WmsInOrderDtl>().Where(x => x.ID == inOrderDtlId).FirstOrDefault();
        }
        public async Task<WmsInOrderDtl> GetWmsInOrderDtlAsync(Int64 inOrderDtlId)
        {
            return await DC.Set<WmsInOrderDtl>().Where(x => x.ID == inOrderDtlId).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 检查入库单明细的状态是否都是传入的状态，如果都是status状态，将入库单主表也更新status;否则将主表状态改为status2
        /// </summary>
        /// <param name="receiptNo"></param>
        /// <param name="status"></param>
        /// <param name="status2"></param>
        public bool UpdateInOrderStatus(string inOrderNo, int? status, int? status2, string invoker)
        {
            var dtls = DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inOrderNo).ToList();
            var statusDtls = dtls.Where(x => x.inDtlStatus == status).ToList();
            if (dtls.Count == statusDtls.Count)
            {
                WmsInOrder wmsInOrder = DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefault();
                wmsInOrder.inStatus = Convert.ToInt32(status);
                wmsInOrder.UpdateBy = invoker;
                wmsInOrder.UpdateTime = DateTime.Now;
                DC.UpdateEntity(wmsInOrder);
                DC.SaveChanges();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsInOrder wmsInOrder = DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefault();
                    wmsInOrder.inStatus = Convert.ToInt32(status2);
                    wmsInOrder.UpdateBy = invoker;
                    wmsInOrder.UpdateTime = DateTime.Now;
                    DC.UpdateEntity(wmsInOrder);
                    DC.SaveChanges();
                }
            }

            return false;
        }
        public async Task<bool> UpdateInOrderStatusAsync(string inOrderNo, int? status, int? status2, string invoker)
        {
            var dtls = await DC.Set<WmsInOrderDtl>().Where(x => x.inNo == inOrderNo).ToListAsync();
            var statusDtls = dtls.Where(x => x.inDtlStatus == status).ToList();
            if (dtls.Count == statusDtls.Count)
            {
                WmsInOrder wmsInOrder = await DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefaultAsync();
                wmsInOrder.inStatus = Convert.ToInt32(status);
                wmsInOrder.UpdateBy = invoker;
                wmsInOrder.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsInOrder);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsInOrder>().SingleUpdateAsync(wmsInOrder);
                await ((DbContext)DC).BulkSaveChangesAsync();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsInOrder wmsInOrder = await DC.Set<WmsInOrder>().Where(x => x.inNo == inOrderNo).FirstOrDefaultAsync();
                    wmsInOrder.inStatus = Convert.ToInt32(status2);
                    wmsInOrder.UpdateBy = invoker;
                    wmsInOrder.UpdateTime = DateTime.Now;
                    //DC.UpdateEntity(wmsInOrder);
                    //DC.SaveChanges();
                    await ((DbContext)DC).Set<WmsInOrder>().SingleUpdateAsync(wmsInOrder);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
            }

            return false;
        }
    }
}
