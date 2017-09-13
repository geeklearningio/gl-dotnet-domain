namespace GeekLearning.Domain.AspnetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;

    public static class AspnetCoreExtensions
    {
        public static IMvcBuilder AddDomainExceptions(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
            });
        }

        public static IMvcBuilder AddExplanationPolicy(this IMvcBuilder mvcBuilder)
        {
            return AddExplanationPolicy(mvcBuilder, Internal.PolicyBuilder.ApplyDefaultPolicy);
        }

        public static IMvcBuilder AddExplanationPolicy(this IMvcBuilder mvcBuilder, Action<Policy.IPolicyBuilder> configure)
        {
            var policyBuilder = new Internal.PolicyBuilder();
            configure(policyBuilder);
            mvcBuilder.Services.TryAddSingleton(policyBuilder.Build());
            return mvcBuilder;
        }
    }
}
