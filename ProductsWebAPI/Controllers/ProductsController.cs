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
        private readonly IDataService _productsService;

        public ProductsController(IDataService productsService)
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
                return NotFound();
            }

            return product;
        }

        /*
         Adds a new product in to the database returns new product's Id
         */
        [HttpPost]
        public ActionResult<int> CreateProduct(ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }

            return _productsService.AddProduct(product, product.FieldValues);
        }

    }
}
