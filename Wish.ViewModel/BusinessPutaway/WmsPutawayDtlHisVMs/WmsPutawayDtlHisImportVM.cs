using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayDtlHisVMs
{
    public partial class WmsPutawayDtlHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.binNo);
        public ExcelPropety docTypeCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.docTypeCode);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.extend9);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.inspectionResult);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.materialCode);
        [Display(Name = "物料编码")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.materialName);
        [Display(Name = "物料规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.materialSpec);
        public ExcelPropety orderDtlId_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.orderDtlId);
        [Display(Name = "关联单据编号")]
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.orderNo);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.palletBarcode);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.proprietorCode);
        [Display(Name = "上架库位")]
        public ExcelPropety ptaBinNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.ptaBinNo);
        [Display(Name = "状态")]
        public ExcelPropety putawayDtlStatus_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.putawayDtlStatus);
        public ExcelPropety putawayNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.putawayNo);
        public ExcelPropety recordId_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.recordId);
        [Display(Name = "数量")]
        public ExcelPropety recordQty_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.recordQty);
        [Display(Name = "库区编号")]
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.regionNo);
        [Display(Name = "巷道")]
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.roadwayNo);
        [Display(Name = "SKU编码")]
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.stockDtlId);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.supplierCode);
        [Display(Name = "供方名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.supplierNameEn);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.whouseNo);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsPutawayDtlHis>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutawayDtlHisImportVM : BaseImportVM<WmsPutawayDtlHisTemplateVM, WmsPutawayDtlHis>
    {

    }

}
