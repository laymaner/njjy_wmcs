using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using WISH.Interface;

namespace Wish.ViewModel.Services
{
    public class MonitorHub : Hub<IMessageInterface>
    {
        //private readonly ILogger _logger;
        private static ILog _logger = LogManager.GetLogger(typeof(WmsTaskVM));
        /// <summary>
        /// 构造方法，注入log和dao的依赖
        /// </summary>
        //public MonitorHub(ILoggerFactory logger)
        public MonitorHub()
        {
            //_logger = logger.CreateLogger("WebSocketLogger");
            _logger.Warn("WebSocketLogger");
        }

        /// <summary>
        /// 连接时
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public async Task Connected(string methodName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, methodName);
            GroupListHandler.GetInstance().AddConnectedId(Context.ConnectionId, methodName);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接时
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            GroupListHandler.GetInstance().RemoveConnectedId(Context.ConnectionId, out var listgroup, out var list);
            //_logger.LogInformation($"{ex.Message}");
            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
        /// 离开组
        /// </summary>
        /// <remarks>
        /// <para>备注</para>
        /// <para>1. 切换时, 必须调用此方法, 否则数据显示存在问题</para>
        /// </remarks>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public async Task LeaveGroup(string methodName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, methodName);
            GroupListHandler.GetInstance().RemoveConnectedId(Context.ConnectionId, out var listgroup, out var list);
        }
    }
}
