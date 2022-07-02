using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace TripinApi.Console;

internal static class ConfigurationExtension
{
    public static IConfigurationBuilder SetupConfigurationBuilder(string[] args) =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFileForEnvironment()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

    private static IConfigurationBuilder AddJsonFileForEnvironment(this IConfigurationBuilder configurationBuilder)
    {
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        if (string.IsNullOrWhiteSpace(environmentName))
            return configurationBuilder;

        return configurationBuilder
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
    }
}
