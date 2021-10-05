using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Entities
{
  public class History : BaseEntity
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public System.Guid Id { get; set; }
    public string Customer { get; set; }
    public uint Count { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}