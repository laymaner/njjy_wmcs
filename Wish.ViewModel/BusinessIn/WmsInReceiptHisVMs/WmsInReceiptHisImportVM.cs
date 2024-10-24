using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptHisVMs
{
    public partial class WmsInReceiptHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.binNo);
        public ExcelPropety cvCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.cvCode);
        public ExcelPropety cvName_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.cvName);
        public ExcelPropety cvNameAlias_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.cvNameAlias);
        public ExcelPropety cvNameEn_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.cvNameEn);
        public ExcelPropety cvType_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.cvType);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.docTypeCode);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.extend9);
        public ExcelPropety externalInId_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.externalInId);
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.externalInNo);
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.inOutTypeNo);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.operationReason);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.orderDesc);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.proprietorCode);
        public ExcelPropety receipter_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.receipter);
        public ExcelPropety receiptNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.receiptNo);
        public ExcelPropety receiptStatus_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.receiptStatus);
        public ExcelPropety receiptTime_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.receiptTime);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.regionNo);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.sourceBy);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.ticketNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInReceiptHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInReceiptHisImportVM : BaseImportVM<WmsInReceiptHisTemplateVM, WmsInReceiptHis>
    {

    }

}
