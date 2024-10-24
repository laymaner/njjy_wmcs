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
    public partial class DBConfigBatchVM : BaseBatchVM<DBConfig, DBConfig_BatchEdit>
    {
        public DBConfigBatchVM()
        {
            ListVM = new DBConfigListVM();
            LinkedVM = new DBConfig_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DBConfig_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
