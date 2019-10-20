namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DomainExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<DomainExceptionFilter> logger;
        private readonly IOptions<DomainOptions> options;
        private readonly FormatterCollection<IOutputFormatter> optionsFormatters;
        private readonly Func<Stream, Encoding, TextWriter> writerFactory;

        public DomainExceptionFilter(
            ILoggerFactory loggerFactory,
            IOptions<DomainOptions> domainOptions,
            IOptions<MvcOptions> mvcOptions,
            IHttpResponseStreamWriterFactory writerFactory)
        {
            this.logger = loggerFactory.CreateLogger<DomainExceptionFilter>();
            this.options = domainOptions;
            this.optionsFormatters = mvcOptions.Value.OutputFormatters;
            this.writerFactory = writerFactory.CreateWriter;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!(context.Exception is DomainException domainException))
            {
                this.logger.LogError(new EventId(1), context.Exception, context.Exception.Message);
                domainException = new Explanations.Unknown(context.Exception).AsException(context.Exception);
            }
            else
            {
                this.logger.LogError(new EventId(1), domainException, domainException.Explanation.Message);
            }

            var maybeResult = new MaybeResult<object>(domainException.Explanation);


            if (this.HasOutputFormatterForAcceptHeaders(context, maybeResult) || IsAjaxRequest(context.HttpContext.Request))
            {
                context.Result = maybeResult;
                context.Exception = domainException;
            }
        }

        private bool HasOutputFormatterForAcceptHeaders(ActionContext context, ObjectResult result)
        {
            var formatterContext = new OutputFormatterWriteContext(
               context.HttpContext,
               this.writerFactory,
               typeof(Response<object>),
               result.Value);

            var acceptableMediaTypes = MediaTypeHeaderValue.ParseList(context.HttpContext.Request.Headers[HeaderNames.Accept]).Where(mt => mt.Quality >= 0.8) //to discuss. The asp.net core technique rounds up to 1 too
                .Where(mt => mt.MediaType.Value != "*/*")
                .ToList();

            if (acceptableMediaTypes.Any())
            {
                for (var i = 0; i < acceptableMediaTypes.Count; i++)
                {
                    var mediaType = acceptableMediaTypes[i];
                    formatterContext.ContentType = mediaType.MediaType;
                    var optionsFormatters = this.optionsFormatters.Where(o => !(o is HttpNoContentOutputFormatter)).ToList();
                    foreach (var formatter in optionsFormatters)
                    {
                        if (formatter.CanWriteResult(formatterContext))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
