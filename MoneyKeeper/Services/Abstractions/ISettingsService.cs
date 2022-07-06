using MoneyKeeper.Infrastructure.Settings.Abstractions;

namespace MoneyKeeper.Services.Abstractions;

internal interface ISettingsService
{
    TSettings GetSettings<TSettings>() where TSettings : class, ISettings<TSettings>, new();
}
