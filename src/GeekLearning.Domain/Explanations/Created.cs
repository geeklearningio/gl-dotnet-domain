namespace GeekLearning.Domain.Explanations
{
    public class Created<T> : Explanation where T : IAggregate
    {
        public Created(string message)
            : base(message, $"AggregateType : { typeof(T).FullName }")
        {
        }

        public Created()
            : this("Aggregate was created")
        {
        }

        public Created(object key)
            : this($"Aggregate was created with key '{ (key ?? "<null>").ToString() }'.")
        {
        }
    }
}
