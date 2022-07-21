using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KubernetesClient.Extensions.Hosting.Alpha.Utilities
{
    internal static class Guard
    {
        /// <summary>
        /// Throw an exception if the value is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The parameter name to use in the thrown exception.</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull(object value, [CallerMemberName] string? paramName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, "Must not be null");
            }
        }

        /// <summary>
        /// Throw an exception if the value is null or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The parameter name to use in the thrown exception.</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNullOrEmpty(string value, [CallerMemberName] string? paramName = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Must not be null or empty", paramName);
            }
        }
    }
}
