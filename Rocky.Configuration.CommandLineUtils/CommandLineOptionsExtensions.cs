using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace Rocky.Configuration.CommandLineUtils
{
    public static class CommandLineOptionsExtensions
    {
        public static IConfigurationBuilder AddCommandLine(this IConfigurationBuilder builder, List<CommandOption> appOptions)
        {
            return builder.Add(new CommandLineOptionsProvider(appOptions));
        }
    }
}
