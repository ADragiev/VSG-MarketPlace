using Application.Models.EmailModels;
using Application.Models.EmailModels.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
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

        public EmailService(IConfiguration config)
        {
            this.config = config;
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
            await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(config["EmailSender:Email"], config["EmailSender:Password"]);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
