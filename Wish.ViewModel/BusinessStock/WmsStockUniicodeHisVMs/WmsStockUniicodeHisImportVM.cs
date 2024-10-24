using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockUniicodeHisVMs
{
    public partial class WmsStockUniicodeHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.areaNo);
        [Display(Name = "批次")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.batchNo);
        [Display(Name = "DC")]
        public ExcelPropety dataCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.dataCode);
        [Display(Name = "有效期冻结")]
        public ExcelPropety delayFrozenFlag_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.delayFrozenFlag);
        [Display(Name = "有效期冻结原因")]
        public ExcelPropety delayFrozenReason_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.delayFrozenReason);
        [Display(Name = "延长有效期原因")]
        public ExcelPropety delayReason_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.delayReason);
        [Display(Name = "延期次数")]
        public ExcelPropety delayTimes_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.delayTimes);
        [Display(Name = "延长有效期至日期")]
        public ExcelPropety delayToEndDate_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.delayToEndDate);
        [Display(Name = "是否烘干报废")]
        public ExcelPropety driedScrapFlag_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.driedScrapFlag);
        [Display(Name = "已烘干次数")]
        public ExcelPropety driedTimes_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.driedTimes);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.erpWhouseNo);
        [Display(Name = "失效日期")]
        public ExcelPropety expDate_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.expDate);
        [Display(Name = "暴露冻结标志")]
        public ExcelPropety exposeFrozenFlag_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.exposeFrozenFlag);
        [Display(Name = "暴露冻结原因")]
        public ExcelPropety exposeFrozenReason_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.exposeFrozenReason);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.chipSize);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.chipThickness);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.chipModel);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.dafType);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.extend9);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.inspectionResult);
        [Display(Name = "入库时间")]
        public ExcelPropety inwhTime_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.inwhTime);
        [Display(Name = "剩余湿敏时长")]
        public ExcelPropety leftMslTimes_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.leftMslTimes);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.materialCode);
        [Display(Name = "规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.materialSpec);
        [Display(Name = "MSL等级")]
        public ExcelPropety mslGradeCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.mslGradeCode);
        [Display(Name = "占用数量")]
        public ExcelPropety occupyQty_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.occupyQty);
        [Display(Name = "封包时间")]
        public ExcelPropety packageTime_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.packageTime);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.palletBarcode);
        public ExcelPropety positionNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.positionNo);
        [Display(Name = "生产日期")]
        public ExcelPropety productDate_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.productDate);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.proprietorCode);
        [Display(Name = "实际暴露时长")]
        public ExcelPropety realExposeTimes_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.realExposeTimes);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.stockDtlId);
        [Display(Name = "库存数量")]
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.qty);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.supplierCode);
        [Display(Name = "供应商暴露时长")]
        public ExcelPropety supplierExposeTimes_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.supplierExposeTimes);
        [Display(Name = "供应商名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.supplierNameEn);
        [Display(Name = "包装条码/SN码")]
        public ExcelPropety uniicode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.uniicode);
        [Display(Name = "拆封状态")]
        public ExcelPropety unpackStatus_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.unpackStatus);
        [Display(Name = "开封时间")]
        public ExcelPropety unpackTime_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.unpackTime);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.whouseNo);
        public ExcelPropety fileedId_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.fileedId);
        [Display(Name = "附件名称")]
        public ExcelPropety fileedName_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.fileedName);
        [Display(Name = "备用项目号")]
        public ExcelPropety projectNoBak_Excel = ExcelPropety.CreateProperty<WmsStockUniicodeHis>(x => x.projectNoBak);

	    protected override void InitVM()
        {
        }

    }

    public class WmsStockUniicodeHisImportVM : BaseImportVM<WmsStockUniicodeHisTemplateVM, WmsStockUniicodeHis>
    {

    }

}
