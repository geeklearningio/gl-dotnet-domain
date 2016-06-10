namespace GeekLearning.Domain.Explanations
{
    public class InvalidAggregateAccess<T> : Explanation where T : IAggregate
    {
        public InvalidAggregateAccess(string dependencyName)
            : base($"Invalid Aggregate Access Exception: {dependencyName}", $"AggregateType : { typeof(T).FullName }")
        {
        }
    }
}
