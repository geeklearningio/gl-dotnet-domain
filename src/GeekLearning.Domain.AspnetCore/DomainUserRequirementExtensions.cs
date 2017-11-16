namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Authorization;

    public static class DomainUserRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireDomainUser(this AuthorizationPolicyBuilder builder)
        {
            return builder.AddRequirements(new DomainUserRequirement());
        }
    }
}
