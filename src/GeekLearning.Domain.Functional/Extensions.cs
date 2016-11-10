using GeekLearning.Domain.Explanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.Functional
{
    public static class FuncExtensions
    {
        public static Lazy<T> ToLazy<T>(this Func<T> func)
        {
            return new Lazy<T>(func);
        }
    }
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            return new List<T> { item };
        }
        public static void AddIfNotNull<T>(this IList<T> list, T item)
        {
            if (item != null)
                list.Add(item);
        }
        public static Maybe<T> SingleOrMaybe<T>(this IEnumerable<T> items, Func<T, bool> predicate) where T:class
        {
            var item = items.SingleOrDefault(predicate);
            if (item == null)
                return new NotFound<T>();
            return item;
        }
    }
}
