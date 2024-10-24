using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBUnitVMs
{
    public partial class BasBUnitTemplateVM : BaseTemplateVM
    {
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.unitCode);
        public ExcelPropety unitName_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.unitName);
        public ExcelPropety unitNameAlias_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.unitNameAlias);
        public ExcelPropety unitNameEn_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.unitNameEn);
        public ExcelPropety unitType_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.unitType);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBUnit>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasBUnitImportVM : BaseImportVM<BasBUnitTemplateVM, BasBUnit>
    {

    }

}
