using GeekLearning.Domain;
using GeekLearning.Domain.Validation;
using Microsoft.EntityFrameworkCore;

namespace GeekLearning.Domain.EntityFramework
{
    public class ContextAggregateBase<TDomain, TEntity, TUser, TValidator> : ValidatableAggregateBase<TDomain, TEntity, TUser, TValidator>, IContextAggregateBase
        where TDomain : ContextDomainBase<TUser>
        where TUser : class, IAggregate
        where TEntity : class
        where TValidator : IValidator
    {
        public ContextAggregateBase(TDomain domain, TEntity entity) : base(domain, entity)
        {
            Domain.Register(this);
        }

        public States State()
        {
            return Domain.StateFor(Entity);
        }
    }
}
