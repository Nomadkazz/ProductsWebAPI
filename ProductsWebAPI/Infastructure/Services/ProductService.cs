using ProductsWebAPI.ApplicationCore.Entities;
using ProductsWebAPI.Data;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Infastructure.Services
{
    public class ProductService:IProductService
    {

        private readonly ProductsContext _context;
        public ProductService(ProductsContext context)
        {
            _context = context;
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

            ProductField field = _context.ProductFields.Where(pf => pf.CategoryId == product.CategoryId).FirstOrDefault();
            if (field != null && productFieldValues != null)
            {
                int fieldId = field.Id;
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
            }
            _context.SaveChanges();

            return productEntity.Id;
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

    }
}
