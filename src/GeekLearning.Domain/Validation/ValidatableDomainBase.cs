namespace GeekLearning.Domain.Validation
{
    using Domain;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public abstract class ValidatableDomainBase<TUser> : IDomain<TUser>
        where TUser : class, IAggregate
    {
        private readonly IValidatorFactory validatorFactory;

        public abstract Maybe<TUser> CurrentUser { get; }

        public ValidatableDomainBase(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        public IValidator GetValidator<TAggregate>()
            where TAggregate : IValidatableAggregate
        {
            return this.validatorFactory.GetValidator<TAggregate>();
        }

        public IValidator GetValidator(Type validatableAggregateType)
        {
            return this.validatorFactory.GetValidator(validatableAggregateType);
        }

        public async Task CommitAsync()
        {
            await this.InnerCommitAsync();
        }

        protected abstract Task InnerCommitAsync();

        public abstract void As(TUser user);

        public abstract void AsAnonymous();

        public abstract void As(ClaimsPrincipal principal);
    }
}
