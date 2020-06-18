namespace GeekLearning.Domain.AspnetCore
{
    public class Response<T>: ResponseBase
    {
        public T Content { get; set; }
    }
}
