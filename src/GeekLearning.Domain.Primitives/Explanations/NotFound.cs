namespace GeekLearning.Domain.Explanations
{
    public class NotFound<T> : Explanation
    {
        public NotFound(string message)
            : base(message, $"Element : { typeof(T).FullName }")
        {
        }

        public NotFound()
            : this("Element was not found")
        {
        }

        public NotFound(object key)
            : this($"Element with key '{ (key ?? "<null>").ToString() }' was not found.")
        {
        }
    }
}
