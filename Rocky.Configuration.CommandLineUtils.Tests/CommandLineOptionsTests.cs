using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Rocky.Configuration.CommandLineUtils.Tests
{
    public class CommandLineOptionsTests
    {
        [Theory]
        // Initialized values
        [InlineData("val1", "-s|--stringvalue|--StringValue", "StringValue", CommandOptionType.SingleValue, "val1")]
        [InlineData("1,2,3", "-l|--listval|--ListValue", "ListValue", CommandOptionType.MultipleValue, "1", "2", "3")]
        [InlineData("True", "-f|--flag|--Flag", "Flag", CommandOptionType.NoValue, "on")]
        // Empty values
        [InlineData(null, "-s|--stringvalue|--StringValue", "StringValue", CommandOptionType.SingleValue)]
        [InlineData(null, "-l|--listval|--ListValue", "ListValue", CommandOptionType.MultipleValue)]
        [InlineData(null, "-f|--flag|--Flag", "Flag", CommandOptionType.NoValue)]
        public void TestParsing(string expected, string template, string fullName, CommandOptionType type, params string[] values)
        {
            // Arrange
            var option = new CommandOption(template, type);
            option.Values.AddRange(values.ToList());
            var optionList = new List<CommandOption> { option };

            // Act
            var provider = new CommandLineOptionsProvider(optionList);
            var builder = new ConfigurationBuilder();
            var config = provider.Build(builder);
            config.TryGet(fullName, out var actual);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestExtensionMethod()
        {
            // Arrange
            string expected = "val1";
            var fullName = "StringValue";
            var option = new CommandOption($"-s|--stringvalue|--{fullName}", CommandOptionType.SingleValue);
            option.Values.Add(expected);
            var optionList = new List<CommandOption> { option };

            // Act
            var builder = new ConfigurationBuilder().AddCommandLine(optionList).AddInMemoryCollection().Build();
            var actual = builder[fullName];

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
