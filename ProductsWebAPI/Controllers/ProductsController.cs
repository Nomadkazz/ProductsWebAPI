using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService productsService)
        {
            _productsService = productsService;
        }

        /*
         Returns all products
         */
        [HttpGet]
        public ActionResult<List<ProductModel>> GetProducts()
        {
            return _productsService.GetProducts();
        }

        /*
         Returns specified product if exists, else Error 404
         */
        [HttpGet("{id}")]
        public ActionResult<ProductModel> GetProduct(int id)
        {
            var product = _productsService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product doesn't exists");
            }

            return product;
        }

        /*
         Returns products in the category if exists, else empty list
         */
        [HttpGet("category/{id}")]
        public ActionResult<List<ProductModel>> GetProductsByCategory(int id)
        {
            return _productsService.GetProductsByCategory(id);
        }

        /*
         Returns products by fields if exists, else empty list
         */
        [HttpGet("field/{id}")]
        public ActionResult<List<ProductModel>> GetProductsByField(int id)
        {
            return _productsService.GetProductsByField(id);
        }

        /*
         Adds a new product in to the database, returns new product's Id
         */
        [HttpPost]
        public ActionResult<int> CreateProduct(ProductModel product)
        {
            var reply = _productsService.AddProduct(product, product.FieldValues);

            if (reply == 0)
            {
                return NotFound("Category doesn't exist");
            }

            return reply;
        }

    }
}
