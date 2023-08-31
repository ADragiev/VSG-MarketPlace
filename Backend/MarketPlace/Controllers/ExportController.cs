using Application.Models.ExportModels;
using Application.Models.ExportModels.Interfaces;
using Application.Models.LentItemModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class ExportController : BaseController
    {
        private readonly IExportService exportService;
        private readonly ILentItemRepository lentItemRepository;

        public ExportController(IExportService exportService,
            ILentItemRepository lentItemRepository)
        {
            this.exportService = exportService;
            this.lentItemRepository = lentItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult>ExportCategories(ReportType reportType)
        {
            
            var bytes =  await exportService.GenerateReport(reportType);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }

        [HttpGet("LentItemsProtocol")]
        public async Task<IActionResult> ExportLentItems(string email, string recepient, string provider)
        {
            var bytes = await exportService.ExportLentProtocol(email, recepient, provider);
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "document.docx");
        }
    }
}
