namespace Application.Models.Settings;

public class IdentitySettings
{
    public int MailConfirmExpirationTime { get; set; }
    public int RecoveryPasswordExpirationTime { get; set; }
}