namespace GeekLearning.Domain.Validation
{
    using System.Threading.Tasks;

    public interface IValidator<in T> : IValidator
    {
        Task<IValidationResult> ValidateAsync(T instance);

        Task ValidateAndThrowAsync(T instance);
    }
}
