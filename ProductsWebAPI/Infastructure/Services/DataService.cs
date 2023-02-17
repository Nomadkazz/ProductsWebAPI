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
                 Id= c.Id,
                 Name = c.Name,                 
            })
                .ToList();


            Console.WriteLine(alist);

            return alist;
        }        

        public CategoryModel GetCategoryById(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            //var fields = GetFieldsForCategory(categoryId);

            if (category == null){
                return null;
            }
            var categoryModel = new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
            };

            return categoryModel;
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

        public List<ProductFieldValueModel> GetFieldsValuesForProduct(int productId)
        {
            var fieldValues = _context.ProductFieldValues.Where(fv => fv.ProductId == productId)
                .Select(fv => new ProductFieldValueModel
                {
                    Id = fv.Id,
                    Value = fv.Value,
                  //  FieldId = fv.FieldId
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
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl,
                CategoryId = product.CategoryId,
                FieldValues = fieldValues
            };

            return productModel;
        }


        public List<ProductModel> GetProducts()
        {
            var alist = _context.Products.Select(p => new ProductModel
            {
                Id = p.Id,
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

        public List<ProductModel> GetProductsByCategory(int categoryId)
        {
            var list = _context.Products.Where(p => p.CategoryId == categoryId)
                .Select(product =>
                        new ProductModel
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            PhotoUrl = product.PhotoUrl,
                            CategoryId = product.CategoryId,
                            FieldValues = product.FieldValues
                            .Select(v => new ProductFieldValueModel
                            {
                                Id = v.Id,
                                Value = v.Value,

                            }).ToList()
                        }
                        )
                .ToList();
            return list;
        }

        public List<ProductModel> GetProductsByField(int fieldId)
        {
            List<ProductModel> products = _context.ProductFieldValues.Where(fv => fv.FieldId == fieldId)
                .Select(fv => new ProductModel()
                {
                    Id = fv.Id,
                    Name = fv.Product.Name,
                    Description = fv.Product.Description,
                    Price = fv.Product.Price,
                    PhotoUrl = fv.Product.PhotoUrl,
                    CategoryId = fv.Product.CategoryId,
                    FieldValues = fv.Product.FieldValues
                    .Select(v => new ProductFieldValueModel
                    {
                        Id = v.Id,
                        Value = v.Value,

                    }).ToList()
                }).ToList();

            return products;
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
            if (category== null)
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
            _context.SaveChanges();

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


    }
}
