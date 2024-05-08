namespace StudentManagement.Api.Options;

public record JwtOptions
{   
    public int ExpiryHours { get; init; }
    
    public string[] ValidIssuers { get; init; } = [];
    
    public string[] ValidAudiences { get; init; } = [];
    
    public string[] IssuerSigningKeys { get; init; } = [];
}
