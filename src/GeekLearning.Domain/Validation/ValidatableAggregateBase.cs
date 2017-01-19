namespace GeekLearning.Domain.Validation
{
    using Domain;
    using System.Threading.Tasks;

    public abstract class ValidatableAggregateBase<TDomain, TUser, TValidator> : AggregateBase<TDomain>, IValidatableAggregate
    where TDomain : ValidatableDomainBase<TUser>
    where TUser : class, IAggregate
    where TValidator : IValidator
    {
        private readonly IValidator validator;

        public ValidatableAggregateBase(TDomain domain)
            : base(domain)
        {
            this.validator = domain.GetValidator(typeof(TValidator));
        }

        public Task<IValidationResult> ValidateAsync()
        {
            return this.validator.ValidateAsync(this);
        }
    }

    public abstract class ValidatableAggregateBase<TDomain, TEntity, TUser, TValidator> : AggregateBase<TEntity>, IValidatableAggregate
        where TDomain : ValidatableDomainBase<TUser>
        where TUser : class, IAggregate
        where TEntity : class
        where TValidator : IValidator
    {
        private readonly IValidator validator;

        public ValidatableAggregateBase(TDomain domain, TEntity entity)
            : base(entity)
        {
            this.validator = domain.GetValidator(typeof(TValidator));
        }

        public Task<IValidationResult> ValidateAsync()
        {
            return this.validator.ValidateAsync(this);
        }
    }
    public abstract class ValidatableAggregateBase<TEntity, TValidator> : AggregateBase<TEntity>, IValidatableAggregate
       where TEntity : class
       where TValidator : IValidator
    {
        protected abstract IValidator validator { get; }

        public ValidatableAggregateBase(TEntity entity)
            : base(entity)
        {
        }

        public Task<IValidationResult> ValidateAsync()
        {
            return this.validator.ValidateAsync(this);
        }

        public Task ValidateAndThrowAsync()
        {
            return this.validator.ValidateAndThrowAsync(this);
        }

    }

}
