using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Product:BaseEntities
{
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BarndId { get; set; }
    public Brand Brand { get; set; }
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    
}
