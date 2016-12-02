namespace GeekLearning.Domain.Explanations
{
    public class Unremovable<T> : Explanation
    {
        public Unremovable(string message)
            : base(message, $"Element : { typeof(T).FullName }")
        {
        }
        public Unremovable(object key)
            : this($"Element with key '{ (key ?? "<null>").ToString() }' cannot be removed because it is referenced elsewhere.")
        {
        }
    }
}
