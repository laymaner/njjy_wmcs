using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBCustomerVMs
{
    public partial class BasBCustomerTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Address_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Address);
        public ExcelPropety Contacts_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Contacts);
        public ExcelPropety CustomerFullname_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerFullname);
        public ExcelPropety CustomerFullnameAlias_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerFullnameAlias);
        public ExcelPropety CustomerFullnameEn_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerFullnameEn);
        public ExcelPropety CustomerName_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerName);
        public ExcelPropety CustomerNameAlias_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerNameAlias);
        public ExcelPropety CustomerNameEn_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerNameEn);
        public ExcelPropety CustomerCode_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.CustomerCode);
        public ExcelPropety Description_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Description);
        public ExcelPropety Fax_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Fax);
        public ExcelPropety Mail_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Mail);
        public ExcelPropety Mobile_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Mobile);
        public ExcelPropety Phone_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Phone);
        public ExcelPropety ProprietorCode_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.ProprietorCode);
        public ExcelPropety UsedFlag_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.UsedFlag);
        public ExcelPropety WhouseNo_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.WhouseNo);
        public ExcelPropety Zip_Excel = ExcelPropety.CreateProperty<BasBCustomer>(x => x.Zip);

	    protected override void InitVM()
        {
        }

    }

    public class BasBCustomerImportVM : BaseImportVM<BasBCustomerTemplateVM, BasBCustomer>
    {

    }

}
