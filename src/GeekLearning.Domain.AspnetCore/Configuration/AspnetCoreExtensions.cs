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

        public static IServiceCollection AddDomain<TDomain>(this IServiceCollection services)
          where TDomain : class, IDomain
        {
            services.TryAddScoped<TDomain>();
            return services;
        }

        public static IServiceCollection AddIdentityDomain<TIdentityDomain>(this IServiceCollection services)
            where TIdentityDomain : class, IIdentityDomain
        {
            AddDomain<TIdentityDomain>(services);
            services.TryAddEnumerable(new ServiceCollection().AddScoped<IIdentityDomain, TIdentityDomain>((provider) => provider.GetRequiredService<TIdentityDomain>()));
            return services;
        }
    }
}
