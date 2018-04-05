namespace GeekLearning.Domain.Explanations
{
    public class Deleted<T> : Deleted 
    {
        public Deleted(string message)
            : base(message, $"ObjectType : { typeof(T).FullName }")
        {
        }

        public Deleted(string message, string internalMessage)
           : base(message, internalMessage)
        {
        }

        public Deleted()
            : this("Object was deleted")
        {
        }

        public Deleted(object key)
            : this($"Object with key '{ (key ?? "<null>").ToString() }' was deleted.")
        {
        }
    }
}
