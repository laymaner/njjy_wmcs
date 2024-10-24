using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcVMs
{
    public partial class WmsItnQcTemplateVM : BaseTemplateVM
    {
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.docTypeCode);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.areaNo);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.extend9);
        public ExcelPropety itnQcLocNo_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.itnQcLocNo);
        public ExcelPropety itnQcNo_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.itnQcNo);
        public ExcelPropety itnQcStatus_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.itnQcStatus);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.orderDesc);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.proprietorCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQc>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnQcImportVM : BaseImportVM<WmsItnQcTemplateVM, WmsItnQc>
    {

    }

}
