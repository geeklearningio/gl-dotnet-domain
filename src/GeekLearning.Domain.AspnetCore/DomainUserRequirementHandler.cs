namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DomainUserRequirementHandler : AuthorizationHandler<DomainUserRequirement>
    {
        private readonly IEnumerable<IIdentityDomain> identityDomains;
        private readonly ILogger<DomainUserRequirementHandler> logger;

        public DomainUserRequirementHandler(IEnumerable<IIdentityDomain> identityDomains, ILogger<DomainUserRequirementHandler> logger)
        {
            this.identityDomains = identityDomains;
            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DomainUserRequirement requirement)
        {
            if (context.User == null
              || context.User.Identity == null
              || !context.User.Identity.IsAuthenticated)
            {
                this.logger.LogInformation($"Failed to provide Identity to the domain. IsAuthenticated: {context.User?.Identity?.IsAuthenticated}, Name: {context.User?.Identity?.Name}");
                foreach (var domain in identityDomains)
                {
                    domain.AsAnonymous();
                }
            }
            else
            {
                this.logger.LogInformation($"Successfuly provided Identity to the domain. Name: {context.User?.Identity?.Name}");
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
