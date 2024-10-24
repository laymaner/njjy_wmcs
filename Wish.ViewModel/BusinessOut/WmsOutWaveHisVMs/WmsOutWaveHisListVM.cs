using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutWaveHisVMs
{
    public partial class WmsOutWaveHisListVM : BasePagedListVM<WmsOutWaveHis_View, WmsOutWaveHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsOutWaveHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsOutWaveHis_View>>{
                this.MakeGridHeader(x => x.allotFlag),
                this.MakeGridHeader(x => x.allotOperator),
                this.MakeGridHeader(x => x.allotTime),
                this.MakeGridHeader(x => x.deliveryLocNo),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.issuedFlag),
                this.MakeGridHeader(x => x.issuedOperator),
                this.MakeGridHeader(x => x.issuedResult),
                this.MakeGridHeader(x => x.issuedTime),
                this.MakeGridHeader(x => x.operationReason),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.waveNo),
                this.MakeGridHeader(x => x.waveStatus),
                this.MakeGridHeader(x => x.waveType),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsOutWaveHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsOutWaveHis>()
                .CheckContain(Searcher.allotOperator, x=>x.allotOperator)
                .CheckContain(Searcher.deliveryLocNo, x=>x.deliveryLocNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.issuedResult, x=>x.issuedResult)
                .CheckContain(Searcher.operationReason, x=>x.operationReason)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.waveNo, x=>x.waveNo)
                .CheckEqual(Searcher.waveStatus, x=>x.waveStatus)
                .CheckEqual(Searcher.waveType, x=>x.waveType)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsOutWaveHis_View
                {
				    ID = x.ID,
                    allotFlag = x.allotFlag,
                    allotOperator = x.allotOperator,
                    allotTime = x.allotTime,
                    deliveryLocNo = x.deliveryLocNo,
                    docTypeCode = x.docTypeCode,
                    issuedFlag = x.issuedFlag,
                    issuedOperator = x.issuedOperator,
                    issuedResult = x.issuedResult,
                    issuedTime = x.issuedTime,
                    operationReason = x.operationReason,
                    proprietorCode = x.proprietorCode,
                    waveNo = x.waveNo,
                    waveStatus = x.waveStatus,
                    waveType = x.waveType,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsOutWaveHis_View : WmsOutWaveHis{

    }
}
