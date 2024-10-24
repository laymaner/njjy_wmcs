using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocVMs
{
    public partial class BasWLocTemplateVM : BaseTemplateVM
    {
        public ExcelPropety locGroupNo_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locGroupNo);
        public ExcelPropety locName_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locName);
        public ExcelPropety locNameAlias_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locNameAlias);
        public ExcelPropety locNameEn_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locNameEn);
        public ExcelPropety locNo_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locNo);
        public ExcelPropety locTypeCode_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.locTypeCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWLoc>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWLocImportVM : BaseImportVM<BasWLocTemplateVM, BasWLoc>
    {

    }

}
