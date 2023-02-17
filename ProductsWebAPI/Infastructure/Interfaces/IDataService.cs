using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Interfaces
{
    public interface IDataService
    {
        //Get
        List<ProductModel> GetProducts();
        List<CategoryModel> GetCategories();
        List<ProductFieldModel> GetFields();
        List<ProductModel> GetProductsByCategory(int categoryId);
        List<ProductModel> GetProductsByField(int fieldId);
        ProductModel GetProductById(int productId);
        CategoryModel GetCategoryById(int categoryId);
        //ProductFieldModel GetProductFieldById(int productFieldId);
        List<ProductFieldModel> GetFieldsForCategory(int categoryId);
        List<ProductFieldValueModel> GetFieldsValuesForProduct(int productId);

        //Set
        int AddCategory(CategoryModel category);
        int AddField(ProductFieldModel productField, int categoryId);
        int AddProduct(ProductModel product, List<ProductFieldValueModel> productFieldValues);


        //Delete
        int DeleteCategory(int categoryId);
        //int DeleteProductField(int productFieldId);
        int DeleteProduct(int productId);

    }
}
