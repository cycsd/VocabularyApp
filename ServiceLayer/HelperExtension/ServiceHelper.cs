using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.HelperExtension
{
    public static class ServiceHelper
    {
        public static Task<T> OrElse<T>(this Task<T> task, Func<Task<T>> fallback)
            => task.ContinueWith(t =>
                  t.Status == TaskStatus.Faulted
                           ? fallback()
                            : Task.FromResult(t.Result)
                ).Unwrap();

    }
}
