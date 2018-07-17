namespace GeekLearning.Domain.Explanations
{
    using System.Collections.Generic;
    using System.Linq;

    public class Updated : Explanation
    {
        public Updated(string message, IEnumerable<Explanation> details)
            : base(message, details)
        {
        }

        public Updated(string message, string innerMessage, IEnumerable<Explanation> details)
           : base(message, details)
        {
        }

        public Updated(string message)
            : this(message, Enumerable.Empty<Explanation>())
        {
        }

        public Updated(string message, string internalMessage)
            : this(message, internalMessage, Enumerable.Empty<Explanation>())
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
    }
}
