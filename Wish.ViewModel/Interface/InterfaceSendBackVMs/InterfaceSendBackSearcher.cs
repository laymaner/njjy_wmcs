using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackSearcher : BaseSearcher
    {
        public String interfaceCode { get; set; }
        public String interfaceName { get; set; }
        public String interfaceSendInfo { get; set; }
        public String interfaceResult { get; set; }
        public Int32? returnFlag { get; set; }
        public Int32? returnTimes { get; set; }

        protected override void InitVM()
        {
        }

    }
}
