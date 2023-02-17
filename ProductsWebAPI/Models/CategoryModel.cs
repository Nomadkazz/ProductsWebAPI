namespace ProductsWebAPI.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public List<ProductFieldModel>? Fields { get; set; }
    }
}
