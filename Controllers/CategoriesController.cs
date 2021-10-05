using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using shop.DTO;
using shop.Entities;

namespace shop.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class CategoriesController : ControllerBase
  {

    private readonly ILogger<CategoriesController> _logger;

    private readonly ApplicationContext _ctx;

    public CategoriesController(ILogger<CategoriesController> logger, ApplicationContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet]
    public ActionResult<Category> Get()
    {
      var categories = this._ctx.Categories
        .Include(c => c.ProductCategory)
          .ThenInclude(pc => pc.Product)
      .Select(c => new
      {
        Id = c.Id,
        Name = c.Name,
        Products = c.ProductCategory.Select(pc => pc.Product).Select(p => new { Id = p.Id, Name = p.Name })
      });
      return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne(int id)
    {

      var category = this._ctx.Categories
        .Include(c => c.ProductCategory)
          .ThenInclude(pc => pc.Product)
      .Select(c => new
      {
        Id = c.Id,
        Name = c.Name,
        Products = c.ProductCategory.Select(pc => pc.Product).Select(p => new { Id = p.Id, Name = p.Name })
      })
      .FirstOrDefault(c => c.Id == id);

      if (category == null)
      {
        return NotFound();
      }
      return Ok(category);
    }

    [HttpPost]
    public IActionResult Create(CategoryDTO category)
    {
      var entity = this._ctx.Add<Category>(new Category { Name = category.Name });
      this._ctx.SaveChanges();

      return Ok(entity);
    }

    [HttpPut]
    public async Task<ActionResult> Update(CategoryUpdateDTO category)
    {

      var entity = this._ctx.Categories.FirstOrDefault(c => c.Id == category.Id);

      if (entity == null)
      {
        return NotFound();
      }

      entity.Name = category.Name;

      if (category.Products != null)
      {
        this._ctx.Product_Category.RemoveRange(this._ctx.Product_Category.Where(pc => pc.CategoryId == category.Id));


        foreach (int productId in category.Products)
        {
          ProductCategory pc = new ProductCategory { ProductId = productId, Category = entity };
          await this._ctx.AddAsync(pc);
        }

      }
      await this._ctx.SaveChangesAsync();
      return Ok(entity);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      var category = this._ctx.Categories.Find(id);
      if (category == null)
      {
        return NotFound();
      }
      this._ctx.Remove(category);
      this._ctx.SaveChanges();
      return Ok("{status: \"ok\"}");
    }
  }
}
