using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderHisVMs
{
    public partial class WmsInOrderHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "区域编码")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.areaNo);
        public ExcelPropety cvCode_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.cvCode);
        public ExcelPropety cvName_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.cvName);
        public ExcelPropety cvNameAlias_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.cvNameAlias);
        public ExcelPropety cvNameEn_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.cvNameEn);
        [Display(Name = "客供类型")]
        public ExcelPropety cvType_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.cvType);
        public ExcelPropety deliverMode_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.deliverMode);
        [Display(Name = "单据类型")]
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.docTypeCode);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.extend9);
        public ExcelPropety externalInId_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.externalInId);
        [Display(Name = "外部入库单号")]
        public ExcelPropety externalInNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.externalInNo);
        [Display(Name = "入库单号")]
        public ExcelPropety inNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.inNo);
        public ExcelPropety inOutName_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.inOutName);
        public ExcelPropety inOutTypeNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.inOutTypeNo);
        [Display(Name = "入库单状态")]
        public ExcelPropety inStatus_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.inStatus);
        [Display(Name = "操作原因")]
        public ExcelPropety operationReason_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.operationReason);
        [Display(Name = "备注说明")]
        public ExcelPropety orderDesc_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.orderDesc);
        public ExcelPropety planArrivalDate_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.planArrivalDate);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.proprietorCode);
        [Display(Name = "数据来源")]
        public ExcelPropety sourceBy_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.sourceBy);
        public ExcelPropety ticketNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.ticketNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsInOrderHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsInOrderHisImportVM : BaseImportVM<WmsInOrderHisTemplateVM, WmsInOrderHis>
    {

    }

}
