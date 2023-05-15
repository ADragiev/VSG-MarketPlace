using Application.Models.EmailModels.Dtos;
using Application.Models.EmailModels.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            this.config = config;
            this.logger = logger;
        }
        public async Task SendEmailAsync(EmailDto dto)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(config["EmailSender:Email"]));
            message.To.Add(MailboxAddress.Parse(dto.Email));
            message.Body = new TextPart(TextFormat.Text)
            {
                Text = dto.Body
            };
            message.Subject = dto.Subject;

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(config["EmailSender:Email"], config["EmailSender:Password"]);

                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
