using CourtApp.Application.DTOs.Mail;
using CourtApp.Application.DTOs.Settings;
using CourtApp.Application.Interfaces.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class SMTPMailService : IMailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<SMTPMailService> _logger { get; }

        public SMTPMailService(IOptions<MailSettings> mailSettings, ILogger<SMTPMailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var smtpClient = new SmtpClient(_mailSettings.Host)
                {
                    Port = _mailSettings.Port,
                    Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password),
                    EnableSsl = true,
                    Timeout = 300000
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_mailSettings.From),
                    Subject = string.IsNullOrWhiteSpace(request.Subject) ? "" : request.Subject.Trim(),
                    Body = request.Body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(request.To);
                await smtpClient.SendMailAsync(mailMessage);
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                _logger.LogError(ex.Message, ex);
            }

        }
    }
}