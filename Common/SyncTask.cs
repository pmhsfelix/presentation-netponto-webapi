using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class SyncTask
    {
        public static Task From(ThreadStart action)
        {
            var tcs = new TaskCompletionSource<object>();
            try
            {
                action();
                tcs.SetResult(null);
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
            return tcs.Task;
        }

        public static Task<T> From<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                tcs.SetResult(action());
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
            return tcs.Task;
        }
    }
}
