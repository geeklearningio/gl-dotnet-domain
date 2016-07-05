namespace GeekLearning.Domain
{
    public abstract class AggregateBase<TDomain, TUser> : IAggregate
        where TDomain : IDomain<TUser>
        where TUser : class, IAggregate
    {
        public AggregateBase(TDomain domain)
        {
            this.Domain = domain;
        }

        protected TDomain Domain { get; }
    }
}
