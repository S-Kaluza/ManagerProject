namespace Application.Models.Settings;

public class JwtSettings
{
    public int ExpirationTime { get; set; }
    public string IssuerSigningKey { get; set; }
}