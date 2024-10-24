using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownHisVMs
{
    public partial class WmsPutdownHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.binNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.docTypeCode);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.loadedType);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.orderNo);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.palletBarcode);
        public ExcelPropety pickupMethod_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.pickupMethod);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.proprietorCode);
        public ExcelPropety putdownNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.putdownNo);
        public ExcelPropety putdownStatus_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.putdownStatus);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.regionNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.stockCode);
        public ExcelPropety targetLocNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.targetLocNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutdownHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutdownHisImportVM : BaseImportVM<WmsPutdownHisTemplateVM, WmsPutdownHis>
    {

    }

}
