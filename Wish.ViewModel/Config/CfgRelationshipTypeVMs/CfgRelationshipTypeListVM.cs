using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipTypeVMs
{
    public partial class CfgRelationshipTypeListVM : BasePagedListVM<CfgRelationshipType_View, CfgRelationshipTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<CfgRelationshipType_View>> InitGridHeader()
        {
            return new List<GridColumn<CfgRelationshipType_View>>{
                this.MakeGridHeader(x => x.developFlag),
                this.MakeGridHeader(x => x.leftAttributes),
                this.MakeGridHeader(x => x.leftTable),
                this.MakeGridHeader(x => x.leftTableCode),
                this.MakeGridHeader(x => x.leftTableCodeLabel),
                this.MakeGridHeader(x => x.leftTableCodeLabelAlias),
                this.MakeGridHeader(x => x.leftTableCodeLabelEn),
                this.MakeGridHeader(x => x.leftTableName),
                this.MakeGridHeader(x => x.leftTableNameAlias),
                this.MakeGridHeader(x => x.leftTableNameEn),
                this.MakeGridHeader(x => x.leftTableNameLabel),
                this.MakeGridHeader(x => x.leftTableNameLabelAlias),
                this.MakeGridHeader(x => x.leftTableNameLabelEn),
                this.MakeGridHeader(x => x.leftWhouse),
                this.MakeGridHeader(x => x.relationshipTypeCode),
                this.MakeGridHeader(x => x.relationshipTypeDesc),
                this.MakeGridHeader(x => x.relationshipTypeName),
                this.MakeGridHeader(x => x.relationshipTypeNameAlias),
                this.MakeGridHeader(x => x.relationshipTypeNameEn),
                this.MakeGridHeader(x => x.rightAttributes),
                this.MakeGridHeader(x => x.rightTable),
                this.MakeGridHeader(x => x.rightTableCode),
                this.MakeGridHeader(x => x.rightTableCodeLabel),
                this.MakeGridHeader(x => x.rightTableCodeLabelAlias),
                this.MakeGridHeader(x => x.rightTableCodeLabelEn),
                this.MakeGridHeader(x => x.rightTableName),
                this.MakeGridHeader(x => x.rightTableNameAlias),
                this.MakeGridHeader(x => x.rightTableNameEn),
                this.MakeGridHeader(x => x.rightTableNameLabel),
                this.MakeGridHeader(x => x.rightTableNameLabelAlias),
                this.MakeGridHeader(x => x.rightTableNameLabelEn),
                this.MakeGridHeader(x => x.rightTablePriority),
                this.MakeGridHeader(x => x.rightWhouse),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CfgRelationshipType_View> GetSearchQuery()
        {
            var query = DC.Set<CfgRelationshipType>()
                .CheckEqual(Searcher.developFlag, x=>x.developFlag)
                .CheckContain(Searcher.leftTable, x=>x.leftTable)
                .CheckContain(Searcher.leftTableCode, x=>x.leftTableCode)
                .CheckContain(Searcher.leftTableCodeLabel, x=>x.leftTableCodeLabel)
                .CheckContain(Searcher.leftTableName, x=>x.leftTableName)
                .CheckEqual(Searcher.leftWhouse, x=>x.leftWhouse)
                .CheckContain(Searcher.relationshipTypeCode, x=>x.relationshipTypeCode)
                .CheckContain(Searcher.rightTable, x=>x.rightTable)
                .CheckContain(Searcher.rightTableCode, x=>x.rightTableCode)
                .CheckEqual(Searcher.rightWhouse, x=>x.rightWhouse)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new CfgRelationshipType_View
                {
				    ID = x.ID,
                    developFlag = x.developFlag,
                    leftAttributes = x.leftAttributes,
                    leftTable = x.leftTable,
                    leftTableCode = x.leftTableCode,
                    leftTableCodeLabel = x.leftTableCodeLabel,
                    leftTableCodeLabelAlias = x.leftTableCodeLabelAlias,
                    leftTableCodeLabelEn = x.leftTableCodeLabelEn,
                    leftTableName = x.leftTableName,
                    leftTableNameAlias = x.leftTableNameAlias,
                    leftTableNameEn = x.leftTableNameEn,
                    leftTableNameLabel = x.leftTableNameLabel,
                    leftTableNameLabelAlias = x.leftTableNameLabelAlias,
                    leftTableNameLabelEn = x.leftTableNameLabelEn,
                    leftWhouse = x.leftWhouse,
                    relationshipTypeCode = x.relationshipTypeCode,
                    relationshipTypeDesc = x.relationshipTypeDesc,
                    relationshipTypeName = x.relationshipTypeName,
                    relationshipTypeNameAlias = x.relationshipTypeNameAlias,
                    relationshipTypeNameEn = x.relationshipTypeNameEn,
                    rightAttributes = x.rightAttributes,
                    rightTable = x.rightTable,
                    rightTableCode = x.rightTableCode,
                    rightTableCodeLabel = x.rightTableCodeLabel,
                    rightTableCodeLabelAlias = x.rightTableCodeLabelAlias,
                    rightTableCodeLabelEn = x.rightTableCodeLabelEn,
                    rightTableName = x.rightTableName,
                    rightTableNameAlias = x.rightTableNameAlias,
                    rightTableNameEn = x.rightTableNameEn,
                    rightTableNameLabel = x.rightTableNameLabel,
                    rightTableNameLabelAlias = x.rightTableNameLabelAlias,
                    rightTableNameLabelEn = x.rightTableNameLabelEn,
                    rightTablePriority = x.rightTablePriority,
                    rightWhouse = x.rightWhouse,
                    usedFlag = x.usedFlag,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CfgRelationshipType_View : CfgRelationshipType{

    }
}
