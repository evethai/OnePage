using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClosedXML.Excel;
using OnePage.Domain.Entities;
using OnePage.Domain.Interfaces;

namespace OnePage.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<IEnumerable<Product>> ParseProductsFromExcelAsync(Stream excelStream)
        {
            var products = new List<Product>();

            using var workbook = new XLWorkbook(excelStream);
            var worksheet = workbook.Worksheet(1); // Read the first sheet
            var rows = worksheet.RangeUsed().RowsUsed();

            bool isFirstRow = true;
            foreach (var row in rows)
            {
                if (isFirstRow)
                {
                    isFirstRow = false;
                    continue; // Skip the header row
                }

                var name = row.Cell(1).GetValue<string>();
                var sku = row.Cell(2).GetValue<string>();
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sku))
                {
                    continue; // Skip empty rows or rows missing critical identifiers
                }

                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    SKU = sku,
                    Description = row.Cell(3).GetValue<string>(),
                    Price = row.Cell(4).GetValue<double>(),
                    StockQuantity = row.Cell(5).GetValue<int>(),
                    PropertiesJson = row.Cell(6).GetValue<string>() ?? "{}",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                products.Add(product);
            }

            return await Task.FromResult(products);
        }

        public async Task<byte[]> ExportProductsToExcelAsync(IEnumerable<Product> products)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Products");

            // Column headers
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "SKU";
            worksheet.Cell(1, 3).Value = "Description";
            worksheet.Cell(1, 4).Value = "Price";
            worksheet.Cell(1, 5).Value = "StockQuantity";
            worksheet.Cell(1, 6).Value = "PropertiesJson";

            int rowIdx = 2;
            foreach (var product in products)
            {
                worksheet.Cell(rowIdx, 1).Value = product.Name;
                worksheet.Cell(rowIdx, 2).Value = product.SKU;
                worksheet.Cell(rowIdx, 3).Value = product.Description;
                worksheet.Cell(rowIdx, 4).Value = product.Price;
                worksheet.Cell(rowIdx, 5).Value = product.StockQuantity;
                worksheet.Cell(rowIdx, 6).Value = product.PropertiesJson;
                rowIdx++;
            }

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return await Task.FromResult(memoryStream.ToArray());
        }
    }
}
