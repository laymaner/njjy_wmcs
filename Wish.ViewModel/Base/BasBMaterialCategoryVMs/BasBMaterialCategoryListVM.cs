using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialCategoryVMs
{
    public partial class BasBMaterialCategoryListVM : BasePagedListVM<BasBMaterialCategory_View, BasBMaterialCategorySearcher>
    {

        protected override IEnumerable<IGridColumn<BasBMaterialCategory_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBMaterialCategory_View>>{
                this.MakeGridHeader(x => x.delayDays),
                this.MakeGridHeader(x => x.materialFlag),
                this.MakeGridHeader(x => x.isAutoDelay),
                this.MakeGridHeader(x => x.materialCategoryDesc),
                this.MakeGridHeader(x => x.materialCategoryName),
                this.MakeGridHeader(x => x.materialCategoryNameAlias),
                this.MakeGridHeader(x => x.materialCategoryNameEn),
                this.MakeGridHeader(x => x.materialCategoryCode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.virtualFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBMaterialCategory_View> GetSearchQuery()
        {
            var query = DC.Set<BasBMaterialCategory>()
                .CheckContain(Searcher.materialFlag, x=>x.materialFlag)
                .CheckContain(Searcher.materialCategoryName, x=>x.materialCategoryName)
                .CheckContain(Searcher.materialCategoryCode, x=>x.materialCategoryCode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasBMaterialCategory_View
                {
				    ID = x.ID,
                    delayDays = x.delayDays,
                    materialFlag = x.materialFlag,
                    isAutoDelay = x.isAutoDelay,
                    materialCategoryDesc = x.materialCategoryDesc,
                    materialCategoryName = x.materialCategoryName,
                    materialCategoryNameAlias = x.materialCategoryNameAlias,
                    materialCategoryNameEn = x.materialCategoryNameEn,
                    materialCategoryCode = x.materialCategoryCode,
                    proprietorCode = x.proprietorCode,
                    usedFlag = x.usedFlag,
                    virtualFlag = x.virtualFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBMaterialCategory_View : BasBMaterialCategory{

    }
}
