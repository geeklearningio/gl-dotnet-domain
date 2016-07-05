namespace GeekLearning.Domain.Explanations
{
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    public class Validation<TAggregate> : Explanation
        where TAggregate : IAggregate
    {
        public Validation(string message)
            : base(message, $"AggregateType : { typeof(TAggregate).FullName }")
        {
        }

        public Validation(string message, IValidationResult result)
            : base(message, $"AggregateType : { typeof(TAggregate).FullName }", result.Errors.Select(e => new ValidationFailure<TAggregate>(e)).ToList())
        {
        }

        public Validation(IValidationResult result)
            : this("Aggregate is invalid.", result)
        {
        }

        public Validation(IEnumerable<Explanation> errors)
            : base("Aggregate is invalid.", $"AggregateType : { typeof(TAggregate).FullName }", errors)
        {
        }
    }
}
