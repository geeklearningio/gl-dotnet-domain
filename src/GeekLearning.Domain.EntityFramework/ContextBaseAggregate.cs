using GeekLearning.Domain;
using GeekLearning.Domain.Validation;

namespace GeekLearning.Domain.EntityFramework
{
    public class ContextBaseAggregate<TDomain, TEntity, TUser, TValidator> : ValidatableAggregateBase<TDomain, TEntity, TUser, TValidator>
        where TDomain : ContextDomainBase<TUser>
        where TUser : class, IAggregate
        where TEntity : class
        where TValidator : IValidator
    {
        public ContextBaseAggregate(TDomain domain, TEntity entity) : base(domain, entity)
        {
            Domain.Register(this);
        }

        public States State => Domain.StateFor(Entity);
    }
}
