namespace ProductsWebAPI.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
        //public List<ProductFieldValue>? FieldValues { get; set; }
    }
}
