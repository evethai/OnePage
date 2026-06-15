using System;

namespace OnePage.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrls { get; set; } = string.Empty; // Comma-separated or JSON list of image URLs
        public string PropertiesJson { get; set; } = "{}"; // Dynamic specifications (maps to JSONB in Postgres, TEXT in SQLite)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
