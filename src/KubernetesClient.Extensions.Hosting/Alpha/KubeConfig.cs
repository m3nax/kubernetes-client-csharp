using System.Runtime.InteropServices;

namespace KubernetesClient.Extensions.Hosting.Alpha
{
    public class KubeConfig
    {
        /// <summary>
        /// Kubeconfig default location.
        /// </summary>
        public static string DefaultLocation =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? @"\", @".kube\config")
                : Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "/", ".kube/config");

        /// <summary>
        /// Kubeconfig default envirnoment variable.
        /// </summary>
        public static string DefaultEnvironmentVariable => "KUBECONFIG";
    }
}
