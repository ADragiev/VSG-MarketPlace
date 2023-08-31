using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ExportModels;
using Application.Models.ExportModels.Interfaces;
using Application.Models.LentItemModels.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Cryptography;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Application.Services
{
    public class ExportService : IExportService
    {
        private readonly ICategoryService categoryService;
        private readonly ILentItemRepository lentItemRepository;

        public ExportService(ICategoryService categoryService,
            ILentItemRepository lentItemRepository)
        {
            this.categoryService = categoryService;
            this.lentItemRepository = lentItemRepository;
        }

        public async Task<byte[]> ExportCategories()
        {
            var categories = await categoryService.AllAsync();

            return await ExportToExcelAsync(categories);
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
                    row.Cells[1].Paragraphs[0].Append(lentItem.ProductName);
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

                // Set header row
                var properties = typeof(T).GetProperties();
                for (int col = 1; col <= properties.Length; col++)
                {
                    worksheet.Cells[1, col].Value = properties[col - 1].Name;
                }

                // Fill data rows
                for (int row = 2; row <= data.Count + 1; row++)
                {
                    for (int col = 1; col <= properties.Length; col++)
                    {
                        worksheet.Cells[row, col].Value = properties[col - 1].GetValue(data[row - 2]);
                    }
                }

                return await package.GetAsByteArrayAsync();
            }
        }
    }
}
