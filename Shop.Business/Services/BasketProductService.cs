using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class BasketProductService : IBasketProductService
{
    AppDbContext appDbContext = new AppDbContext();

    public void AddBasketProduct(int ProductId,int Quantity, int UserId)
    {
        Product product =new Product();
        product=appDbContext.products.Where(x=>x.Id== ProductId).FirstOrDefault();
        if (product==null)
        {
            throw new Exception("Bu id-li mehsul yoxdur.");
        }
        if (product.Quantity < Quantity)
        {
            throw new Exception("Bu məhsuldan siz istediyiniz sayda  stokda yoxdur.");
        }

        Basket basket = appDbContext.baskets.Where(x => x.UserId == UserId).Where(x => x.IsDeleted == false).FirstOrDefault();
        BasketProduct basketProduct = new BasketProduct();
        basketProduct.ProductId = ProductId;
        basketProduct.ProductQuantity = Quantity;

        if (basket == null) 
        {
            Basket newBasket = new Basket();
            newBasket.UserId = UserId;
            appDbContext.baskets.Add(newBasket);
            appDbContext.SaveChanges();
            basketProduct.BasketId = newBasket.Id;
        }
        else 
        {
            basketProduct.BasketId = basket.Id;
        }
        appDbContext.Add(basketProduct);
        appDbContext.SaveChanges();
        Console.WriteLine("Mehsul zenbile elave olundu");
    }

    public void GetUserBasketProducts(int UserId)
    {
        Basket basket = new Basket();
        basket = appDbContext.baskets.Include(x=>x.BasketProducts).ThenInclude(x=>x.Product).Where(x=>x.UserId==UserId && x.IsDeleted==false).FirstOrDefault();
        if (basket != null)
        {
            if (basket.BasketProducts != null)
            {
                foreach (var item in basket.BasketProducts)
                {
                    Console.WriteLine("id: " + item.Id + "product name: " + item.Product.Name +" price "+ item.Product.Price+ " quantity: " + item.ProductQuantity + " Toplam qiymet "+item.Product.Price*item.ProductQuantity);
                }
            }
            else
            {
                Console.WriteLine("zenbil bosdur");
            }
        }
        else 
        { 
            Console.WriteLine("zenbil yaradilmayib");
        }
    }
 
}
//for now 
