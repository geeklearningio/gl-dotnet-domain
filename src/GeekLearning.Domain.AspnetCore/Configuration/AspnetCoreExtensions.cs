using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GeekLearning.Domain.AspnetCore
{
    public static class AspnetCoreExtensions
    {
        public static IMvcBuilder AddDomain(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options => options.Filters.Add(typeof(DomainExceptionFilter)));
            mvcBuilder.Services.AddTransient<Internal.MaybeResultMapper>();
            mvcBuilder.Services.AddScoped<IRequestIdProvider, DefaultRequestIdProvider>();
            return mvcBuilder;
        }
    }
}
