using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    public class MaybeResult<T> : ObjectResult where T : class
    {
        private Maybe<T> maybe;

        public MaybeResult(Maybe<T> maybe) : base(null)
        {
            this.maybe = maybe;
        }

        public Maybe<T> Maybe { get; set; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var resultMapper = context.HttpContext.RequestServices.GetRequiredService<Internal.MaybeResultMapper>();
            var requestIdProvider = context.HttpContext.RequestServices.GetRequiredService<IRequestIdProvider>();
            var options = context.HttpContext.RequestServices.GetRequiredService<IOptions<DomainOptions>>();
            bool isDebugEnabled = options.Value.Debug;
            var response = new Response<T>
            {
                Content = this.maybe.Value,
                Status = new ReponseStatus
                {
                    Code = resultMapper.GetResult(this.maybe.Explanations),
                    Reasons = (this.maybe.Explanations ?? Enumerable.Empty<Explanations.Explanation>())
                        .Select(reason => new ResponseExplanation
                        {
                            Message = reason.Message,
                            Type = reason.GetType().Name.Replace("Reason", ""),
                            DebugData = isDebugEnabled ? reason.InternalMessage : null
                        }).ToArray(),
                    RequestId = requestIdProvider.RequestId
                }
            };

            this.StatusCode = response.Status.Code;
            this.Value = response;
            return base.ExecuteResultAsync(context);
        }
    }
}
