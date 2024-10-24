using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysDictionaryVMs
{
    public partial class SysDictionaryTemplateVM : BaseTemplateVM
    {
        public ExcelPropety developFlag_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.developFlag);
        public ExcelPropety dictionaryCode_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryCode);
        public ExcelPropety dictionaryItemCode_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryItemCode);
        public ExcelPropety dictionaryItemName_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryItemName);
        public ExcelPropety dictionaryItemNameAlias_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryItemNameAlias);
        public ExcelPropety dictionaryItemNameEn_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryItemNameEn);
        public ExcelPropety dictionaryName_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryName);
        public ExcelPropety dictionaryNameAlias_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryNameAlias);
        public ExcelPropety dictionaryNameEn_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.dictionaryNameEn);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<SysDictionary>(x => x.usedFlag);

	    protected override void InitVM()
        {
        }

    }

    public class SysDictionaryImportVM : BaseImportVM<SysDictionaryTemplateVM, SysDictionary>
    {

    }

}
