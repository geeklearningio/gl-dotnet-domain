namespace GeekLearning.Domain.Explanations
{
    public class NotLoaded<T> : Explanation
    {
        public NotLoaded(string message)
            : base(message, $"Element : { typeof(T).FullName }")
        {
        }

        public NotLoaded()
            : this("Element was not loaded")
        {
        }

        public NotLoaded(object key)
            : this($"Element with key '{ (key ?? "<null>").ToString() }' was not loaded.")
        {
        }
    }
}
