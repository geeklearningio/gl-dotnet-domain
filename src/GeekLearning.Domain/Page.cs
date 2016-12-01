namespace GeekLearning.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Page<TAggregate>
    {
        public Page(int pageIndex, int pageSize, IList<TAggregate> items, int totalCount)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Items = items;
            this.TotalCount = totalCount;
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public IList<TAggregate> Items { get; }

        public int TotalCount { get; }

        public int PageCount => (int)Math.Ceiling((decimal)this.TotalCount / (decimal)this.PageSize);
    }
}
