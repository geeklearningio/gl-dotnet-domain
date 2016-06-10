namespace GeekLearning.Domain.AspnetCore
{
    public class DefaultRequestIdProvider : IRequestIdProvider
    {
        public DefaultRequestIdProvider()
        {
            var idGenerator = new D64.TimebasedId(false);
            this.RequestId = idGenerator.NewId();
        }

        public string RequestId { get; }
    }
}
