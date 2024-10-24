using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WISH.Interface
{
    public interface IMessageInterface
    {
        /// <summary>
        /// 获取库位监控信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        Task SendMessage(string msg);
    }
}
