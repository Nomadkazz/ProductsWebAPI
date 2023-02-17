namespace ProductsWebAPI.ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductFieldValue>? FieldValues { get; set; }
    }
}
