using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Data;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Controllers
{
    public class FieldValueController : ControllerBase
    {
        private readonly ProductsContext _context;
        public FieldValueController(ProductsContext context)
        {
            _context = context;
        }
        /*
         Returns specified field value if exists, else Error 404
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFieldValue>> GetFieldValue(int id)
        {
            var product = await _context.ProductFieldValues.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        /*
         Returns values for given products if exists, else Error 404
         */
        [HttpGet("products/{id}")]
        public async Task<ActionResult<List<ProductFieldValue>>> GetFieldValueByProduct(int id)
        {
            var fieldValues = await _context.ProductFieldValues.Where(fv => fv.ProductId == id).ToListAsync();

            if (fieldValues == null)
            {
                return NotFound();
            }

            return fieldValues;
        }

        /*
         Sets a value for the field
         */
        [HttpPost]
        public async Task<ActionResult<Product>> SetValue(ProductFieldValue productFieldValue)
        {
            _context.ProductFieldValues.Add(productFieldValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFieldValue), new { id = productFieldValue.Id }, productFieldValue);
        }
    }
}
