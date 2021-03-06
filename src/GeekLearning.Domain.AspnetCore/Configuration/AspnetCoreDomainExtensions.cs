﻿namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class AspnetCoreDomainExtensions
    {
        public static IMvcBuilder AddDomain(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(DomainUserFilter));
            });

            AspnetCoreExtensions.AddDomainExceptions(mvcBuilder);
            AspnetCoreExtensions.AddExplanationPolicy(mvcBuilder);
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
