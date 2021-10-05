using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shop.Entities;
using Microsoft.EntityFrameworkCore;

namespace shop.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class ReviewsController : ControllerBase
  {

    private readonly ILogger<ReviewsController> _logger;
    private readonly ApplicationContext _ctx;
    public ReviewsController(ILogger<ReviewsController> logger, ApplicationContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet]
    public ActionResult<Review> Get()
    {
      var reviews = this._ctx.Reviews.Include(r => r.Product);
      return Ok(reviews);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetOne(Guid id)
    {
      var review = this._ctx.Reviews.Find(id);
      if (review == null)
      {
        return NotFound();
      }
      return Ok(review);
    }

    [HttpPost]
    public IActionResult Create(Review review)
    {
      this._ctx.Add(review);
      this._ctx.SaveChanges();
      return Ok(review);
    }

    [HttpPut]
    public IActionResult Update(Review review)
    {
      this._ctx.Update(review);
      this._ctx.SaveChanges();
      return Ok(review);
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
      var review = this._ctx.Reviews.Find(id);
      if (review == null)
      {
        return NotFound();
      }
      this._ctx.Remove(review);
      this._ctx.SaveChanges();
      return Ok("{status: \"ok\"}");
    }
  }
}
