namespace GeekLearning.Domain.Explanations
{
    public class Created<T> : Created
    {
        public Created(string message)
            : base(message, $"ObjectType : { typeof(T).FullName }")
        {
        }

        public Created()
            : this("Object was created")
        {
        }

        public Created(object key)
            : base(key)
        {
        }
    }
}
