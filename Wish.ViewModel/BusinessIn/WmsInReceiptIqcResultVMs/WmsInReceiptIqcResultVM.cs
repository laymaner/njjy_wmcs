using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Wish.ViewModel.Common.Dtos;
using log4net;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{
    public partial class WmsInReceiptIqcResultVM : BaseCRUDVM<WmsInReceiptIqcResult>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WmsInReceiptIqcResultVM));
        public WmsInReceiptIqcResultVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
