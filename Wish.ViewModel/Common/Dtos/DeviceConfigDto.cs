using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    internal class DeviceConfigDto
    {
    }
    public class SaveDeviceUseFlagDto
    {
        public List<long> ids { get; set; } = new List<long>();
        public int? isUseFlag { get; set; }
        public string invoker {  get; set; }
    }
}
