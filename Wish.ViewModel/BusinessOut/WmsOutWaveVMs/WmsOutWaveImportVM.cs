using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutWaveVMs
{
    public partial class WmsOutWaveTemplateVM : BaseTemplateVM
    {
        public ExcelPropety allotFlag_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.allotFlag);
        [Display(Name = "分配操作人")]
        public ExcelPropety allotOperator_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.allotOperator);
        [Display(Name = "分配开始时间")]
        public ExcelPropety allotTime_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.allotTime);
        public ExcelPropety deliveryLocNo_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.deliveryLocNo);
        [Display(Name = "单据类型")]
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.docTypeCode);
        public ExcelPropety issuedFlag_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.issuedFlag);
        public ExcelPropety issuedOperator_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.issuedOperator);
        public ExcelPropety issuedResult_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.issuedResult);
        public ExcelPropety issuedTime_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.issuedTime);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.operationReason);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.proprietorCode);
        [Display(Name = "波次号")]
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.waveNo);
        [Display(Name = "波次单状态")]
        public ExcelPropety waveStatus_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.waveStatus);
        [Display(Name = "波次类型")]
        public ExcelPropety waveType_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.waveType);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutWave>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutWaveImportVM : BaseImportVM<WmsOutWaveTemplateVM, WmsOutWave>
    {

    }

}
