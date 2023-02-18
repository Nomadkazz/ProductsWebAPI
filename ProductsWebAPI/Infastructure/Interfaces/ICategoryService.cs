using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategories();
        CategoryModel GetCategoryById(int categoryId);
        List<ProductFieldModel> GetFields();
        List<ProductFieldModel> GetFieldsForCategory(int categoryId);
        int AddField(ProductFieldModel productField, int categoryId);
        int AddCategory(CategoryModel category);
        int DeleteCategory(int categoryId);
    }
}
