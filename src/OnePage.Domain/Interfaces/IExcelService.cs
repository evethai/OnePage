using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OnePage.Domain.Entities;

namespace OnePage.Domain.Interfaces
{
    public interface IExcelService
    {
        Task<IEnumerable<Product>> ParseProductsFromExcelAsync(Stream excelStream);
        Task<byte[]> ExportProductsToExcelAsync(IEnumerable<Product> products);
    }
}
