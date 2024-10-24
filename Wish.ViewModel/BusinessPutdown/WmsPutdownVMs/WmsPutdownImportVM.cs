using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.binNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.docTypeCode);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.loadedType);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.orderNo);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.palletBarcode);
        public ExcelPropety pickupMethod_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.pickupMethod);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.proprietorCode);
        public ExcelPropety putdownNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.putdownNo);
        public ExcelPropety putdownStatus_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.putdownStatus);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.regionNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.stockCode);
        public ExcelPropety targetLocNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.targetLocNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutdown>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutdownImportVM : BaseImportVM<WmsPutdownTemplateVM, WmsPutdown>
    {

    }

}
