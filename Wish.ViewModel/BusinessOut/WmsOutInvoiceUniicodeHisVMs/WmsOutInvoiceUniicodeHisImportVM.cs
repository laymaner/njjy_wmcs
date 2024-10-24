using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeHisVMs
{
    public partial class WmsOutInvoiceUniicodeHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "分配数量")]
        public ExcelPropety allotQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.allotQty);
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.areaNo);
        [Display(Name = "批次号")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.batchNo);
        [Display(Name = "DC")]
        public ExcelPropety dataCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.dataCode);
        [Display(Name = "延期原因")]
        public ExcelPropety delayReason_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.delayReason);
        [Display(Name = "延期次数")]
        public ExcelPropety delayTimes_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.delayTimes);
        public ExcelPropety delayToEndDate_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.delayToEndDate);
        [Display(Name = "是否烘干报废")]
        public ExcelPropety driedScrapFlag_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.driedScrapFlag);
        [Display(Name = "已烘干次数")]
        public ExcelPropety driedTimes_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.driedTimes);
        public ExcelPropety erpBinNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.erpBinNo);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.erpWhouseNo);
        [Display(Name = "失效日期")]
        public ExcelPropety expDate_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.expDate);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.extend9);
        [Display(Name = "质检结果")]
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.inspectionResult);
        public ExcelPropety invoiceDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.invoiceDtlId);
        [Display(Name = "发货单号")]
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.invoiceNo);
        public ExcelPropety invoiceRecordId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.invoiceRecordId);
        [Display(Name = "剩余湿敏时长")]
        public ExcelPropety leftMslTimes_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.leftMslTimes);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.materialSpec);
        [Display(Name = "MSL等级")]
        public ExcelPropety mslGradeCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.mslGradeCode);
        [Display(Name = "封包时间")]
        public ExcelPropety packageTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.packageTime);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.palletBarcode);
        [Display(Name = "拣选数量")]
        public ExcelPropety pickQty_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.pickQty);
        [Display(Name = "拣选任务编号")]
        public ExcelPropety pickTaskNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.pickTaskNo);
        [Display(Name = "生产日期")]
        public ExcelPropety productDate_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.productDate);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.proprietorCode);
        [Display(Name = "实际暴露时长")]
        public ExcelPropety realExposeTimes_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.realExposeTimes);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.stockDtlId);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.supplierCode);
        [Display(Name = "供应商暴露时长")]
        public ExcelPropety supplierExposeTimes_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.supplierExposeTimes);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.supplierNameEn);
        [Display(Name = "包装条码/SN码")]
        public ExcelPropety uniicode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.uniicode);
        [Display(Name = "开封时间")]
        public ExcelPropety unpackTime_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.unpackTime);
        [Display(Name = "波次号")]
        public ExcelPropety waveNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.waveNo);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.whouseNo);
        [Display(Name = "出库条码")]
        public ExcelPropety outBarcode_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.outBarcode);
        public ExcelPropety ouniiStatus_Excel = ExcelPropety.CreateProperty<WmsOutInvoiceUniicodeHis>(x => x.ouniiStatus);

	    protected override void InitVM()
        {
        }

    }

    public class WmsOutInvoiceUniicodeHisImportVM : BaseImportVM<WmsOutInvoiceUniicodeHisTemplateVM, WmsOutInvoiceUniicodeHis>
    {

    }

}
