using System;
using System.Collections.Generic;
using System.Linq;
using CatalogAPI.Context;
using CatalogAPI.Entity;
using CatalogAPI.Interfaces;

namespace CatalogAPI.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsByPrice()
        {
            return Get().OrderBy(p => p.Price).ToList();
        }
    }
}
