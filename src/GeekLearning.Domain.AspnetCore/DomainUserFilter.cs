namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Formatters.Internal;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.IO;
    using System.Text;

    public class DomainUserFilter : ActionFilterAttribute
    {
        private IEnumerable<IIdentityDomain> identityDomains;
        private readonly ILogger<DomainUserFilter> logger;

        public DomainUserFilter(
            ILogger<DomainUserFilter> logger,
            IEnumerable<IIdentityDomain> identityDomains )
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
