using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    internal class DeviceAlarmLogDto
    {
    }
    public class AlarmLogDto
    {
        public string name { get; set; }

        public string value1 { get; set; }
        public DateTime? value2 { get; set; }
    }
}
