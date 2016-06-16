namespace GeekLearning.Domain.Validation
{
    using System.Threading.Tasks;

    public interface IValidator
    {
        Task<IValidationResult> ValidateAsync(object instance);
    }
}
