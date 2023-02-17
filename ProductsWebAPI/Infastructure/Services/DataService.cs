using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.ApplicationCore.Entities;
using ProductsWebAPI.Data;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Services
{
    public class DataService: IDataService

    {
        private readonly ProductsContext _context;
        public DataService(ProductsContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetCategories()
        {
            var alist = _context.Categories.Select(c => new CategoryModel
            {
                 Name = c.Name,                 
            })
                .ToList();


            Console.WriteLine(alist);

            return alist;
        }
        public List<CategoryModel> GetCategoriesWithFields()
        {
            var alist = _context.Categories.Select(c => new CategoryModel
            {
                Name = c.Name,
                Fields = c.Fields.Select(pf => new ProductFieldModel
                {
                    Name = pf.Name
                }).ToList()
            })
            .ToList();

            Console.WriteLine(alist);

            return alist;
        }

        public CategoryModel GetCategoryById(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            var fields = GetFieldsForCategory(categoryId);

            if (category == null){
                return null;
            }
            var categoryModel = new CategoryModel()
            {
                Name = category.Name,
                Fields = fields
            };

            return categoryModel;
        }

        public List<ProductFieldModel> GetFields()
        {
            return _context.ProductFields.Select(c => new ProductFieldModel
            {
                Name = c.Name,
            })
                .ToList();

        }

        public List<ProductFieldModel> GetFieldsForCategory(int categoryId)
        {
            var alist = _context.ProductFields.Where(pf => pf.CategoryId == categoryId)
                .Select(pf => new ProductFieldModel
                {
                            Name = pf.Name,
                        })
                .ToList();

            return alist;
        }

        public List<ProductFieldValueModel> GetFieldsValuesForProduct(int productId)
        {
            var fieldValues = _context.ProductFieldValues.Where(fv => fv.ProductId == productId)
                .Select(fv => new ProductFieldValueModel
                {
                    Value = fv.Value,
                })
                .ToList();

            return fieldValues;
        }

        public ProductModel GetProductById(int productId)
        {
            var product = _context.Products.Find(productId);
            var fieldValues = GetFieldsValuesForProduct(productId);

            if (product == null)
            {
                return null;
            };
            var productModel = new ProductModel()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl,
                CategoryId = product.CategoryId,
                FieldValues = fieldValues
            };

            return productModel;
        }

        public ProductFieldModel GetProductFieldById(int productFieldId)
        {
            var entity = _context.ProductFields.Find(productFieldId);
            if(entity == null)
            {
                return null;
            };
            var productFieldModel = new ProductFieldModel()
            {
                Name = entity.Name
            };

            return productFieldModel;
        }

        public List<ProductModel> GetProducts()
        {
            var alist = _context.Products.Select(p => new ProductModel
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PhotoUrl = p.PhotoUrl,
                CategoryId = p.CategoryId
            })
                .ToList();


            Console.WriteLine(alist);

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

        public int AddProduct(ProductModel product, List<ProductFieldValueModel> productFieldValues)
        {
            if (product.CategoryId == null || product.CategoryId == 0)
            {
                return 0;
            }
            Product productEntity = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl,
                CategoryId = product.CategoryId,
            };
            _context.Products.Add(productEntity);
            _context.SaveChanges();

            if (productFieldValues != null || !productFieldValues.Any())
            {
                int fieldId = _context.ProductFields.Where(pf => pf.CategoryId == product.CategoryId).FirstOrDefault().Id;
                productFieldValues.ForEach(fieldValue =>
                {
                    ProductFieldValue productFieldValueEntity = new ProductFieldValue()
                    {
                        Value = fieldValue.Value,
                        ProductId = productEntity.Id,
                        FieldId = fieldId
                    };
                    _context.ProductFieldValues.Add(productFieldValueEntity);
                }
                );
                _context.SaveChanges();
            }
            //_context.SaveChanges();

            return productEntity.Id;
        }

        public int DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null)
            {
                return 0;
            }
            var reply = _context.Categories.Remove(category);
            _context.SaveChanges();
            return 1;
        }

        public int DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return 0;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return 1;
        }

        public int DeleteProductField(int productFieldId)
        {
            var productField = _context.ProductFields.Find(productFieldId);

            if (productField == null)
            {
                return 0;
            }
            _context.ProductFields.Remove(productField);
            _context.SaveChanges();
            return 1;
        }

    }
}
