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
        Task<byte[]> GenerateReport(ReportType reportType);
        Task<byte[]> ExportLentProtocol(ExportLentItemsProtocolDto protocolDto);
    }
}
