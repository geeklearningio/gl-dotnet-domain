namespace GeekLearning.Domain.Explanations
{
    public class NotLoaded : Explanation
    {
        public NotLoaded(string message)
            : base(message)
        {
        }

        public NotLoaded(string message, string internalMessage)
            : base(message, internalMessage)
        {
        }

        public NotLoaded()
            : this("Object was not loaded")
        {
        }

        public NotLoaded(object key)
            : this($"Object with key '{ (key ?? "<null>").ToString() }' was not loaded.")
        {
        }
    }
}
