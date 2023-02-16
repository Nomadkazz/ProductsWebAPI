namespace ProductsWebAPI.Models
{
    public class ProductFieldValue
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FieldId { get; set; }
        public string Value { get; set; }
    }
}
