using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WISH.Helper.Common;

namespace Wish.ViewModel.Common
{
    public static class InterfaceHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(InterfaceHelper));
        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="sendParams"></param>
        /// <returns></returns>
        public static async Task<ApiResponseRes> SendRequest(string url, string sendParams)
        {
            using (HttpClient client = new HttpClient())
            {
                ApiResponseRes apiResponseResult = new ApiResponseRes(-1, "请求失败", null);

                if (!string.IsNullOrEmpty(sendParams))
                {
                    url += sendParams;
                }
                // 从配置文件中读取超时时间
                var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json") // 假设配置文件名为 appsettings.json
                   .Build();
                var timeoutSeconds = config.GetValue<int>("HttpClientOptions:TimeoutSeconds");

                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    logger.Warn($"----->【接口信息】----->{responseData} ");
                    var responseResult = JsonConvert.DeserializeObject<ApiResponseRes>(responseData);

                    return apiResponseResult = new ApiResponseRes { Code = 0, Message = "请求成功", Data = responseResult.Data };
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();

                    return apiResponseResult = new ApiResponseRes { Code = -1, Message = "请求失败", Data = null };
                }
            }
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="sendParams"></param>
        /// <returns></returns>
        public static async Task<ApiResponseRes> SendRequestPost<T>(string url, T sendParams)
        {
            using (HttpClient client = new HttpClient())
            {
                ApiResponseRes apiResponseResult = new ApiResponseRes(-1, "请求失败", null);
                // 请求的 URL
                //string url = "https://example.com/api/postendpoint";

                // 构造要发送的数据（可以是字符串、字节数组、对象等）
                string postData = "{\"key\":\"value\"}";
                string strJson = JsonConvert.SerializeObject(sendParams);
                postData = strJson;

                // 构建 StringContent 对象，用于发送 JSON 数据
                StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                // 从配置文件中读取超时时间
                var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json") // 假设配置文件名为 appsettings.json
                   .Build();
                var timeoutSeconds = config.GetValue<int>("HttpClientOptions:TimeoutSeconds");
                //超时时间
                client.Timeout= TimeSpan.FromSeconds(timeoutSeconds);
                // 发起 POST 请求
                HttpResponseMessage response = await client.PostAsync(url, content);

                // 检查是否请求成功
                if (response.IsSuccessStatusCode)
                {
                    // 读取响应内容
                    string responseData = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<ApiResponseRes>(responseData);

                    return apiResponseResult = new ApiResponseRes { Code = 0, Message = "请求成功", Data = responseResult.Data };
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();

                    return apiResponseResult = new ApiResponseRes { Code = -1, Message = "请求失败", Data = null };
                }
            }

        }
    }
}
