using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWebAPI.ApplicationCore.Entities
{
    public class ProductField : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductFieldValue>? ProductFieldValues { get; set; }
    }
}
