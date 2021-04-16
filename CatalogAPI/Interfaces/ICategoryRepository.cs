using System;
using System.Collections.Generic;
using CatalogAPI.Entity;

namespace CatalogAPI.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetCategoryProducts();
    }
}
