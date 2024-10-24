using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class BasWErpWhouseBinDto
    {
        public string erpWhouseNo { get; set; }

        public List<WRackBinSaveDto> racks { get; set; } = new List<WRackBinSaveDto>();
    }


    public class GetErpWhouseBinsInDto
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string erpWhouseNo { get; set; }
        public string areaNo { get; set; }
        public string regionNo { get; set; }
        public string binNo { get; set; }
        public bool? usedFlag { get; set; }
    }

    public class BinsDto
    {
        public string ID { get; set; }
        public string areaNo { get; set; }
        public string erpWhouseNo { get; set; }
        public string regionNo { get; set; }
        public string binNo { get; set; }
        public bool usedFlag { get; set; }
    }

    public class UpdateErpWhouseBinDto
    {
        public string areaNo { get; set; }
        public string regionNo { get; set; }
        public string erpWhouseNo { get; set; }
        public List<string> ids { get; set; }
        public bool usedFlag { get; set; }
    }
}
