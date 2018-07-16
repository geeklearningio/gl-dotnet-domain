namespace GeekLearning.Domain
{
    public interface IDomain<TUser> : IIdentityDomain
        where TUser : class, IAggregate
    {
        void As(TUser user);

        Maybe<TUser> CurrentUser { get; }
    }
}
