using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.TaskConfig.Model;
using log4net;


namespace Wish.ViewModel.WcsCmd.SrmCmdVMs
{
    public partial class SrmCmdVM : BaseCRUDVM<SrmCmd>
    {
        private static ILog logger = LogManager.GetLogger(typeof(SrmCmdVM));
        public SrmCmdVM()
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
