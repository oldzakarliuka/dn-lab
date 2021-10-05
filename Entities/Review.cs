using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Entities
{
  public class Review : BaseEntity
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public System.Guid Id { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public int Rating { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}