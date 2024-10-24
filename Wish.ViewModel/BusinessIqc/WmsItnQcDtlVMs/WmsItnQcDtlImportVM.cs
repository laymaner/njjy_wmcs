using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcDtlVMs
{
    public partial class WmsItnQcDtlTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.areaNo);
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.batchNo);
        public ExcelPropety confirmQty_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.confirmQty);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.erpWhouseNo);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.inspectionResult);
        public ExcelPropety itnQcDtlStatus_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.itnQcDtlStatus);
        public ExcelPropety itnQcNo_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.itnQcNo);
        public ExcelPropety itnQcQty_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.itnQcQty);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.materialSpec);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.proprietorCode);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQcDtl>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnQcDtlImportVM : BaseImportVM<WmsItnQcDtlTemplateVM, WmsItnQcDtl>
    {

    }

}
