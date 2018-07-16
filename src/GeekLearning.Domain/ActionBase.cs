namespace GeekLearning.Domain
{
    public abstract class ActionBase<TDomain, TUser, TAggregate, TEntity> : IAction<TAggregate>
        where TDomain : IDomain<TUser>
        where TAggregate : IAggregate
        where TEntity : class
        where TUser : class, IAggregate
    {
        public ActionBase(TDomain domain)
        {
            this.Domain = domain;
        }

        protected TDomain Domain { get; }

        protected void ThrowNotFoundIfNull<TKey>(TKey id, TEntity entity) where TKey : struct
        {
            if (entity == null)
            {
                throw new Explanations.NotFound<TAggregate>(id).AsException();
            }
        }

        protected void ThrowIfAnonymous()
        {
            if (!this.Domain.CurrentUser.HasValue)
            {
                throw new Explanations.Anonymous().AsException();
            }
        }
    }
}
