using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionTypeVMs
{
    public partial class BasWRegionTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety regionTypeCode_Excel = ExcelPropety.CreateProperty<BasWRegionType>(x => x.regionTypeCode);
        public ExcelPropety regionTypeFlag_Excel = ExcelPropety.CreateProperty<BasWRegionType>(x => x.regionTypeFlag);
        public ExcelPropety regionTypeName_Excel = ExcelPropety.CreateProperty<BasWRegionType>(x => x.regionTypeName);
        public ExcelPropety regionTypeNameAlias_Excel = ExcelPropety.CreateProperty<BasWRegionType>(x => x.regionTypeNameAlias);
        public ExcelPropety regionTypeNameEn_Excel = ExcelPropety.CreateProperty<BasWRegionType>(x => x.regionTypeNameEn);

	    protected override void InitVM()
        {
        }

    }

    public class BasWRegionTypeImportVM : BaseImportVM<BasWRegionTypeTemplateVM, BasWRegionType>
    {

    }

}
