using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalogAPI.Validations;

namespace CatalogAPI.Entity
{
    [Table("Categories")]
    public class Category : IValidatableObject
    {
        public Category()
        {
            Products = new Collection<Product>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]       
        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                string firstLetter = this.Name[0].ToString();
                if (firstLetter != null)
                {
                    yield return new
                        ValidationResult("First letter must be upper case!",
                        new[] { nameof(this.Name) });
                }
            }
        }
    }
}
