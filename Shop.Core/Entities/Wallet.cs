using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Wallet:BaseEntities
{
    public string CardName { get; set; }
    public int CardNumber { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
