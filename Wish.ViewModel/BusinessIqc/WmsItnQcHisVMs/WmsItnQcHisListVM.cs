using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcHisVMs
{
    public partial class WmsItnQcHisListVM : BasePagedListVM<WmsItnQcHis_View, WmsItnQcHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnQcHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnQcHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.extend1),
                this.MakeGridHeader(x => x.extend10),
                this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.extend12),
                this.MakeGridHeader(x => x.extend13),
                this.MakeGridHeader(x => x.extend14),
                this.MakeGridHeader(x => x.extend15),
                this.MakeGridHeader(x => x.extend2),
                this.MakeGridHeader(x => x.extend3),
                this.MakeGridHeader(x => x.extend4),
                this.MakeGridHeader(x => x.extend5),
                this.MakeGridHeader(x => x.extend6),
                this.MakeGridHeader(x => x.extend7),
                this.MakeGridHeader(x => x.extend8),
                this.MakeGridHeader(x => x.extend9),
                this.MakeGridHeader(x => x.itnQcLocNo),
                this.MakeGridHeader(x => x.itnQcNo),
                this.MakeGridHeader(x => x.itnQcStatus),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnQcHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnQcHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.itnQcLocNo, x=>x.itnQcLocNo)
                .CheckContain(Searcher.itnQcNo, x=>x.itnQcNo)
                .CheckEqual(Searcher.itnQcStatus, x=>x.itnQcStatus)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnQcHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    docTypeCode = x.docTypeCode,
                    erpWhouseNo = x.erpWhouseNo,
                    extend1 = x.extend1,
                    extend10 = x.extend10,
                    extend11 = x.extend11,
                    extend12 = x.extend12,
                    extend13 = x.extend13,
                    extend14 = x.extend14,
                    extend15 = x.extend15,
                    extend2 = x.extend2,
                    extend3 = x.extend3,
                    extend4 = x.extend4,
                    extend5 = x.extend5,
                    extend6 = x.extend6,
                    extend7 = x.extend7,
                    extend8 = x.extend8,
                    extend9 = x.extend9,
                    itnQcLocNo = x.itnQcLocNo,
                    itnQcNo = x.itnQcNo,
                    itnQcStatus = x.itnQcStatus,
                    orderDesc = x.orderDesc,
                    proprietorCode = x.proprietorCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnQcHis_View : WmsItnQcHis{

    }
}
