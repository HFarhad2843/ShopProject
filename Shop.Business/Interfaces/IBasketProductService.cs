namespace Shop.Business.Interfaces;

public interface IBasketProductService
{
    void AddBasketProduct(int ProductId,int Quantity, int UserId);
    void GetUserBasketProducts( int UserId);

}
