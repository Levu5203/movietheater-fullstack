using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MovieTheater.Business.ViewModels.Auth;

namespace MovieTheater.Business.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
public class EmailService : IEmailService
{
    private readonly EmailViewModel _emailViewModel;

    public EmailService(IOptions<EmailViewModel> emailViewModel)
    {
        _emailViewModel = emailViewModel.Value;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var client = new SmtpClient(_emailViewModel.SmtpServer, _emailViewModel.Port)
        {
            Credentials = new NetworkCredential(_emailViewModel.SenderEmail, _emailViewModel.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailViewModel.SenderEmail, _emailViewModel.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);
        await client.SendMailAsync(mailMessage);
    }
}