namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Http;

    public class TraceIdentifierAsRequestIdProvider : IRequestIdProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TraceIdentifierAsRequestIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string RequestId => this.httpContextAccessor.HttpContext.TraceIdentifier;
    }
}
