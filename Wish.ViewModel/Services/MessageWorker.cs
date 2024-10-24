using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;
using WISH.Interface;
using Microsoft.Extensions.DependencyInjection;
using WalkingTec.Mvvm.Core;
using Wish.Service;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;

namespace Wish.ViewModel.Services
{
    
    public class MessageWorker : BackgroundService
    {
        private WTMContext wtm;
        private void InitWTM()
        {
            var services = new ServiceCollection();
            try
            {
                services.AddWtmContextForConsole();
            }
            catch (Exception ex)
            {
                ContextService.Log.Error(ex);
            }
            ServiceProvider provider = services.BuildServiceProvider();
            wtm = provider.GetRequiredService<WTMContext>();
        }

        private readonly IHubContext<MonitorHub, IMessageInterface> message;
        //private readonly ILogger _logger;
        private static ILog _logger = LogManager.GetLogger(typeof(MessageWorker));
        /// <summary>
        /// 定时器的轮询间隔
        /// </summary>
        private readonly int interval;


        public MessageWorker(

            IHubContext<MonitorHub, IMessageInterface> message
           )
        {
            this.message = message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            SrmCmdVM srmCmdVM = wtm.CreateVM<SrmCmdVM>();

            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            var noDicCamelCasesetting = new JsonSerializerSettings { ContractResolver = new NoDicCamelCaseResolver() };
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var item in GroupListHandler.GetInstance().GroupList)
                    {
                        //ParallelLoopResult result = Parallel.ForEach(GroupListHandler.GetInstance().GroupList, async (item) =>
                        //{`
                        if (item.Key == null)
                        {
                            continue;
                        }
                        string method = item.Key.Split('|')[0];
                        string no = item.Key.Split('|')[1];
                        /*if (method == "Logistic")
                        {
                            var result = await _binMotAppService.GetBinStatusInfo(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, noDicCamelCasesetting));
                        }
                        if (method == "HCFR")
                        {
                            var result = await _binMotAppService.GetBinStatusInfoHCFR(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, noDicCamelCasesetting));
                        }
                        if (method == "Product")
                        {
                            var result = await _binMotAppService.GetBinStatusInfoProduct(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, noDicCamelCasesetting));
                        }
                        if (method == "Layout")
                        {
                            var result = await _layoutMotAppService.GetDevStatusAsync();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        }
                        if (method == "Device_GetDeviceOverView")
                        {
                            var result = await _deviceMotAppService.GetDeviceOverViewAsync();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetCrfTask")
                        {
                            var result = await _deviceMotAppService.GetCrfTaskAsync(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetMotDevPart")
                        {
                            var result = await _deviceMotAppService.GetMotDevPartAsync(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetStepListByCmdType")
                        {
                            var result = await _deviceMotAppService.GetStepListByCmdTypeAsync(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetWcsCraneMotInfo")
                        {
                            var result = await _deviceMotAppService.GetWcsCraneMotInfoAsync(no.Split("_")[0], no.Split("_")[1]);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetWcsRobotMotInfo")
                        {
                            var result = await _deviceMotAppService.GetWcsRobotMotInfoAsync(no.Split("_")[0], no.Split("_")[1]);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetWcsTranMotInfo")
                        {
                            var result = await _deviceMotAppService.GetWcsTranMotInfoAsync(no.Split("_")[0], no.Split("_")[1]);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetCmdInfo")
                        {
                            var result = await _deviceMotAppService.GetCmdAsync(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "Device_GetTaskByDevNo")
                        {
                            var result = await _deviceMotAppService.GetCrfTaskByDevNoAsync(no);
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }
                        if (method == "CMD_GetList")
                        {
                            var result = await _cmdAppService.GetListAsync();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        }*/
                        if (method == "GetDeviceInfos")
                        {
                            var result = await srmCmdVM.GetDeviceInfo();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        }
                        if (method == "GetAlarmInfos")
                        {
                            var result = await srmCmdVM.GetAlarmInfos();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        }
                        //if (method == "GetBinInfos")
                        //{
                        //    var result = await _wcsDataAnalysisAppService.GetBinInfosByRoadway();
                        //    await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        //}
                        //if (method == "GetTaskInfos")
                        //{
                        //    var result = await srmCmdVM.GetTaskInfos(no);
                        //    await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        //}
                        if (method == "Layout")
                        {
                            var result = await srmCmdVM.GetDevDetailInfos();
                            await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result));
                        }
                        //if (method == "DownLoadTaskByUser")
                        //{
                        //    GetDownLoadTaskListInput input = new GetDownLoadTaskListInput();

