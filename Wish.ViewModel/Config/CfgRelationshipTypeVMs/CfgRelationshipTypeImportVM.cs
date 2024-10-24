using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipTypeVMs
{
    public partial class CfgRelationshipTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety developFlag_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.developFlag);
        public ExcelPropety leftAttributes_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftAttributes);
        public ExcelPropety leftTable_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTable);
        public ExcelPropety leftTableCode_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableCode);
        public ExcelPropety leftTableCodeLabel_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableCodeLabel);
        public ExcelPropety leftTableCodeLabelAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableCodeLabelAlias);
        public ExcelPropety leftTableCodeLabelEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableCodeLabelEn);
        public ExcelPropety leftTableName_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableName);
        public ExcelPropety leftTableNameAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableNameAlias);
        public ExcelPropety leftTableNameEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableNameEn);
        public ExcelPropety leftTableNameLabel_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableNameLabel);
        public ExcelPropety leftTableNameLabelAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableNameLabelAlias);
        public ExcelPropety leftTableNameLabelEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftTableNameLabelEn);
        public ExcelPropety leftWhouse_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.leftWhouse);
        public ExcelPropety relationshipTypeCode_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.relationshipTypeCode);
        public ExcelPropety relationshipTypeDesc_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.relationshipTypeDesc);
        public ExcelPropety relationshipTypeName_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.relationshipTypeName);
        public ExcelPropety relationshipTypeNameAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.relationshipTypeNameAlias);
        public ExcelPropety relationshipTypeNameEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.relationshipTypeNameEn);
        public ExcelPropety rightAttributes_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightAttributes);
        public ExcelPropety rightTable_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTable);
        public ExcelPropety rightTableCode_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableCode);
        public ExcelPropety rightTableCodeLabel_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableCodeLabel);
        public ExcelPropety rightTableCodeLabelAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableCodeLabelAlias);
        public ExcelPropety rightTableCodeLabelEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableCodeLabelEn);
        public ExcelPropety rightTableName_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableName);
        public ExcelPropety rightTableNameAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableNameAlias);
        public ExcelPropety rightTableNameEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableNameEn);
        public ExcelPropety rightTableNameLabel_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableNameLabel);
        public ExcelPropety rightTableNameLabelAlias_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableNameLabelAlias);
        public ExcelPropety rightTableNameLabelEn_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTableNameLabelEn);
        public ExcelPropety rightTablePriority_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightTablePriority);
        public ExcelPropety rightWhouse_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.rightWhouse);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgRelationshipType>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class CfgRelationshipTypeImportVM : BaseImportVM<CfgRelationshipTypeTemplateVM, CfgRelationshipType>
    {

    }

}
