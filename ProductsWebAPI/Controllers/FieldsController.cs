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
        private readonly ICategoryService _categoryService;

        public FieldsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /*
         Returns all fields
         */
        [HttpGet]
        public ActionResult<List<ProductFieldModel>> GetFields()
        {
            return _categoryService.GetFields();
        }

        /*
         Returns all the fields created for the category, else empty list
         */
        [HttpGet("category/{categoryId}")]
        public ActionResult<List<ProductFieldModel>> GetFieldsByCategory(int categoryId)
        {
            return _categoryService.GetFieldsForCategory(categoryId);
        }

        /*
         Adds a new field for the category and returns its Id
         */
        [HttpPost("category/{categoryId}")]
        public ActionResult<int> CreateField(int categoryId, ProductFieldModel field)
        {
            var reply = _categoryService.AddField(field, categoryId);
            if(reply == 0)
            {
                return NotFound("Category doesn't exist");
            }

            return reply;
        }
    }
}
