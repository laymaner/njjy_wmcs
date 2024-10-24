using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialTypeVMs
{
    public partial class BasBMaterialTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety materialTypeDesc_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.materialTypeDesc);
        public ExcelPropety materialTypeName_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.materialTypeName);
        public ExcelPropety materialTypeNameAlias_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.materialTypeNameAlias);
        public ExcelPropety materialTypeNameEn_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.materialTypeNameEn);
        public ExcelPropety materialTypeCode_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.materialTypeCode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.proprietorCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBMaterialType>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasBMaterialTypeImportVM : BaseImportVM<BasBMaterialTypeTemplateVM, BasBMaterialType>
    {

    }

}
