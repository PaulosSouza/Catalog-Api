using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public float Stock { get; set; }

        public DateTime Created_at { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
