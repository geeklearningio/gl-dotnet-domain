using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.Explanations
{
    public class Duplicated : Explanation
    {
        public Duplicated(string message)
            : base(message)
        {
        }

        public Duplicated(string message, string internalMessage)
          : base(message, internalMessage)
        {
        }

        public Duplicated(string what, object value)
            : base($"The operation due to a duplicated {what}. Saw : {value ?? "<null>"}")
        {
        }
    }
}
