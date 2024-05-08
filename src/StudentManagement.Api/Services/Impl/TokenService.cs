namespace StudentManagement.Api.Services.Impl;

internal sealed class TokenService(IOptions<JwtOptions> options) : ITokenService
{
    private static readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly SigningCredentials _signingCredentials = new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.IssuerSigningKeys.First())),
        SecurityAlgorithms.HmacSha256);

    public (string Token, DateTime Expiry) Generate(Student? student)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, student?.Id.ToString() ?? string.Empty),
            new(ClaimTypes.Name, student?.Name ?? string.Empty),
            new(ClaimTypes.Role, student is null ? "Default" : "Student"),
        };

        var token = new JwtSecurityToken(
            issuer: options.Value.ValidIssuers.FirstOrDefault(),
            audience: options.Value.ValidAudiences.FirstOrDefault(),
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(options.Value.ExpiryHours),
            signingCredentials: _signingCredentials);

        return (_tokenHandler.WriteToken(token), token.ValidTo);
    }
}
