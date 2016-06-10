namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AspnetCoreControllerExtensions
    {
        public static MaybeResult<T> Maybe<T>(this ControllerBase controller, Maybe<T> result) where T : class
        {
            return new MaybeResult<T>(result);
        }

        public static MaybeResult<TDataTransfer> Maybe<TAggregate, TDataTransfer>(
            this ControllerBase controller,
            Maybe<TAggregate> aggregate,
            Func<TAggregate, TDataTransfer> dataTransferFromAggregate) where TAggregate : class where TDataTransfer : class
        {
            if (!aggregate.HasValue)
            {
                return new MaybeResult<TDataTransfer>(aggregate.Explanation);
            }

            var dataTransfer = dataTransferFromAggregate(aggregate.Value);

            return new MaybeResult<TDataTransfer>(Domain.Maybe.Some(dataTransfer, aggregate.Explanation));
        }

        public static MaybeResult<IEnumerable<TDataTransfer>> Maybe<TAggregate, TDataTransfer>(
            this ControllerBase controller,
            Maybe<IEnumerable<TAggregate>> aggregates,
            Func<TAggregate, TDataTransfer> dataTransferFromAggregate) where TAggregate : class where TDataTransfer : class
        {
            if (!aggregates.HasValue)
            {
                return new MaybeResult<IEnumerable<TDataTransfer>>(aggregates.Explanation);
            }

            IEnumerable<TDataTransfer> dataTransfers = aggregates.Value
                .Select(m => dataTransferFromAggregate(m))
                .ToList();

            return new MaybeResult<IEnumerable<TDataTransfer>>(Domain.Maybe.Some(dataTransfers, aggregates.Explanation));
        }

        public static MaybeResult<TDataTransfer> Maybe<TAggregate, TDataTransfer>(
            this ControllerBase controller,
            Maybe<TAggregate> aggregate,
            Func<TAggregate, TDataTransfer> dataTransferFromAggregate,
            object routeValues) where TAggregate : class where TDataTransfer : class
        {
            if (!aggregate.HasValue)
            {
                return new MaybeResult<TDataTransfer>(aggregate.Explanation);
            }

            var dataTransfer = dataTransferFromAggregate(aggregate.Value);

            return new MaybeResult<TDataTransfer>(
                Domain.Maybe.Some(dataTransfer, aggregate.Explanation),
                routeValues);
        }
    }
}
