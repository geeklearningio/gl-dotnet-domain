namespace GeekLearning.Domain
{
    using Explanations;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        public static void AddIfNotNull<T>(this IList<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }

        public static Maybe<T> SingleOrMaybe<T>(this IEnumerable<T> items, Func<T, bool> predicate) where T : class
        {
            var item = items.SingleOrDefault(predicate);
            if (item == null)
            {
                return new NotFound<T>();
            }

            return item;
        }
    }
}
