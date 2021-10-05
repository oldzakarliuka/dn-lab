using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using shop.Entities;
using Microsoft.AspNetCore.Http;
using shop.DTO;

namespace shop.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {

    private readonly ILogger<ProductsController> _logger;
    private readonly ApplicationContext _ctx;
    public ProductsController(ILogger<ProductsController> logger, ApplicationContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }


    [HttpGet]
    public ActionResult<Product> Get()
    {
      var products = this._ctx.Products
         .Include(p => p.ProductCategory)
          .ThenInclude(pc => pc.Category)
        .Select(p => new
        {
          Id = p.Id,
          Name = p.Name,
          Price = p.Price,
          Count = p.Count,
          Categories = p.ProductCategory.Select(pc => pc.Category).Select(c => new { Id = c.Id, Name = c.Name })
        });
      return Ok(products);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne(int id)
    {
      var product = this._ctx.Products

        .Include(p => p.Histories)
        .Include(p => p.Reviews)
        .Include(p => p.ProductCategory)
          .ThenInclude(pc => pc.Category)
          .FirstOrDefault(p => p.Id == id);
      if (product == null)
      {
        return NotFound();
      }
      return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(ProductDTO product)
    {

      if (product.Count < 0)
      {
        return BadRequest("count must be bigger then 0");
      }
      this._ctx.Products.Add(product.toProduct());
      this._ctx.SaveChanges();
      return Ok(product);
    }

    [HttpPut]
    public IActionResult Update(ProductUpdateDTO product)
    {
      Product prdct = this._ctx.Products.Find(product.Id);

      if (prdct == null)
      {
        return NotFound();
      }

      if (product.Count < 0)
      {
        return BadRequest("count must be bigger then 0");
      }


      prdct.Count = product.Count;
      prdct.Name = product.Name;
      prdct.Thumb = product.Thumb;
      prdct.Price = product.Price;

      this._ctx.SaveChanges();
      return Ok(prdct);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      var product = this._ctx.Products.Find(id);
      if (product == null)
      {
        return NotFound();
      }
      this._ctx.Remove(product);
      this._ctx.SaveChanges();
      return Ok("{status: \"ok\"}");
    }
  }
}
