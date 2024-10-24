using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierBinVMs
{
    public partial class BasBSupplierBinTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.binNo);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.regionNo);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.supplierCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.whouseNo);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<BasBSupplierBin>(x => x.erpWhouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasBSupplierBinImportVM : BaseImportVM<BasBSupplierBinTemplateVM, BasBSupplierBin>
    {

    }

}
