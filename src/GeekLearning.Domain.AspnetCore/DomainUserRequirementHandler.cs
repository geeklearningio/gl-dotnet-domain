namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DomainUserRequirementHandler : AuthorizationHandler<DomainUserRequirement>
    {
        private readonly IEnumerable<IIdentityDomain> identityDomains;

        public DomainUserRequirementHandler(IEnumerable<IIdentityDomain> identityDomains)
        {
            this.identityDomains = identityDomains;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DomainUserRequirement requirement)
        {
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

                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
