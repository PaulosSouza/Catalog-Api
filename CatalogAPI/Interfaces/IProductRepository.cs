using System;
using System.Collections.Generic;
using CatalogAPI.Entity;

namespace CatalogAPI.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<Product> GetProductsByPrice();
    }
}
