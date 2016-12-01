namespace GeekLearning.Domain.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class PagingExtensions
    {
        public static async Task<Page<TAggregate>> ToPageAsync<TData, TAggregate>(this IQueryable<TData> query, int pageIndex, int pageSize, Func<TData, TAggregate> selector)
        {
            return new Page<TAggregate>(
                pageIndex,
                pageSize,
                await query.Skip(pageIndex * pageSize).Take(pageSize).ToAsyncEnumerable().Select(selector).ToList(),
                await query.CountAsync()
            );
        }
    }
}
