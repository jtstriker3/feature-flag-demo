using System;
using feature_flag_demo.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace feature_flag_demo
{
    public class FeatureFlagServiceRegistrationBuilder<TInterface> where TInterface : class
    {
        private readonly IDictionary<string, Type> versionedServices = new Dictionary<string, Type>();
        private readonly IServiceCollection serviceCollection;
        private readonly string flag;
        private readonly ServiceLifetime serviceLifeTime;

        public FeatureFlagServiceRegistrationBuilder(IServiceCollection serviceCollection, ServiceLifetime serviceLifeTime, string flag, string version, Type serviceType)
        {
            versionedServices.Add(version, serviceType);
            this.serviceCollection = serviceCollection;
            this.serviceLifeTime = serviceLifeTime;
            this.flag = flag;

            serviceCollection.TryAddSingleton<FeatureFlagClient>();
            AddService(serviceType);
            serviceCollection.AddScoped<TInterface>(sp =>
            {
                var client = sp.GetRequiredService<FeatureFlagClient>();
                var version = client.GetFlagValue(flag);

                if (versionedServices.ContainsKey(version))
                    return (TInterface)sp.GetRequiredService(versionedServices[version]);

                throw new CouldNotResolveVersionedServiceException(typeof(TInterface), flag, version);
            });
        }

        public FeatureFlagServiceRegistrationBuilder<TInterface> WithVersion<TService>(string version) where TService : TInterface
        {
            versionedServices.Add(version, typeof(TService));
            AddService(typeof(TService));
            return this;
        }

        private void AddService(Type service)
        {
            switch (serviceLifeTime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped(service);
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton(service);
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient(service);
                    break;
            }
        }
    }
}

