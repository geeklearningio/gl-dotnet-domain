namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class DomainUserFilter : ActionFilterAttribute
    {
        private readonly IEnumerable<IIdentityDomain> identityDomains;
        private readonly ILogger<DomainUserFilter> logger;

        public DomainUserFilter(
            ILogger<DomainUserFilter> logger,
            IEnumerable<IIdentityDomain> identityDomains)
        {
            this.logger = logger;
            this.identityDomains = identityDomains;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User == null
                   || context.HttpContext.User.Identity == null
                   || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                foreach (var domain in this.identityDomains)
                {
                    domain.AsAnonymous();
                }
            }
            else
            {
                var principal = context.HttpContext.User;
                foreach (var domain in this.identityDomains)
                {
                    domain.As(principal);
                }
            }
        }
    }
}
