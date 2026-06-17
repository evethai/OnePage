using System;
using System.Collections.Generic;

namespace OnePage.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty; // URL-friendly routing slug
        public string Description { get; set; } = string.Empty;

        // Navigation property for related products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
