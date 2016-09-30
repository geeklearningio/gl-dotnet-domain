using GeekLearning.Domain.EntityFramework;
using GeekLearning.Domain.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.EntityFramework
{
    public enum States
    {
        Stale = 0,
        Unchanged = 1,
        ToBeDeleted = 2,
        ToBeModified = 3,
        ToBeCreated = 4
    }
    public abstract class ContextDomainBase<T> : ValidatableDomainBase<T> where T : class, IAggregate
    {
        public readonly DbContext Context;
        private readonly IList<IValidatableAggregate> _aggregates;

        public ContextDomainBase(DbContext context, IValidatorFactory validatorFactory) : base(validatorFactory)
        {
            Context = context;
            _aggregates = new List<IValidatableAggregate>();
        }

        protected override async Task InnerCommitAsync()
        {
            foreach (var item in _aggregates)
            {
                var res = await item.ValidateAsync();
                if (!res.IsValid)
                {
                    throw new Explanations.Validation<IAggregate>(res).AsException();
                }
            }
            await Context.SaveChangesAsync();
        }
        public States StateFor(object o) => (States)Context.Entry(o).State;

        public void Register(IValidatableAggregate aggregate)
        {
            _aggregates.Add(aggregate);
        }
    }
}
