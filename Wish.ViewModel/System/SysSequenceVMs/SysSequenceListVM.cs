using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.System.Model;


namespace Wish.ViewModel.System.SysSequenceVMs
{
    public partial class SysSequenceListVM : BasePagedListVM<SysSequence_View, SysSequenceSearcher>
    {

        protected override IEnumerable<IGridColumn<SysSequence_View>> InitGridHeader()
        {
            return new List<GridColumn<SysSequence_View>>{
                this.MakeGridHeader(x => x.SeqCode),
                this.MakeGridHeader(x => x.SeqDesc),
                this.MakeGridHeader(x => x.SeqType),
                this.MakeGridHeader(x => x.NowSn),
                this.MakeGridHeader(x => x.MinSn),
                this.MakeGridHeader(x => x.MaxSn),
                this.MakeGridHeader(x => x.SeqSnLen),
                this.MakeGridHeader(x => x.SeqPrefix),
                this.MakeGridHeader(x => x.SeqDate),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SysSequence_View> GetSearchQuery()
        {
            var query = DC.Set<SysSequence>()
                .CheckContain(Searcher.SeqCode, x=>x.SeqCode)
                .CheckEqual(Searcher.SeqType, x=>x.SeqType)
                .CheckEqual(Searcher.NowSn, x=>x.NowSn)
                .CheckEqual(Searcher.MinSn, x=>x.MinSn)
                .CheckEqual(Searcher.MaxSn, x=>x.MaxSn)
                .CheckEqual(Searcher.SeqSnLen, x=>x.SeqSnLen)
                .Select(x => new SysSequence_View
                {
				    ID = x.ID,
                    SeqCode = x.SeqCode,
                    SeqDesc = x.SeqDesc,
                    SeqType = x.SeqType,
                    NowSn = x.NowSn,
                    MinSn = x.MinSn,
                    MaxSn = x.MaxSn,
                    SeqSnLen = x.SeqSnLen,
                    SeqPrefix = x.SeqPrefix,
                    SeqDate = x.SeqDate,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SysSequence_View : SysSequence{

    }
}
