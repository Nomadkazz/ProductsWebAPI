using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.ApplicationCore.Entities;
using ProductsWebAPI.Data;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ProductsContext _context;
        public CategoryService(ProductsContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetCategories()
        {
            var alist = _context.Categories.Select(c => new CategoryModel
            {
                Id = c.Id,
                Name = c.Name,
                Fields = c.Fields.Select(pf => new ProductFieldModel
                {
                    Id = pf.Id,
                    Name = pf.Name,

                })
                .ToList()
            })
                .ToList();


            Console.WriteLine(alist);

            return alist;
        }
        public CategoryModel GetCategoryById(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category != null)
            {
                var categoryModel = new CategoryModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Fields = GetFieldsForCategory(categoryId)
            };

                return categoryModel;
            }
            return null;
        }

        public List<ProductFieldModel> GetFields()
        {
            return _context.ProductFields.Select(c => new ProductFieldModel
            {
                Id = c.Id,
                Name = c.Name,
            })
                .ToList();
        }

        public List<ProductFieldModel> GetFieldsForCategory(int categoryId)
        {
            var alist = _context.ProductFields.Where(pf => pf.CategoryId == categoryId)
                .Select(pf => new ProductFieldModel
                {
                    Id = pf.Id,
                    Name = pf.Name,

                })
                .ToList();

            return alist;
        }

        public int AddCategory(CategoryModel category)
        {
            Category entity = new Category()
            {
                Name = category.Name
            };

            _context.Categories.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int AddField(ProductFieldModel productField, int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                return 0;
            };
            ProductField entity = new ProductField()
            {
                Name = productField.Name,
                CategoryId = categoryId,
                ProductFieldValues = null
            };
            _context.ProductFields.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null)
            {
                return 0;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return 1;
        }
    }
}
