﻿namespace ProductsWebAPI.ApplicationCore.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductField>? Fields { get; set; }
    }
}
