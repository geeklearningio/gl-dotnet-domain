namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Builder;
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

        public static IMvcBuilder AddDomainExceptions(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
            });
        }

        public static IMvcBuilder AddMaybeResults(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddTransient<Internal.MaybeResultMapper>();
            return mvcBuilder;
        }

        public static IApplicationBuilder UseIdentityDomain(this IApplicationBuilder app)
        {
            app.UseMiddleware<Internal.DomainUserMiddleware>();
            return app;
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
