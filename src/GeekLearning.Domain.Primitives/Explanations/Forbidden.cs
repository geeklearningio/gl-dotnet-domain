namespace GeekLearning.Domain.Explanations
{
    public class Forbidden : Explanation
    {
        public Forbidden() 
            : this("The action is forbidden for the current user.")
        {
        }
        
        public Forbidden(string message)
            : base(message)
        {
        }

        public Forbidden(string message, string internalMessage)
            : base(message, internalMessage)
        {
        }
    }
}
