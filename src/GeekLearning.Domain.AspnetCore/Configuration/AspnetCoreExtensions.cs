namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.Extensions.DependencyInjection;

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
