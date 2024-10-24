using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs
{
    public partial class BasWErpWhouseBinTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.binNo);
        public ExcelPropety delFlag_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.delFlag);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.erpWhouseNo);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.regionNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWErpWhouseBin>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWErpWhouseBinImportVM : BaseImportVM<BasWErpWhouseBinTemplateVM, BasWErpWhouseBin>
    {

    }

}
