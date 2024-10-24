using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierVMs
{
    public partial class BasBSupplierTemplateVM : BaseTemplateVM
    {
        public ExcelPropety address_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.address);
        public ExcelPropety contacts_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.contacts);
        public ExcelPropety description_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.description);
        public ExcelPropety fax_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.fax);
        public ExcelPropety mail_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.mail);
        public ExcelPropety mobile_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.mobile);
        public ExcelPropety phone_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.phone);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.proprietorCode);
        public ExcelPropety suppilerFullname_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.suppilerFullname);
        public ExcelPropety suppilerFullnameAlias_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.suppilerFullnameAlias);
        public ExcelPropety suppilerFullnameEn_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.suppilerFullnameEn);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.supplierNameEn);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.supplierCode);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.whouseNo);
        public ExcelPropety zip_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.zip);
        public ExcelPropety companyCode_Excel = ExcelPropety.CreateProperty<BasBSupplier>(x => x.companyCode);

	    protected override void InitVM()
        {
        }

    }

    public class BasBSupplierImportVM : BaseImportVM<BasBSupplierTemplateVM, BasBSupplier>
    {

    }

}
