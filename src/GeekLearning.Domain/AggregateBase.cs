namespace GeekLearning.Domain
{
    public abstract class AggregateBase<TDomain> : IAggregate
        where TDomain : IDomain
    {
        public AggregateBase(TDomain domain)
        {
            this.Domain = domain;
        }

        protected TDomain Domain { get; }
    }
}
