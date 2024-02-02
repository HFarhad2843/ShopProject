using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class User:BaseEntities
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string Email {  get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public ICollection<Wallet> Wallets { get; set; }
}
