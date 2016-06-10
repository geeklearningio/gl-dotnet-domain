namespace GeekLearning.Domain.Explanations
{
    public abstract class Validation<T> : Explanation where T : IAggregate
    {
        public Validation(string message)
            : base(message, $"AggregateType : { typeof(T).FullName }")
        {
        }

        public Validation()
            : this("Aggregate is invalid.")
        {
        }
    }
}
