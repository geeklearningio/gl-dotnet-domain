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

        public static IServiceCollection AddDomain<TDomain>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
          where TDomain : IDomain
        {
            services.TryAdd(new ServiceDescriptor(typeof(TDomain), typeof(TDomain), serviceLifetime));
            return services;
        }

        public static IServiceCollection AddIdentityDomain<TIdentityDomain>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TIdentityDomain : IIdentityDomain
        {
            AddDomain<TIdentityDomain>(services, serviceLifetime);
            services.TryAddEnumerable(new ServiceDescriptor(typeof(IIdentityDomain), serviceProvider => serviceProvider.GetRequiredService<TIdentityDomain>(), serviceLifetime));
            return services;
        }
    }
}
