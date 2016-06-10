namespace GeekLearning.Domain.Explanations
{
    public class NotFound<T> : Explanation where T : IAggregate
    {
        public NotFound(string message)
            : base(message, $"AggregateType : { typeof(T).FullName }")
        {
        }

        public NotFound()
            : this("Aggregate was not found")
        {
        }

        public NotFound(object key)
            : this($"Aggregate with key '{ (key ?? "<null>").ToString() }' was not found.")
        {
        }
    }
}
