using System;
using System.Collections.Generic;
using CatalogAPI.Context;
using CatalogAPI.Entity;
using CatalogAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoryProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
