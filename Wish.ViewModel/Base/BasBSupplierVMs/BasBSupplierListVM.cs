using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierVMs
{
    public partial class BasBSupplierListVM : BasePagedListVM<BasBSupplier_View, BasBSupplierSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBSupplier_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBSupplier_View>>{
                this.MakeGridHeader(x => x.address),
                this.MakeGridHeader(x => x.contacts),
                this.MakeGridHeader(x => x.description),
                this.MakeGridHeader(x => x.fax),
                this.MakeGridHeader(x => x.mail),
                this.MakeGridHeader(x => x.mobile),
                this.MakeGridHeader(x => x.phone),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.suppilerFullname),
                this.MakeGridHeader(x => x.suppilerFullnameAlias),
                this.MakeGridHeader(x => x.suppilerFullnameEn),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.zip),
                this.MakeGridHeader(x => x.companyCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBSupplier_View> GetSearchQuery()
        {
            var query = DC.Set<BasBSupplier>()
                .CheckContain(Searcher.address, x=>x.address)
                .CheckContain(Searcher.contacts, x=>x.contacts)
                .CheckContain(Searcher.mail, x=>x.mail)
                .CheckContain(Searcher.mobile, x=>x.mobile)
                .CheckContain(Searcher.phone, x=>x.phone)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.companyCode, x=>x.companyCode)
                .Select(x => new BasBSupplier_View
                {
				    ID = x.ID,
                    address = x.address,
                    contacts = x.contacts,
                    description = x.description,
                    fax = x.fax,
                    mail = x.mail,
                    mobile = x.mobile,
                    phone = x.phone,
                    proprietorCode = x.proprietorCode,
                    suppilerFullname = x.suppilerFullname,
                    suppilerFullnameAlias = x.suppilerFullnameAlias,
                    suppilerFullnameEn = x.suppilerFullnameEn,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                    zip = x.zip,
                    companyCode = x.companyCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBSupplier_View : BasBSupplier{

    }
}
