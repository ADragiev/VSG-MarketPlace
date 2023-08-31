using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ExportModels;
using Application.Models.ExportModels.Interfaces;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using OfficeOpenXml;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Application.Services
{
    public class ExportService : IExportService
    {
        private readonly ICategoryService categoryService;
        private readonly ILentItemRepository lentItemRepository;
        private readonly IProductRepository productRepository;

        public ExportService(ICategoryService categoryService,
            ILentItemRepository lentItemRepository,
            IProductRepository productRepository)
        {
            this.categoryService = categoryService;
            this.lentItemRepository = lentItemRepository;
            this.productRepository = productRepository;
        }

        public async Task<byte[]> GenerateReport(ReportType reportType)
        {
            byte[] bytes = { };

            switch (reportType)
            {
                case ReportType.Product:
                    var products = await productRepository.GetProductsForExport();
                    bytes = await ExportToExcelAsync(products);
                    break;
            }

            return bytes;
        }

        public async Task<byte[]> ExportLentProtocol(string email, string recepient, string provider)
        {
            var lentItems = (await lentItemRepository.GetLentItemsByUsernameAsync(email)).Where(l => l.EndDate == null);

            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, "Reports", "Hand-over protocol.docx");

            using (DocX document = DocX.Load(path))
            {
                document.ReplaceText("{Recipient}", recepient);
                document.ReplaceText("{Provider}", provider);

                Table table = document.Tables[0];

                foreach (var lentItem in lentItems)
                {
                    var row = table.InsertRow();
                    row.Height = table.Rows[0].Height;

                    row.Cells[0].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[1].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[2].Paragraphs[0].Alignment = Alignment.center;

                    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

                    row.Cells[0].Paragraphs[0].Append(lentItem.StartDate.ToString(DateFormatConstants.DefaultDateFormatWithoutTime));
                    row.Cells[1].Paragraphs[0].Append($"{lentItem.ProductName} ({lentItem.Qty})");
                    row.Cells[2].Paragraphs[0].Append(lentItem.ProductDescription);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    document.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        private async Task<byte[]> ExportToExcelAsync<T>(List<T> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                var properties = typeof(T).GetProperties();
                properties = properties.Where(p => p.GetCustomAttributes(typeof(NotMappedAttribute), false).Any() == false).ToArray();
                for (int col = 1; col <= properties.Length; col++)
                {
                    var name = properties[col - 1].GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? properties[col-1].Name;
                    worksheet.Cells[1, col].Value = name;
                }

                for (int row = 2; row <= data.Count + 1; row++)
                {
                    for (int col = 1; col <= properties.Length; col++)
                    {
                        worksheet.Cells[row, col].Value = properties[col - 1].GetValue(data[row - 2]);
                    }
                }
                worksheet.Cells.AutoFitColumns();
                return await package.GetAsByteArrayAsync();
            }
        }
    }
}
