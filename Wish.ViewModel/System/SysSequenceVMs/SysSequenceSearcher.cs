using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace Wish.ViewModel.System.SysSequenceVMs
{
    public partial class SysSequenceSearcher : BaseSearcher
    {
        public String SeqCode { get; set; }
        public Int32? SeqType { get; set; }
        public Int32? NowSn { get; set; }
        public Int32? MinSn { get; set; }
        public Int32? MaxSn { get; set; }
        public Int32? SeqSnLen { get; set; }

        protected override void InitVM()
        {
        }

    }
}
