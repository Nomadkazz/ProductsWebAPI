namespace ProductsWebAPI.Models
{
    public class ProductField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        //public List<ProductFieldValue>? ProductFieldValues { get; set; }
    }
}
