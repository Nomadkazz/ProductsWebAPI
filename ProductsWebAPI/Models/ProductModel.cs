using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWebAPI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        public List<ProductFieldValueModel> FieldValues { get; set; }
    }
}
