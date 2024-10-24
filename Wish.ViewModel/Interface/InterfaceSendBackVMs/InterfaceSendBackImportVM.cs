using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackTemplateVM : BaseTemplateVM
    {
        public ExcelPropety interfaceCode_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.interfaceCode);
        public ExcelPropety interfaceName_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.interfaceName);
        public ExcelPropety interfaceSendInfo_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.interfaceSendInfo);
        public ExcelPropety interfaceResult_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.interfaceResult);
        public ExcelPropety returnFlag_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.returnFlag);
        public ExcelPropety returnTimes_Excel = ExcelPropety.CreateProperty<InterfaceSendBack>(x => x.returnTimes);

	    protected override void InitVM()
        {
        }

    }

    public class InterfaceSendBackImportVM : BaseImportVM<InterfaceSendBackTemplateVM, InterfaceSendBack>
    {

    }

}
