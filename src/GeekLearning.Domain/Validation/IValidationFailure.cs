namespace GeekLearning.Domain.Validation
{
    public interface IValidationFailure
    {
        string ErrorMessage { get; }

        object AttemptedValue { get; }

        string PropertyName { get; }
    }
}