                        //    /*int days = 1;
                        //    bool b = int.TryParse(no, out days);*/
                        //    input.Days = 30;
                        //    input.UserName = no;
                        //    var result = await _downLoadTaskAppService.GetDownLoadTaskByUser(input);
                        //    //  _logger.LogInformation("DownLoadTaskByUser:" + result);
                        //    await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        //}

                        //if (method == "GetDownLoadTaskCount")
                        //{
                        //    var result = await _downLoadTaskAppService.GetDownLoadTaskCount();
                        //    await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, setting));
                        //}
                        //if (method == "Logistic")
                        //{
                        //    var result = await _binMotAppService.GetBinStatusInfoAsync(no);
                        //    await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, noDicCamelCasesetting));
                        //}
                        /*if (method == "Notification")
                        {
                            var result = await _significantNoticeService.GetNotificationListAsync();
                            if (result.Any() || result.Count > 0)
                                await message.Clients.Group(item.Key).SendMessage(JsonConvert.SerializeObject(result, noDicCamelCasesetting));
                        }*/
                    }
                }
                catch (Exception ex)
                {
                    _logger.Warn($"{ex.Message}");
                }
                await Task.Delay(interval);
            }
        }

        #region 定时器的轮询间隔配置

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetAppseting(string key)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);
            var _config = builder.Build();
            return _config["AppSettings:" + key];
        }

        #endregion 定时器的轮询间隔配置

        public class NoDicCamelCaseResolver : DefaultContractResolver
        {
            private static readonly object TypeContractCacheLock = new object();
            private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();
            private static Dictionary<StructMultiKey<Type, Type>, JsonContract>? _contractCache;

            /// <summary>
            /// Initializes a new instance of the <see cref="CamelCasePropertyNamesContractResolver"/> class.
            /// </summary>
            public NoDicCamelCaseResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = false,
                    OverrideSpecifiedNames = true
                };
            }

            /// <summary>
            /// Resolves the contract for a given type.
            /// </summary>
            /// <param name="type">The type to resolve a contract for.</param>
            /// <returns>The contract for a given type.</returns>
            public override JsonContract ResolveContract(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                // for backwards compadibility the CamelCasePropertyNamesContractResolver shares contracts between instances
                StructMultiKey<Type, Type> key = new StructMultiKey<Type, Type>(GetType(), type);
                Dictionary<StructMultiKey<Type, Type>, JsonContract>? cache = _contractCache;
                if (cache == null || !cache.TryGetValue(key, out JsonContract contract))
                {
                    contract = CreateContract(type);

                    // avoid the possibility of modifying the cache dictionary while another thread is accessing it
                    lock (TypeContractCacheLock)
                    {
                        cache = _contractCache;
                        Dictionary<StructMultiKey<Type, Type>, JsonContract> updatedCache = (cache != null)
                            ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(cache)
                            : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
                        updatedCache[key] = contract;

                        _contractCache = updatedCache;
                    }
                }

                return contract;
            }
        }

        internal readonly struct StructMultiKey<T1, T2> : IEquatable<StructMultiKey<T1, T2>>
        {
            public readonly T1 Value1;
            public readonly T2 Value2;

            public StructMultiKey(T1 v1, T2 v2)
            {
                Value1 = v1;
                Value2 = v2;
            }

            public override int GetHashCode()
            {
                return (Value1?.GetHashCode() ?? 0) ^ (Value2?.GetHashCode() ?? 0);
            }

            public override bool Equals(object obj)
            {
                if (!(obj is StructMultiKey<T1, T2> key))
                {
                    return false;
                }

                return Equals(key);
            }

            public bool Equals(StructMultiKey<T1, T2> other)
            {
                return (Equals(Value1, other.Value1) && Equals(Value2, other.Value2));
            }
        }
    }
}
