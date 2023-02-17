using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWebAPI.ApplicationCore.Entities
{
    public class ProductFieldValue: BaseEntity
    {
        public string Value { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int FieldId { get; set; }
        public ProductField Field { get; set; }
    }
}
