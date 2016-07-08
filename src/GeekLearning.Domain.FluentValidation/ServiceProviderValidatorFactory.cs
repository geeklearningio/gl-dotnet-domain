namespace GeekLearning.Domain.Validation
{
    using Domain;
    using System;

    public class ServiceProviderValidatorFactory : FluentValidation.ValidatorFactoryBase, IValidatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override FluentValidation.IValidator CreateInstance(Type validatorType)
        {
            return this.serviceProvider.GetService(validatorType) as FluentValidation.IValidator;
        }

        IValidator IValidatorFactory.GetValidator(Type validatorType)
        {
            if (!validatorType.IsSubclassOfRawGeneric(typeof(DomainValidator<>)))
            {
                throw new NotSupportedException("The validator must be a DomainValidator.");
            }

            return this.serviceProvider.GetService(validatorType) as IValidator;
        }

        IValidator<T> IValidatorFactory.GetValidator<T>()
        {
            return (IValidator<T>)(this as IValidatorFactory).GetValidator(typeof(T));
        }
    }
}
