namespace Application.Models.Settings;

public class EmailSettings
{
    public string FrontendUrl { get; set; }
    
    public string From { get; set; }
    
    public string SenderLogin { get; set; }
    
    public string SenderAdress { get; set; }
    
    public string Host { get; set; }
    
    public string Password { get; set; }
    
    public int Port { get; set; }
    
    public string GeneratePasswordResetLink { get; set; }
    
    public string GenerateConfirmAccountLink { get; set; }
}