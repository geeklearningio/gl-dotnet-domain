namespace GeekLearning.Domain.Explanations
{
    public class Updated<T> : Explanation where T : IAggregate
    {
        public Updated(string message)
            : base(message, $"AggregateType : { typeof(T).FullName }")
        {
        }

        public Updated()
            : this("Aggregate was updated")
        {
        }

        public Updated(object key)
            : this($"Aggregate with key '{ (key ?? "<null>").ToString() }' was updated.")
        {
        }
    }
}
