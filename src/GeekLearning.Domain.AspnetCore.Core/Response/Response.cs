namespace GeekLearning.Domain.AspnetCore
{
    public class Response<T>
    {
        public T Content { get; set; }

        public ReponseStatus Status { get; set; }
    }
}
