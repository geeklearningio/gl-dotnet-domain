namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class AspnetCoreExtensions
    {
        public static IMvcBuilder AddDomain(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.Filters.Add(typeof(DomainUserFilter));
            });
            mvcBuilder.Services.AddTransient<Internal.MaybeResultMapper>();
            return mvcBuilder;
        }

        public static IServiceCollection AddIdentityDomain<TDomain>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TDomain : IIdentityDomain
        {
            services.TryAddEnumerable(new ServiceDescriptor(typeof(IIdentityDomain), typeof(TDomain), serviceLifetime));
            return services;
        }
    }
}
