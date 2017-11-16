namespace GeekLearning.Domain.AspnetCore.Internal
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DomainUserMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<DomainUserMiddleware> logger;

        public DomainUserMiddleware(RequestDelegate next, ILogger<DomainUserMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var identityDomains = context.RequestServices.GetRequiredService<IEnumerable<IIdentityDomain>>();

            if (context.User == null
               || context.User.Identity == null
               || !context.User.Identity.IsAuthenticated)
            {
                logger.LogInformation("User : " + "Anonymous");
                foreach (var domain in identityDomains)
                {
                    domain.AsAnonymous();
                }
            }
            else
            {
                logger.LogInformation("User : " + context.User.Identity.Name);
                var principal = context.User;
                foreach (var domain in identityDomains)
                {
                    domain.As(principal);
                }
            }


            await this.next.Invoke(context);
        }
    }
}
