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
        Basket basket=appDbContext.baskets.Where(x=>x.UserId== UserId).FirstOrDefault();
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
        basket = appDbContext.baskets.Include(x=>x.BasketProducts).Where(x=>x.UserId==UserId && x.IsDeleted==false).FirstOrDefault();
        if (basket != null)
        {
            if (basket.BasketProducts != null)
            {
                foreach (var item in basket.BasketProducts)
                {
                    Console.WriteLine("id: " + item.Id + "product name: " + item.Product.Name + " quantity: " + item.ProductQuantity);
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
