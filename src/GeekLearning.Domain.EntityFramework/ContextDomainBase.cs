namespace GeekLearning.Domain.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Validation;

    [Flags]
    public enum States
    {
        Unknown = 0,
        Unchanged = 1,
        ToBeDeleted = 2,
        ToBeModified = 3,
        ToBeCreated = 4
    }

    public abstract class ContextDomainBase<T> : ValidatableDomainBase<T> where T : class, IAggregate
    {
        public readonly DbContext DbContext;
        private readonly IList<IContextAggregateBase> aggregates;

        public ContextDomainBase(DbContext context, IValidatorFactory validatorFactory) : base(validatorFactory)
        {
            this.DbContext = context;
            this.aggregates = new List<IContextAggregateBase>();
        }

        protected override async Task InnerCommitAsync()
        {
            var toValidate = aggregates.Where(s => s.State() != (States.Unknown | States.Unchanged)).OfType<IValidatableAggregate>();
            foreach (var item in toValidate)
            {
                var res = await item.ValidateAsync();
                if (!res.IsValid)
                {
                    throw new Explanations.Validation<IAggregate>(res).AsException();
                }
            }

            await DbContext.SaveChangesAsync();
        }

        public States StateFor(object o) => (States)DbContext.Entry(o).State;

        public void Register(IContextAggregateBase aggregate)
        {
            aggregates.Add(aggregate);
        }
    }
}
