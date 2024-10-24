using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class CommonValidModel
    {
        public int status { get; set; }

        public string msg { get; set; }

        public CommonValidModel vaildError(string _msg)
        {
            status = 1;
            msg = _msg;
            return this;
        }

        public CommonValidModel vaildOk(string _msg = "校验通过")
        {
            status = 0;
            msg = _msg;
            return this;
        }
    }
}
