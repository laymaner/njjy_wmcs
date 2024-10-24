using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using log4net;
using Wish.ViewModel.Interface.InterfaceConfigVMs;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigVM : BaseCRUDVM<DeviceConfig>
    {
        private static ILog logger = LogManager.GetLogger(typeof(DeviceConfigVM));
        public DeviceConfigVM()
        {
            SetInclude(x => x.StandardDevice);
            SetInclude(x => x.PlcConfig);
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
