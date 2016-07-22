namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
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

        public static ViewResult ErrorView(
            this Controller controller,
            string domainErrorViewName,
            string errorViewName)
        {
            var feature = controller.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            var domainException = exception as DomainException;

            if (domainException == null)
            {
                return controller.View(errorViewName);
            }

            var resultMapper = controller.HttpContext.RequestServices.GetRequiredService<Internal.MaybeResultMapper>();
            var statusCode = resultMapper.GetResult(domainException.Explanation);

            controller.HttpContext.Response.StatusCode = statusCode;
            controller.HttpContext.Response.Headers.Add("x-request-id", controller.HttpContext.TraceIdentifier);

            return controller.View(domainErrorViewName, domainException);
        }
    }
}
