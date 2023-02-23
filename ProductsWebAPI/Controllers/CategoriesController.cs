using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.ApplicationCore.Entities;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Infastructure.Services;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /*
         Returns all categories
         */
        [HttpGet]
        public ActionResult<List<CategoryModel>> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        /*
         Returns specified category if exists, else Error 404
         */
        [HttpGet("{id}")]
        public ActionResult<CategoryModel> GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound("Category doesn't exist");
            }

            return category;
        }


        /*
         Adds new category in to the database and returns its Id
         */
        [HttpPost]
        public ActionResult<int> CreateCategory(CategoryModel category)
        {
            return _categoryService.AddCategory(category);
        }

        /*
         Deletes the category from the database
         */
        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCategory(int id)
        {

            var reply = _categoryService.DeleteCategory(id);

            if (reply == 0)
            {
                return NotFound("Category doesn't exist");
            }

            return reply;
        }
    }
}
