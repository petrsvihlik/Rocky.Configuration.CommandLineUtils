using System;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace Rocky.Configuration.CommandLineUtils
{
    /// <summary>
    /// Configuration source for Microsoft.Extensions.CommandLineUtils.Option
    /// </summary>
    public class CommandLineOptionsProvider : ConfigurationProvider, IConfigurationSource
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="appOptions">Initializes the collection of settings with data provided by the list of <see cref="CommandOption"/>.</param>
        public CommandLineOptionsProvider(List<CommandOption> appOptions)
        {
            foreach (var commandOption in appOptions)
            {
                switch (commandOption.OptionType)
                {
                    case CommandOptionType.SingleValue:
                        string value = commandOption.Value();
                        if (!string.IsNullOrEmpty(value))
                        {
                            Data.Add(commandOption.LongName, value);
                        }
                        break;

                    case CommandOptionType.MultipleValue:
                        if (commandOption.Values.Count > 0)
                        {
                            Data.Add(commandOption.LongName, string.Join(",", commandOption.Values));
                        }
                        break;

                    case CommandOptionType.NoValue:
                        if (commandOption.HasValue())
                        {
                            Data.Add(commandOption.LongName, true.ToString());
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }

        /// <inheritdoc cref="IConfigurationSource.Build"/>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }
    }
}