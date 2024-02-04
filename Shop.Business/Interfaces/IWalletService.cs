using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IWalletService
{
    void GetWalletByUserId(int userId);
    void CreateWallet(Wallet wallet);
}
