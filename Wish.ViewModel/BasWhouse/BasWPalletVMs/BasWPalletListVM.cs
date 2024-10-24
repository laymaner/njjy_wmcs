using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletVMs
{
    public partial class BasWPalletListVM : BasePagedListVM<BasWPallet_View, BasWPalletSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWPallet_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWPallet_View>>{
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.palletDesc),
                this.MakeGridHeader(x => x.palletDescAlias),
                this.MakeGridHeader(x => x.palletDescEn),
                this.MakeGridHeader(x => x.palletTypeCode),
                this.MakeGridHeader(x => x.printsQty),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWPallet_View> GetSearchQuery()
        {
            var query = DC.Set<BasWPallet>()
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.palletTypeCode, x=>x.palletTypeCode)
                .CheckEqual(Searcher.printsQty, x=>x.printsQty)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWPallet_View
                {
				    ID = x.ID,
                    palletBarcode = x.palletBarcode,
                    palletDesc = x.palletDesc,
                    palletDescAlias = x.palletDescAlias,
                    palletDescEn = x.palletDescEn,
                    palletTypeCode = x.palletTypeCode,
                    printsQty = x.printsQty,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWPallet_View : BasWPallet{

    }
}
