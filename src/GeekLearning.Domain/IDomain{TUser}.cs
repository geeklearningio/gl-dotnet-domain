namespace GeekLearning.Domain
{
    using System.Threading.Tasks;

    public interface IDomain<TUser> : IDomain
        where TUser: class, IAggregate
    {
        Maybe<TUser> CurrentUser { get; }
    }
}
