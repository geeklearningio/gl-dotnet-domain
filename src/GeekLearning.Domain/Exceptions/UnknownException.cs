using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.Exceptions
{
    public class UnknownException: DomainException
    {
        public UnknownException() : base(new Explanations.UnknownExplanation())
        {
        }

        public UnknownException(Exception exception) : base(new Explanations.UnknownExplanation(exception))
        {
        }
    }
}
