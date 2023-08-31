using Application.Models.ExportModels;
using Application.Models.ExportModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class ExportController : BaseController
    {
        private readonly IExportService exportService;

        public ExportController(IExportService exportService)
        {
            this.exportService = exportService;
        }

        [HttpGet]
        public async Task<IActionResult>ExportCategories()
        {
            var bytes =  await exportService.ExportCategories();
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
