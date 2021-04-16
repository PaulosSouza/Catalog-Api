using System.Collections.Generic;
using System.Linq;
using CatalogAPI.Entity;
using CatalogAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public ProductsController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Index()
        {
            return _context.ProductRepository.Get().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "GetOneProduct")]
        public ActionResult<Product> Show(int id)
        {
            var product = _context.ProductRepository
                .GetById(p => p.ProductId == id);  

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult Store([FromBody] Product product)
        {
            try
            {
                _context.ProductRepository.Add(product);
                _context.Commit();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Something wrong occoured" });
            }


            return new CreatedAtRouteResult("GetOneProduct",
                new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.ProductRepository.Update(product);
            _context.Commit();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = _context.ProductRepository
                .GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.ProductRepository.Delete(product);
            _context.Commit();

            return NoContent();
        }
    }
}
