namespace GeekLearning.Domain.Explanations
{
    public class NotLoaded<T> : NotLoaded
    {
        public NotLoaded(string message)
            : base(message, $"Object : { typeof(T).FullName }")
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
