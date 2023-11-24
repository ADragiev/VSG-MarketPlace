using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ExportModels;
using Application.Models.ExportModels.Interfaces;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Application.Services
{
    public class ExportService : IExportService
    {
        private readonly ILentItemRepository lentItemRepository;
        private readonly IProductRepository productRepository;

        public ExportService(ILentItemRepository lentItemRepository,
            IProductRepository productRepository)
        {
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

        public async Task<byte[]> ExportLentProtocol(ExportLentItemsProtocolDto protocolDto)
        {
            var lentItems = (await lentItemRepository.GetLentItemsByUsernameAsync(protocolDto.Email)).Where(l => l.EndDate == null);

            return QuestPDF.Fluent.Document.Create(container =>
             {
                 container.Page(page =>
                 {
                     page.Size(PageSizes.A4);
                     page.Margin(2, Unit.Centimetre);
                     page.PageColor(Colors.White);
                     page.DefaultTextStyle(x => x.FontSize(20));

                     page.Header().Width(7, Unit.Centimetre)
                         .Image("./Images/vsg.png").WithRasterDpi(123);


                     page.Content()
                         .PaddingVertical(1, Unit.Centimetre)
                         .Column(x =>
                         {
                             x.Spacing(20);

                             x.Item().Text(text =>
                             {
                                 text.Line("Hand-Over Protocol").Italic().Bold().FontFamily(Fonts.Calibri); ;
                                 text.Line("Приемно-Предавателен Протокол").Italic().Bold().FontFamily(Fonts.Calibri); ;
                                 text.AlignCenter();
                             });

                             x.Item().Text(text =>
                             {
                                 text.Line($"Recepient/Приел: {protocolDto.Recepient}")
                                 .FontSize(13).Italic().FontFamily(Fonts.Calibri).Bold();
                                 text.Span($"Provider/Предал: {protocolDto.Provider}")
                                 .FontSize(13).Bold().Italic().FontFamily(Fonts.Calibri); ;
                                 text.EmptyLine();
                             });

                             x.Item().Table(table =>
                             {
                                 table.ColumnsDefinition(columns =>
                                 {
                                     columns.ConstantColumn(110);
                                     columns.ConstantColumn(180);
                                     columns.ConstantColumn(180);
                                 });
                                 table.Cell().Row(1).Column(1).Border(1).Text(text =>
                                 {
                                     text.Span("Date/Дата").FontSize(14).Italic().Bold().FontFamily(Fonts.Calibri);
                                     text.AlignCenter();
                                 });
                                 table.Cell().Row(1).Column(2).Border(1).Text(text =>
                                 {
                                     text.Span("Device/Устройство").FontSize(14).Italic().Bold().FontFamily(Fonts.Calibri);
                                     text.AlignCenter();
                                 });
                                 table.Cell().Row(1).Column(3).Border(1).Text(text =>
                                 {
                                     text.Span("Description/Описание").FontSize(14).Italic().Bold().FontFamily(Fonts.Calibri);
                                     text.AlignCenter();
                                 });
                                 foreach (var lentItem in lentItems)
                                 {
                                     var i = 2;
                                     table.Cell().Row((uint)(i)).Column(1).Border(1).Text(text =>
                                     {
                                         text.Span(lentItem.StartDate.ToString(DateFormatConstants.DefaultDateFormatWithoutTime)).FontSize(14).FontFamily(Fonts.Calibri);
                                         text.AlignCenter();
                                     });
                                     table.Cell().Row((uint)(i)).Column(2).Border(1).Text(text =>
                                     {
                                         text.Span($"{lentItem.ProductName} ({lentItem.Qty})").FontSize(14).FontFamily(Fonts.Calibri);
                                         text.AlignCenter();
                                     });
                                     table.Cell().Row((uint)(i)).Column(3).Border(1).Text(text =>
                                     {
                                         text.Span(lentItem.ProductDescription).FontSize(14).FontFamily(Fonts.Calibri);
                                         text.AlignCenter();
                                     });
                                     i++;
                                 }
                                 
                             });
                         });
                     page.Footer().Text(text =>
                     {
                         text.Span("Recepient/Приел: ...........................")
                         .FontSize(12).Bold().Italic().FontFamily(Fonts.Calibri);
                         text.Span("                           ");
                         text.Span("Provider/Предал: ...........................")
                        .FontSize(12).Bold().Italic().FontFamily(Fonts.Calibri);
                     });
                 });
             })
             .GeneratePdf();
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
                    var name = properties[col - 1].GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? properties[col - 1].Name;
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
