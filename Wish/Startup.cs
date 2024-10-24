﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using VueCliMiddleware;
using WalkingTec.Mvvm.Core;
using Wish.Service;
using WISH.Custome;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Core.Support.FileHandlers;
using WalkingTec.Mvvm.Mvc;
using static Wish.Service.WcsViewSocketServer;
using Microsoft.AspNetCore.Http.Connections;
using System;
using Wish.ViewModel.Services;


namespace Wish
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }

        public Startup(IWebHostEnvironment env, IConfiguration config)
        {
            ConfigRoot = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWtmWorkflow(ConfigRoot);
            services.AddDistributedMemoryCache();
            services.AddWtmSession(3600, ConfigRoot);
            services.AddWtmCrossDomain(ConfigRoot);
            services.AddWtmAuthentication(ConfigRoot);
            services.AddWtmHttpClient(ConfigRoot);
            services.AddWtmSwagger();
            services.AddWtmMultiLanguages(ConfigRoot);
            services.AddSignalR();
            services.AddMvc(options =>
            {
                options.UseWtmMvcOptions();
            })
            .AddJsonOptions(options => {
                options.UseWtmJsonOptions();
                //options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;//解决接口返回全部转String的问题
            })
            
            .ConfigureApiBehaviorOptions(options =>
            {
                options.UseWtmApiOptions();
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddWtmDataAnnotationsLocalization(typeof(Program));

            services.AddWtmContext(ConfigRoot, (options) => {
                options.DataPrivileges = DataPrivilegeSettings();
                options.CsSelector = CSSelector;
                options.FileSubDirSelector = SubDirSelector;
                options.ReloadUserFunc = ReloadUser;
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptionsMonitor<Configs> configs, IHostEnvironment env)
        {
            app.UseExceptionHandler(configs.CurrentValue.ErrorHandler);
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseWtmStaticFiles();
            app.UseSpaStaticFiles();
            app.UseWtmSwagger(false);
            app.UseRouting();
            app.UseWtmMultiLanguages();
            app.UseWtmCrossDomain();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseWtm();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<WCSMSMonitorHub>("/api/WCSMS/monitor");
                endpoints.MapHub<MonitorHub>("/api/WCSMS/monitor", (options) =>
                {
                    options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(5);
                    options.Transports = HttpTransportType.WebSockets;
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areaRoute",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToFile(env.IsDevelopment() ? "index_dev.html" : "");

            });
            
            app.UseWtmContext();

            //////////////////////////////////////////////////////////////////////////
            /// 自定义服务
            //////////////////////////////////////////////////////////////////////////
            //app.UseCors(
            //    plcService =>
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            var obj = PlcService.GetInstance();
            //        });
            //    });
            app.UseCors(
                socketService =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        var obj = SocketClientService.GetClient();
                    });
                });

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/api/WCSMS/monitor/negotiate?negotiateVersion=1" && context.WebSockets.IsWebSocketRequest)
                {
                    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var webSocketHandler = new WebSocketHandler();
                    await webSocketHandler.HandleWebSocketAsync(webSocket);
                }
                else
                {
                    await next();
                }
            });

            //app.UseCors(
            //    webService =>
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            var obj = WcsViewSocketServer.GetInstanceAsync();
            //            obj.Start();
            //        });
            //    }
            //);
            //app.UseCors(
            //    ob1 =>
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            var obj = new P260_OB1();
            //            obj.Start();
            //            MsgService.GetInstance().Start();
            //        });
            //    }
            //);
            app.UseCors(
                ob1 =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        var obj = new P260_Socket1();
                        obj.Start();
                        MsgService.GetInstance().Start();
                    });
                }
            );
        }

        /// <summary>
        /// Wtm will call this function to dynamiclly set connection string
        /// 框架会调用这个函数来动态设定每次访问需要链接的数据库
        /// </summary>
        /// <param name="context">ActionContext</param>
        /// <returns>Connection string key name</returns>
        public string CSSelector(ActionExecutingContext context)
        {
            //To override the default logic of choosing connection string,
            //change this function to return different connection string key
            //根据context返回不同的连接字符串的名称
            return null;
        }

        /// <summary>
        /// Set data privileges that system supports
        /// 设置系统支持的数据权限
        /// </summary>
        /// <returns>data privileges list</returns>
        public List<IDataPrivilege> DataPrivilegeSettings()
        {
            List<IDataPrivilege> pris = new List<IDataPrivilege>();
            //Add data privilege to specific type
            //指定哪些模型需要数据权限
            return pris;
        }

        /// <summary>
        /// Set sub directory of uploaded files
        /// 动态设置上传文件的子目录
        /// </summary>
        /// <param name="fh">IWtmFileHandler</param>
        /// <returns>subdir name</returns>
        public string SubDirSelector(IWtmFileHandler fh)
        {
            return null;
        }

        /// <summary>
        /// Custom Reload user process when cache is not available
        /// 设置自定义的方法重新读取用户信息，这个方法会在用户缓存失效的时候调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public LoginUserInfo ReloadUser(WTMContext context, string account)
        {
            return null;
        }
    }
}
