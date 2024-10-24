using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialCategoryVMs
{
    public partial class BasBMaterialCategoryTemplateVM : BaseTemplateVM
    {
        public ExcelPropety delayDays_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.delayDays);
        public ExcelPropety materialFlag_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialFlag);
        public ExcelPropety isAutoDelay_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.isAutoDelay);
        public ExcelPropety materialCategoryDesc_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialCategoryDesc);
        public ExcelPropety materialCategoryName_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialCategoryName);
        public ExcelPropety materialCategoryNameAlias_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialCategoryNameAlias);
        public ExcelPropety materialCategoryNameEn_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialCategoryNameEn);
        public ExcelPropety materialCategoryCode_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.materialCategoryCode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.proprietorCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBMaterialCategory>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasBMaterialCategoryImportVM : BaseImportVM<BasBMaterialCategoryTemplateVM, BasBMaterialCategory>
    {

    }

}
