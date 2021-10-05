using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shop.Entities;

namespace shop.DTO
{
  public class ProductDTO
  {
    public string Name { get; set; }
    public string Thumb { get; set; }
    public decimal Price { get; set; }
    public uint Count { get; set; }

    public Product toProduct()
    {
      return (new Product
      {
        Name = this.Name,
        Thumb = this.Thumb,
        Price = this.Price,
        Count = this.Count,
      });
    }
  }

  public class ProductUpdateDTO : ProductDTO
  {
    public int Id { get; set; }
  }
}