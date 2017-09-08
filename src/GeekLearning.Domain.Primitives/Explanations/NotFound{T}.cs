namespace GeekLearning.Domain.Explanations
{
    public class NotFound<T> : NotFound
    {
        public NotFound(string message)
            : base(message, $"Object : { typeof(T).FullName }")
        {
        }

        public NotFound()
            : this("Object was not found")
        {
        }

        public NotFound(object key)
            : this($"Object with key '{ (key ?? "<null>").ToString() }' was not found.")
        {
        }
    }
}
