using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Brand:BaseEntities
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }

}
