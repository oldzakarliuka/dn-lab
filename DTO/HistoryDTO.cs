using System.Collections.Generic;
using shop.Entities;

namespace shop.DTO
{
  public class HistoryDTO
  {
    public string Customer { get; set; }
    public uint Count { get; set; }
    public int ProductId { get; set; }

    public History toHistory()
    {
      return (new History
      {
        Customer = this.Customer,
        Count = this.Count,
        ProductId = this.ProductId
      });
    }

  }

  public class HistoryUpdateDTO : HistoryDTO
  {
    public System.Guid Id { get; set; }

  }


}