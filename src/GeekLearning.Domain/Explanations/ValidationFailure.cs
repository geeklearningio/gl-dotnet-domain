namespace GeekLearning.Domain.Explanations
{
    using GeekLearning.Domain.Validation;

    public class ValidationFailure : Invalid
    {
        public ValidationFailure(IValidationFailure failure)
            : base(failure.ToString())
        {
            this.Failure = failure;
        }

        public ValidationFailure(IValidationFailure failure, string internalMessage)
            : base(failure.ToString(), internalMessage)
        {
            this.Failure = failure;
        }

        public IValidationFailure Failure { get; }
    }


    public class ValidationFailure<TAggregate> : ValidationFailure
    {
        public ValidationFailure(IValidationFailure failure)
            : base(failure, $"AggregateType : { typeof(TAggregate).FullName }")
        {
        }
    }
}
