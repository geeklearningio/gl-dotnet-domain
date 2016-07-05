namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Updated<T> : Explanation where T : IAggregate
    {
        public Updated(string message, IEnumerable<Explanation> details)
            : base(message, $"AggregateType : { typeof(T).FullName }", details)
        {
        }

        public Updated(string message)
            : this(message, Enumerable.Empty<Explanation>())
        {
        }

        public Updated()
            : this("Aggregate was updated", Enumerable.Empty<Explanation>())
        {
        }

        public Updated(Guid key)
            : this($"Aggregate with key '{ key.ToString() }' was updated.", Enumerable.Empty<Explanation>())
        {
        }

        public Updated(Guid key, IEnumerable<Explanation> details)
            : this($"Aggregate with key '{ key.ToString() }' was updated.", details)
        {
        }
    }
}
