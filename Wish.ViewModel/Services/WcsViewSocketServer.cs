//=============================================================================
//                                 A220101
//=============================================================================
//
// 页面展示设备界面socket通讯服务。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/11 
//      创建
//
//-----------------------------------------------------------------------------

using Fleck;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System;
using Wish.Service;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using log4net.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace Wish.Service
{
    public class WcsViewSocketServer
    {
        private static WcsViewSocketServer instance;

        WebSocketServer server;
        List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        /// <summary>
        /// 写给页面的数据,Json格式。
        /// </summary>
        private ConcurrentQueue<string> Server2ClientMsgQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// 页面写给服务的数据,Json格式。
        /// </summary>
        private ConcurrentQueue<string> Client2ServerMsgQueue = new ConcurrentQueue<string>();

        private WcsViewSocketServer()
        {
            string ipAddress = ConfigReader.ReadConfig("Domains:WebSocket:Address");
            if (!string.IsNullOrEmpty(ipAddress))
            {
                server = new WebSocketServer($"{ipAddress}/api/WCSMS/monitor");
            }
            else
            {
                // 默认地址或者处理配置读取失败的情况
                server = new WebSocketServer("http://*:6338/api/WCSMS/monitor");
            }
        }

        public static WcsViewSocketServer GetInstanceAsync()
        {
            if (instance == null)
            {
                instance = new WcsViewSocketServer();
            }
            return instance;
        }

        /// <summary>
        /// 启动WebService服务。
        /// </summary>
        public void Start()
        {
            server.Start(
                socket =>
                {
                    socket.OnOpen = () =>
                    {
                        allSockets.Add(socket);
                        //PlcService.GetInstance().notifyNewClient();
                        SocketClientService.GetClient().notifyNewClient();
                    };

                    socket.OnClose = () =>
                    {
                        allSockets.Remove(socket);
                    };

                    socket.OnMessage = message =>
                    {
                        Client2ServerMsgQueue.Enqueue(message);
                        //allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                    };

                    socket.OnPing = ping =>
                    {
                        socket.Send("Echo: ping");
                    };
                });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {

                    //Server2ClientMsgQueue.Enqueue("{\"srms\":[{\"code\":\"SRM02\",\"taskNo\":\"T101\",\"tray\":true,\"alarm\":false,\"connect\":true,\"column\":" + x + "}]}");
                    //Server2ClientMsgQueue.Enqueue("{\"rgvs\":[{\"code\":\"RGVROAD2\",\"taskNo\":\"T101\",\"tray\":true,\"alarm\":false,\"connect\":true,\"column\":" + x + "}]}");
                    //Server2ClientMsgQueue.Enqueue("{\"S1\":     { \"front_cargoswitch_1\" :    { \"value\": true },   \"front_y\": {\"value\": "+x+"},     \"front_task\": {\"value\":123 },   \"front_cargoswitch_1\":{ \"value\": true } ,\"sys_status\":5    }   }");
                    string msg;
                    while (Server2ClientMsgQueue.TryDequeue(out msg))
                    {
                        foreach (var socket in allSockets.ToList())
                        {
                            socket.Send(msg);
                        }
                    }
                    Thread.Sleep(500);
                }
            });
        }

        /// <summary>
        /// 发送刷新数据到页面。
        /// </summary>
        /// <param name="message"></param>
        public void SendWcsWebJsonMssage(string message)
        {
            Server2ClientMsgQueue.Enqueue(message);
        }
        public class ConfigReader
        {
            public static string ReadConfig(string configName)
            {
                var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                return configuration[configName];
            }
        }

        public class WebSocketHandler
        {
            public async Task HandleWebSocketAsync(WebSocket webSocket)
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;

                while (webSocket.State == WebSocketState.Open)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"You sent: {message}")), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    }
                }
            }
        }

        public class WCSMSMonitorHub : Hub
        {
            //public override async Task OnConnectedAsync()
            public async Task Connected(string methodName)
            {
                //await Clients.Caller.SendAsync("SendMessage", "Connected successfully.");
                await Groups.AddToGroupAsync(Context.ConnectionId, methodName);
                await base.OnConnectedAsync();
            }

            public async Task SendMessageToClients(string message)
            {
                await Clients.All.SendAsync("SendMessage", message);
            }

            // 添加这个方法以响应前端的调用
            //public async Task Connected()
            //{
            //    // 可以在这里执行一些逻辑，例如向调用者发送确认消息
            //    await Clients.Caller.SendAsync("SendMessage", "Connected method called on server.");
            //}
        }

    }
}
