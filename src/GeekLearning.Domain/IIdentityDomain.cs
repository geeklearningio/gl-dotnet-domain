namespace GeekLearning.Domain
{
    using System.Security.Claims;

    public interface IIdentityDomain : IDomain
    {
        void AsAnonymous();

        void As(ClaimsPrincipal principal);
    }
}
