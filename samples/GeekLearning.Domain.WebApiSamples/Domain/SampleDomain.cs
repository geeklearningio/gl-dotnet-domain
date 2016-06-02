using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.WebApiSamples.Domain
{
    public class SampleDomain : IDomain
    {
        public Maybe<SampleAggregate> GetSomeData(int id, bool throwNow)
        {
            if (throwNow)
            {
                throw new Exceptions.DomainException(new Explanations.InvalidAggregateAccessExplanation("SomeDependency"));
            }
            else
            {
                if (id > 5)
                {
                    return Maybe.None<SampleAggregate>(new Explanations.NotFoundExplanation());
                }
                return Maybe.Some(new SampleAggregate());
            }
        }

        public Task Commit()
        {
            return Task.FromResult(true);
        }
    }
}
