namespace GeekLearning.Domain.Explanations
{
    public class NotFound : Explanation
    {
        public NotFound(string message)
            : base(message)
        {
        }

        public NotFound(string message, string internalMessage)
            : base(message, internalMessage)
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
