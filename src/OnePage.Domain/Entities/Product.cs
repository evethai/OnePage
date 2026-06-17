using System;
using System.Collections.Generic;

namespace OnePage.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty; // Unique inventory identifier
        public string Slug { get; set; } = string.Empty; // URL-friendly path
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } // Standard financial decimal type
        public int StockQuantity { get; set; } = 0;
        public bool IsActive { get; set; } = true; // Visibility flag

        // External media and manuals
        public string? VideoUrl { get; set; } // e.g. Youtube review url
        public string? ManualUrl { get; set; } // e.g. Technical PDF guide url

        // Dynamic attributes (maps to JSONB in Postgres, TEXT in SQLite)
        public string PropertiesJson { get; set; } = "{}";

        // SEO meta-tags for the Single-Page View
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key & Navigation properties
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
