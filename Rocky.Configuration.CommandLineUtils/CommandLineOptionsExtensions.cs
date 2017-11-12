using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace Rocky.Configuration.CommandLineUtils
{
    /// <summary>
    /// IConfigurationBuilder extension methods for the CommandLineOptionsProvider.
    /// </summary>
    public static class CommandLineOptionsExtensions
    {
        /// <summary>
        /// Adds the command line options provider to <paramref name="configurationBuilder" />.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
        /// <param name="appOptions">Command line options.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddCommandLine(this IConfigurationBuilder configurationBuilder, List<CommandOption> appOptions)
        {
            return configurationBuilder.Add(new CommandLineOptionsProvider(appOptions));
        }
    }
}
