using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain
{
    public static class FuncExtensions
    {
        public static Lazy<T> ToLazy<T>(this Func<T> func)
        {
            return new Lazy<T>(func);
        }
    }
}
