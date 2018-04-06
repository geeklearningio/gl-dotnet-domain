namespace GeekLearning.Domain.Explanations
{
    public class Optional : Explanation 
    {
        public Optional(): base("Item is optional and wasn't provided")
        {
        }

        public Optional(string message)
           : base(message)
        {
        }

        public Optional(string message, string internalMessage)
          : base(message, internalMessage)
        {
        }
    }
}
