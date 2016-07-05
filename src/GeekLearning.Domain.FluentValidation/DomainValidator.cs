namespace GeekLearning.Domain.Validation
{
    using System.Threading.Tasks;

    public abstract class DomainValidator<T> : FluentValidation.AbstractValidator<T>, IValidator<T>
    {
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
    }
}
