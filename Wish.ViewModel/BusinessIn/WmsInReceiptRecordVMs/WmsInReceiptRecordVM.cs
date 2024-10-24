using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs
{
    public partial class WmsInReceiptRecordVM : BaseCRUDVM<WmsInReceiptRecord>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WmsInReceiptRecordVM));
        public WmsInReceiptRecordVM()
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
