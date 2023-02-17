using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Infastructure.Services;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService _productsService;

        public CategoriesController(IDataService productsService)
        {
            _productsService = productsService;
        }

        /*
         Returns all categories
         */
        [HttpGet]
        public ActionResult<List<CategoryModel>> GetCategories()
        {

            Console.WriteLine("IN COntroller get all");
            Console.WriteLine(_productsService);
            return _productsService.GetCategories();
        }

        /*
         Returns specified category if exists, else Error 404
         */
        [HttpGet("{id}")]
        public ActionResult<CategoryModel> GetCategory(int id)
        {
            var category = _productsService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        /*
         Adds new category in to the database and returns its Id
         */
        [HttpPost]
        public ActionResult<int> CreateCategory(CategoryModel category)
        {

            return _productsService.AddCategory(category);
        }

        /*
         Deletes the category from the database
         */
        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCategory(int id)
        {
            return _productsService.DeleteCategory(id);
        }
    }
}
