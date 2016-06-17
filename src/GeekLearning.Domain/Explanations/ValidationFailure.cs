namespace GeekLearning.Domain.Explanations
{
    using Validation;

    public class ValidationFailure<TAggregate> : Explanation
    {
        public ValidationFailure(IValidationFailure failure)
            : base(failure.ToString(), $"AggregateType : { typeof(TAggregate).FullName }")
        {
        }
    }
}
