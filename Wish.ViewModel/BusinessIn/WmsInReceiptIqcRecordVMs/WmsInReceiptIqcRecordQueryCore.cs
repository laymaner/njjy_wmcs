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


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordVMs
{
    public partial class WmsInReceiptIqcRecordVM
    {
        /// <summary>
        /// 根据入库单明细ID获取质检录入信息
        /// </summary>
        /// <param name="iqcRecordNo"></param>
        /// <returns></returns>
        public WmsInReceiptIqcRecord GetWmsInReceiptIqcRecordByInDtlId(Int64 inDtlId)
        {
            return DC.Set<WmsInReceiptIqcRecord>().Where(x => x.inDtlId == inDtlId).FirstOrDefault();
        }
        /// <summary>
        /// 根据入库单明细ID获取质检录入信息async
        /// </summary>
        /// <param name="inDtlId"></param>
        /// <returns></returns>
        public async Task<WmsInReceiptIqcRecord> GetWmsInReceiptIqcRecordByInDtlIdAsync(Int64 inDtlId)
        {
            return await DC.Set<WmsInReceiptIqcRecord>().Where(x => x.inDtlId == inDtlId).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据收货单单明细ID获取质检录入信息
        /// </summary>
        /// <param name="receiptDtlId"></param>
        /// <returns></returns>
        public WmsInReceiptIqcRecord GetWmsInReceiptIqcRecordByReceiptDtlId(Int64 receiptDtlId)
        {
            return DC.Set<WmsInReceiptIqcRecord>().Where(x => x.receiptDtlId == receiptDtlId).FirstOrDefault();
        }
        /// <summary>
        /// 根据收货单单明细ID获取质检录入信息async
        /// </summary>
        /// <param name="receiptDtlId"></param>
        /// <returns></returns>
        public async Task<WmsInReceiptIqcRecord> GetWmsInReceiptIqcRecordByReceiptDtlIdAsync(Int64 receiptDtlId)
        {
            return await DC.Set<WmsInReceiptIqcRecord>().Where(x => x.receiptDtlId == receiptDtlId).FirstOrDefaultAsync();
        }
    }
}
