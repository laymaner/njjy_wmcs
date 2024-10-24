using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{
    public partial class InterfaceConfigSearcher : BaseSearcher
    {
        public String interfaceCode { get; set; }
        public String interfaceName { get; set; }
        public String interfaceUrl { get; set; }
        public Int32? retryMaxTimes { get; set; }
        public Int32? retryInterval { get; set; }

        protected override void InitVM()
        {
        }

    }
}
