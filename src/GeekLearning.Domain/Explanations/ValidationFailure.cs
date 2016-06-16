namespace GeekLearning.Domain.Explanations
{
    using Validation;

    public class ValidationFailure<TAggregate> : Explanation
    {
        internal ValidationFailure(IValidationFailure failure)
            : base(failure.ToString(), $"AggregateType : { typeof(TAggregate).FullName }")
        {
        }
    }
}
