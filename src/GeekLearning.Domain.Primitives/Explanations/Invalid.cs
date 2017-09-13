namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Invalid : Explanation
    {
        public Invalid(string message, IEnumerable<Explanation> details)
            : base(message,  details)
        {
        }

        public Invalid(string message, string innerMessage, IEnumerable<Explanation> details)
           : base(message, details)
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
