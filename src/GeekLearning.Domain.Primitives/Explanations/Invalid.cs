namespace GeekLearning.Domain.Explanations
{
    using System.Collections.Generic;
    using System.Linq;

    public class Invalid : Explanation
    {
        public Invalid(string message, IEnumerable<Explanation> details)
            : base(message, details)
        {
        }

        public Invalid(string message, string innerMessage, IEnumerable<Explanation> details)
           : base(message, innerMessage, details)
        {
        }

        public Invalid(string message, string innerMessage, IEnumerable<Explanation> details, object data)
            : base(message, innerMessage, details, data)
        {
        }

        public Invalid(string message, string internalMessage)
            : base(message, internalMessage)
        {
        }

        public Invalid(string message)
            : this(message, Enumerable.Empty<Explanation>())
        {
        }

        public Invalid()
            : this("Object was invalid", Enumerable.Empty<Explanation>())
        {
        }

        public Invalid(object key)
            : this($"Object with '{ key.ToString() }' was invalid.", Enumerable.Empty<Explanation>())
        {
        }

        public Invalid(object key, IEnumerable<Explanation> details)
            : this($"Object with '{ key.ToString() }' was invalid.", details)
        {
        }
    }
}
