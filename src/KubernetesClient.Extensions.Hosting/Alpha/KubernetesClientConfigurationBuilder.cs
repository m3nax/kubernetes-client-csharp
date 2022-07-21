using k8s;
using k8s.Exceptions;
using k8s.KubeConfigModels;
using KubernetesClient.Extensions.Hosting.Alpha.Utilities;

namespace KubernetesClient.Extensions.Hosting.Alpha
{
    public class KubernetesClientConfigurationBuilder
    {
        private K8SConfiguration? k8SConfiguration;
        private Context? currentContext;
        private string? masterUrl;

        /// <summary>
        /// Load kubernetes configuration from kubeconfig file.
        /// </summary>
        /// <returns>The <see cref="KubernetesClientConfigurationBuilder"/> so that additional calls can be chained.</returns>
        public KubernetesClientConfigurationBuilder FromConfigFile()
            => FromConfigFile(new FileInfo(KubeConfig.DefaultLocation));

        /// <summary>
        /// Load kubernetes configuration from kubeconfig file.
        /// </summary>
        /// <param name="kubeConfigPath">Explicit file path to kubeconfig.</param>
        /// <returns>The <see cref="KubernetesClientConfigurationBuilder"/> so that additional calls can be chained.</returns>
        public KubernetesClientConfigurationBuilder FromConfigFile(string kubeConfigPath)
        {
            if (string.IsNullOrWhiteSpace(kubeConfigPath))
            {
                throw new ArgumentException($"'{nameof(kubeConfigPath)}' cannot be null or whitespace.", nameof(kubeConfigPath));
            }

            return FromConfigFile(new FileInfo(kubeConfigPath));
        }

        /// <summary>
        /// Load kubernetes configuration from kubeconfig file.
        /// </summary>
        /// <param name="kubeConfig">Fileinfo of the kubeconfig, cannot be null.</param>
        /// <returns>The <see cref="KubernetesClientConfigurationBuilder"/> so that additional calls can be chained.</returns>
        public KubernetesClientConfigurationBuilder FromConfigFile(FileInfo kubeConfig)
        {
            Guard.ThrowIfNull(kubeConfig);

            if (!kubeConfig.Exists)
            {
                throw new KubeConfigException($"kubeconfig file not found at {kubeConfig.FullName}");
            }

            using (var kubeConfigStream = File.OpenRead(kubeConfig.FullName))
            {
                this.k8SConfiguration = KubernetesYaml.LoadFromStreamAsync<K8SConfiguration>(kubeConfigStream).GetAwaiter().GetResult();
            }

            return this;
        }

        // kccb.FromKubeconfigEviomentVariable();
        // kccb.FromInClusterConfig();


        /// <summary>
        /// Set which context to use for communication with the cluster.
        /// </summary>
        /// <param name="contextName">Name of context.</param>
        /// <returns>The <see cref="KubernetesClientConfigurationBuilder"/> so that additional calls can be chained.</returns>
        public KubernetesClientConfigurationBuilder UseContext(string contextName)
        {
            Guard.ThrowIfNullOrEmpty(contextName);

            if (this.k8SConfiguration is null)
            {
                throw new KubeConfigException("KubeConfing not loaded");
            }

            var context = this.k8SConfiguration.Contexts.FirstOrDefault(c => c.Name.Equals(contextName, StringComparison.OrdinalIgnoreCase));
            if (context is null)
            {
                throw new KubeConfigException($"CurrentContext: {currentContext} not found in contexts in kubeconfig");
            }

            this.currentContext = context;

            return this;
        }

        public KubernetesClientConfigurationBuilder WithMasterUrl(string masterUrl)
        {
            Guard.ThrowIfNullOrEmpty(masterUrl);

            if (this.k8SConfiguration is null)
            {
                throw new KubeConfigException("KubeConfing not loaded");
            }

            if (this.currentContext is null)
            {
                throw new KubeConfigException("Context not selected");
            }

            this.masterUrl = masterUrl;

            return this;
        }

        /// <summary>
        /// Build the <see cref="KubernetesClientConfiguration"/>.
        /// </summary>
        /// <returns>The generated <see cref="KubernetesClientConfiguration"/>.</returns>
        internal KubernetesClientConfiguration Build()
        {
            if (k8SConfiguration is null)
            {
                throw new KubeConfigException("KubeConfing not loaded");
            }

            if (currentContext is null)
            {
                throw new KubeConfigException("Context not selected");
            }

            var kubernetesClientConfiguration = KubernetesClientConfiguration.BuildConfigFromConfigObject(this.k8SConfiguration, this.currentContext.Name, this.masterUrl);

            throw new NotImplementedException();
        }
    }
}
