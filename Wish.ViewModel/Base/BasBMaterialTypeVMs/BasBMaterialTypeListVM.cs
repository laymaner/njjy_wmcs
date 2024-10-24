using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialTypeVMs
{
    public partial class BasBMaterialTypeListVM : BasePagedListVM<BasBMaterialType_View, BasBMaterialTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBMaterialType_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBMaterialType_View>>{
                this.MakeGridHeader(x => x.materialTypeDesc),
                this.MakeGridHeader(x => x.materialTypeName),
                this.MakeGridHeader(x => x.materialTypeNameAlias),
                this.MakeGridHeader(x => x.materialTypeNameEn),
                this.MakeGridHeader(x => x.materialTypeCode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.virtualFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBMaterialType_View> GetSearchQuery()
        {
            var query = DC.Set<BasBMaterialType>()
                .CheckContain(Searcher.materialTypeName, x=>x.materialTypeName)
                .CheckContain(Searcher.materialTypeCode, x=>x.materialTypeCode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckEqual(Searcher.virtualFlag, x=>x.virtualFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasBMaterialType_View
                {
				    ID = x.ID,
                    materialTypeDesc = x.materialTypeDesc,
                    materialTypeName = x.materialTypeName,
                    materialTypeNameAlias = x.materialTypeNameAlias,
                    materialTypeNameEn = x.materialTypeNameEn,
                    materialTypeCode = x.materialTypeCode,
                    proprietorCode = x.proprietorCode,
                    usedFlag = x.usedFlag,
                    virtualFlag = x.virtualFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBMaterialType_View : BasBMaterialType{

    }
}
