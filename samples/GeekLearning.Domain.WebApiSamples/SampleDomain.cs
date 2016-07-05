namespace GeekLearning.Domain.WebApiSamples
{
    using System.Threading.Tasks;

    public class SampleDomain : IDomain
    {
        public Maybe<SampleAggregate> GetSomeData(int id, bool throwNow)
        {
            if (throwNow)
            {
                throw new Explanations.InvalidAggregateAccess<SampleAggregate>("SomeDependency").AsException();
            }
            else
            {
                if (id > 5)
                {
                    return new Explanations.NotFound<SampleAggregate>();
                }

                return new SampleAggregate();
            }
        }

        public Task CommitAsync()
        {
            return Task.FromResult(true);
        }
    }
}
