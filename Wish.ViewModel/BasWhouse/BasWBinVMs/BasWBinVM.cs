using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using log4net;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinVM : BaseCRUDVM<BasWBin>
    {
        private static ILog logger = LogManager.GetLogger(typeof(BasWBinVM));
        public BasWBinVM()
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
