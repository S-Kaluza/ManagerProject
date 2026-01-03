using System.Net;
using System.Net.Mail;
using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.Settings;

namespace Application.Helpers.Email;

public class EmailHelper
{
    public static async Task SendEmail(string to, EmailSettings emailSettings, string body, string subject)
    {
        using (MailMessage mail = new())
        {
            mail.To.Add(to);
            mail.From = new MailAddress(emailSettings.SenderAdress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            var smtp = new SmtpClient(emailSettings.Host, emailSettings.Port);
            try
            {
                smtp.Credentials = new NetworkCredential(emailSettings.SenderLogin, emailSettings.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                throw new DomainException(ErrorCodeEnum.EmailNotSent, ErrorCodeEnum.EmailNotSent.GetDescription());
            }
        }
    }
}