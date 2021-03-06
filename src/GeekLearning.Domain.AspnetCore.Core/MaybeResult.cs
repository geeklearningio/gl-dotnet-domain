﻿namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class MaybeResult<T> : ObjectResult where T : class
    {
        private Maybe<T> maybe;

        public MaybeResult(Maybe<T> maybe)
            : base(null)
        {
            this.maybe = maybe;
        }

        public MaybeResult(Maybe<T> maybe, object routeValues)
            : this(maybe)
        {
            this.RouteValues = routeValues == null ? null : new RouteValueDictionary(routeValues);
        }

        public IUrlHelper UrlHelper { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var requestIdProvider = context.HttpContext.RequestServices.GetService<IRequestIdProvider>();
            var resultMapper = context.HttpContext.RequestServices.GetRequiredService<Policy.IPolicy>();
            var options = context.HttpContext.RequestServices.GetRequiredService<IOptions<DomainOptions>>();
            bool isDebugEnabled = options.Value.Debug;

            var requestId = (requestIdProvider == null) ? context.HttpContext.TraceIdentifier : requestIdProvider.RequestId;
            this.StatusCode = (int)resultMapper.GetStatusCode(this.maybe.Explanation);
            context.HttpContext.Response.Headers.Add("x-request-id", requestId);

            if (this.StatusCode != (int)HttpStatusCode.NoContent)
            {
                this.Value = new Response<T>
                {
                    Content = this.maybe.HasValue ? this.maybe.Value : null,
                    Status = new ReponseStatus
                    {
                        Code = this.StatusCode.Value,
                        RequestId = requestId,
                        Explanation = ResponseExplanation.From(this.maybe.Explanation, isDebugEnabled),
                    }
                };
            }

            return base.ExecuteResultAsync(context);
        }

        public override void OnFormatting(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnFormatting(context);

            if (this.RouteValues == null)
            {
                return;
            }

            var urlHelper = this.UrlHelper;
            if (urlHelper == null)
            {
                var services = context.HttpContext.RequestServices;
                urlHelper = services.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(context);
            }

            var url = urlHelper.Link(null, this.RouteValues);

            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException("No route matches the supplied values.");
            }

            context.HttpContext.Response.Headers[HeaderNames.Location] = url;
        }
    }
}
