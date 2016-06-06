namespace GeekLearning.Domain.AspnetCore
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    public class DomainExceptionFilter : IActionFilter
    {
        private ILoggerFactory loggerFactory;
        private IOptions<DomainOptions> options;

        public DomainExceptionFilter(ILoggerFactory loggerFactory, IOptions<DomainOptions> domainOptions)
        {
            this.loggerFactory = loggerFactory;
            this.options = domainOptions;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var domainException = context.Exception as Exceptions.DomainException;
                var logger = this.loggerFactory.CreateLogger<DomainExceptionFilter>();
                if (domainException == null)
                {
                    logger.LogError(new EventId(1, "Unknown error"), context.Exception.Message, context.Exception);
                    domainException = new Exceptions.UnknownException(context.Exception);
                }
                context.Result = new MaybeResult<object>(Maybe.None(domainException.Explanations));
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
