using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Data;
using ProductsWebAPI.Models;
using System.Collections.Generic;

namespace ProductsWebAPI.Controllers
{
    [Route("api/fields")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public FieldsController(ProductsContext context)
        {
            _context = context;
        }

        /*
         Returns all fields
         */
        [HttpGet]
        public async Task<ActionResult<List<ProductField>>> GetFields()
        {
            return await _context.ProductFields.ToListAsync();
        }

        /*
         Returns specified field if exists, else Error 404
         */
        [HttpGet("{fieldId}")]
        [ActionName(nameof(GetField))]
        public async Task<ActionResult<ProductField>> GetField(int fieldId)
        {
            var propductField = await _context.ProductFields.FindAsync(fieldId);

            if (propductField == null)
            {
                return NotFound();
            }

            return propductField;
        }

        /*
         Returns all the fields created for the category, else Error 404
         */
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ProductField>>> GetFieldsByCategory(int categoryId)
        {
            
            var category = await _context.ProductFields.Where(pf => pf.CategoryId == categoryId).ToListAsync();
            //var category = _context.Categories.Include(c => c.Fields).FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            return category;

        }

        /*
         Adds a new field for the category and returns its Id
         */
        [HttpPost("category/{categoryId}")]
        public async Task<ActionResult<ProductField>> CreateField(int categoryId, ProductField fieldInput)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            /*var productfield = new ProductField
            {
                Name = fieldInput.Name,                
                CategoryId = categoryId
            };*/
            Console.WriteLine(category);
            /*var fields = new List<ProductField>();
            fields.Append(fieldInput);
            category.Fields = fields;*/
            _context.Categories.Update(category);
            _context.ProductFields.Add(fieldInput);
            _context.SaveChanges();

            return CreatedAtAction("GetField", new { id = fieldInput.Id }, fieldInput);
        }

        /*
         Deletes the field
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.ProductFields.FindAsync(id);

            if (field == null)
            {
                return NotFound();
            }
            _context.ProductFields.Remove(field);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
