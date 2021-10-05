using System.Collections.Generic;
using shop.Entities;

namespace shop.DTO
{
  public class CategoryDTO
  {
    public string Name { get; set; }

  }

  public class CategoryUpdateDTO : CategoryDTO
  {
    public int Id { get; set; }
    public List<int> Products { get; set; }

  }


}