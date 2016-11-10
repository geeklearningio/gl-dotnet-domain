namespace GeekLearning.Domain.Explanations
{
    public class Deleted<T> : Explanation 
    {
        public Deleted(string message)
            : base(message, $"AggregateType : { typeof(T).FullName }")
        {
        }

        public Deleted()
            : this("Aggregate was deleted")
        {
        }

        public Deleted(object key)
            : this($"Aggregate with key '{ (key ?? "<null>").ToString() }' was deleted.")
        {
        }
    }
}
