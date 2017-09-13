namespace GeekLearning.Domain.Explanations
{
    public class Created : Explanation
    {
        public Created(string message)
            : base(message)
        {
        }

        public Created(string message, string internalMessage)
            : base(message, internalMessage)
        {
        }


        public Created()
            : this("Object was created")
        {
        }

        public Created(object key)
            : this($"Object was created with key '{ (key ?? "<null>").ToString() }'.")
        {
        }
    }
}
