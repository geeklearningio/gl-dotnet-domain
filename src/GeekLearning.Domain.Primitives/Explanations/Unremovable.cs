namespace GeekLearning.Domain.Explanations
{
    public class Unremovable : Explanation
    {
        public Unremovable(string message)
            : base(message)
        {
        }

        public Unremovable(string message, string internalMessage)
            : base(message, internalMessage)
        {
        }

        public Unremovable(object key)
            : this($"Object with key '{ (key ?? "<null>").ToString() }' cannot be removed because it is referenced elsewhere.")
        {
        }
    }
}
