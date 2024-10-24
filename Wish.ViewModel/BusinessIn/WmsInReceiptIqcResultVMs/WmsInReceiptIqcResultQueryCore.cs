using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Wish.ViewModel.Common.Dtos;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{
    public partial class WmsInReceiptIqcResultVM 
    {
        /// <summary>
        /// 根据质检结果单号获取质检结果
        /// </summary>
        /// <param name="iqcResultNo"></param>
        /// <returns></returns>
        public WmsInReceiptIqcResult GetInReceiptIqcResultByIQCResultNo(string iqcResultNo)
        {
            return DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcResultNo == iqcResultNo).FirstOrDefault();
        }

        /// <summary>
        /// 根据收货明细ID查询质检结果
        /// </summary>
        /// <param name="iqcResultNo"></param>
        /// <returns></returns>
        public WmsInReceiptIqcResult GetInReceiptIqcResultByReceiptDtlID(Int64 receiptDtlID)
        {
            return DC.Set<WmsInReceiptIqcResult>().Where(x => x.receiptDtlId == receiptDtlID).FirstOrDefault();
        }

        public WmsInReceiptIqcResult GetWmsInReceiptIqcResult(string iqcResultNo)
        {
            WmsInReceiptIqcResult WmsInReceiptIqcResult = DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcResultNo == iqcResultNo).FirstOrDefault();
            return WmsInReceiptIqcResult;
        }
        public async Task<WmsInReceiptIqcResult> GetWmsInReceiptIqcResultAsync(string iqcResultNo)
        {
            WmsInReceiptIqcResult WmsInReceiptIqcResult = await DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcResultNo == iqcResultNo).FirstOrDefaultAsync();
            return WmsInReceiptIqcResult;
        }
    }
}
