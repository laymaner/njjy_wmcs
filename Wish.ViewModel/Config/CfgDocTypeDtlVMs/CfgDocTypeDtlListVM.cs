using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeDtlVMs
{
    public partial class CfgDocTypeDtlListVM : BasePagedListVM<CfgDocTypeDtl_View, CfgDocTypeDtlSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgDocTypeDtl_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgDocTypeDtl_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.businessModuleCode),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.paramCode),
                this.MakeGridHeader(x => x.paramValueCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgDocTypeDtl_View> GetSearchQuery()
        {
            var query = DC.Set<CfgDocTypeDtl>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.businessModuleCode, x=>x.businessModuleCode)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.paramCode, x=>x.paramCode)
                .CheckContain(Searcher.paramValueCode, x=>x.paramValueCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new CfgDocTypeDtl_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    businessModuleCode = x.businessModuleCode,
                    docTypeCode = x.docTypeCode,
                    paramCode = x.paramCode,
                    paramValueCode = x.paramValueCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgDocTypeDtl_View : CfgDocTypeDtl{

    }
}
