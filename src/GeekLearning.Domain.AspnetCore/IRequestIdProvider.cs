namespace GeekLearning.Domain.AspnetCore
{
    public interface IRequestIdProvider
    {
        string RequestId { get; }
    }
}
