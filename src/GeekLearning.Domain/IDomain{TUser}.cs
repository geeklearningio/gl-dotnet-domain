namespace GeekLearning.Domain
{
    using System.Threading.Tasks;

    public interface IDomain<TUser> : IIdentityDomain
        where TUser: class, IAggregate
    {
        void As(TUser user);

        Maybe<TUser> CurrentUser { get; }
    }
}
