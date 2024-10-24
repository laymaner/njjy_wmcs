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
    public partial class BasBCustomerSearcher : BaseSearcher
    {
        public String Address { get; set; }
        public String Contacts { get; set; }
        public String CustomerFullname { get; set; }
        public String CustomerName { get; set; }
        public String CustomerCode { get; set; }
        public String Description { get; set; }
        public String Fax { get; set; }
        public String Mail { get; set; }
        public String Mobile { get; set; }
        public String Phone { get; set; }
        public String ProprietorCode { get; set; }
        public String WhouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
