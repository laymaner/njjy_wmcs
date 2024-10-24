using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseVMs
{
    public partial class BasWErpWhouseTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.areaNo);
        public ExcelPropety erpWhouseName_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.erpWhouseName);
        public ExcelPropety erpWhouseNameAlias_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.erpWhouseNameAlias);
        public ExcelPropety erpWhouseNameEn_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.erpWhouseNameEn);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.erpWhouseNo);
        public ExcelPropety erpWhouseType_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.erpWhouseType);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouse>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWErpWhouseImportVM : BaseImportVM<BasWErpWhouseTemplateVM, BasWErpWhouse>
    {

    }

}
