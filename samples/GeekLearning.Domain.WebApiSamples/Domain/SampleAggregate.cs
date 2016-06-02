using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.WebApiSamples.Domain
{
    public class SampleAggregate : IAggregate
    {
        public string Hello { get; } = "Hello";
    }
}
