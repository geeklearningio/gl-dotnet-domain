namespace GeekLearning.Domain
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IIdentityDomain: IDomain
    {
        void AsAnonymous();

        void As(ClaimsPrincipal principal);
    }
}
