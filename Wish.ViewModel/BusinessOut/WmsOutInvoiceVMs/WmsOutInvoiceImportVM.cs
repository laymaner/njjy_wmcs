using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceTemplateVM : BaseTemplateVM
    {
        public ExcelPropety cvCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.cvCode);
        [Display(Name = "客供名称")]
        public ExcelPropety cvName_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.cvName);
        public ExcelPropety cvNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.cvNameAlias);
        public ExcelPropety cvNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.cvNameEn);
        public ExcelPropety cvType_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.cvType);
        [Display(Name = "单据类型")]
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.docTypeCode);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.extend9);
        public ExcelPropety externalOutDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.externalOutDtlId);
        [Display(Name = "外部出库单号")]
        public ExcelPropety externalOutNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.externalOutNo);
        [Display(Name = "成品名称")]
        public ExcelPropety fpName_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.fpName);
        [Display(Name = "成品编码")]
        public ExcelPropety fpNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.fpNo);
        [Display(Name = "成品数量")]
        public ExcelPropety fpQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.fpQty);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.inOutTypeNo);
        [Display(Name = "发货单号")]
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.invoiceNo);
        [Display(Name = "发货单状态")]
        public ExcelPropety invoiceStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.invoiceStatus);
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.operationReason);
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.orderDesc);
        [Display(Name = "单据优先级")]
        public ExcelPropety orderPriority_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.orderPriority);
        [Display(Name = "生产地点")]
        public ExcelPropety productLocation_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.productLocation);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.proprietorCode);
        [Display(Name = "数据来源")]
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.sourceBy);
        [Display(Name = "工单号")]
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.ticketNo);
        public ExcelPropety ticketType_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.ticketType);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.whouseNo);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.projectNo);
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoice>(x => x.waveNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceImportVM : BaseImportVM<WmsOutInvoiceTemplateVM, WmsOutInvoice>
    {

    }

}
