namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Updated<T> : Updated
    {
        public Updated(string message, IEnumerable<Explanation> details)
            : base(message, $"AggregateType : { typeof(T).FullName }", details)
        {
        }

        public Updated(string message)
            : this(message, Enumerable.Empty<Explanation>())
        {
        }

        public Updated(string message, string internalMessage)
            : base(message, internalMessage, Enumerable.Empty<Explanation>())
        {
        }

        public Updated()
            : this("Object was updated", Enumerable.Empty<Explanation>())
        {
        }

        public Updated(object key)
            : this($"Object with key '{ key.ToString() }' was updated.", Enumerable.Empty<Explanation>())
        {
        }

        public Updated(object key, IEnumerable<Explanation> details)
            : this($"Object with key '{ key.ToString() }' was updated.", details)
        {
        }


        public Updated(Guid key)
            : this($"Object with key '{ key.ToString() }' was updated.", Enumerable.Empty<Explanation>())
        {
        }

        public Updated(Guid key, IEnumerable<Explanation> details)
            : this($"Object with key '{ key.ToString() }' was updated.", details)
        {
        }
    }
}
