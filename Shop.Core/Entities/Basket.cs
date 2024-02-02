using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Basket:BaseEntities
{
    public int UserId { get; set; }
    public User User { get; set; }
}
