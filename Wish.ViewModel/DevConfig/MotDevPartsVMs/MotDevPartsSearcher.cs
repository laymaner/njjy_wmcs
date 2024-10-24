using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.DevConfig;


namespace Wish.ViewModel.DevConfig.MotDevPartsVMs
{
    public partial class MotDevPartsSearcher : BaseSearcher
    {
        public String DevNo { get; set; }
        public String PartNo { get; set; }
        public String PartLocNo { get; set; }
        public Int32? DevRunMode { get; set; }
        public String SrmRoadway { get; set; }
        public Int32? SrmExecStep { get; set; }
        public Int32? IsAlarming { get; set; }
        public String AlarmCode { get; set; }
        public Int32? IsFree { get; set; }
        public Int32? IsHasGoods { get; set; }
        public String CmdNo { get; set; }
        public String PalletNo { get; set; }
        public String ReadPalletNo { get; set; }
        public Int32? CurrentX { get; set; }
        public Int32? CurrentY { get; set; }
        public Int32? CurrentZ { get; set; }

        protected override void InitVM()
        {
        }

    }
}
