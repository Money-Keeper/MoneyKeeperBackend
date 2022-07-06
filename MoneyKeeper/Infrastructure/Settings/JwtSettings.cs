using MoneyKeeper.Infrastructure.Settings.Abstractions;

namespace MoneyKeeper.Infrastructure.Settings;

internal sealed class JwtSettings : ISettings<JwtSettings>
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public TimeSpan DefaultLifetime { get; set; }

    public override string ToString() => nameof(JwtSettings);
}
