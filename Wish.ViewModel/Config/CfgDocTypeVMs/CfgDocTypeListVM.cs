using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeVMs
{
    public partial class CfgDocTypeListVM : BasePagedListVM<CfgDocType_View, CfgDocTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgDocType_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgDocType_View>>{
                this.MakeGridHeader(x => x.businessCode),
                this.MakeGridHeader(x => x.cvType),
                this.MakeGridHeader(x => x.developFlag),
                this.MakeGridHeader(x => x.docPriority),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.docTypeDesc),
                this.MakeGridHeader(x => x.docTypeName),
                this.MakeGridHeader(x => x.docTypeNameAlias),
                this.MakeGridHeader(x => x.docTypeNameEn),
                this.MakeGridHeader(x => x.externalDocTypeCode),
                this.MakeGridHeader(x => x.taskMaxQty),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgDocType_View> GetSearchQuery()
        {
            var query = DC.Set<CfgDocType>()
                .CheckContain(Searcher.businessCode, x=>x.businessCode)
                .CheckContain(Searcher.cvType, x=>x.cvType)
                .CheckEqual(Searcher.developFlag, x=>x.developFlag)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new CfgDocType_View
                {
				    ID = x.ID,
                    businessCode = x.businessCode,
                    cvType = x.cvType,
                    developFlag = x.developFlag,
                    docPriority = x.docPriority,
                    docTypeCode = x.docTypeCode,
                    docTypeDesc = x.docTypeDesc,
                    docTypeName = x.docTypeName,
                    docTypeNameAlias = x.docTypeNameAlias,
                    docTypeNameEn = x.docTypeNameEn,
                    externalDocTypeCode = x.externalDocTypeCode,
                    taskMaxQty = x.taskMaxQty,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgDocType_View : CfgDocType{

    }
}
