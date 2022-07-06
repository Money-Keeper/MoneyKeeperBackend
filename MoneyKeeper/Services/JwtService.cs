using Microsoft.IdentityModel.Tokens;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Factories;
using MoneyKeeper.Infrastructure.Settings;
using MoneyKeeper.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyKeeper.Services;

internal sealed class JwtService : IJwtService
{
    private const string IdClaim = "id";
    private const string ExpirationClaim = "exp";

    private readonly ISettingsService _settingsService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly TokenValidationParametersFactory _parametersFactory;
    private readonly JwtSecurityTokenHandler _jwtHandler;

    public JwtService(
        ISettingsService settingsService,
        IDateTimeProvider dateTimeProvider,
        TokenValidationParametersFactory parametersFactory)
    {
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _parametersFactory = parametersFactory ?? throw new ArgumentNullException(nameof(parametersFactory));
        _jwtHandler = new JwtSecurityTokenHandler();
    }

    public string GetToken(Guid userId, TimeSpan? lifeTime = null)
    {
        JwtSettings settings = _settingsService.GetSettings<JwtSettings>();
        DateTime expiresTime = _dateTimeProvider.NowUtc.Add(lifeTime ?? settings.DefaultLifetime);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

        var jwt = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: new[] { new Claim(IdClaim, userId.ToString()) },
            expires: expiresTime,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature));

        return _jwtHandler.WriteToken(jwt);
    }

    public bool ValidateToken(string token, out Guid? userId)
    {
        TokenValidationParameters validationParameters = _parametersFactory.Create(_settingsService);

        try
        {
            _jwtHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            var jwt = (JwtSecurityToken)validatedToken;

            userId = new Guid(jwt.Claims.Single(x => x.Type == IdClaim).Value);
        }
        catch
        {
            userId = null;
        }

        return userId != null;
    }
}
