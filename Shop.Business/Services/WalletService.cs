using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;


public class WalletService : IWalletService
{
    AppDbContext appDbContext = new AppDbContext();
    public void GetWalletByUserId(int userId)
    {
        List<Wallet> userWallets = new List<Wallet>();
        userWallets = appDbContext.wallets.Where(x => x.UserId == userId).ToList();
        foreach (Wallet wallet in userWallets)
        {
            Console.WriteLine("WalletId: " + wallet.Id + " Card Name: " + wallet.CardName + " Card Number " + wallet.CardNumber + " Balance: " + wallet.Balance);
        }
    }
    public void CreateWallet(Wallet wallet)
    {
        appDbContext.wallets.Add(wallet);
        appDbContext.SaveChanges();
    }
}
