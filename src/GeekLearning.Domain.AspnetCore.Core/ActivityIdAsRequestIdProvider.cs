using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    public class TraceIdentifierAsRequestIdProvider: IRequestIdProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TraceIdentifierAsRequestIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string RequestId => this.httpContextAccessor.HttpContext.TraceIdentifier;
    }
}
