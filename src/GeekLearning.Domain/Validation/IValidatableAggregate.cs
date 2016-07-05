namespace GeekLearning.Domain.Validation
{
    using System.Threading.Tasks;

    public interface IValidatableAggregate
    {
        Task<IValidationResult> ValidateAsync();
    }
}
