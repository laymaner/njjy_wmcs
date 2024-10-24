using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockReconcileResultHisVMs
{
    public partial class WmsStockReconcileResultHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety differQty_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.differQty);
        public ExcelPropety erpStockNo_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.erpStockNo);
        public ExcelPropety erpStockQty_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.erpStockQty);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.erpWhouseNo);
        public ExcelPropety materialCategoryCode_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.materialCategoryCode);
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.materialCode);
        public ExcelPropety materialTypeCode_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.materialTypeCode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.proprietorCode);
        public ExcelPropety reconcileNo_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.reconcileNo);
        public ExcelPropety reconcileOperator_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.reconcileOperator);
        public ExcelPropety reconcileTime_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.reconcileTime);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.whouseNo);
        public ExcelPropety wmsStockQty_Excel = ExcelPropety.CreateProperty<WmsStockReconcileResultHis>(x => x.wmsStockQty);

	    protected override void InitVM()
        {
        }

    }

    public class WmsStockReconcileResultHisImportVM : BaseImportVM<WmsStockReconcileResultHisTemplateVM, WmsStockReconcileResultHis>
    {

    }

}
