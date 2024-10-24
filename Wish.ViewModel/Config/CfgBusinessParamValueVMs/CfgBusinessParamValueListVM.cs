using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamValueVMs
{
    public partial class CfgBusinessParamValueListVM : BasePagedListVM<CfgBusinessParamValue_View, CfgBusinessParamValueSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgBusinessParamValue_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgBusinessParamValue_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.businessModuleCode),
                this.MakeGridHeader(x => x.defaultFlag),
                this.MakeGridHeader(x => x.paramCode),
                this.MakeGridHeader(x => x.paramValueCode),
                this.MakeGridHeader(x => x.paramValueDesc),
                this.MakeGridHeader(x => x.paramValueName),
                this.MakeGridHeader(x => x.paramValueNameAlias),
                this.MakeGridHeader(x => x.paramValueNameEn),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgBusinessParamValue_View> GetSearchQuery()
        {
            var query = DC.Set<CfgBusinessParamValue>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.businessModuleCode, x=>x.businessModuleCode)
                .CheckContain(Searcher.paramCode, x=>x.paramCode)
                .CheckContain(Searcher.paramValueCode, x=>x.paramValueCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgBusinessParamValue_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    businessModuleCode = x.businessModuleCode,
                    defaultFlag = x.defaultFlag,
                    paramCode = x.paramCode,
                    paramValueCode = x.paramValueCode,
                    paramValueDesc = x.paramValueDesc,
                    paramValueName = x.paramValueName,
                    paramValueNameAlias = x.paramValueNameAlias,
                    paramValueNameEn = x.paramValueNameEn,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgBusinessParamValue_View : CfgBusinessParamValue{

    }
}
