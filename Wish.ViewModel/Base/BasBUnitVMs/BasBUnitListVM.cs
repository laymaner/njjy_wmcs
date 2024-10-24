using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBUnitVMs
{
    public partial class BasBUnitListVM : BasePagedListVM<BasBUnit_View, BasBUnitSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBUnit_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBUnit_View>>{
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.unitName),
                this.MakeGridHeader(x => x.unitNameAlias),
                this.MakeGridHeader(x => x.unitNameEn),
                this.MakeGridHeader(x => x.unitType),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBUnit_View> GetSearchQuery()
        {
            var query = DC.Set<BasBUnit>()
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.unitName, x=>x.unitName)
                .CheckContain(Searcher.unitType, x=>x.unitType)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasBUnit_View
                {
				    ID = x.ID,
                    unitCode = x.unitCode,
                    unitName = x.unitName,
                    unitNameAlias = x.unitNameAlias,
                    unitNameEn = x.unitNameEn,
                    unitType = x.unitType,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBUnit_View : BasBUnit{

    }
}
