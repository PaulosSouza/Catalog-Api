using System;
using System.Collections.Generic;
using System.Linq;
using CatalogAPI.Context;
using CatalogAPI.Entity;
using CatalogAPI.Filters;
using CatalogAPI.Interfaces;
using CatalogAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CatalogAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger _logger;
        
        public CategoriesController(IUnitOfWork context,
            ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> Index()
        {
            _logger.LogInformation("=========Get api/categories ============");

            try
            {
                return _context.CategoryRepository
                    .Get()
                    .ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }
        }

        [HttpGet("{id}", Name = "GetOneCategory")]
        public ActionResult<Category> Show(int id)
        {
            try
            {
                var category = _context.CategoryRepository
                    .GetById(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound(new { message = "Category not found" });
                }


                return category;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }
        }

        [HttpPost]
        public ActionResult Store([FromBody] Category category)
        {

            try
            {

                _context.CategoryRepository.Add(category);
                _context.Commit();


                return new CreatedAtRouteResult("GetOneCategory",
                    new { id = category.CategoryId }, category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest(new { message = "Request id divergent" });
                }

                _context.CategoryRepository.Update(category);
                _context.Commit();

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            try
            {
                var category = _context.CategoryRepository
                    .GetById(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound(new { message = "Category not found" });
                }

                _context.CategoryRepository.Delete(category);
                _context.Commit();

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }
        }
    }
}
