﻿namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Formatters.Internal;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.IO;
    using System.Text;

    public class DomainExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<DomainExceptionFilter> logger;
        private readonly IOptions<DomainOptions> options;
        private readonly FormatterCollection<IOutputFormatter> optionsFormatters;
        private readonly Func<Stream, Encoding, TextWriter> writerFactory;
        private readonly Internal.MaybeResultMapper maybeResultMapper;

        public DomainExceptionFilter(
            ILoggerFactory loggerFactory,
            IOptions<DomainOptions> domainOptions,
            IOptions<MvcOptions> mvcOptions,
            IHttpResponseStreamWriterFactory writerFactory,
            Internal.MaybeResultMapper maybeResultMapper)
        {
            this.logger = loggerFactory.CreateLogger<DomainExceptionFilter>();
            this.options = domainOptions;
            this.optionsFormatters = mvcOptions.Value.OutputFormatters;
            this.writerFactory = writerFactory.CreateWriter;
            this.maybeResultMapper = maybeResultMapper;
        }

        public override void OnException(ExceptionContext context)
        {
            var domainException = context.Exception as DomainException;
            if (domainException == null)
            {
                domainException = new Explanations.Unknown().AsException(context.Exception);
            }

            this.logger.LogError(new EventId(1), domainException, domainException.Explanation.Message);

            var maybeResult = new MaybeResult<object>(domainException.Explanation);
            var mvcError = !this.HasOutputFormatterForAcceptHeaders(context, maybeResult);

            if (!mvcError)
            {
                context.Result = maybeResult;
                context.Exception = domainException;
                context.ExceptionHandled = true;
            }
        }

        private bool HasOutputFormatterForAcceptHeaders(ActionContext context, ObjectResult result)
        {
            var formatterContext = new OutputFormatterWriteContext(
               context.HttpContext,
               this.writerFactory,
               typeof(Response<object>),
               result.Value);

            var acceptableMediaTypes = this.GetAcceptableMediaTypes(context.HttpContext.Request)
                .Where(mt => mt.Quality == 1)
                .ToList();

            if (acceptableMediaTypes.Any())
            {
                for (var i = 0; i < acceptableMediaTypes.Count; i++)
                {
                    var mediaType = acceptableMediaTypes[i];
                    formatterContext.ContentType = mediaType.MediaType;

                    for (var j = 0; j < this.optionsFormatters.Count; j++)
                    {
                        var formatter = this.optionsFormatters[j];
                        if (!(formatter is HttpNoContentOutputFormatter) && formatter.CanWriteResult(formatterContext))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private List<MediaTypeSegmentWithQuality> GetAcceptableMediaTypes(HttpRequest request)
        {
            var result = new List<MediaTypeSegmentWithQuality>();
            AcceptHeaderParser.ParseAcceptHeader(request.Headers[HeaderNames.Accept], result);

            result.Sort((left, right) => left.Quality > right.Quality ? -1 : (left.Quality == right.Quality ? 0 : 1));

            return result;
        }
    }
}
