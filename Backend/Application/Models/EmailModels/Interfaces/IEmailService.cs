using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.EmailModels.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string body);
    }
}
