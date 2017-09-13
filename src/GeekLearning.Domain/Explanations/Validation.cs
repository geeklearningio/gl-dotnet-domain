namespace GeekLearning.Domain.Explanations
{
    using GeekLearning.Domain.Validation;
    using System.Collections.Generic;
    using System.Linq;

    public class Validation<TAggregate> : Explanation
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

        public Validation(IEnumerable<IValidationFailure> errors)
      : base($"AggregateType : { typeof(TAggregate).FullName }", errors.Select(e => new ValidationFailure<TAggregate>(e)).ToList())
        {
        }
    }
}
