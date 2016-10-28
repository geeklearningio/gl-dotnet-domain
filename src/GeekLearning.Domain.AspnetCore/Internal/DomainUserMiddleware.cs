namespace GeekLearning.Domain.AspnetCore.Internal
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DomainUserMiddleware
    {
        private readonly RequestDelegate next;

        public DomainUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var identityDomains = context.RequestServices.GetRequiredService<IEnumerable<IIdentityDomain>>();

            if (context.User == null
               || context.User.Identity == null
               || !context.User.Identity.IsAuthenticated)
            {
                foreach (var domain in identityDomains)
                {
                    domain.AsAnonymous();
                }
            }
            else
            {
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
