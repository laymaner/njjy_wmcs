using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceSendBackHisVMs
{
    public partial class InterfaceSendBackHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety interfaceCode_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.interfaceCode);
        public ExcelPropety interfaceName_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.interfaceName);
        public ExcelPropety interfaceSendInfo_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.interfaceSendInfo);
        public ExcelPropety interfaceResult_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.interfaceResult);
        public ExcelPropety returnFlag_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.returnFlag);
        public ExcelPropety returnTimes_Excel = ExcelPropety.CreateProperty<InterfaceSendBackHis>(x => x.returnTimes);

	    protected override void InitVM()
        {
        }

    }

    public class InterfaceSendBackHisImportVM : BaseImportVM<InterfaceSendBackHisTemplateVM, InterfaceSendBackHis>
    {

    }

}
