using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceHisVMs
{
    public partial class WmsOutInvoiceHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety cvCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.cvCode);
        public ExcelPropety cvName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.cvName);
        public ExcelPropety cvNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.cvNameAlias);
        public ExcelPropety cvNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.cvNameEn);
        public ExcelPropety cvType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.cvType);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.docTypeCode);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.extend9);
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.externalOutDtlId);
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.externalOutNo);
        public ExcelPropety fpName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.fpName);
        public ExcelPropety fpNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.fpNo);
        public ExcelPropety fpQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.fpQty);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.inOutTypeNo);
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.invoiceNo);
        public ExcelPropety invoiceStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.invoiceStatus);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.operationReason);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.orderDesc);
        [Display(Name = "单据优先级")]
        public ExcelPropety orderPriority_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.orderPriority);
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.productLocation);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.proprietorCode);
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.sourceBy);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.ticketNo);
        public ExcelPropety ticketType_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.ticketType);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.whouseNo);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.projectNo);
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceHis>(x => x.waveNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceHisImportVM : BaseImportVM<WmsOutInvoiceHisTemplateVM, WmsOutInvoiceHis>
    {

    }

}
