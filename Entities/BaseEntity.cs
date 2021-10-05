using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Entities
{
  public class BaseEntity
  {

    public BaseEntity()
    {
      this.CreatedAt = DateTime.UtcNow;
      this.UpdatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}