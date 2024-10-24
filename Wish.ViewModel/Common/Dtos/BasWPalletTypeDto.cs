using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;

namespace Wish.ViewModel.Common.Dtos
{
    public class BasWPalletTypeDto
    {
        public BasWPalletType basWPalletType { get; set; }
        public PalletTypeExtend palletType { get; set; }
    }
}
