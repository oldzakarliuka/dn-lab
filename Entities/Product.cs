using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Entities
{
  public class Product : BaseEntity
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public string Thumb { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public uint Count { get; set; }

    public ICollection<ProductCategory> ProductCategory { get; set; }

    public ICollection<Review> Reviews { get; set; }
    public ICollection<History> Histories { get; set; }

  }
}