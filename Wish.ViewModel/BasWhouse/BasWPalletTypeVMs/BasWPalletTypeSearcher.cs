using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletTypeVMs
{
    public partial class BasWPalletTypeSearcher : BaseSearcher
    {
        public Int32? barcodeFlag { get; set; }
        public Int32? developFlag { get; set; }
        public String palletTypeCode { get; set; }
        public Int32? palletTypeFlag { get; set; }
        public String palletTypeName { get; set; }
        public Decimal? palletWeight { get; set; }
        public Int32? palletWidth { get; set; }
        public Int32? positionCol { get; set; }
        public Int32? positionDirect { get; set; }
        public Int32? positionFlag { get; set; }
        public Int32? positionRow { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
