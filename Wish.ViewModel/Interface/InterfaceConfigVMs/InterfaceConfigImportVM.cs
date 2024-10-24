using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{
    public partial class InterfaceConfigTemplateVM : BaseTemplateVM
    {
        public ExcelPropety interfaceCode_Excel = ExcelPropety.CreateProperty<InterfaceConfig>(x => x.interfaceCode);
        public ExcelPropety interfaceName_Excel = ExcelPropety.CreateProperty<InterfaceConfig>(x => x.interfaceName);
        public ExcelPropety interfaceUrl_Excel = ExcelPropety.CreateProperty<InterfaceConfig>(x => x.interfaceUrl);
        public ExcelPropety retryMaxTimes_Excel = ExcelPropety.CreateProperty<InterfaceConfig>(x => x.retryMaxTimes);
        public ExcelPropety retryInterval_Excel = ExcelPropety.CreateProperty<InterfaceConfig>(x => x.retryInterval);

	    protected override void InitVM()
        {
        }

    }

    public class InterfaceConfigImportVM : BaseImportVM<InterfaceConfigTemplateVM, InterfaceConfig>
    {

    }

}
