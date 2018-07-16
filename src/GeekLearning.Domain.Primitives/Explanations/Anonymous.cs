namespace GeekLearning.Domain.Explanations
{
    public class Anonymous : Explanation
    {
        public Anonymous()
            : base("The current user is anonymous.")
        {
        }

        public Anonymous(string message)
            : base(message)
        {
        }

        public Anonymous(string message, string internalMessage)
           : base(message, internalMessage)
        {
        }
    }
}
