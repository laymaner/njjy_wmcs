using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;
using log4net;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{

    public partial class InterfaceConfigVM : BaseCRUDVM<InterfaceConfig>
    {
        private static ILog logger = LogManager.GetLogger(typeof(InterfaceConfigVM));
        public InterfaceConfigVM()
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
