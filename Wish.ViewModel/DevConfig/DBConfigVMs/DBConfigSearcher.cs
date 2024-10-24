using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DBConfigVMs
{
    public partial class DBConfigSearcher : BaseSearcher
    {
        [Display(Name = "DBInfo.DBNo")]
        public String Block_Code { get; set; }
        [Display(Name = "DBInfo.DBName")]
        public String Block_Name { get; set; }
        [Display(Name = "DBInfo.Offset")]
        public Int32? Block_Offset { get; set; }
        [Display(Name = "DBInfo.TotalLength")]
        public Int32? Block_Length { get; set; }
        public long? PlcConfigId { get; set; }

        protected override void InitVM()
        {
        }

    }
}
