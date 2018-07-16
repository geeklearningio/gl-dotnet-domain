namespace GeekLearning.Domain.Validation
{
    using Explanations;
    using System.Threading.Tasks;

    public abstract class DomainValidator<T> : FluentValidation.AbstractValidator<T>, IValidator<T>
    {
        Task IValidator<T>.ValidateAndThrowAsync(T instance)
        {
            return ValidateAndThrowInternalAsync(instance);
        }

        Task IValidator.ValidateAndThrowAsync(object instance)
        {
            return ValidateAndThrowInternalAsync(instance);
        }

        async Task<IValidationResult> IValidator.ValidateAsync(object instance)
        {
            ValidationResult result = await this.ValidateAsync((T)instance);
            return result;
        }

        async Task<IValidationResult> IValidator<T>.ValidateAsync(T instance)
        {
            ValidationResult result = await this.ValidateAsync(instance);
            return result;
        }

        private async Task ValidateAndThrowInternalAsync(object instance)
        {
            ValidationResult result = await this.ValidateAsync((T)instance);
            if (!result.IsValid)
                throw new Validation<T>(result.Errors).AsException();
        }
    }
}
