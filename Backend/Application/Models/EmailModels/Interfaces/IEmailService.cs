using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.EmailModels.Dtos;

namespace Application.Models.EmailModels.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto dto);
    }
}
