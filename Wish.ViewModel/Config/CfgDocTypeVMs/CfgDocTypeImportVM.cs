using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeVMs
{
    public partial class CfgDocTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety businessCode_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.businessCode);
        public ExcelPropety cvType_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.cvType);
        public ExcelPropety developFlag_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.developFlag);
        public ExcelPropety docPriority_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docPriority);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docTypeCode);
        public ExcelPropety docTypeDesc_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docTypeDesc);
        public ExcelPropety docTypeName_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docTypeName);
        public ExcelPropety docTypeNameAlias_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docTypeNameAlias);
        public ExcelPropety docTypeNameEn_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.docTypeNameEn);
        public ExcelPropety externalDocTypeCode_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.externalDocTypeCode);
        public ExcelPropety taskMaxQty_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.taskMaxQty);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<CfgDocType>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class CfgDocTypeImportVM : BaseImportVM<CfgDocTypeTemplateVM, CfgDocType>
    {

    }

}
