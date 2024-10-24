using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayVM : BaseCRUDVM<WmsPutaway>
    {
        /// <summary>
        /// 根据托盘编号、库存编码、上架单状态获取上架单
        /// </summary>
        /// <param name="palletCode"></param>
        /// <param name="stockCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<WmsPutaway> GetPutwayByPalletInfo(string palletCode, string stockCode, int status)
        {
            return await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == palletCode && x.putawayStatus == status && x.stockCode == stockCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据库存明细获取上架单信息
        /// </summary>
        /// <param name="wmsStockDtlID"></param>
        /// <returns></returns>
        public WmsPutawayDtl GetPutwayByWmsStockDtl(Int64 wmsStockDtlID)
        {
            return DC.Set<WmsPutawayDtl>().Where(x => x.stockDtlId == wmsStockDtlID).FirstOrDefault();
        }
    }
}
