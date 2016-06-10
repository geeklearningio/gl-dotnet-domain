namespace GeekLearning.Domain
{
    public abstract class AggregateBase<TDomain>: IAggregate
        where TDomain : IDomain
    {
        private readonly TDomain domain;

        public AggregateBase(TDomain domain)
        {
            this.domain = domain;
        }

        protected TDomain Domain { get { return this.domain; } }
    }
}
