using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutWaveHisVMs
{
    public partial class WmsOutWaveHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety allotFlag_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.allotFlag);
        public ExcelPropety allotOperator_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.allotOperator);
        public ExcelPropety allotTime_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.allotTime);
        public ExcelPropety deliveryLocNo_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.deliveryLocNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.docTypeCode);
        public ExcelPropety issuedFlag_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.issuedFlag);
        public ExcelPropety issuedOperator_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.issuedOperator);
        public ExcelPropety issuedResult_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.issuedResult);
        public ExcelPropety issuedTime_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.issuedTime);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.operationReason);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.proprietorCode);
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.waveNo);
        public ExcelPropety waveStatus_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.waveStatus);
        public ExcelPropety waveType_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.waveType);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutWaveHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutWaveHisImportVM : BaseImportVM<WmsOutWaveHisTemplateVM, WmsOutWaveHis>
    {

    }

}
