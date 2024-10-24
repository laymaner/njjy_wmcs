using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveHisVMs
{
    public partial class WmsItnMoveHisListVM : BasePagedListVM<WmsItnMoveHis_View, WmsItnMoveHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnMoveHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnMoveHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.moveNo),
                this.MakeGridHeader(x => x.moveStatus),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putdownLocNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnMoveHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnMoveHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.moveNo, x=>x.moveNo)
                .CheckEqual(Searcher.moveStatus, x=>x.moveStatus)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.putdownLocNo, x=>x.putdownLocNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnMoveHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    docTypeCode = x.docTypeCode,
                    moveNo = x.moveNo,
                    moveStatus = x.moveStatus,
                    orderDesc = x.orderDesc,
                    proprietorCode = x.proprietorCode,
                    putdownLocNo = x.putdownLocNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnMoveHis_View : WmsItnMoveHis{

    }
}
