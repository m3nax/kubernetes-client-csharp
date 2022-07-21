using k8s;
using Microsoft.Extensions.DependencyInjection;

namespace KubernetesClient.Extensions.Hosting.Alpha
{
    /// <summary>
    /// Extension methods for setting up Kubernetes client services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class KubernetesClientServicesExtensions
    {
        /// <summary>
        /// Adds Kubernetes client to the specified <see cref="IServiceCollection" /> with default configuration.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddKubernetesClient(this IServiceCollection services)
            => AddKubernetesClient(services, KubernetesClientConfiguration.BuildDefaultConfig());

        /// <summary>
        /// Adds Kubernetes client to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">Callback action to configure the <see cref="KubernetesClientConfigurationBuilder"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddKubernetesClient(this IServiceCollection services, Action<KubernetesClientConfigurationBuilder> configure)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var builder = new KubernetesClientConfigurationBuilder();

            configure(builder);

            var kubernetesClientConfig = builder.Build();

            return services.AddSingleton<IKubernetes>(sp => new Kubernetes(kubernetesClientConfig));
        }

        /// <summary>
        /// Adds Kubernetes client to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configuration">Kubernetes client configuration.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddKubernetesClient(this IServiceCollection services, KubernetesClientConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return services.AddSingleton<IKubernetes>(sp => new Kubernetes(configuration));
        }
    }
}
