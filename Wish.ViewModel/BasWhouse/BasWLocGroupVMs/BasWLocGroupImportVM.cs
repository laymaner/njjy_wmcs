using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocGroupVMs
{
    public partial class BasWLocGroupTemplateVM : BaseTemplateVM
    {
        public ExcelPropety locGroupName_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.locGroupName);
        public ExcelPropety locGroupNameAlias_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.locGroupNameAlias);
        public ExcelPropety locGroupNameEn_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.locGroupNameEn);
        public ExcelPropety locGroupNo_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.locGroupNo);
        public ExcelPropety locGroupType_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.locGroupType);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWLocGroup>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWLocGroupImportVM : BaseImportVM<BasWLocGroupTemplateVM, BasWLocGroup>
    {

    }

}
