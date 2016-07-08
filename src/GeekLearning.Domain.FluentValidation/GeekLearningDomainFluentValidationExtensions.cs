namespace GeekLearning.Domain.Validation
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class GeekLearningDomainFluentValidationExtensions
    {
        public static IServiceCollection AddDomainFluentValidation(this IServiceCollection services)
        {
            services.TryAddSingleton<FluentValidation.IValidatorFactory, ServiceProviderValidatorFactory>();
            services.TryAddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();

            return services;
        }
    }
}
