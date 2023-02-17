using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWebAPI.Models
{
    public class ProductFieldValueModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
