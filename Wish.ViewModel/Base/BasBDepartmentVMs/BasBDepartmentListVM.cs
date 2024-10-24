using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.Base;


namespace Wish.ViewModel.Base.BasBDepartmentVMs
{
    public partial class BasBDepartmentListVM : BasePagedListVM<BasBDepartment_View, BasBDepartmentSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBDepartment_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBDepartment_View>>{
                this.MakeGridHeader(x => x.DepartmentCode),
                this.MakeGridHeader(x => x.DepartmentName),
                this.MakeGridHeader(x => x.DepartmentNameAlias),
                this.MakeGridHeader(x => x.DepartmentNameEn),
                this.MakeGridHeader(x => x.UsedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBDepartment_View> GetSearchQuery()
        {
            var query = DC.Set<BasBDepartment>()
                .CheckContain(Searcher.DepartmentCode, x=>x.DepartmentCode)
                .CheckContain(Searcher.DepartmentName, x=>x.DepartmentName)
                .CheckEqual(Searcher.UsedFlag, x=>x.UsedFlag)
                .Select(x => new BasBDepartment_View
                {
				    ID = x.ID,
                    DepartmentCode = x.DepartmentCode,
                    DepartmentName = x.DepartmentName,
                    DepartmentNameAlias = x.DepartmentNameAlias,
                    DepartmentNameEn = x.DepartmentNameEn,
                    UsedFlag = x.UsedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBDepartment_View : BasBDepartment{

    }
}
