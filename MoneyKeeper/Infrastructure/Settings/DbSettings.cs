using MoneyKeeper.Infrastructure.Settings.Abstractions;

namespace MoneyKeeper.Infrastructure.Settings;

internal sealed class DbSettings : ISettings<DbSettings>
{
    public string ConnectionString { get; set; } = default!;

    public override string ToString() => nameof(DbSettings);
}