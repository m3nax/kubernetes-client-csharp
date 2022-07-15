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
        /// Adds Kubernetes client to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddKubernetesClient(this IServiceCollection services)
        {
            var kubernetesClientConfig = KubernetesClientConfiguration.BuildDefaultConfig();

            return services.AddSingleton<IKubernetes>(sp => new Kubernetes(kubernetesClientConfig));
        }
    }
}
