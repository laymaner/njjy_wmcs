using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.DtoModel.Common.Dtos;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;



namespace Wish.ViewModel.BusinessIn.WmsInReceiptVMs
{
    public partial class WmsInReceiptVM : BaseCRUDVM<WmsInReceipt>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WmsInReceiptVM));
        public WmsInReceiptVM()
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
