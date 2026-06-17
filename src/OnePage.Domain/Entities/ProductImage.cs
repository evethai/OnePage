using System;

namespace OnePage.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; } = string.Empty; // Image storage location URL
        public bool IsPrimary { get; set; } = false; // Primary thumbnail flag
        public int SortOrder { get; set; } = 0; // Display sorting order (lower first)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property mapping back to the product
        public virtual Product? Product { get; set; }
    }
}
