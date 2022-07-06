using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Services;

internal sealed class SettingsService : ISettingsService
{
    private readonly IConfiguration _configuration;

    public SettingsService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    TType ISettingsService.GetSettings<TType>()
    {
        TType settings = new TType();

        _configuration.GetRequiredSection(settings.ToString()).Bind(settings);

        return settings;
    }
}
