using Microsoft.IdentityModel.Tokens;
using MoneyKeeper.Infrastructure.Settings;
using MoneyKeeper.Services.Abstractions;
using System.Text;

namespace MoneyKeeper.Factories;

internal sealed class TokenValidationParametersFactory
{
    public TokenValidationParameters Create(ISettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService, nameof(settingsService));

        JwtSettings settings = settingsService.GetSettings<JwtSettings>();

        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = settings.Issuer,
            ValidateAudience = true,
            ValidAudience = settings.Audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,
        };
    }
}
