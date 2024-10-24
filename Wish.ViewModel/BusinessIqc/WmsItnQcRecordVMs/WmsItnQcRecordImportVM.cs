using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcRecordVMs
{
    public partial class WmsItnQcRecordTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.areaNo);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.extend9);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.inspectionResult);
        public ExcelPropety itnQcDtlId_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.itnQcDtlId);
        public ExcelPropety itnQcLocNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.itnQcLocNo);
        public ExcelPropety itnQcNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.itnQcNo);
        public ExcelPropety itnQcStatus_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.itnQcStatus);
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.materialName);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.materialCode);
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.materialSpec);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.palletBarcode);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.proprietorCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.stockDtlId);
        public ExcelPropety stockQty_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.stockQty);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.supplierNameEn);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.supplierCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnQcRecord>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnQcRecordImportVM : BaseImportVM<WmsItnQcRecordTemplateVM, WmsItnQcRecord>
    {

    }

}
