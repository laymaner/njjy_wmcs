//=============================================================================
//                                 A220101
//=============================================================================
//
// 数据库服务。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/24
//      创建
//
//-----------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Service
{
    public class DCService
    {

        private WTMContext wtm;

        private DCService() { }
        private static DCService dCService;
        public static DCService GetInstance()
        {
            if(dCService == null)
            {
                dCService = new DCService();
            }
            return dCService;
        }

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        /// <returns></returns>
        public IDataContext GetDC()
        {
            if (wtm == null) 
                InitDC();
            return wtm.CreateDC();
        }

        private void InitDC()
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

        public void NewEntity(PersistPoco t)
        {
            IDataContext datacontext = GetDC();
            datacontext.AddEntity(t);
            datacontext.SaveChanges();
            //await Task.Delay(10);
        }

        public void UpdateEntity(PersistPoco t)
        {
            IDataContext datacontext = GetDC();
            datacontext.UpdateEntity(t);
            datacontext.SaveChanges();
            //await Task.Delay(10);
        }
    }
}
