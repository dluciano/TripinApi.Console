using Microsoft.Extensions.Options;

namespace TripinApi.Console;

sealed class AppSettings
{
    public const string ConfigurationSectionName = nameof(AppSettings);
    public string TripPinApiUrl { get; init; } = string.Empty;

    internal sealed class AppSettingsValidator : IValidateOptions<AppSettings>
    {
        public ValidateOptionsResult Validate(string name, AppSettings options)
        {
            var failures = new List<string>();
            if (!Uri.IsWellFormedUriString(name, UriKind.Absolute))
                failures.Add($"Invalid {nameof(TripPinApiUrl)} option. The url must be a valid absolute URL");

            if (failures.Any())
                return ValidateOptionsResult.Fail(failures.ToArray());

            return ValidateOptionsResult.Success;
        }
    }
}