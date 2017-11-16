namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Authorization;

    public class DomainUserRequirement : IAuthorizationRequirement
    {
        public DomainUserRequirement()
        {
        }
    }
}
