using System;
using Microsoft.Extensions.DependencyInjection;

namespace feature_flag_demo
{
    public static class ServiceCollectionExtensions
    {
        public static FeatureFlagServiceRegistrationBuilder<TInterface> AddScopedVersionedService<TInterface, TImpl>(this IServiceCollection services, string flag, string version)
            where TInterface : class
            where TImpl : TInterface
        {
            var builder = new FeatureFlagServiceRegistrationBuilder<TInterface>(services, ServiceLifetime.Scoped, flag, version, typeof(TImpl));
            return builder;
        }

        public static FeatureFlagServiceRegistrationBuilder<TInterface> AddSingletonVersionedService<TInterface, TImpl>(this IServiceCollection services, string flag, string version)
            where TInterface : class
            where TImpl : TInterface
        {
            var builder = new FeatureFlagServiceRegistrationBuilder<TInterface>(services, ServiceLifetime.Singleton, flag, version, typeof(TImpl));
            return builder;
        }

        public static FeatureFlagServiceRegistrationBuilder<TInterface> AddTransientVersionedService<TInterface, TImpl>(this IServiceCollection services, string flag, string version)
            where TInterface : class
            where TImpl : TInterface
        {
            var builder = new FeatureFlagServiceRegistrationBuilder<TInterface>(services, ServiceLifetime.Transient, flag, version, typeof(TImpl));
            return builder;
        }
    }
}

