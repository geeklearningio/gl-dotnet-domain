namespace GeekLearning.Domain.AspnetCore
{
    public class ReponseStatus
    {
        public int Code { get; set; }

        public string RequestId { get; set; }

        public ResponseExplanation[] Reasons { get; set; }
    }
}
