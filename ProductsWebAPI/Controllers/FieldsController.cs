using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Infastructure.Services;
using ProductsWebAPI.Models;
using System.Collections.Generic;

namespace ProductsWebAPI.Controllers
{
    [Route("api/fields")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IDataService _productsService;

        public FieldsController(IDataService productsService)
        {
            _productsService = productsService;
        }

        /*
         Returns all fields
         */
        [HttpGet]
        public ActionResult<List<ProductFieldModel>> GetFields()
        {
            return _productsService.GetFields();
        }

        /*
         Returns specified field if exists, else Error 404
         */
        /*[HttpGet("{fieldId}")]
        [ActionName(nameof(GetField))]
        public ActionResult<ProductFieldModel> GetField(int fieldId)
        {
            var propductField = _productsService.GetProductFieldById(fieldId);

            if (propductField == null)
            {
                return NotFound();
            }

            return propductField;
        }*/

        /*
         Returns all the fields created for the category, else empty list
         */
        [HttpGet("category/{categoryId}")]
        public ActionResult<List<ProductFieldModel>> GetFieldsByCategory(int categoryId)
        {
            var fields = _productsService.GetFieldsForCategory  (categoryId);

            if (fields == null || !fields.Any())
            {
                return new List<ProductFieldModel>();
            }

            return fields;

        }

        /*
         Adds a new field for the category and returns its Id
         */
        [HttpPost("category/{categoryId}")]
        public ActionResult<int> CreateField(int categoryId, ProductFieldModel field)
        {
            if (field == null)
            {
                return NotFound();
            }

            return _productsService.AddField(field, categoryId);
        }

        /*
         Deletes the field
         */
        /*[HttpDelete("{id}")]
        public ActionResult<int> DeleteField(int id)
        {
            var field = _productsService.DeleteProductField(id);

            if (field == 0)
            {
                return NotFound();
            }
            return field;
        }*/
    }
}
