using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.System;


namespace Wish.ViewModel.System.SysDictionaryVMs
{
    public partial class SysDictionarySearcher : BaseSearcher
    {
        public Int32? developFlag { get; set; }
        public String dictionaryCode { get; set; }
        public String dictionaryItemCode { get; set; }
        public String dictionaryName { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
