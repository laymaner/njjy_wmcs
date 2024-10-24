using Com.Wish.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class BasBMaterialDto
    {
        public BasBMaterial basBMaterial { get; set; }
        public BasBMaterialCategory basBMaterialCategory { get; set; }

        public BasBMaterialType basBMaterialType { get; set; }
    }
}
