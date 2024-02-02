using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class BasketProduct:BaseEntities
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int BasketId { get; set; }
    public Basket Basket { get; set; }
    public int ProductQuantity { get; set; }

}
