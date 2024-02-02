using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Category:BaseEntities
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}
