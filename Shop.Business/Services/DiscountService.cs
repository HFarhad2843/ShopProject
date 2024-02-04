using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class DiscountService : IDiscountService
{
    AppDbContext appDbContext = new AppDbContext();

    public void CreateDiscount(Discount discount)
    {
       appDbContext.discounts.Add(discount);
       appDbContext.SaveChanges();
       Console.WriteLine("Endrim elave edildi");
    }

    public void ShowAll()
    {
        List<Discount> list = new List<Discount>();
        list = appDbContext.discounts.Where(x => x.IsDeleted == false).ToList();
        foreach (var item in list)
        {
            Console.WriteLine("id: " + item.Id + " name: " + item.Name +" endrim faizi " +item.DiscountPercent+" etrafli melumat "+item.Description);
            Console.WriteLine(" Baslangic muddet " + item.StartTime +" Son muddet "+item.EndTime);
        }
    }
}
