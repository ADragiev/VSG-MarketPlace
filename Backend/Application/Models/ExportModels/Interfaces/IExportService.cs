using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExportModels.Interfaces
{
    public interface IExportService
    {
        Task<byte[]> ExportCategories();
        Task<byte[]> ExportLentProtocol(string email, string recepient, string provider);
    }
}
