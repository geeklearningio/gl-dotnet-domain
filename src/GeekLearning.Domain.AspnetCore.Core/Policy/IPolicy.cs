namespace GeekLearning.Domain.AspnetCore.Policy
{
    using GeekLearning.Domain.Explanations;
    using System.Net;

    public interface IPolicy
    {
        HttpStatusCode GetStatusCode(Explanation explanation);
    }
}
