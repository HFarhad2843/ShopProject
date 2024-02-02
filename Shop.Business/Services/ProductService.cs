using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class ProductService
{
    AppDbContext appDbContext = new AppDbContext();
    public void ShowAll()
    {
        List<Product> list = new List<Product>();
        list = appDbContext.products.Include(x=>x.Category).Include(x=>x.Brand).Include(x=>x.Discount).ToList();
        foreach (var item in list)
        {
            Console.WriteLine("Id: " + item.Id + " Name: " + item.Name + " Category: "+item.Category.Name+ " Brand: " + item.Brand.Name +" Price: "+ item.Price +" Quantity: "+item.Quantity );
            if(item.Discount!=null)
            {
                Console.WriteLine("Endrim"  + item.Discount.Name + " Endrim faizi: " + item.Discount.DiscountPercent);
            }
        }
    }
}
