namespace Application.Models.Settings;

public class SecuritySettings
{
    public JwtSettings JwtSettings { get; set; }
    public IdentitySettings IdentitySettings { get; set; }
    public string XCSRFHeader { get; set; }
    public string CORSOrigin { get; set; }
    public string CookieAllowCredentials { get; set; }
}