using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common
{
    public class ReportLockProvider
    {
        static readonly ConcurrentDictionary<string, SemaphoreSlim> lockDictionary = new ConcurrentDictionary<string, SemaphoreSlim>();


        static Dictionary<string, string> KeyDict = new Dictionary<string, string>();

        public static object GetKey(string id)
        {
            if (KeyDict.ContainsKey(id))
            {
                return KeyDict[id];
            }
            return null;
        }

        public static void SetKey(string id, string key)
        {
            KeyDict[id] = key;
        }
        public static void RemoveKey(string id)
        {
            KeyDict.Remove(id);
        }


        public static async Task WaitAsync(string id)
        {
            await lockDictionary.GetOrAdd(id, new SemaphoreSlim(1, 1)).WaitAsync();
        }
        public static void Release(string id)
        {
            try
            {
                SemaphoreSlim semaphore;
                if (lockDictionary.TryGetValue(id, out semaphore))
                {
                    semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
