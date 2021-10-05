using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shop.Entities;
using Microsoft.EntityFrameworkCore;
using shop.DTO;

namespace shop.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class HistoryController : ControllerBase
  {

    private readonly ILogger<HistoryController> _logger;
    private readonly ApplicationContext _ctx;
    public HistoryController(ILogger<HistoryController> logger, ApplicationContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var histories = this._ctx.History.Include(
        p => p.Product
      );
      return Ok(histories);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne(int id)
    {
      var history = this._ctx.History.Find(id);
      if (history == null)
      {
        return NotFound();
      }
      return Ok(history);
    }

    [HttpPost]
    public IActionResult Create(HistoryDTO history)
    {
      Product product = this._ctx.Products.Find(history.ProductId);

      if (product == null)
      {
        return NotFound();
      }

      if (product.Count < history.Count)
      {
        return BadRequest("goods is not enough");
      }
      product.Count -= history.Count;

      this._ctx.Add(history.toHistory());
      this._ctx.SaveChanges();
      return Ok(history);
    }

    [HttpPut]
    public IActionResult Update(HistoryUpdateDTO history)
    {
      var entity = this._ctx.History.FirstOrDefault(h => h.Id == history.Id);

      if (entity == null)
      {
        return NotFound();
      }

      entity.Customer = history.Customer;

      this._ctx.SaveChanges();
      return Ok(entity);
    }
  }
}
