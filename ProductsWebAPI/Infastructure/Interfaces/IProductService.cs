using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Interfaces
{
    public interface IProductService
    {
        List<ProductModel> GetProducts();
        List<ProductModel> GetProductsByCategory(int categoryId);
        List<ProductModel> GetProductsByField(int fieldId);
        ProductModel GetProductById(int productId);
        List<ProductFieldValueModel> GetFieldsValuesForProduct(int productId);
        int AddProduct(ProductModel product, List<ProductFieldValueModel> productFieldValues);
        int DeleteProduct(int productId);

    }
}
