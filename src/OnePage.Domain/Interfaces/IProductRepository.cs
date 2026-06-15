using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnePage.Domain.Entities;

namespace OnePage.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> GetBySkuAsync(string sku);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task AddRangeAsync(IEnumerable<Product> products);
        Task SaveChangesAsync();
    }
}
